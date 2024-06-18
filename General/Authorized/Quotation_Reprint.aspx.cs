using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Authorized_Quotation_Reprint : System.Web.UI.Page
{
    EncryptDecrypt dc = new EncryptDecrypt();
    Dictionary<string, string> qs = new Dictionary<string, string>();
    string RefID = "", poliID = "";
    public string errorMsg { get; set; }
 public string polno { get; set; }
    
    protected void Page_Load(object sender, EventArgs e)
    {

        string strReq = "";
        strReq = Request.RawUrl;
        string h = strReq.Substring(strReq.Length - 1);
        if (h == "#")
        {
            errorMsg = "No url";
            Response.Redirect("~/Errors/InternalError.htm");
        }
        else
        {
            strReq = strReq.Substring(strReq.IndexOf('?') + 1);

            EncryptDecrypt enc = new EncryptDecrypt();
            strReq = enc.Decrypt(strReq);
            if (strReq == "#")
            {
                errorMsg = "No Parameters Passed";
                Response.Redirect("~/Errors/InternalError.htm");
            }
            else
            {
                Dictionary<string, string> paraList = new Dictionary<string, string>();
                paraList = enc.getParameters(strReq);

                if (paraList.ContainsKey("refN0"))
                {
                    RefID = paraList["refN0"];
					
					if (paraList.ContainsKey("polNo"))
                    {
                        polno = paraList["polNo"];
                    }
                    else if (RefID.Contains("GTP"))
                    {
                        polno = RefID;
                    }

                    string refId = RefID.Trim();

                        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                        if (refId.Contains("MP"))
                        {
                            AMP_Quotation_print pdfPrint = new AMP_Quotation_print();
                            Proposal amp = new Proposal(refId);

                            if (amp != null)
                            {
                                string startDate = "";
                                string endDate = "";
                                string SA ="";

                                startDate = amp.comenmentDate;
                                endDate = amp.endDate;
                                SA = paraList["SA"];
                                SA = SA.Replace("Rs. ", "").Trim(); // from the grid 

                                Dictionary<string, string> dc = new Dictionary<string, string>();
                                dc.Add("refN0", refId.Trim());
                                //dc.Add("P0lNo", refId.Trim());
                                dc.Add("P0lNo", amp.policy_no.Trim());
                                dc.Add("StaDt", startDate);
                                dc.Add("EndDt", endDate);
                                dc.Add("SM", SA);

                                Response.Redirect(enc.url_encrypt("Policy_Reprint.aspx", dc));
                            }
                        
                        }
                        else if (refId.Contains("HIP"))
                        {
                            HP_Quotation_Print pdfPrint = new HP_Quotation_Print();

                            pdfPrint.print_quotation(refId, Page.User.Identity.Name, ip, true);
                            //pdfPrint.print_policy (refId, Page.User.Identity.Name, ip, true);
                        }
                        else if (refId.Contains("GTI"))
                        {
                            GT_print_pdf pdfPrint = new GT_print_pdf();
                            //pdfPrint.print_quotation(refId, Page.User.Identity.Name, ip, true);
                            poliID = paraList["P0lNo"];
                            pdfPrint.print_policy(refId, poliID, Page.User.Identity.Name, ip, true);
                        }
						else if (polno.Contains("GTP"))
                       {
                           TRV_print_pdf trvpdfPrint = new TRV_print_pdf();
                           //pdfPrint.print_quotation(refId, Page.User.Identity.Name, ip, true);
                            
                           trvpdfPrint.print_policy(polno, Page.User.Identity.Name, ip, true);
                        }
                    
                }
            }
        }
    }
}