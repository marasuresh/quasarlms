using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using sec = System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Tools_Administration_CreateUserEditor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            passwordTextBox.Text = GenerateRandomPassword();
        }
    }

    const string set1 = "eyuioa";
    const string set2 = "qwrtpsdfghjklzxcvbnm";
    const string set3 = "0123456789";

    string GenerateRandomPassword()
    {
        char[] passwordChars = new char[6];
        
        Random rnd = new Random();

        for( int i = 0; i < passwordChars.Length; i++ )
        {
            switch( i % 3 )
            {
                case 0:
                    passwordChars[i] = set1[rnd.Next(set1.Length)];
                    break;

                case 1:
                    passwordChars[i] = set2[rnd.Next(set1.Length)];
                    break;

                case 2:
                    passwordChars[i] = set3[rnd.Next(set1.Length)];
                    break;
            }
        }

        return new string(passwordChars);
    }

    protected void createButton_Click(object sender, EventArgs e)
    {
        DceUserService.CreateUser(loginTextBox.Text);
        DceUserService.SetUserPassword(loginTextBox.Text, passwordTextBox.Text);

        DceUser user = DceUserService.GetUserByLogin(loginTextBox.Text);

        string[] nameParts = fullNameTextBox.Text.Split(' ');
        user.FirstName = nameParts[0];
        if( nameParts.Length>1 )
        {
            user.Patronymic = nameParts[1];
            if( nameParts.Length==3 )
                user.LastName = nameParts[2];
            else
                user.LastName = string.Join(" ",nameParts,2,nameParts.Length-2);
        }

        user.EMail = emailTextBox.Text;
        user.RegionID = RegionEditControl1.RegionGuid;

		string _role = RoleSelect1.SelectedRole;
		string _userName = user.Login;
		if(sec.Roles.RoleExists(_role) && null != sec.Membership.GetUser(_userName)) {
			sec.Roles.AddUserToRole(_userName, _role);
		}

        DceUserService.UpdateUser(user);
        Response.Redirect("User.aspx?id=" + user.ID);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect(".");
    }
}
