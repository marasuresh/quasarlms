/* Copyright (c) Microsoft Corporation. All rights reserved. */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Xml;
using DotNetSCORM.LearningAPI;
using Microsoft.LearningComponents.Storage;

namespace Microsoft.LearningComponents.Frameset
{
	#region Delegates
	/// <summary>
	/// Delegate to return previously registered error information.
	/// </summary>
	/// <param name="hasError">If true, an error has been registered. If false, other parameter values 
	/// should be ignored.</param>
	/// <param name="errorTitle">The registered error title.</param>
	/// <param name="errorMsg">The registered error message.</param>
	/// <param name="asInfo">The registered error is information only.</param>
	public delegate void GetErrorInfo(out bool hasError, out string errorTitle, out string errorMsg, out bool asInfo);

	/// <summary>
	/// Delegate to retrieve the title to display for the session. Return value should be plain text.
	/// </summary>
	public delegate PlainTextString GetSessionTitle(LearningSession session);

	/// <summary>
	/// Delegate to allow the caller to process the end of the session. Returned messages (in text format)
	/// are displayed to the user.
	/// </summary>
	public delegate void ProcessSessionEnd(LearningSession session, ref string sessionEndMsgTitle, ref string sessionEndMsg);

	/// <summary>
	/// Delegate to allow application to append the details to the content frame URL that provides information
	/// about, for instance, view and attempt id. In the content frame url, this will be inserted between the 
	/// Content.aspx and resource path.
	/// For instance, if this call returns '/0/24' (indicating, in BWP the view and attempt id), then the content 
	/// frame URL, when loading foo.gif in a package will be:
	/// Content.aspx/0/24/foo.gif
	/// </summary>
	/// <remarks>This method is not called if the current activity entry point is an absolute url.</remarks>
	public delegate void AppendContentFrameDetails(LearningSession session, StringBuilder contentFrameUrl);

	#endregion Delegates

	/// <summary>
	/// Code used to assist in rendering and processing the hidden frame. This code is shared between SLK and BWP framesets.
	/// </summary>
	public class HiddenHelper : PostableFrameHelper
	{
		bool m_pageLoadSuccessful;  // set to true if doPageLoad completed without exception

		private IsNavValidResponseData m_isNavValidResponse;    // information about Is*Valid request
		
		// If true, the page is being rendered for the first time, as part of frameset.
		private bool m_isFramesetInitialization;

		private GetSessionTitle m_getSessionTitle;
		private GetSessionTitle GetSessionTitle { get { return m_getSessionTitle; } }

		private string m_sessionEndedMsgTitle;
		private string m_sessionEndedMsg;

		private bool m_isPostedPage;

		private bool m_saveOnly;    // if true, the only command being processed is a save command

		public HiddenHelper(HttpRequest request, HttpResponse response, Uri embeddedUIPath)
			: base(request, response, embeddedUIPath)
		{
		}


		/// <summary>
		/// Do the processing required for page loading. 
		/// </summary>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"),  // this is code that allows displaying error in content frame
		SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly"),    // parameters that are called directly as methods are cased as methods, others are cased as parameters
		SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]   // it's not worth changing this now
		public void ProcessPageLoad(PackageStore packageStore,
									GetSessionTitle getSessionTitle,
									TryGetViewInfo TryGetViewInfo,
									TryGetAttemptInfo TryGetAttemptInfo,
									AppendContentFrameDetails appendContentFrameDetails,
									RegisterError registerError,
									GetErrorInfo getErrorInfo,
									ProcessSessionEnd ProcessSessionEnd,
									ProcessViewRequest ProcessViewRequest,
									GetFramesetMsg getFramesetMsg,  // messages that appear that are unique to different framesets
									bool isPostBack)
		{
			try {
				RegisterError = registerError;
				GetErrorInfo = getErrorInfo;
				AppendContentFrameDetails = appendContentFrameDetails;
				GetFramesetMsg = getFramesetMsg;

				m_getSessionTitle = getSessionTitle;
				m_isPostedPage = isPostBack;

				AttemptItemIdentifier attemptId;
				SessionView view;
				LoadContentFrame = true;
				ActivityHasChanged = false;

				if (!TryGetViewInfo(false, out view)) {
					WriteError(ResHelper.GetMessage(FramesetResources.FRM_ViewNotSupportedMsg));
					return;
				}

				switch (view) {
					case SessionView.Execute: {
							if (!TryGetAttemptInfo(false, out attemptId))
								return;

							Session = new StoredLearningSession(view, attemptId, packageStore);
							if (!ProcessViewRequest(view, Session))
								return;

							// If the session has ended, allow the application to deal with it.
							ProcessSessionEnd(Session, ref m_sessionEndedMsgTitle, ref m_sessionEndedMsg);
						}
						break;
					case SessionView.Review: {
							if (!TryGetAttemptInfo(false, out attemptId))
								return;

							Session = new StoredLearningSession(view, attemptId, packageStore);
							// Do not set logging options in review view.

							if (!ProcessViewRequest(view, Session))
								return;
						}
						break;
					case SessionView.RandomAccess: {
							// Note: RandomAccess is not supported in BWP, however that would have been caught before 
							// displaying this frame.
							if (!TryGetAttemptInfo(false, out attemptId))
								return;

							Session = new StoredLearningSession(view, attemptId, packageStore);
							// Do not set logging options in random access view.

							if (!ProcessViewRequest(view, Session))
								return;

							// Move to the first activity with a resource.
							MoveToNextActivity(Session);
						}
						break;
					default:
						WriteError(ResHelper.GetMessage(FramesetResources.FRM_ViewNotSupportedMsg));
						return;
				}

				// If the page is posted, process posted data. Remember that all posted data should be considered hostile! 
				// Note that if the session has already ended, then none of the posted data is saved or processed.
				if (isPostBack && !SessionIsEnded) {
					// Process any datamodel changes before doing any navigation. This does not save any data.
					ProcessDataModelValues(Request.Form[HiddenFieldNames.DataModel], Request.Form[HiddenFieldNames.ObjectiveIdMap]);


					// Assume we do not have to reload the content frame
					ActivityHasChanged = false;
					LoadContentFrame = false;

					// if the view requires more data, get it
					if ((Session.View == SessionView.Review) || (Session.View == SessionView.RandomAccess)) {
						// Get the current activity from the posted data and set the session to that activity
						string strActivityId = Request.Form[HiddenFieldNames.ActivityId];
						long activityId;
						if (String.IsNullOrEmpty(strActivityId)
								|| !long.TryParse(strActivityId, out activityId)) {
							WriteError(ResHelper.GetMessage(FramesetResources.HID_InvalidActivityId, strActivityId));
						} else {
							MoveToActivity(Session, activityId);
						}
					}

					// Find out what the commands are and do them.
					m_saveOnly = true;
					ICollection<CommandInfo> commands = GetCommands();
					foreach (CommandInfo cmdInfo in commands) {
						switch (cmdInfo.Command) {
							case Commands.DoNext: {
									if (!Session.HasCurrentActivity || !ProcessNavigationRequests(Session)) {
										if (Session.IsMoveToNextValid()) {
											MoveToNextActivity(Session);
											ActivityHasChanged = true;
											LoadContentFrame = true;
										}
									} else {
										ActivityHasChanged = true;
										LoadContentFrame = true;
									}

									if (!ActivityHasChanged) {
										// Moving to the next activity is not valid. It's possible that when the current
										// activity was unloaded, it exited or suspended itself. If that's the case, we 
										// try to reactivate it. Note this causes the attempt count on the activity to 
										// increment, so on "poorly" written content, the user may not see their data 
										// anymore.
										ActivateCurrentActivity();
										WriteError(ResHelper.Format(GetFramesetMsg(FramesetStringId.MoveToNextFailedHtml), ThemeFolderPath), true);

									}
									m_saveOnly = false;
								}
								break;
							case Commands.DoPrevious: {

									if (!Session.HasCurrentActivity || !ProcessNavigationRequests(Session)) {
										if (Session.IsMoveToPreviousValid()) {
											MoveToPreviousActivity(Session);
											ActivityHasChanged = true;
											LoadContentFrame = true;
										}
									} else {
										ActivityHasChanged = true;
										LoadContentFrame = true;
									}

									if (!ActivityHasChanged) {

										// Moving to the previous activity is not valid. It's possible that when the current
										// activity was unloaded, it exited or suspended itself. If that's the case, we 
										// try to reactivate it. Note this causes the attempt count on the activity to 
										// increment, so on "poorly" written content, the user may not see their data 
										// anymore.
										ActivateCurrentActivity();
										WriteError(ResHelper.Format(GetFramesetMsg(FramesetStringId.MoveToPreviousFailedHtml), ThemeFolderPath), true);
									}
									m_saveOnly = false;
								}
								break;
							case Commands.DoChoice: {
									// This command is used to navigate to activities, primarily from either within a link in an
									// error page, or the submit page returning to the current activity. This command does not 
									// create a new attempt if the requested activity is already the current activity.

									// Requesting to move to a specific activity. The cmdData will include a numeric activity id.
									string cmdData = cmdInfo.CommandData;
									long activityId;
									if (long.TryParse(cmdData, out activityId)) {
										// If the requested activity is the current activity, then do not do the navigation.
										// We skip it because moving to the activity basically exits the current attempt and creates
										// a new one and in this case, that is not the desired behavior. 
										// That new one also increments the attempt count. If we don't do the move, we 
										// pretend it was done. This will force the content frame to be reloaded with the current 
										// activity.
										if (IsCurrentActiveActivity(activityId)) {
											ActivityHasChanged = true;
											LoadContentFrame = true;
										} else {
											// If there is no current activity, or if any navigation requests did not 
											// result in a move, then continue with the choice.
											if (!Session.HasCurrentActivity || !ProcessNavigationRequests(Session)) {
												if (Session.IsMoveToActivityValid(activityId)) {
													MoveToActivity(Session, activityId);
													ActivityHasChanged = true;
													LoadContentFrame = true;
												}
											} else {
												ActivityHasChanged = true;
												LoadContentFrame = true;
											}
										}
									}

									if (!ActivityHasChanged) {
										// Moving to the selected activity is not valid. It's possible that when the current
										// activity was unloaded, it exited or suspended itself. If that's the case, we 
										// try to reactivate it. Note this causes the attempt count on the activity to 
										// increment, so on "poorly" written content, the user may not see their data 
										// anymore.

										ActivateCurrentActivity();
										WriteError(ResHelper.Format(GetFramesetMsg(FramesetStringId.MoveToActivityFailedHtml), ThemeFolderPath), true);
									}
									m_saveOnly = false;
								}
								break;
							case Commands.DoTocChoice: {
									// This command is used to navigate to activities in response to a user selecting a node
									// in the TOC. In this case, even if the current activity is the activity that is requested,
									// then a MoveToActivity() is requested. This will cause the attempt count on the activity
									// to be incremented and the RTE to be reinitialized. This may be a surprise to the user, but 
									// is a requirement of the SCORM 2004 conformance tests.

									// If the selected page is the submit page (either in Execute or RandomAccess views), then
									// display the message and don't ask the session to move to a new activity.

									string cmdData = cmdInfo.CommandData;
									if (String.CompareOrdinal(cmdData, SubmitId) == 0) {
										// Requesting submit page. Do not change the current activity, but mark it as changed so that 
										// it appears to the user that it has changed.
										ActivityHasChanged = true;
										LoadContentFrame = true;
										string title = GetFramesetMsg(FramesetStringId.SubmitPageTitleHtml);
										string message;
										string saveBtn = GetFramesetMsg(FramesetStringId.SubmitPageSaveButtonHtml);
										if (Session.HasCurrentActivity)
											message = GetFramesetMsg(FramesetStringId.SubmitPageMessageHtml);
										else
											message = GetFramesetMsg(FramesetStringId.SubmitPageMessageNoCurrentActivityHtml);
										WriteSubmitPage(title, message, saveBtn);
									} else {
										// Requesting to move to a specific activity. The cmdData will include a numeric activity id.

										long activityId;
										if (long.TryParse(cmdData, out activityId)) {
											// If there is no current activity, or if any navigation requests did not 
											// result in a move, then continue with the choice.
											if (!Session.HasCurrentActivity || !ProcessNavigationRequests(Session)) {
												if (Session.IsMoveToActivityValid(activityId)) {
													MoveToActivity(Session, activityId);
													ActivityHasChanged = true;
													LoadContentFrame = true;
												}
											} else {
												ActivityHasChanged = true;
												LoadContentFrame = true;
											}
										}
									}

									if (!ActivityHasChanged) {
										// Moving to the selected activity is not valid. It's possible that when the current
										// activity was unloaded, it exited or suspended itself. If that's the case, we 
										// try to reactivate it. Note this causes the attempt count on the activity to 
										// increment, so on "poorly" written content, the user may not see their data 
										// anymore.

										ActivateCurrentActivity();
										WriteError(ResHelper.Format(GetFramesetMsg(FramesetStringId.MoveToActivityFailedHtml), ThemeFolderPath), true);
									}
									m_saveOnly = false;
								}
								break;
							case Commands.DoIsChoiceValid: {
									string activityKey = cmdInfo.CommandData;
									bool isValid = false;
									if (!String.IsNullOrEmpty(activityKey)) {
										isValid = Session.IsMoveToActivityValid(activityKey);
									}
									m_isNavValidResponse = new IsNavValidResponseData(Commands.DoIsChoiceValid, activityKey, isValid);
									m_saveOnly = false;
								}
								break;
							case Commands.DoIsNavigationValid: {
									string navCommand = cmdInfo.CommandData;
									if (!String.IsNullOrEmpty(navCommand)) {
										bool isValid = false;
										if (navCommand == Commands.DoNext) {
											isValid = Session.IsMoveToNextValid();
										} else if (navCommand == Commands.DoPrevious) {
											isValid = Session.IsMoveToPreviousValid();
										}
										m_isNavValidResponse = new IsNavValidResponseData(Commands.DoIsNavigationValid, navCommand, isValid);
									}
									m_saveOnly = false;
								}
								break;
							case Commands.DoSave: {
									// Do nothing. The information will be saved since the page was posted.
								}
								break;
							case Commands.DoTerminate: // end the current activity
                                {
									try {
										if (Session.View == SessionView.Execute) {
											// Keep track of state before calling ProcessNavigationRequests so that we can 
											// detect if the call changes it in a way that requires reloading the content frame.
											long activityBeforeNavigation = Session.CurrentActivityId;
											int activityAttemptCount = Session.CurrentActivityDataModel.ActivityAttemptCount;

											LearningSession session = Session;

											if (session.HasCurrentActivity)
												session.ProcessNavigationRequests();

											// The activity has changed if...
											//  ... the session now does not have a current activity (since it did before we did the ProcessNavRequests call, OR
											//  ... the session's current activity is not activity anymore (since it was before the call), OR
											//  ... the session's activity id has changed
											//  ... the session's activity id has not changed, but the attempt count has

											if (!session.HasCurrentActivity) {
												ActivityHasChanged = true;
												LoadContentFrame = true;
											} else if ((session.View == SessionView.Execute) && (!Session.CurrentActivityIsActive)) {
												// In Execute view, it started as active or would have thrown an exception.
												ActivityHasChanged = true;

												// do not load content frame, as that causes messages to flash while switching activities
												LoadContentFrame = false;
											} else if (activityBeforeNavigation != session.CurrentActivityId) {
												// The current activity has changed.
												ActivityHasChanged = true;
												LoadContentFrame = true;
											} else if ((activityBeforeNavigation == session.CurrentActivityId)
														 && (activityAttemptCount != session.CurrentActivityDataModel.ActivityAttemptCount)) {
												// The activity has not changed, but the attempt count has.
												ActivityHasChanged = true;
												LoadContentFrame = true;
											} else {
												// In all other cases, it has not changed
												ActivityHasChanged = false;
												LoadContentFrame = false;
											}
										} else if ((Session.View == SessionView.Review) || (Session.View == SessionView.RandomAccess)) {
											// The activity has changed simply by calling this. This allows the client RTE to reinitialize and behave 
											// as if navigation has happened.
											ActivityHasChanged = true;
											LoadContentFrame = true;
										}
									} catch (SequencingException ex) {
										WriteError(ResHelper.GetMessage(FramesetResources.HID_TerminateFailed, HttpUtility.HtmlEncode(ex.Message)));
									}
									m_saveOnly = false;
								}
								break;
							case Commands.DoSubmit: {
									// Submit the attempt -- meaning, do an ExitAll
									if (Session.View == SessionView.Execute) {
										if (Session.HasCurrentActivity) {
											ProcessNavigationRequests(Session);
										}
										Session.Exit();
									} else if (Session.View == SessionView.RandomAccess) {
										// This may also be a request to end grading (for SLK), in which case 
										// we just set a flag, get the right strings to display and call it done
										SessionIsEnded = true;
										ProcessSessionEnd(Session, ref m_sessionEndedMsgTitle, ref m_sessionEndedMsg);
									}

									ActivityHasChanged = true;
									LoadContentFrame = true;
									m_saveOnly = false;
								}
								break;
						}
					}
				} else {
					// Is this the first page load, as part of frameset? (Some hidden values are only rendered in this case.)
					string param = Request.QueryString[FramesetQueryParameter.Init];
					if (!String.IsNullOrEmpty(param)) {
						m_isFramesetInitialization = true;
					}
				}

				// If this was not simply a save operation and there is no current activity, then display a message
				// asking the user to select one.
				if (!m_saveOnly && !Session.HasCurrentActivity) {
					RegisterError(GetFramesetMsg(FramesetStringId.SelectActivityTitleHtml),
								   GetFramesetMsg(FramesetStringId.SelectActivityMessageHtml), true);
				}

				// In Execute view, ProcessSessionEnd may write to the database and change state of data related to the attempt.
				// Therefore, session changes must be written in the same transation as the session end changes.
				TransactionOptions transactionOptions = new TransactionOptions();
				transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.Serializable;
				using (LearningStoreTransactionScope scope =
					new LearningStoreTransactionScope(transactionOptions)) {
					if (!SessionIsReadOnly) {
						// Save all changes
						Session.CommitChanges();
					}

					if (Session.View == SessionView.Execute) {
						// The rollup and/or sequencing process may have changed the state of the attempt. If so, there are some cases
						// that cannot continue so show an error message. Allow the application to process this, since the messages are 
						// different.
						ProcessSessionEnd(Session, ref m_sessionEndedMsgTitle, ref m_sessionEndedMsg);
					}

					// finish the transaction
					scope.Complete();
				}

				this.HiddenControls = this.GetHiddenControlInfo();

				m_pageLoadSuccessful = true;
			} catch (ThreadAbortException) {
				// Do nothing -- thread is leaving.
				throw;
			} catch (Exception) {
				m_pageLoadSuccessful = false;
				throw;
			}
		}

		/// <summary>
		/// Gets the list of hidden controls that will be rendered on the page.
		/// </summary>
		IEnumerable<HiddenControlInfo> HiddenControls { get; set; }

		/// <summary>
		/// Initialize information for the hidden controls. This sets up the information to create hidden fields in the form
		/// and to update the framesetMgr on page load.
		/// </summary>
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]   // it's not worth changing this now
		IEnumerable<HiddenControlInfo> GetHiddenControlInfo()
		{
			var hiddenControlInfos = new List<HiddenControlInfo>();

			// If the session is attempt-based, then write attempt information
			if (Session != null) {
				hiddenControlInfos.Add(new HiddenControlInfo {
					Id = HiddenFieldNames.AttemptId,
					Value = FramesetUtil.GetString(Session.AttemptId),
					FrameManagerInitializationScript = @"
frameMgr.SetAttemptId({0});",
				});
			}

			// If the session has ended (that is, is suspended, completed or abandoned), then just 
			// tell the framesetmgr and return. Nothing else is required on the client.
			if (SessionIsEnded) {
				hiddenControlInfos.Add(new HiddenControlInfo {
					FrameManagerInitializationScript =
						new JScriptString(ResHelper.Format(@"
frameMgr.TrainingComplete({0}, {1});",
								JScriptString.QuoteString(m_sessionEndedMsgTitle, false),
								JScriptString.QuoteString(m_sessionEndedMsg, false))),
				});
				return hiddenControlInfos;
			}

			// Write view to display. 
			hiddenControlInfos.Add(new HiddenControlInfo {
				Id = HiddenFieldNames.View,
				Value = FramesetUtil.GetString(Session.View),
				FrameManagerInitializationScript = @"
frameMgr.SetView({0});",
			});

			// Write frame and form to post. They depend on whether this is LRM content or SCORM content. If the submit 
			// page is being displayed, it is always this hidden frame that is posted.
			
			// Post the content frame on the next post if LRM content is being displayed.
			// -OR-
			// Post hidden frame if there is no current activity, if the current activity is not LRM content, or 
			// if there is an error being displayed. This may happen if the current 
			// activity was exited or suspended and a new activity was not automagically determined.
			var _condition =
				!m_saveOnly
				&& !SubmitPageDisplayed
				&& !HasError
				&& (Session.HasCurrentActivity
					&& (Session.CurrentActivityResourceType == ResourceType.Lrm));
			
			hiddenControlInfos.Add(new HiddenControlInfo {
				Id = HiddenFieldNames.PostFrame,
				Value = _condition ? "frameContent" : "frameHidden",
				FrameManagerInitializationScript = @"
frameMgr.SetPostFrame({0});",
			});

			// Set postable form. 
			hiddenControlInfos.Add(new HiddenControlInfo {
				FrameManagerInitializationScript =
					ResHelper.Format(@"
frameMgr.SetPostableForm({0});",
						_condition
							? "GetContentFrame().contentWindow.document.forms[0]"
							: "$('form').get(0)"),
			});

			// If a new activity has been identified, then instruct frameMgr to reinitialize the RTE. 
			// BE CAREFUL to do this before setting any other data related to the rte! 
			if (ActivityHasChanged && !SubmitPageDisplayed) {
				hiddenControlInfos.Add(new HiddenControlInfo {
					FrameManagerInitializationScript =
						ResHelper.FormatInvariant(@"
frameMgr.InitNewActivity( {0} );",
								(Session.HasCurrentActivity
								&& CurrentActivityRequiresRte)
									.ToString().ToLower()),
				});
			}

			var _strCurrentActivityId =
				FramesetUtil.GetStringInvariant(Session.CurrentActivityId);
			
			// Write the current activity Id if it has changed. 
			// Write "SUBMIT" if the submit page is being shown. Otherwise, write -1 if there isn't a current activity.
			hiddenControlInfos.Add(new HiddenControlInfo {
				Id = HiddenFieldNames.ActivityId,
				
				// The value of the field is always the current activity id. The value the frameMgr gets is the value of the TOC element.
				Value = Session.HasCurrentActivity
						? _strCurrentActivityId
						: "-1",
				
				FrameManagerInitializationScript =
					ResHelper.FormatInvariant(@"
frameMgr.SetActivityId({0});",
						// the value to set in the frameMgr for the current activity id.
						JScriptString.QuoteString(
							SubmitPageDisplayed
								? SubmitId
							// Only set the actual activity id if this is the first rendering or if it has changed in this rendering,
							// and if there is a current activity.
								: (m_isFramesetInitialization || ActivityHasChanged)
								&& Session.HasCurrentActivity
									? _strCurrentActivityId
									: "-1", false)),
			});

			// Write the navigation control state. Format of the control state is a series of T (to show) or F (to hide)
			// values, separated by semi-colons. The order of controls is: 
			// showNext, showPrevious, showAbandon, showExit, showSave
			var _flags = new[] {
				Session.ShowNext,
				Session.ShowPrevious,
				Session.ShowAbandon,
				Session.ShowExit,
				Session.ShowSave,
			};
			
			hiddenControlInfos.Add(new HiddenControlInfo {
			// Issue: What is the hidden field used for? 
				Id = HiddenFieldNames.ShowUI,
				
				Value = string.Join(string.Empty,
						Array.ConvertAll<bool, string>(_flags,
							_flag => _flag.ToString()[0] + ";"
							)),
			
				FrameManagerInitializationScript =
					ResHelper.FormatInvariant(@"
frameMgr.SetNavVisibility( {0}, {1}, {2}, {3}, {4});",
						Array.ConvertAll<bool, string>(_flags,
					// If the submit page is being displayed, don't show UI elements
							_flag => (!this.SubmitPageDisplayed && _flag).ToString().ToLower()
							)),
			});

			// If there was an error, write it to the client. Note that if the submit page is being rendered, this code 
			// will execute, as it appears in the same form as an error message.
			if (!String.IsNullOrEmpty(ErrorMessage)) {
				hiddenControlInfos.Add(new HiddenControlInfo {
					Id = HiddenFieldNames.ErrorMessage,
					Value = ErrorMessage,
					FrameManagerInitializationScript =
						ResHelper.Format(@"
frameMgr.SetErrorMessage($({0}).val()"
								+ (string.IsNullOrEmpty(ErrorTitle)
									? ");"
									: ", {1}, {2});"),
								JScriptString.QuoteString(
									"#"+HiddenFieldNames.ErrorMessage,
									false),
								JScriptString.QuoteString(ErrorTitle, false),
								ErrorAsInfo.ToString().ToLower()),
				});
			}

			// If this is the first time rendering the frameset, need to write initialization information.
			if (m_isFramesetInitialization) {
				hiddenControlInfos.Add(new HiddenControlInfo {
					Id = HiddenFieldNames.Title,
					Value = GetSessionTitle(Session).ToHtmlString().ToString(),
					FrameManagerInitializationScript = @"
frameMgr.SetTitle({0});",
				});
			} else {
				// Only update the toc when this is not the first rendering of the frameset. (The first time, the toc page itself
				// will get it correct.
				hiddenControlInfos.Add(new HiddenControlInfo {
					Id = HiddenFieldNames.TocState,
					Value = this.GetTocStates(),
					FrameManagerInitializationScript = @"
frameMgr.SetTocNodes({0});",
				});
			}
			
			var _contentFrameUrl =
					new UrlString(
					// If we need to load the content frame because the current activity changed, or we need to render the content 
					// frame URL because there was an error, then figure out the content frame URL.
						(LoadContentFrame || HasError)
						// The activity has changed, so find the new Url for the content frame. 
						&& Session.HasCurrentActivity
							? GetContentFrameUrl()
							: string.Empty
					).ToAscii();

			// Write content href value (value to GET into content frame) -- only if it's required
			hiddenControlInfos.Add(new HiddenControlInfo {
				Id = HiddenFieldNames.ContentHref,
				Value = _contentFrameUrl,
				FrameManagerInitializationScript = @"
frameMgr.SetContentFrameUrl({0});",
			});

			// Write the data model information, only if the current activity requires it.
			if (!SubmitPageDisplayed && CurrentActivityRequiresRte) {
				// There are 3 controls to update: the data model values and the map between n and id for interactions and objectives
				RteDataModelConverter converter = RteDataModelConverter.Create(Session);

				DataModelValues dmValues = converter.GetDataModelValues(FormatDataModelValueForClient);

				hiddenControlInfos.Add(new HiddenControlInfo {
					Id = HiddenFieldNames.ObjectiveIdMap,
					Value = dmValues.ObjectiveIdMap,
				});

				hiddenControlInfos.Add(new HiddenControlInfo {
					Id = HiddenFieldNames.DataModel,
					Value = dmValues.Values,
					FrameManagerInitializationScript =
						ResHelper.Format(@"
var $hidDM = $({0});
var $hidObjectiveMap = $({1});
frameMgr.InitDataModelValues($hidDM.val(), $hidObjectiveMap.val());
$hidDM.val('');
$hidObjectiveMap.val('');",
							JScriptString.QuoteString(
								"#" + HiddenFieldNames.DataModel,
								false),
							JScriptString.QuoteString(
								"#" + HiddenFieldNames.ObjectiveIdMap,
								false)),
				});
			}

			// If there was an IsMove*Valid request, send the response
			if (m_isNavValidResponse != null) {
				hiddenControlInfos.Add(new HiddenControlInfo {
					Id = HiddenFieldNames.IsNavigationValidResponse,
					Value = m_isNavValidResponse.ClientFieldResponse,
					FrameManagerInitializationScript = @"
frameMgr.SetIsNavigationValid({0})",
				});
			}

			if (m_isPostedPage) {
				// Set PostIsComplete. THIS MUST BE THE LAST VALUE SET! 
				hiddenControlInfos.Add(new HiddenControlInfo {
					FrameManagerInitializationScript = @"
frameMgr.PostIsComplete();",
				});
			}

			return hiddenControlInfos;
		}

		/// <summary>
		/// If true, the content frame needs to be reloaded.
		/// If true, the command is written to load the content frame with a new URL
		/// </summary>
		private bool LoadContentFrame { get; set; }

		/// <summary>
		/// This is a callback function for the RteConverter class. This method is called when a name / value pair 
		/// needs to be added to the buffer <paramref name="sb"/> for being sent to the client.
		/// </summary>
		/// <param name="sb">Buffer to write to.</param>
		/// <param name="name">The name of the data model value. This cannot be null or empty.</param>
		/// <param name="value">The value of the data model eleemnt. This can be null or empty.</param>
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]    // They are validated
		public void FormatDataModelValueForClient(StringBuilder sb, string name, string value)
		{
			FramesetUtil.ValidateNonNullParameter("name", name);

			// Pattern to encode is name@Evalue@N (think of @E as 'equals', @N as 'new line').

			if (String.IsNullOrEmpty(value)) {
				value = "";
			} else {
				value = value.Replace("@", "@A").Replace(">", "@G").Replace("<", "@L");
			}

			// Replace any @ characters with @A in name and value (eg, me@mycompany.com becomes me@Amycompany.com)
			// Replace any < with @L, any > with @G, to avoid any form field data issues
			name = name.Replace("@", "@A").Replace(">", "@G").Replace("<", "@L");

			// Concatenate name and value, using @E between them and @N at the end
			sb.Append(name);
			sb.Append("@E");
			sb.Append(value);
			sb.Append("@N");
		}

		// Process the string returned from the client with updated data model values. It's ok if the 
		// dataModelValues have no information -- the method will return without error.
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")] // all exceptions are caught and put into log
		private void ProcessDataModelValues(string dataModelValues, string objectivesIdMap)
		{
			// Values are never set in Review view
			if (Session.View == SessionView.Review)
				return;

			// If there's no current activity, ignore the data
			if (!Session.HasCurrentActivity)
				return;

			// If there is no data, don't do any processing
			if (String.IsNullOrEmpty(dataModelValues))
				return;

			// If the current activity is not a SCO, don't save any data model values.
			if (Session.CurrentActivityResourceType != ResourceType.Sco)
				return;

			RteDataModelConverter converter = RteDataModelConverter.Create(Session);
			string[] nameValuePairs;
			// If any conversions or SetValue calls fail, keep all errors and display them at once.
			StringBuilder errors = new StringBuilder(100);

			// Read the interactions map and initialize converter with it.
			InitializeIndexerMapping(converter.ObjectiveIndexer, objectivesIdMap, errors);

			// Process data model values and set the values in the session
			dataModelValues = dataModelValues.Replace("@G", ">").Replace("@L", "<").Replace("@A", "@");
			nameValuePairs = dataModelValues.Split(new string[] { "@N" }, StringSplitOptions.None);

			// Each nvPair string in the array is of the form <name>@E<value>
			foreach (string nvPair in nameValuePairs) {
				if (String.IsNullOrEmpty(nvPair))
					continue;

				string[] elements = nvPair.Split(new string[] { "@E" }, StringSplitOptions.None);
				if (elements.Length != 2)
					continue;

				try {
					converter.SetValue(new PlainTextString(elements[0]), new PlainTextString(elements[1]));
				} catch (Exception e) {
					LogDataModelError(errors, FramesetResources.CONV_SetValueException, elements[0], TruncateMessage(elements[1]), e.Message);
				}
			}

			Collection<string> finalSetValueErrors;
			try {
				// Let the converter add any pending objects to the data model. It may return a list of errors.
				finalSetValueErrors = converter.FinishSetValue();

				foreach (string error in finalSetValueErrors) {
					LogDataModelError(errors, null, error);
				}
			} catch (Exception e) {
				LogDataModelError(errors, null, e.Message);
			}



			// If there were errors in the process, write them to the response
			if (errors.Length > 0) {
				EndLogDataModelErrors(errors);
				WriteError(errors.ToString(), true);
			}
		}

		// If the message exceeds 100 characters, truncate it and add ellipsis.
		private static string TruncateMessage(string message)
		{
			if (message.Length > 100) {
				return ResHelper.Format("{0}...", message.Substring(0, 100));
			}
			return message;
		}

		// 
		/// <summary>
		/// Helper function to put error message into the message log. 
		/// </summary>
		/// <param name="errorLog">The log to write the error to.</param>
		/// <param name="messageResource">This message id to load. If this is null, messageParts[0] is displayed. Text (not html) format.</param>
		/// <param name="messageParts">The message parts to fill in the tokens in the string loaded by messageResource. Text (not html) format.</param>
		private static void LogDataModelError(StringBuilder errorLog, string messageResource, params string[] messageParts)
		{
			// If this is the first error, add a header to the message.
			if (errorLog.Length == 0) {
				errorLog.AppendLine(ResHelper.GetMessage(FramesetResources.HID_SetValueMappingFailedHtml));
				errorLog.AppendLine("<br><br><ul> ");
			}
			string messageHtml;

			if (messageResource == null) {
				messageHtml = HttpUtility.HtmlEncode(messageParts[0]);
			} else {
				messageHtml = HttpUtility.HtmlEncode(ResHelper.GetMessage(messageResource, messageParts));
			}
			errorLog.AppendLine(ResHelper.Format("<li style=\"margin-top: 10;\">{0}</li>", messageHtml));
		}

		// Helper function to end the list of errors in the data model
		private static void EndLogDataModelErrors(StringBuilder errorLog)
		{
			errorLog.AppendLine("</ul>");
		}

		// Given a string containing a list of mappings between index ('n') and ids, clear the list then 
		// add them to the list of ids, indexed by 'n'.
		// 
		// The format of idMapValues is index@Eid@N, repeated. 
		private static void InitializeIndexerMapping(List<string> indexerInfos, string idMapValues, StringBuilder errorLog)
		{
			// if there are no mapping values, just create an empty list
			if (String.IsNullOrEmpty(idMapValues))
				return;

			// Make sure list is empty.
			indexerInfos.Clear();

			string[] indexValuePairs;

			idMapValues = idMapValues.Replace("@A", "@");
			indexValuePairs = idMapValues.Split(new string[] { "@N" }, StringSplitOptions.None);

			// Each nvPair string in the array is of the form <name>@E<value>
			foreach (string nvPair in indexValuePairs) {
				if (String.IsNullOrEmpty(nvPair))
					continue;

				string[] elements = nvPair.Split(new string[] { "@E" }, StringSplitOptions.None);
				if (elements.Length != 2) {
					LogDataModelError(errorLog, FramesetResources.HID_SetValuePostedDataInvalid);
					continue;
				}

				try {
					int index = XmlConvert.ToInt32(elements[0]);
					indexerInfos.Insert(index, elements[1]);
				} catch (FormatException e) {
					// if elements[0] is wrong type
					LogDataModelError(errorLog, e.Message);
				} catch (ArgumentOutOfRangeException e) {
					// if index is not valid
					LogDataModelError(errorLog, e.Message);
				}
			}
		}

		#region called from aspx

		/// <summary>
		/// Render all hidden controls on to the page. 
		/// </summary>
		public void WriteHiddenControls()
		{
			// If the page did not load successfully, don't do anything
			if (!m_pageLoadSuccessful)
				return;

			// Error situation. Nothing to write.
			if (Session == null)
				return;

			HtmlStringWriter sw = new HtmlStringWriter(Response.Output);
			
			new List<HiddenControlInfo>(this.HiddenControls)
				.ForEach(_ctl => _ctl.WriteFieldTo(sw));
			
			sw.EndRender();
		}

		/// <summary>
		/// Write the script to initialize the frameset manager.
		/// </summary>
		[Obsolete]
		public void WriteFrameMgrInit()
		{
			this.Response.Write(this.GetFrameMgrInit());
		}

		public string GetFrameMgrInit()
		{
			var _sb = new StringBuilder();
			
			// If the page did not load successfully, just make sure the error string gets written.
			if (!m_pageLoadSuccessful) {
				// If there is an error, then write it. Basically, there are cases where the page did not 
				// load enough to write out the message earlier. 
				if (!String.IsNullOrEmpty(ErrorMessage)) {
					
					var js = new JScriptString(
						ResHelper.Format(@"
frameMgr.SetErrorMessage({0});",
						JScriptString.QuoteString(ErrorMessage, false)));
					
					_sb.AppendLine(js.ToString());
				}

				if (m_isPostedPage) {
					// Clear any state waiting for additional information
					_sb.AppendLine(new JScriptString(@"
frameMgr.WaitForContentCompleted(0);").ToString());
				}

				return _sb.ToString();
			}
			
			_sb.Append(
				string.Join(string.Empty,
					new List<HiddenControlInfo>(this.HiddenControls)
						// Some cases do not have code to initialize frameMgr
						.FindAll(_ctl => !string.IsNullOrEmpty(_ctl.FrameManagerInitializationScript))
						.ConvertAll(_ctl => _ctl.FrameManagerInitializationScript.ToString())
						.ToArray()));
			
			return _sb.ToString();
		}

		/// <summary>
		/// Returns true if the data model is being written as the page is being rendered.
		/// </summary>
		public bool WriteDataModel
		{
			get
			{
				// If the page did not load successfully, don't do anything
				if (!m_pageLoadSuccessful)
					return false;

				// Only write data model on a GET request if this is an initialization process 
				// and the current activity is a SCO.
				return ((Session.CurrentActivityResourceType == ResourceType.Sco) && (m_isFramesetInitialization));
			}
		}
		#endregion  // called from aspx
		
		// Small helper class to save information about the pending response to an IsMove*Valid request.
		internal class IsNavValidResponseData
		{
			readonly string m_command;
			readonly string m_commandData;
			readonly bool m_response;

			public IsNavValidResponseData(string command, string commandData, bool response)
			{
				m_command = command;
				m_commandData = commandData;
				m_response = response;
			}

			// Gets the string to return to the client in a hidden field to convey the response to the IsMove*Valid request.
			public string ClientFieldResponse
			{
				// The format of field is:
				// command@Evalue@N
				// The value will be "true" or "false".
				// In the case of a choice command, the command will be of the form "C,strActivityId" and strActivityId will 
				// have any @ values replaced by @A.
				get
				{
					string command;
					if (m_command == Commands.DoIsChoiceValid) {
						string commandData = m_commandData.Replace("@", "@A");
						command = ResHelper.FormatInvariant("C,{0}", commandData);
					} else {
						// For IsMoveNextValid or IsMovePreviousValid, the command data determines whether it's N or P.
						command = m_commandData;
					}
					return ResHelper.FormatInvariant("{0}@E{1}@N", command, m_response ? "true" : "false");
				}
			}
		}
	}

	class HiddenControlInfo
	{
		#region Properties

		public string Id { private get; set; }
		public string Value { private get; set; }

		string m_js;
		/// <summary>
		/// Script to inject into the onload handler to initialize frameManager with this information.
		/// </summary>
		public string FrameManagerInitializationScript {
			get {
				return
					string.Format(
						this.m_js ?? string.Empty,
						JScriptString.QuoteString(this.Value));
			}
			set { this.m_js = value; }
		}

		public string ExtractFieldValueExpression {
			get {
				return
					string.IsNullOrEmpty(this.Id)
						? ""
						: "$('#" + this.Id + "').val()";
			}
		}

		#endregion Properties

		#region Methods

		public void WriteFieldTo(HtmlStringWriter writer)
		{
			if(!string.IsNullOrEmpty(this.Id) && !string.IsNullOrEmpty(this.Value)) {
				WriteHiddenControl(writer, this.Id, this.Value);
				writer.WriteText("\r\n");
			}
		}

		/// <summary>
		/// Write a hidden control to the string writer. The controlId shows up in id= and name= attributes. The 
		/// value is the Value of the control.
		/// </summary>
		static void WriteHiddenControl(
			HtmlStringWriter sw,
			PlainTextString controlId,
			PlainTextString value)
		{
			sw.AddAttribute(HtmlTextWriterAttribute.Type, new HtmlString("hidden"));
			sw.AddAttribute(HtmlTextWriterAttribute.Id, controlId.ToHtmlString());
			sw.AddAttribute(HtmlTextWriterAttribute.Name, controlId.ToHtmlString());
			sw.AddAttribute(HtmlTextWriterAttribute.Value, value.ToHtmlString());
			sw.RenderBeginTag(HtmlTextWriterTag.Input);
			sw.RenderEndTag();
		}

		#endregion Methods
	}
}
