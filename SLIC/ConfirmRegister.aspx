<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmRegister.aspx.cs" Inherits="index" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SLIC Account Activation</title>

    <style>
body
{
background: #ffffff
}
.container {
  width: 100%;
  padding-right: 5px;
  padding-left: 5px;
  margin-top: -.4vw;
  margin-right: auto;
  margin-left: auto;
}
@media (max-width: 499px) {
  .container {
    max-width: 100%;
  }
  #divTitile {
     max-width: 75%;max-height: 40px;
  }
  .describTable
  {
  	 min-width: 78%;font-size: 78%
  }
  .bannerID
  {
  	max-width: 5.5vw
  }
  #mgsDiv
  {
      height: 10vw;line-height: 10vw;
  }
  #bannerLogo
  {
      max-width:18vw;
  }
  .h1
  {
  	font-size: 3vw
  }
   #btnLogin
        {
    height: 10vw;font-size: 5vw;padding: 2vw
    }
    
   #divBtn
    {
        margin-top: 10vw
    }
  .padingSize{
       padding-top: 5vw;padding-bottom: 5vw
   }
  #divBottom{

    position: absolute; bottom: -2vw; height: 3vw;font-family: 'Arial Unicode MS'; font-size: 2vw;
}
}
@media (min-width: 500px) {
  .container {
    max-width: 100%;
  }
  #divTitile {
     max-width: 450px;max-height: 50px;
  }
  .describTable
  {
  	 min-width: 465px;font-size: 16px
  }
  .bannerID
  {
  	max-width: 28px
  }
  .h1
  {
  	font-size: 3vw
  }
  #btnLogin
        {
    height: 15vw;font-size: 5vw;padding: 2vw
    }
    
   #divBtn
    {
        margin-top: 20vw
    }
   #bannerLogo
  {
      max-width: 18vw;
  }
   .padingSize{
       padding-top: 5vw;padding-bottom: 5vw
   }
   #divBottom{

    position: absolute; bottom: -2vw; height: 3vw;font-family: 'Arial Unicode MS'; font-size: 2vw;
}
    #mgsDiv
  {
      height: 10vw;line-height: 10vw;
  }
}

@media (min-width: 768px) {
  .container {
    max-width: 100%;
  }
  #divTitile {
     max-width: 720px;max-height: 60px;
  }
  .describTable
  {
  	 min-width: 720px;font-size: 18px
  }
   .bannerID
  {
  	max-width: 6.8vw
  }
    #bannerLogo
  {
      max-width: 16vw;
  }
  .h1
  {
  	font-size: 3vw
  }
    #btnLogin
        {
    height: 15vw;font-size: 5vw;padding: 3vw
    }
    
   #divBtn
    {
        margin-top: 20vw
    }
   .padingSize{
       padding-top: 5vw;padding-bottom: 5vw
   }
   #divBottom{

    position: absolute; bottom: -2vw; height: 3vw;font-family: 'Arial Unicode MS'; font-size: 2.2vw;
}
    #mgsDiv
  {
      height: 10vw;line-height: 10vw;
  }
}

@media (min-width: 992px) {
  .container {
    max-width: 100%;
  }
   #divTitile {
     max-width: 960px;max-height: 80px;
  }
   .describTable
  {
  	 min-width: 960px;font-size: 22px
  }
  .bannerID
  {
  	max-width: 3.5vw;
  }
  .h1
  {
  	font-size: 3.2vw
  }
  #btnLogin
    {
    height: 2vw;font-size: 2vw;padding: 1vw
    }
   #divBtn
    {
        margin-top: 5vw
    }
   #bannerLogo
  {
      max-width: 18vw;
  }
    .padingSize{
       padding-top: 4vw;padding-bottom: 1vw
   }
    #divBottom{

    position: absolute; bottom: -2vw; height: 3vw;font-family: 'Arial Unicode MS'; font-size: 1vw;
}
}

@media (min-width: 1200px) {
  .container {
    max-width: 100%;
  }
  #divTitile {
     max-width: 1140px;max-height: 100px;
  }
   .describTable
  {
  	 min-width: 1140px;font-size: 25px
  }
  .bannerID
  {
  	max-width: 3.5vw;
  }
  .h1
  {
  	font-size: 2vw
  }
  #btnLogin
    {
    height: 2vw;font-size: 2vw;padding: 1vw
    }
   #divBtn
    {
        margin-top: 5vw
    }
   #bannerLogo
  {
      max-width: 18vw
  }
   .padingSize{
       padding-top: 4vw;padding-bottom: 1vw
   }
    #divBottom{

    position: absolute; bottom: -2vw; height: 3vw;font-family: 'Arial Unicode MS'; font-size: 1vw;
}
}


.container-fluid {
  width: 100%;
  padding-right: 15px;
  padding-left: 15px;
  margin-right: auto;
  margin-left: auto;
}
	
td
{
	font-family: arial;color: #000;padding: 5px
}

 .save_but_default
        {
            border: .1vw #dedede solid;border-radius: 1.5vw;padding-left: 2vw;padding-right: 2vw;padding-top: .7vw;padding-bottom: .7vw;background: #028a97;font-family: Arial, Helvetica, sans-serif;line-height: .5vw;color: #fffaff;font-weight: bold;outline: none;cursor: pointer;text-decoration: none;
            -webkit-transition: color .5s linear, background .5s linear;
    -moz-transition: color .5s linear, background .5s linear;
    -ms-transition: color .5s linear, background .5s linear;
    -o-transition: color .5s linear, background .5s linear;
    transition: color .5s linear, background .5s linear;
        }
        .save_but_default:hover
        {
            border: .1vw #dedede solid;border-radius: 1.5vw;padding-left: 2vw;padding-right: 2vw;padding-top: .7vw;padding-bottom: .7vw;background: #919591;font-family: Arial, Helvetica, sans-serif;line-height: .5vw;color: #b8bab8;font-weight: bold;outline: none;cursor: pointer;text-decoration: none;
            -webkit-transition: color .5s linear, background .5s linear;
            -moz-transition: color .5s linear, background .5s linear;
            -ms-transition: color .5s linear, background .5s linear;
            -o-transition: color .5s linear, background .5s linear;
            transition: color .5s linear, background .5s linear;
        }
        .save_but_default
        {
            border: .1vw #dedede solid;border-radius: 1.5vw;padding-left: 2vw;padding-right: 2vw;padding-top: .7vw;padding-bottom: .7vw;background: #028a97;font-family: Arial, Helvetica, sans-serif;line-height: .5vw;color: #fffaff;font-weight: bold;outline: none;cursor: pointer;text-decoration: none;
    -webkit-transition: color .5s linear, background .5s linear;
    -moz-transition: color .5s linear, background .5s linear;
    -ms-transition: color .5s linear, background .5s linear;
    -o-transition: color .5s linear, background .5s linear;
    transition: color .5s linear, background .5s linear;

    

        }

        .link_default
        {
            font-family: Arial, Helvetica, sans-serif;line-height: .5vw;color: #919591;font-weight: bold;outline: none;cursor: pointer;text-decoration: none;
            -webkit-transition: color .5s linear, background .5s linear;
    -moz-transition: color .5s linear, background .5s linear;
    -ms-transition: color .5s linear, background .5s linear;
    -o-transition: color .5s linear, background .5s linear;
    transition: color .5s linear, background .5s linear;
        }
        .link_default:hover
        {
            font-family: Arial, Helvetica, sans-serif;line-height: .5vw;color: #bec3be;font-weight: bold;outline: none;cursor: pointer;text-decoration: none;
            -webkit-transition: color .5s linear, background .5s linear;
            -moz-transition: color .5s linear, background .5s linear;
            -ms-transition: color .5s linear, background .5s linear;
            -o-transition: color .5s linear, background .5s linear;
            transition: color .5s linear, background .5s linear;
        }
        .link_default
        {
            font-family: Arial, Helvetica, sans-serif;line-height: .5vw;color: #919591;font-weight: bold;outline: none;cursor: pointer;text-decoration: none;
    -webkit-transition: color .5s linear, background .5s linear;
    -moz-transition: color .5s linear, background .5s linear;
    -ms-transition: color .5s linear, background .5s linear;
    -o-transition: color .5s linear, background .5s linear;
    transition: color .5s linear, background .5s linear;

    

        }

      
}
</style>
</head>
<body class="container">
    <form id="form1" runat="server">
       
         <div class="" id="divTop" runat="server"></div>
        <div class="padingSize" style="background: #e7fafc;text-align:center;width: 100%; height: 14vw;line-height: 14vw">
            <div>
                <div  >
                    <img id="bannerLogo" src="images/appLogo.png" />
                </div>
               
            </div>
            

        </div>
       
    <div runat="server" id="mgsDiv">
        <div style="padding-left:1vw;float:left;height: 7.5vw;line-height: 9.5vw">
            <img id="bannerID1" class="bannerID" runat="server" />
        </div>
       
        <div style="height: 7.5vw;line-height: 7.5vw;margin-top: 5vw">
                <div id="h1" class="h1"  runat="server" style="font-family: arial;padding-left: 1vw" >Processing...</div>
            </div>
       <%--  <div id="addErr" runat="server">
           
            <div style="float:left;width: 100%">
                <hr style="color: #fda1a0;" />
                 </div>
        <div style="padding-left:1vw;float:left;height: 7.5vw;line-height: 9.5vw;margin-top: 5vw">
            <img id="bannerID2" class="bannerID" runat="server" />
        </div>
         <div style="height: 7.5vw;line-height: 7.5vw;margin-top: 5vw">
            <div  class="h1"  runat="server" style="font-family: arial;padding-left: 1vw;margin-top: 5vw" > Your registration token is invalid or has expired. Sign up for a new account.</div>
             </div>

              <div style="float:left;width: 100%">
                <hr style="color: #fda1a0;"  />
                 </div>

             <div style="padding-left:1vw;float:left;height: 7.5vw;line-height: 9.5vw;margin-top: 5vw">
            <img id="bannerID3" class="bannerID" runat="server" />
        </div>
         <div style="height: 7.5vw;line-height: 7.5vw;margin-top: 5vw">
            <div  class="h1"  runat="server" style="font-family: arial;padding-left: 1vw" > Your registration token is invalid or has expired. Sign up for a new account.</div>
             </div>
        </div>--%>

        <%--<div style="background: #e2ffec; height: 10vw;line-height: 7.5vw;width: 100%;color: #4caf50">

      
        <div style="height: 7.5vw;line-height: 7.5vw; margin-top: 5vw;text-align:center">
                <span  class="h1"  runat="server" style="font-family: arial;padding-left: 1vw" >Your registration token is invalid or has expired. Sign up for a new account.</span>
            <span  class="h1"  runat="server" style="font-family: arial;padding-left: 1vw" >Your registration token is invalid or has expired. Sign up for a new account.</span>
            </div>
              </div>--%>
    </div>
        <div id="divBtn" runat="server" style="width:100%;text-align: center;margin-bottom: 5vw">
            <asp:HyperLink ID="btnLogin" CssClass="save_but_default"  runat="server">Log In</asp:HyperLink>
             
        </div>
        <div runat="server" id="divWeb" style="width: 100%;background: #f2f3f3;height: 10vw;text-align:center;padding: 2vw;bottom: 2vw; ">
            <table style="width: 99%;padding-top: 1vw">
                <tr>
                    <td>
                        <div style="padding: .5vw">
                           <a href="http://www.srilankainsurance.com/" class="link_default">Corporate Site</a> 
                        </div>
                        <div style="padding: .5vw">
                            <a href="http://www.srilankainsurance.com/about-us/" class="link_default">About Us</a>
                        </div>
                        <div style="padding: .5vw">
                            <a href="http://www.srilankainsurance.com/press-room/" class="link_default">News</a>
                        </div>
                    </td>

                    <td>
                        <div style="padding: .5vw">
                            <a href="https://www.srilankainsurance.net/" class="link_default">Client Portal</a>
                        </div>
                        <div style="padding: .5vw">
                           <a href="https://www.srilankainsurance.net/ContactUs.aspx" class="link_default">Contact us</a> 
                        </div>
                        <div style="padding: .5vw">
                           &nbsp;
                        </div>
                       
                    </td>


                    <td>
                        <div style="padding: .5vw">
                            <a href="https://www.srilankainsurance.net/#" class="link_default">Quick Links</a>
                        </div>
                        <div style="padding: .5vw">
                            <a href="http://www.motortraffic.wp.gov.lk/" class="link_default" target="_blank">e-Revenue License</a>
                        </div>
                        <div runat="server" id="divApp" style="padding: .5vw">
                            <a class="link_default" style="cursor:pointer" id="divAppHref" runat="server"><span id="divAppTile" runat="server"></span></a>
                        </div>
                    </td>

                  
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="padding: .5vw;font-size: 1vw">
                            © 2018 Sri Lanka Insurance, All rights reserved
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        
            <div class="" id="divBottom" style="width: 100%;padding-top:8vw;background: #00adbb;text-align:center;padding: 0.5vw;" runat="server">  © 2018 Sri Lanka Insurance, All rights reserved.</div>
        
    </form>
</body>
</html>
