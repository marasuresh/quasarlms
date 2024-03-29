<%@ Page Language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" Inherits="yaf.install._default" %>

<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="yaf" %>
<html>
<head>
    <title>Yet Another Forum.net Installation</title>
    <link type="text/css" rel="stylesheet" href="../forum.css">
    <link type="text/css" rel="stylesheet" href="../themes/FlatEarth/theme.css">
</head>
<body>
    <form runat="server">
        <table class="content" width="100%" cellspacing="1" cellpadding="0">
            <tr>
                <td class="header1">
                    Yet Another Forum.NET Installation</td>
            </tr>
            <tr>
                <td class="post">
                    <table cellspacing="1" cellpadding="0" width="100%" runat="server" id="stepWelcome">
                        <tr>
                            <td class="header2" colspan="2">
                                Welcome</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <p>
                                    This installation wizard that will guide you through the installation of Yet Another
                                    Forum.NET.
                                </p>
                                <p>
                                    Before you begin you should make sure you have setup your MS SQL server correctly.
                                    You can use an existing database, or you can create a new one.
                                </p>
                                <p>
                                    To begin the installation, click next.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="1" cellpadding="0" width="100%" runat="server" visible="false"
                        id="stepConfig">
                        <tr>
                            <td class="header2" colspan="2">
                                Step 1 of 5: Configuration setup</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <p>
                                    This step will attempt to verify your Web.config file. By default, YetAnotherForum.NET
                                    Web.config file is named Default.config. You might need to rename "<%= Server.MapPath("../Default.config") %>"
                                    to "<%= Server.MapPath("../Web.config") %>" if this is a new installation.
                                </p>
                                <asp:PlaceHolder runat="server" ID="ConfigSample" Visible="False">
                                    <p>
                                        Here is an example of a valid yafnet.config file (for NET v2.0):
                                    </p>
                                    <pre>
&lt;configuration&gt;
	&lt;configSections&gt;
		&lt;section name="yafnet" type="yaf.SectionHandler,yaf" /&gt;
	&lt;/configSections&gt;
	&lt;yafnet&gt;
		&lt;connstr&gt;user id=yaf;password=yafpass;data source=(local);initial catalog=yetanotherforum.net&lt;/connstr&gt;
		&lt;language&gt;english.xml&lt;/language&gt;
		&lt;theme&gt;standard.xml&lt;/theme&gt;
		&lt;uploaddir&gt;/yetanotherforum.net/upload/&lt;/uploaddir&gt;
	&lt;/yafnet&gt;
	&lt;system.web&gt;
		&lt;pages validateRequest="false" smartNavigation="false"/&gt;
		&lt;authentication mode="Forms"&gt;
			&lt;forms name="YAFNET_Authentication" timeout="525600" /&gt;
		&lt;/authentication&gt;
    &lt;/system.web&gt;	
&lt;/configuration&gt;
		</pre>
                                </asp:PlaceHolder>
                                <p>
                                    NOTE: Please make sure "smartNavigation" is set to false in your Web.config file
                                    or you will have problems with YetAnotherForum.NET. YetAnotherForum.NET includes
                                    it's own smartNavigation-like system.
                                    <br />
                                    Click next to verify the configuration.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="1" cellpadding="0" width="100%" runat="server" visible="false"
                        id="stepConnect">
                        <tr>
                            <td class="header2" colspan="2">
                                Step 2 of 5: Connect to database</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <p>
                                    Please verify that you have manually modified the connection string found in "<%= Server.MapPath("../yafnet.config") %>"
                                    to point to your database.
                                </p>
                                <p>
                                    When you have entered the correct connection string, click next to continue with
                                    the installation.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="1" cellpadding="0" width="100%" runat="server" visible="false"
                        id="stepDatabase">
                        <tr>
                            <td class="header2">
                                Step 3 of 5: Initialize database</td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    Your database will now be initialized. In a fresh install this means creating all
                                    tables and stored procedures. If this is an update to an already existing version,
                                    your database will be updated to the latest version.
                                </p>
                                <p>
                                    Click next to setup your database.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellspacing="1" cellpadding="0" runat="server" visible="false"
                        id="stepForum">
                        <tr>
                            <td colspan="2" class="header2">
                                Step 4 of 5: Create Forum</td>
                        </tr>
                        <tr>
                            <td class="postheader">
                                <b>Forum Name:</b><br />
                                The name of your forum.</td>
                            <td class="post">
                                <asp:TextBox ID="TheForumName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="postheader">
                                <b>Time Zone:</b><br />
                            </td>
                            <td class="post">
                                <asp:DropDownList ID="TimeZones" runat="server" DataTextField="Name" DataValueField="Value">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="postheader">
                                <b>Forum Email:</b><br />
                                The official forum email address.</td>
                            <td class="post">
                                <asp:TextBox ID="ForumEmailAddress" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="postheader">
                                <b>SMTP Server:</b><br />
                                The name of a smtp server used to send emails.</td>
                            <td class="post">
                                <asp:TextBox ID="SmptServerAddress" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="postheader">
                                <b>Admin User Name:</b><br />
                                The name of the admin user.</td>
                            <td class="post">
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="postheader">
                                <b>Admin Email:</b><br />
                                The administrators email address.</td>
                            <td class="post">
                                <asp:TextBox ID="AdminEmail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="postheader">
                                <b>Admin Password:</b><br />
                                The password of the admin user.</td>
                            <td class="post">
                                <asp:TextBox ID="Password1" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="postheader">
                                <b>Re-type Password:</b><br />
                                Verify the password.</td>
                            <td class="post">
                                <asp:TextBox ID="Password2" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="1" cellpadding="0" width="100%" runat="server" visible="false"
                        id="stepFinished">
                        <tr>
                            <td class="header2">
                                Done</td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    Your forum is now ready to use. If this was a fresh install you will need to enter
                                    the admin section of the forum to setup categories and forums.
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="footer1" align="center">
                    <asp:Button ID="back" Text="Back" runat="server" Enabled="false"></asp:Button>
                    <asp:Button ID="next" Text="Next" runat="server"></asp:Button>
                    <asp:Button ID="finish" Text="Finish" runat="server" Enabled="false"></asp:Button>
                    <asp:Label ID="cursteplabel" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <yaf:SmartScroller runat="server" />
    </form>
</body>
</html>
