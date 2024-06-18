using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class LoginTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {
        
        ULCustomer customer = new ULCustomer();
        int failedLogincount = customer.GetFailedLoginCount(LoginUser.UserName);
        if (failedLogincount > -1 && failedLogincount < 3)
        {
            bool authenticated = customer.ValidateLogin(LoginUser.UserName, LoginUser.Password, failedLogincount);

            if (authenticated)
            {
                LoginUser.UserName = customer.getLoginName(LoginUser.UserName); 
                FormsAuthentication.RedirectFromLoginPage(LoginUser.UserName, LoginUser.RememberMeSet);
            }
            else
            {
                LoginUser.FailureText = "Your login attempt was not successful. Please visit below link if you have forgotten password.";
            }
        }
        else if (failedLogincount >= 3)
        {
            LoginUser.FailureText = "Your account has been locked out. Please visit link below to reset your password";
        }
        else
        {
            LoginUser.FailureText = "Error occured at login. Please contact support.";
        }
    
    }
}