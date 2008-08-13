<%@ Application Language="C#" %>
<script RunAt="server">
	void Application_Start(object sender, EventArgs e)
	{
		string[] _roles = "Administrator,BusinessTutor,BusinessTutorRegional,Student,Tutor,TutorRegional".Split(new char[] {','});
		
		Array.ForEach(
			_roles,
			new Action<string>(
				delegate(string role)
				{
					if(!Roles.RoleExists(role)) {
						Roles.CreateRole(role);
					}
				}));
	}

	void Application_End(object sender, EventArgs e)
	{
		//  Code that runs on application shutdown

	}

	void Application_Error(object sender, EventArgs e)
	{
		if (HttpContext.Current.Error != null)
			MailErrorLog.SendErrorReport(HttpContext.Current.Error);
	}

	void Session_Start(object sender, EventArgs e)
	{
		// Code that runs when a new session is started
		Session["StudentExist"]=false;
	}

	void Session_End(object sender, EventArgs e)
	{
		// Code that runs when a session ends. 
		// Note: The Session_End event is raised only when the sessionstate mode
		// is set to InProc in the Web.config file. If session mode is set to StateServer 
		// or SQLServer, the event is not raised.

	}
</script>