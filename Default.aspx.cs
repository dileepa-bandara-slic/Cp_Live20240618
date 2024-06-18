using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    public string errorMsg { get; set; }
    public int server_rand_no { get; set; }
    EncryptDecrypt dc = new EncryptDecrypt();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AppendHeader("Refresh", "900");


        if (!Page.IsPostBack)
        {


            set_randNo();


            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
            {
                System.Web.UI.WebControls.Login lg = (System.Web.UI.WebControls.Login)LoginView1.Controls[0].Controls[0].FindControl("LoginUser");

                Literal FailureText = (Literal)lg.FindControl("FailureText");

                PlaceHolder plh_error = (PlaceHolder)lg.FindControl("plh_error");

                if (Request.QueryString.Keys.Count > 0)
                {
                    string strReq = "";
                    errorMsg = "";
                    strReq = Request.RawUrl;
                    string h = strReq.Substring(strReq.Length - 1);
                    if (h == "#")
                    {
                        errorMsg = "No url";
                        Server.Transfer("ErrorPage.aspx");
                    }
                    else
                    {


                        strReq = strReq.Substring(strReq.IndexOf('?') + 1);
                        //strReq = strReq.Replace("%20", "+"); // mehema wenne ai, I dont know why
                        //strReq = strReq.Replace("%2f", "/"); 

                        strReq = dc.Decrypt(strReq);
                        if (strReq == "#")
                        {
                            errorMsg = "No Parameters Passed";
                            //Server.Transfer("ErrorPage.aspx");
                            //Response.Write(Request.RawUrl);
                        }
                        else
                        {
                            Dictionary<string, string> paraList = new Dictionary<string, string>();
                            ULCustomer customer = new ULCustomer();
                            int failedLogincount = customer.GetFailedLoginCount(lg.UserName);
                            paraList = dc.getParameters(strReq);

                            if (paraList.ContainsKey("ErC0de"))
                            {
                                int id = -1;
                                int errlogCnt = 0;
                                try
                                {
                                    id = Convert.ToInt32(paraList["ErC0de"]);
                                }
                                catch
                                {
                                    id = 0;
                                }
                                try
                                {
                                    errlogCnt = Convert.ToInt32(paraList["ErAttempt"]);
                                }
                                catch
                                {
                                    errlogCnt = 0;
                                }

                                switch (id)
                                {
                                    case 101:
                                        if (errlogCnt >= 2)
                                        {
                                            FailureText.Text = "Your login attempt was not successful. Please visit below link if you have forgotten password.";
                                            plh_error.Visible = true;
                                            break;
                                        }
                                        else
                                        {
                                            FailureText.Text = "Your login attempt was not successful. You have " + (2 - errlogCnt) + " attempt(s) left. Please visit below link if you have forgotten password.";
                                            plh_error.Visible = true;
                                            break;
                                        }

                                    case 102:
                                        FailureText.Text = "Dear Customer,\n Required to further review the information entered. We will revert soon.";
                                        plh_error.Visible = true;
                                        break;

                                    case 202:
                                        FailureText.Text = "Your account has been locked out. Please visit link below to reset your password.";
                                        plh_error.Visible = true;
                                        break;

                                    case 303:
                                        FailureText.Text = "Error occured at login. Please contact support.";
                                        plh_error.Visible = true;
                                        break;

                                    case 404:
                                        FailureText.Text = "Please enter credentials again.";
                                        plh_error.Visible = true;
                                        break;
                                    default:
                                        FailureText.Text = "";
                                        plh_error.Visible = true;
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }


        }
    }

    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {

        int authenticated = -1;

        System.Web.UI.WebControls.Login lg = (System.Web.UI.WebControls.Login)LoginView1.Controls[0].Controls[0].FindControl("LoginUser");

        HiddenField hdn_hashed_pwd = (HiddenField)lg.FindControl("hdn_pwd_sbmt");
        HiddenField hdn_server_code = (HiddenField)lg.FindControl("hdn_server");
        HiddenField hdn_client_code = (HiddenField)lg.FindControl("hdn_client");

        if (Session["severTag"] != null)
        {
            ULCustomer customer = new ULCustomer();
            int failedLogincount = customer.GetFailedLoginCount(lg.UserName);
            if (failedLogincount > -1 && failedLogincount < 3)
            {
                //bool authenticated = customer.ValidateLogin(LoginUser.UserName, LoginUser.Password, failedLogincount, (string)Session["severTag"], hdn_client_code.Value);
                
                authenticated = customer.ValidateLogin(lg.UserName, hdn_hashed_pwd.Value, failedLogincount, (string)Session["severTag"], hdn_client_code.Value);

                if (authenticated == 0)
                {
                    Session["severTag"] = null;
                    lg.UserName = customer.getLoginName(lg.UserName);
                    //FormsAuthentication.RedirectFromLoginPage(LoginUser.UserName, LoginUser.RememberMeSet);
                    FormsAuthentication.SetAuthCookie(lg.UserName, lg.RememberMeSet);
                    Response.Redirect("/");
                }
                else
                {
                    //authenticated = 101;
                    //set_randNo();
                    //lg.FailureText = "Your login attempt was not successful. You have " + (2 - failedLogincount) + " attempt(s) left. Please visit below link if you have forgotten password.";
                    //Dictionary<string, string> dic = new Dictionary<string, string>();
                    //dic.Add("ErC0de", authenticated.ToString());
                    //dic.Add("ErAttempt", failedLogincount.ToString());
                    //Response.Redirect(dc.url_encrypt("Default.aspx", dic));

                    if (authenticated == -2)
                    {
                        authenticated = 102;
                        set_randNo();
                        lg.FailureText = "Dear Customer,\n Required to further review the information entered. We will revert soon.";
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("ErC0de", authenticated.ToString());
                        dic.Add("ErAttempt", failedLogincount.ToString());
                        Response.Redirect(dc.url_encrypt("Default.aspx", dic));
                    }
                    else
                    {
                        authenticated = 101;
                        set_randNo();
                        lg.FailureText = "Your login attempt was not successful. You have " + (2 - failedLogincount) + " attempt(s) left. Please visit below link if you have forgotten password.";
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("ErC0de", authenticated.ToString());
                        dic.Add("ErAttempt", failedLogincount.ToString());
                        Response.Redirect(dc.url_encrypt("Default.aspx", dic));
                    }

                }
            }
            else if (failedLogincount >= 3)
            {
                authenticated = 202;
                set_randNo();
                lg.FailureText = "Your account has been locked out. Please visit link below to reset your password";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ErC0de", authenticated.ToString());
                Response.Redirect(dc.url_encrypt("Default.aspx", dic));

            }
            else
            {
                authenticated = 303;
                set_randNo();
                lg.FailureText = "Error occured at login. Please contact support.";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ErC0de", authenticated.ToString());
                Response.Redirect(dc.url_encrypt("Default.aspx", dic));

            }
        }
        else
        {
            authenticated = 404;
            set_randNo();
            lg.FailureText = "Please enter credentials again.";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ErC0de", authenticated.ToString());
            Response.Redirect(dc.url_encrypt("Default.aspx", dic));

        }


        //ULCustomer customer = new ULCustomer();
        //System.Web.UI.WebControls.Login lg = (System.Web.UI.WebControls.Login)LoginView1.Controls[0].Controls[0].FindControl("LoginUser");
        //TextBox txt_username = (TextBox)lg.FindControl("UserName");
        //TextBox txt_password = (TextBox)lg.FindControl("Password");
        //Label Label1 = (Label)lg.FindControl("Label1");
        //PlaceHolder pl = (PlaceHolder)lg.FindControl("logn");
        //pl.Visible = false;




        //int failedLogincount = customer.GetFailedLoginCount(txt_username.Text);
        //if (failedLogincount > -1 && failedLogincount < 3)
        //{
        //    bool authenticated = customer.ValidateLogin(txt_username.Text, txt_password.Text, failedLogincount);

        //    if (authenticated)
        //    {
        //        pl.Visible = false;

        //        lg.UserName = customer.getLoginName(lg.UserName);
        //        //LoginUser.UserName = customer.getLoginName(LoginUser.UserName); 

        //        //FormsAuthentication.RedirectFromLoginPage(lg.UserName, lg.RememberMeSet);
        //        FormsAuthentication.SetAuthCookie(lg.UserName, lg.RememberMeSet);
        //        Response.Redirect("/");
        //        Label1.Text = "";

                

        //    }
        //    else
        //    {
        //        pl.Visible = true;
        //        lg.FailureText = "Your login attempt was not successful. Please visit <a style='color:#FFF' href='ForgotPassword.aspx'>&nbsp;<u>this link</u></a>&nbsp;if you have forgotten your password.";
        //        Label1.Text = "Your login attempt was not successful. Please visit <a style='color:#FFF' href='ForgotPassword.aspx'>&nbsp;<u>this link</u></a>&nbsp;if you have forgotten your password.";
        //    }

        //}
        //else if (failedLogincount >= 3)
        //{
        //    pl.Visible = true;
        //    lg.FailureText = "Your account has been locked out. Please visit <a style='color:#FFF' href='ForgotPassword.aspx'>&nbsp;<u>this link</u></a>&nbsp; to reset your password";
        //    Label1.Text = "Your account has been locked out. Please visit <a style='color:#FFF' href='ForgotPassword.aspx'>&nbsp;<u>this link</u></a>&nbsp; to reset your password";
        //}
        //else
        //{
        //    pl.Visible = true;
        //    lg.FailureText = "Error occured at login. Please contact support.";
        //    Label1.Text = "Error occured at login. Please contact support.";
        //}
    }
    private void set_randNo()
    {
        int seed = Convert.ToInt32(DateTime.Now.ToString("ddHHmmss"));
        Random rnd = new Random(seed);
        server_rand_no = rnd.Next(100, 999);

        try
        {
            System.Web.UI.WebControls.Login lgu = (System.Web.UI.WebControls.Login)LoginView1.FindControl("LoginUser");


            HiddenField server_rand = (HiddenField)lgu.FindControl("hdn_server");

            server_rand.Value = server_rand_no.ToString();
            Session["severTag"] = server_rand_no.ToString();
        }
        catch
        { }
    }

}