using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class Life_Authorized_Products_Payment_Confirmation_Amex : System.Web.UI.Page
{
    private string PTInvoice = "";
    private string EncryptedInvoice = "";
    private int iTransType = 0;

    Properties propAmex = new Properties("Life", "Amex");
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //btnSubmit.PostBackUrl = WebConfigurationManager.AppSettings["IPGServer"].ToString();
        btnSubmit.PostBackUrl = propAmex.HostUrl;

        if (!IsPostBack)
        {
            if (Request.Form["action"] != null)
                iTransType = int.Parse("1");
            lblTransType.Text = iTransType.ToString();
            //populateInterface();
        }
        else
        {
            iTransType = int.Parse(lblTransType.Text.ToString());

        }

        //lblSvrUrl.Text = WebConfigurationManager.AppSettings["IPGServer"].ToString();
        lblSvrUrl.Text = propAmex.HostUrl;
        encryptData();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("buy.html", true);
    }

    //protected void btnEncrypt_Click(object sender, EventArgs e)
    //{
    //    encryptData();
    //}

    public void encryptData()
    {

        switch (iTransType)
        {
            case 1:

                PTInvoice = "<req>";
                PTInvoice += "<mer_id>" + Request.Form["mer_id"] + "</mer_id>";
                PTInvoice += "<mer_txn_id>" + Request.Form["mer_txn_id"] + "</mer_txn_id>";
                PTInvoice += "<action>" + Request.Form["action"] + "</action>";
                PTInvoice += "<txn_amt>" + Request.Form["txn_amt"] + "</txn_amt>";
                PTInvoice += "<cur>" + Request.Form["cur"] + "</cur>";
                PTInvoice += "<lang>" + Request.Form["lang"] + "</lang>";
                PTInvoice += "<ret_url>" + Request.Form["ret_url"] + "</ret_url>";

                //if (txtSaleTxnRtnUrl.Text.Trim().Length > 0)
                //    PTInvoice += "<ret_url>" + txtSaleTxnRtnUrl.Text + "/Response.aspx</ret_url>";

                //if (txtSaleTxnMV1.Text.Trim().Length > 0)
                //    PTInvoice += "<mer_var1>" + txtSaleTxnMV1.Text + "</mer_var1>";

                //if (txtSaleTxnMV2.Text.Trim().Length > 0)
                //    PTInvoice += "<mer_var2>" + txtSaleTxnMV2.Text + "</mer_var2>";

                //if (txtSaleTxnMV3.Text.Trim().Length > 0)
                //    PTInvoice += "<mer_var3>" + txtSaleTxnMV3.Text + "</mer_var3>";

                //if (txtSaleTxnMV4.Text.Trim().Length > 0)
                //    PTInvoice += "<mer_var4>" + txtSaleTxnMV4.Text + "</mer_var4>";

                PTInvoice += "</req>";

                //pnlSaleTxn.Visible = false;
                break;

        }

        try
        {
            IShroff objIShroff = new IShroff();

            //Un comment and edit below 2 lines if your security keys are present in a custom folder 
            //other than <iPay Client Service>\keys

            //string strKeySet_1 = WebConfigurationManager.AppSettings["IPGKeyPath_1"];

            string strKeySet_1 = propAmex.IPGKeyPath_1;
            objIShroff.setSecurityKeysPath(strKeySet_1);


            bool bResult = objIShroff.setPlainTextInvoice(PTInvoice);
            
            if (bResult)
            {
                EncryptedInvoice = objIShroff.getEncryptedInvoice();
                hdf_Amex_Err_State.Value = "FALSE";
            }
            else
            {
                log logger = new log();
                logger.write_log("Failed at iPay client service error : Error Code " + objIShroff.getErrorCode() + " and Error Description : " + objIShroff.getErrorMessage());

                hdf_Amex_Err_State.Value = "TRUE";
                btnSubmit.Visible = false;
                btnSubmit.Enabled = false;
                //Response.Write("Error Code:" + objIShroff.getErrorCode());
                //Response.Write("<BR> Error Description :" + objIShroff.getErrorMessage());
            }

            txtPlainInvoice.Text = PTInvoice;
            encryptedInvoicePay.Text = EncryptedInvoice;
            //pnlEncryptedData.Visible = true;

            //btnEncrypt.Visible = false;
            btnSubmit.Visible = true;
            //lblHeader.Visible = false;
            btnBack.Visible = false;
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.Message);
        }

        
    }
}