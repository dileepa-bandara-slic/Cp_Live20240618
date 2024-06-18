using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
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

            Literal FailureText = (Literal)LoginUser.FindControl("FailureText");

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
                                        break;
                                    }
                                    else
                                    {
                                        FailureText.Text = "Your login attempt was not successful. You have " + (2 - errlogCnt) + " attempt(s) left. Please visit below link if you have forgotten password.";
                                        break;
                                    }

                                case 102:
                                    FailureText.Text = "Dear Customer,\n Required to further review the information entered. We will revert soon.";
                                    break;

                                case 202:
                                    FailureText.Text = "Your account has been locked out. Please visit link below to reset your password.";
                                    break;

                                case 303:
                                    FailureText.Text = "Error occured at login. Please contact support.";
                                    break;

                                case 404:
                                    FailureText.Text = "Please enter credentials again.";
                                    break;
                                default:
                                    FailureText.Text = "";
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

    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {
        int authenticated = -1;

        HiddenField hdn_hashed_pwd = (HiddenField)LoginUser.FindControl("hdn_pwd_sbmt");
        HiddenField hdn_server_code = (HiddenField)LoginUser.FindControl("hdn_server");
        HiddenField hdn_client_code = (HiddenField)LoginUser.FindControl("hdn_client");
        //Response.Redirect("http://www.google.lk?q=" + hdn_hashed_pwd.Value);

        if (Session["severTag"] != null)
        {
            //if ((string)Session["severTag"].Equals())
            ULCustomer customer = new ULCustomer();
            int failedLogincount = customer.GetFailedLoginCount(LoginUser.UserName);
            if (failedLogincount > -1 && failedLogincount < 3)
            {
                //bool authenticated = customer.ValidateLogin(LoginUser.UserName, LoginUser.Password, failedLogincount, (string)Session["severTag"], hdn_client_code.Value);
                authenticated = customer.ValidateLogin(LoginUser.UserName, hdn_hashed_pwd.Value, failedLogincount, (string)Session["severTag"], hdn_client_code.Value);

                if (authenticated == 0)
                {
                    Session["severTag"] = null;
                    LoginUser.UserName = customer.getLoginName(LoginUser.UserName);
                    //FormsAuthentication.RedirectFromLoginPage(LoginUser.UserName, LoginUser.RememberMeSet);
                    FormsAuthentication.SetAuthCookie(LoginUser.UserName, LoginUser.RememberMeSet);
                    Response.Redirect("/");
                }
                else
                {
                    if (authenticated == -2)
                    {
                        authenticated = 102;
                        set_randNo();
                        LoginUser.FailureText = "Dear Customer,\n Required to further review the information entered. We will revert soon.";
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("ErC0de", authenticated.ToString());
                        dic.Add("ErAttempt", failedLogincount.ToString());
                        Response.Redirect(dc.url_encrypt("Login.aspx", dic));
                    }
                    else
                    {
                        authenticated = 101;
                        set_randNo();
                        LoginUser.FailureText = "Your login attempt was not successful. You have " + (2 - failedLogincount) + " attempt(s) left. Please visit below link if you have forgotten password.";
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("ErC0de", authenticated.ToString());
                        dic.Add("ErAttempt", failedLogincount.ToString());
                        Response.Redirect(dc.url_encrypt("Login.aspx", dic));
                    }
                    

                }
            }
            else if (failedLogincount >= 3)
            {
                authenticated = 202;
                set_randNo();
                LoginUser.FailureText = "Your account has been locked out. Please visit link below to reset your password";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ErC0de", authenticated.ToString());
                Response.Redirect(dc.url_encrypt("Login.aspx", dic));

            }
            else
            {
                authenticated = 303;
                set_randNo();
                LoginUser.FailureText = "Error occured at login. Please contact support.";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ErC0de", authenticated.ToString());
                Response.Redirect(dc.url_encrypt("Login.aspx", dic));

            }
        }
        else
        {
            authenticated = 404;
            set_randNo();
            LoginUser.FailureText = "Please enter credentials again.";
            Dictionary<string, string> dic = new Dictionary<string,string>();
            dic.Add("ErC0de", authenticated.ToString());
            Response.Redirect(dc.url_encrypt("Login.aspx", dic));

        }
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {

    }

    private void set_randNo()
    {
        int seed = Convert.ToInt32(DateTime.Now.ToString("ddHHmmss"));
        Random rnd = new Random(seed);
        server_rand_no = rnd.Next(100, 999);
        HiddenField server_rand = (HiddenField)LoginUser.FindControl("hdn_server");
        server_rand.Value = server_rand_no.ToString();
        Session["severTag"] = server_rand_no.ToString();
    }
}