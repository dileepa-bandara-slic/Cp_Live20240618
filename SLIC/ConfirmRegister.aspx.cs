using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var Curent_browser = new DeviceFinder();
        bool registerConfirmed = false;
        //string strError = " Oops! Account activation failed or already activated !";
        //string strDone = " Congratulations! Your account has been activated.";

        string strError = "";
        string strDone = "";

        try
        {
            if (Request.QueryString["regtokn"] == null || Request.QueryString["regtokn"].ToString() == "")
            {
                strError = " Oops! Account activation failed or already activated !";
                //addErr.Visible = false;
            }
            else if (Request.QueryString["regtokn"].ToString() != "")
            {
                string regToken = Request.QueryString["regtokn"].ToString();
                ULCustomer guest = new ULCustomer();
                if (guest.isValidRegToken(regToken))
                {
                    registerConfirmed = guest.ConfirmRegistration(regToken);

                    if (registerConfirmed)
                    {

                        strDone = " Congratulations! Your account has been activated.";

                        //addErr.Visible = false;


                    }
                    else
                    {
                        //addErr.Visible = false;
                        strError = " Registration failed due to internal error. ";
                        mgsDiv.Attributes.Add("Style", "background: #fee6e5; height: 7.5vw;line-height: 7.5vw;width: 100%;color: #f76260");
                    }
                }
                else
                {
                    //addErr.Visible = true;
                    //bannerID2.Src = "images/iconFailed.png";
                    //bannerID3.Src = "images/iconFailed.png";
                    strError = " Oops! Your account already activated and this link no longer valid.";
                    mgsDiv.Attributes.Add("Style", "background: #fee6e5; height:  7.5vw;line-height:  7.5vw;width: 100%;color: #f76260;");

                    if (Request.QueryString["devType"] != null && Request.QueryString["devType"].ToString() == "Phone")
                    {
                        strError = " Your registration token is invalid or has expired.";
                        mgsDiv.Attributes.Add("Style", "background: #fee6e5; height: 7.5vw;line-height: 7.5vw;width: 100%;color: #f76260");

                    }

                }
            }
        }
        catch (Exception ex)
        {
            log logger = new log();
            logger.write_log("Failed at ConfirmRegister: " + ex.ToString());
            strError = " Registration failed due to some internal error.";
        }


        // check for device
        if (Curent_browser.getDeviceOS().Substring(0, 6) == "iPhone")
        {
            //h1.InnerText = Curent_browser.getDeviceOS();
            btnLogin.Attributes.Add("OnClick", "window.open('https://apps.apple.com/lk/app/slic-customer/id1476655848')");
            divAppTile.InnerText = "Client Portal Login";
            divAppHref.Attributes.Add("OnClick", "window.open('https://apps.apple.com/lk/app/slic-customer/id1476655848','_blank')");
            divWeb.Visible = false;
            divTop.Attributes.Add("Style", "width: 100%;margin-top: -2vw;padding-top:10vw;background: #00adbb");
            divBottom.Visible = true;
           

            if(registerConfirmed)
            {
                btnLogin.Visible = true ;

                //succesful mgs

                bannerID1.Src = "images/iconsDone00.png";
                h1.InnerText = strDone;
                mgsDiv.Attributes.Add("Style", "background: #e2ffec; min-height: 7.5vw;line-height: 7.5vw;width: 100%;color: #4caf50");
            }
            else
            {
                btnLogin.Visible = false;

                //failure mgs

                bannerID1.Src = "images/iconFailed.png";
                h1.InnerText = strError;
                //mgsDiv.Attributes.Add("Style", "background: #fee6e5; height: 7.5vw;line-height: 7.5vw;width: 100%;color: #f76260");
            }
        }
        else if (Curent_browser.getDeviceOS() == "Kindle Fire / Android")
        {
            
            btnLogin.NavigateUrl = "intent://slic.customer.com#Intent;scheme=http;package=com.slic.customer;end";
            divAppTile.InnerText = "Client Portal Login";
            divAppHref.Attributes.Add("OnClick", "window.open('https://www.srilankainsurance.net/Login.aspx','_blank')");
            divWeb.Visible = false;
            divTop.Attributes.Add("Style", "width: 100%;margin-top: -2vw;padding-top:10vw;background: #00adbb");
            divBottom.Visible = true;

            if (registerConfirmed)
            {
                btnLogin.Visible = true;

                //succesful mgs

                bannerID1.Src = "images/iconsDone00.png";
                h1.InnerText = strDone;
                mgsDiv.Attributes.Add("Style", "background: #e2ffec; height: 7.5vw;line-height: 7.5vw;width: 100%;color: #4caf50");
            }
            else
            {
                btnLogin.Visible = false;

                //failure mgs

                bannerID1.Src = "images/iconFailed.png";
                h1.InnerText = strError;
                //mgsDiv.Attributes.Add("Style", "background: #fee6e5; height: 7.5vw;line-height: 7.5vw;width: 100%;color: #f76260");
            }
        }
        else if (Curent_browser.getDeviceOS().Substring(0, 4) == "iPad")
        {

            btnLogin.Attributes.Add("OnClick", "window.open('https://apps.apple.com/lk/app/slic-customer/id1476655848')");
            divAppTile.InnerText = "Client Portal Login";
            divAppHref.Attributes.Add("OnClick", "window.open('https://apps.apple.com/lk/app/slic-customer/id1476655848','_blank')");
            divWeb.Visible = false;
            divTop.Attributes.Add("Style", "width: 100%;margin-top: -2vw;padding-top:10vw;background: #00adbb");
            divBottom.Visible = true;

            if (registerConfirmed)
            {
                btnLogin.Visible = true;

                //succesful mgs

                bannerID1.Src = "images/iconsDone00.png";
                h1.InnerText = strDone;
                mgsDiv.Attributes.Add("Style", "background: #e2ffec; height: 7.5vw;line-height: 7.5vw;width: 100%;color: #4caf50");
            }
            else
            {
                btnLogin.Visible = false;

                //failure mgs

                bannerID1.Src = "images/iconFailed.png";
                h1.InnerText = strError;
                //mgsDiv.Attributes.Add("Style", "background: #fee6e5; height: 7.5vw;line-height: 7.5vw;width: 100%;color: #f76260");
            }
        }
        else 
        {
            
            btnLogin.NavigateUrl = "https://www.srilankainsurance.net/Login.aspx";
            divAppTile.InnerText = "Customer App";
            divAppHref.Attributes.Add("OnClick", "window.open('https://play.google.com/store/apps/details?id=com.slic.customer','_blank')");
            divWeb.Visible = true;
            divWeb.Visible = btnLogin.Visible = false;
            divTop.Attributes.Add("Style", "width: 100%;margin-top: -2vw;padding-top:3.5vw;background: #00adbb");
            divBottom.Visible = true;
            if (registerConfirmed)
            {
                divBtn.Visible = true;
                divBtn.InnerText = "Now you can login to SLIC Mobile App";
                divBtn.Attributes.Add("Style", "margin: 10px 0px 0px 0px;text-align:center;font-size: 2.5vw;font-family: arial;color: #4caf50");
                //succesful mgs

                bannerID1.Src = "images/iconsDone00.png";
                h1.InnerText = strDone;
                mgsDiv.Attributes.Add("Style", "background: #e2ffec; height: 7.5vw;line-height: 7.5vw;width: 100%color: #4caf50");
            }
            else
            {
                divBtn.Visible = false;
                divBtn.InnerText = "Now you can login to SLIC Mobile App";
                divBtn.Attributes.Add("Style", "margin: 10px 0px 0px 0px;text-align:center;font-size: 2.5vw;font-family: arial;color: #4caf50");
                //failure mgs

                bannerID1.Src = "images/iconFailed.png";
                h1.InnerText = strError;
                //mgsDiv.Attributes.Add("Style", "background: #fee6e5; height: 7.5vw;line-height: 7.5vw;width: 100%;color: #f76260");
            }

           
        }
    }
    
}