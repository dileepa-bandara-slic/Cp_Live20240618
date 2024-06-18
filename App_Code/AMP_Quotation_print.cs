using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Data;


/// <summary>
/// Summary description for Print_pdf
/// </summary>
public class AMP_Quotation_print
{
    DataManager dm = new DataManager();

	public AMP_Quotation_print()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void print_quotation(string qid, string epf, string ip, bool reprint, string QoP)
    {
        bool females_exists = true;

        AMP_Quotation_mast qmas = new AMP_Quotation_mast(qid);
        females_exists = qmas.females_exists(qid);

        Parameters par = new Parameters(qmas.Enrty_Date);

        Document document = new Document(PageSize.A4, 50, 50, 10, 10);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        if (reprint)
             phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf+" REPRINT", new Font(Font.COURIER, 8));
        else 
             phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        // top & bottom borders on by default 
        header.Border = Rectangle.NO_BORDER;
        // center header
        header.Alignment = 1;
        /*
         * HeaderFooter => add header __before__ opening document
         */
        document.Footer = header;

        


        Font titleFont1 = FontFactory.GetFont("Times New Roman", 9, Font.BOLD, new Color(0, 0, 0));
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_sml = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold_sml = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);

        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.gif");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(25f);
        //logo.SetAbsolutePosition(50, 770);
       // logo.SetAbsolutePosition(260, 760);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
       // watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark);

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        document.Add(logo);

        

        document.Add(new Paragraph("\n\n\n\n\n"));
        //document.Add(new Paragraph("SRI LANKA INSURANCE HEALTH ANNUAL MEDICAL PLAN", titleFont1));

        document.Add(new Paragraph("\n", bodyFont));
        Chunk titch1 = new Chunk( (QoP.Equals("P")? "Proposal form":"Quotation"), boldTableFont);
        titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);
        int[] clmwidths111 = { 8,1, 20 };

        PdfPTable tbl14 = new PdfPTable(3);

        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 10;
        tbl14.SpacingAfter = 10;
        tbl14.DefaultCell.Border = 0;

        
        
        var qmem = qmas.members.FirstOrDefault(o => o.member_id == qid + "_1" );

        tbl14.AddCell(new Phrase("Policy type", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase("Medi Plus Plan", bodyFont));

        tbl14.AddCell(new Phrase("Quotation no", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qid, bodyFont));

        tbl14.AddCell(new Phrase("Issued Date", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qmas.Enrty_Date, bodyFont));

        tbl14.AddCell(new Phrase("Name of the Proposer", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qmas.status+" "+qmas.name_1+" "+qmas.name_2, bodyFont));

        tbl14.AddCell(new Phrase("Address of the Proposer", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qmas.add_1+" "+qmas.add_2+" "+qmas.add_3+" "+qmas.add_4, bodyFont));

        tbl14.AddCell(new Phrase("Insured’s date of birth", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qmem.dob, bodyFont));

        if (qmas.no_persons >= 1)
        {
            DataTable data = new DataTable();
            data = qmas.get_members_lst(qid);

            if (data != null && data.Rows.Count > 0)
            {

            tbl14.AddCell(new Phrase("Members", bodyFont));
            tbl14.AddCell(new Phrase(": ", bodyFont));
            
            int[] clmwidths3 = { 5, 3,3 ,10};

            PdfPTable tbl2 = new PdfPTable(4);

            tbl2.SetWidths(clmwidths3);

            tbl2.WidthPercentage = 75;
            tbl2.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl2.SpacingBefore = 0;
            tbl2.SpacingAfter = 0;
            tbl2.DefaultCell.Border = 0;

           

            

                foreach (DataRow dr in data.Rows)
                {
                    string mem_type = dr[0].ToString().Trim();
                    string child_gendr = dr[1].ToString().Trim();
                    int count = Convert.ToInt32(dr[2].ToString().Trim());



                    string memtp = "";

                    switch (mem_type.ToUpper().Trim())
                    {
                        case "C":
                            memtp = "Children";
                            break;
                        case "S":
                            memtp = "Spouse";
                            break;
                    }

                    tbl2.AddCell(new Phrase(memtp, bodyFont));
                    tbl2.AddCell(new Phrase(count.ToString(), bodyFont));
                    tbl2.AddCell(new Phrase((child_gendr == "M" ? "Male" : (child_gendr == "F" ? "Female" : "-")), bodyFont));
                    tbl2.AddCell(new Phrase(""));


                }

                PdfPCell pp = new PdfPCell(tbl2);
                pp.HorizontalAlignment = 0;
                pp.BorderWidth = 0;
                tbl14.AddCell(pp);
            }

        }

        double d_amoumt_limit = Convert.ToDouble(qmas.plan_limit);
        tbl14.AddCell(new Phrase("Selected Plan", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qmas.plan , bodyFont));
        //tbl14.AddCell(new Phrase(qmas.plan + " (Basic Sum Insured Rs. " + d_amoumt_limit.ToString("N2")+" )", bodyFont));




        tbl14.AddCell(new Phrase("Basic Sum Insured", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(d_amoumt_limit.ToString("N2"), bodyFont));
       

       

        

        document.Add(tbl14);


        int[] clmwidths112 = { 3,30, 20 };

        PdfPTable tbl15 = new PdfPTable(3);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 5;
        tbl15.SpacingAfter = 0;
        tbl15.DefaultCell.Border = 0;



        PdfPCell cell = new PdfPCell(new Phrase("", bodyFont_bold_sml));
        cell.HorizontalAlignment = 0;
        cell.BackgroundColor = new Color(180, 180, 180);
        cell.BorderColor = new Color(200, 200, 200);

        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase("Benefit type", bodyFont_bold_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(200, 200, 200);
        cell.BackgroundColor = new Color(180, 180, 180);
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase("Annual Benefits", bodyFont_bold_sml));
        cell.HorizontalAlignment = 0;
        cell.BackgroundColor = new Color(180, 180, 180);
        cell.BorderColor = new Color(200, 200, 200);

        tbl15.AddCell(cell);

       // string sql = "select    D.MAINID, D.TITLE, D.CHILDREN, vod, B.LIMIT_AMOUNT , descr  from sligen.amp_benefits B, sligen.amp_benefits_def D " +
        //         " where D.MAINID = B.MAINID and plan_id = '" + qmas.plan.Trim() + "' and (sysdate between B.effect_from and B.effect_to ) order by D.mainid";

        string sql = "";

        if (females_exists)
            sql = "select    D.MAINID, D.TITLE, D.CHILDREN, vod, B.LIMIT_AMOUNT , descr  "+
            " from sligen.amp_benefits_v2 B, sligen.amp_benefits_def D  where " +
            " D.MAINID = B.MAINID and plan_id = '" + qmas.plan.Trim() + "' and (to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') >=  D.effect_from and to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') < D.effect_to ) " +
            " and (to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') >=  B.effect_from and to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') < B.effect_to )  order by D.mainid";
        else
            sql = "select    D.MAINID, D.TITLE, D.CHILDREN, vod, B.LIMIT_AMOUNT , descr  " +
           " from sligen.amp_m_benefits_V2 B, sligen.amp_M_benefits_def D  where " +
           " D.MAINID = B.MAINID and plan_id = '" + qmas.plan.Trim() + "' and (to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') >=  D.effect_from and to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') < D.effect_to ) " +
           " and (to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') >=  B.effect_from and to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') < B.effect_to )  order by D.mainid";


        DataSet ds = new DataSet();
        ds = dm.getrow(sql);

        DataTable dt = ds.Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            string main_id = row[0].ToString().Trim();
            string title = row[1].ToString().Trim();
            string child = row[2].ToString().Trim();
            string vod = row[3].ToString().Trim();

            string value="";
            if (vod == "V")
                value = Convert.ToDouble(row[4].ToString().Trim()).ToString("N2");
            else
                value = row[5].ToString().Trim();
           

            if (child == "Y")
            {

                cell = new PdfPCell(new Phrase(main_id, bodyFont_bold_sml));
                cell.HorizontalAlignment = 0;
                cell.BorderWidth = 0.1f;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0.5f;
                
                cell.BorderWidthRight = 0;
                cell.BorderColor = new Color(200, 200, 200);
                tbl15.AddCell(cell);

                cell = new PdfPCell(new Phrase(title, bodyFont_bold_sml));
                cell.HorizontalAlignment = 0;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0.5f;
                cell.BorderColor = new Color(200, 200, 200);
                tbl15.AddCell(cell);

                cell = new PdfPCell(new Phrase(value, bodyFont_bold_sml));
                cell.HorizontalAlignment = 0;
                cell.Border = 1;
                cell.HorizontalAlignment = 0;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0;
                cell.BorderColor = new Color(200, 200, 200);
                cell.BorderWidthRight = 0.5f;
                tbl15.AddCell(cell);
            }
            else
            {

                cell = new PdfPCell(new Phrase( main_id , bodyFont_sml));
                cell.HorizontalAlignment = 0;
                cell.BorderColor = new Color(200, 200, 200);
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0.5f;
                cell.BorderWidthRight = 0;
                tbl15.AddCell(cell);

                cell = new PdfPCell(new Phrase(title, bodyFont_sml));
                cell.HorizontalAlignment = 0;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderColor = new Color(200, 200, 200);
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0.5f;
                tbl15.AddCell(cell);

                cell = new PdfPCell(new Phrase(value, bodyFont_sml));
                cell.HorizontalAlignment = 0;
                cell.Border = 1;
                cell.HorizontalAlignment = 0;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderColor = new Color(200, 200, 200);
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0.5f;
                tbl15.AddCell(cell);
            }


        }

        if (females_exists)
        {

            cell = new PdfPCell(new Phrase("*Notes\nMaternity Cover (Both Private and Government Hospitals): Only receivable after two years waiting period for policies of female lives renewed without any break\nConditions Apply", bodyFont_bold_sm));
            cell.HorizontalAlignment = 0;
            cell.Colspan = 3;
            cell.BorderColor = new Color(200, 200, 200);
            cell.BorderWidthBottom = 0.5f;
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0.5f;
            cell.BorderWidthRight = 0.5f;
            tbl15.AddCell(cell);
        }



        document.Add(tbl15);

       document.NewPage();
       document.Add(new Paragraph("\n"));
        watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
       watermark.SetAbsolutePosition(65, 170);
       //document.Add(watermark);

        ////////////////////////////////
    
       document.Add(new Phrase("Premium Calculation", bodyFont_bold));
       int[] clmwidths5 = { 30, 30 };

       PdfPTable tbl151 = new PdfPTable(2);

       tbl151.SetWidths(clmwidths5);

       tbl151.WidthPercentage = 40;
       tbl151.HorizontalAlignment = Element.ALIGN_LEFT;
       tbl151.SpacingBefore = 0;
       tbl151.SpacingAfter = 20;
       tbl151.DefaultCell.Border = 0;

       cell = new PdfPCell(new Phrase("(LKR) ", bodyFont));
       cell.HorizontalAlignment = 2;
       cell.Colspan = 2;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase("Net Premium", bodyFont));
       cell.HorizontalAlignment = 0;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase(qmas.net_premium.ToString("N2"), bodyFont));
       cell.HorizontalAlignment = 2;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase("Administration Fee", bodyFont));
       cell.HorizontalAlignment = 0;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase((qmas.admin_fee + qmas.nbt).ToString("N2"), bodyFont));
       cell.HorizontalAlignment = 2;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase("Policy Fee", bodyFont));
       cell.HorizontalAlignment = 0;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase(qmas.policy_fee.ToString("N2"), bodyFont));
       cell.HorizontalAlignment = 2;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       /*cell = new PdfPCell(new Phrase("Social Security Contribution", bodyFont));
       cell.HorizontalAlignment = 0;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase(qmas.nbt.ToString("N2"), bodyFont));
       cell.HorizontalAlignment = 2;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);*/

       cell = new PdfPCell(new Phrase("VAT", bodyFont));
       cell.HorizontalAlignment = 0;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase(qmas.vat .ToString("N2"), bodyFont));
       cell.HorizontalAlignment = 2;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       cell = new PdfPCell(new Phrase("Total Premium", bodyFont));
       cell.HorizontalAlignment = 0;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       Chunk ch1131 = new Chunk(qmas.final_premium.ToString("N2"), bodyFont);
       ch1131.SetUnderline(0.5f, -1.5f);
       ch1131.SetUnderline(0.8f, -3f);
       ch1131.SetUnderline(0.5f, 10f);
       //document.Add(ch1131);

       cell = new PdfPCell(new Phrase(ch1131));
       cell.HorizontalAlignment = 2;
       cell.BorderWidth = 0f;
       tbl151.AddCell(cell);

       document.Add(tbl151);
        ///////////////////////////////
    

       Chunk ch1 = new Chunk("Note", bodyFont_bold);
       ch1.SetUnderline(0.5f, -1.5f);
       document.Add(ch1);

       document.Add(new Paragraph("\n"));

       int[] clmwidths114 = { 1, 30 };

       PdfPTable tbl17 = new PdfPTable(2);

       tbl17.SetWidths(clmwidths114);

       tbl17.WidthPercentage = 100;
       tbl17.HorizontalAlignment = Element.ALIGN_CENTER;
       tbl17.SpacingBefore = 0;
       tbl17.SpacingAfter = 10;
       tbl17.DefaultCell.Border = 0;

       tbl17.AddCell(new Phrase("•	", bodyFont));
       tbl17.AddCell(new Phrase("Above premiums are subject to government tax revision", bodyFont));

       tbl17.AddCell(new Phrase("•	", bodyFont));
       tbl17.AddCell(new Phrase("Quotation is valid for " + par.quot_val_days + " days from the issuing date assuming that the age limit is not changed insured at the stage of policy issuing", bodyFont));

       tbl17.AddCell(new Phrase("•	", bodyFont));
       tbl17.AddCell(new Phrase("This is an annual general insurance policy.", bodyFont));

       tbl17.AddCell(new Phrase("•	", bodyFont));
       tbl17.AddCell(new Phrase("Applicants who categorized under female dependent Children will not be eligible for maternity benefits.", bodyFont));

       tbl17.AddCell(new Phrase("•	", bodyFont));
       tbl17.AddCell(new Phrase("Any pre existing medical & surgical conditions ( Eg:- Diabetes Cholesterol, Blood Pressure, Heart failures) are excluded.", bodyFont));

       tbl17.AddCell(new Phrase("•	", bodyFont));
       tbl17.AddCell(new Phrase("30 days waiting period is applicable for claims from the date of commencement of the policy except for accidental injuries.", bodyFont));
       
       tbl17.AddCell(new Phrase("•	", bodyFont));
       tbl17.AddCell(new Phrase("For more clarifications please contact our 24 /7 contact centre on 0112 357 357.", bodyFont));

       document.Add(tbl17);


        ///////////////////////////////////




        int Val_days = par.sick_wait_mnths;

        Chunk ch_1 = new Chunk("Waiting period for specified diseases/ailments/conditions:",  bodyFont_bold);
        document.Add(ch_1);
        Chunk ch_2 = new Chunk(" From the time of inception of the cover, the policy will not cover the following conditions for duration of " + Val_days + " months. This exclusion will be deleted after one year, provided the policy has been continuously renewed with the Company without any break.", bodyFont);
        document.Add(ch_2);
        document.Add(new Paragraph("\n"));

        sql = "select * from sligen.amp_sickness where sysdate between effect_from and effect_to  order by seqid";
        ds = new DataSet();
        ds = dm.getrow(sql);

        dt = ds.Tables[0];

        int[] clmwidths113 = { 2, 30};

        PdfPTable tbl16 = new PdfPTable(2);

        tbl16.SetWidths(clmwidths113);

        tbl16.WidthPercentage = 100;
        tbl16.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl16.SpacingBefore = 10;
        tbl16.SpacingAfter = 10;
        tbl16.DefaultCell.Border = 0;

        foreach (DataRow row in dt.Rows)
        {
            string main_id = row[1].ToString().Trim();
            string descr = row[2].ToString().Trim();
             
            //document.Add(new Paragraph(main_id+ " "+descr, bodyFont ));

            tbl16.AddCell(new Phrase(main_id, bodyFont));
            tbl16.AddCell(new Phrase(descr, bodyFont));

        }

        document.Add(tbl16);

        
        document.Add(new Paragraph("All other terms, conditions and exclusions as per standard Sri Lanka Insurance health insurance policy.", bodyFont_bold));
        
        document.NewPage();
        document.Add(new Paragraph("\n"));
        watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        watermark.SetAbsolutePosition(65, 160);
        //document.Add(watermark);




        document.Add(new Paragraph("\nPremium can be varied according to insured's health conditions.", bodyFont_bold));

        document.Add(new Paragraph("\nWe trust the above would meet with your requirements and await your advice to proceed. If you need further clarifications please feel free to contact us.", bodyFont));
        document.Add(new Paragraph("\n\n"));
        document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont5));

        //document.Add(new Paragraph("\n\n\n"));
        //document.Add(new Paragraph("Manager ", bodyFont));
        //document.Add(new Paragraph("General Accident Insurance Department", bodyFont));
        //document.Add(new Paragraph("Sri Lanka Insurance Corporation Limited", bodyFont));
        
        document.Close();

        //output.Position = 0;

        string file_name = "";
        file_name = (QoP.Equals("P") ? "Proposal_Document" : "Quotation_Document");

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=AMP_{0}.pdf", file_name));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
 
    }

    public void print_medicalRpts(string form, string epf, string ip, bool reprint)
    {
        Document document = new Document(PageSize.A4, 50, 50, 10, 10);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        if (reprint)
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf + " REPRINT", new Font(Font.COURIER, 8));
        else
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        // top & bottom borders on by default 
        header.Border = Rectangle.NO_BORDER;
        // center header
        header.Alignment = 1;
        /*
         * HeaderFooter => add header __before__ opening document
         */
        document.Footer = header;




        Font titleFont1 = FontFactory.GetFont("Times New Roman", 9, Font.BOLD, new Color(0, 0, 0));
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_sml = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold_sml = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/health_logo.png");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(25f);
        logo.SetAbsolutePosition(50, 770);
        //logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark);

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        document.Add(logo);



        document.Add(new Paragraph("\n\n\n"));
        document.Add(new Paragraph("SRI LANKA INSURANCE MEDI PLUS PLAN", titleFont1));

        document.Add(new Paragraph("\n", bodyFont));
       
        Paragraph titleLine = new Paragraph("MEDICAL REPORT FORM ("+form.ToUpper()+")", boldTableFont);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        Chunk titch1 = new Chunk("TO BE SUBMITTED BY ALL INSURERD PERSONS ABOVE 45 YEARS OF AGE", bodyFont_bold);
        titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine2 = new Paragraph(titch1);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);

        
        Paragraph titleLine3 = new Paragraph("(NOTE: THE DOCTOR’S CERTIFICATED AND TEST REPORTS HAVE TO BE SUBMITTED IN DUPLICTE)", bodyFont);
        titleLine3.SetAlignment("Center");
        document.Add(titleLine3);

        int[] clmwidths111 = { 1,15 };

        PdfPTable tbl14 = new PdfPTable(2);

        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 20;
        tbl14.SpacingAfter = 10;
        tbl14.DefaultCell.Border = 0;

        PdfPCell cell = new PdfPCell(new Phrase("PLEASE ATTACH THE FOLLOWING TEST REPORTS WITH THE DOCTOR’S CERTIFICATE GIVEN BELOW.", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0;
        cell.BorderWidthTop = 0.5f;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl14.AddCell(cell);


        string sql = "SELECT MD_REPORT FROM SLIGEN.AMP_MEDICAL_REPORTS WHERE TYPE='"+form.ToUpper()+"'";
        DataSet ds = new DataSet();
        ds = dm.getrow(sql);

        DataTable dt = ds.Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            string main_id = row[0].ToString().Trim();

            cell = new PdfPCell(new Phrase("•", bodyFont_sml));
            cell.HorizontalAlignment = 1;
            cell.Colspan = 1;
            cell.BorderColor = new Color(120, 120, 120);
            cell.BorderWidthBottom = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0.5f;
            cell.BorderWidthRight = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(main_id, bodyFont_sml));
            cell.HorizontalAlignment = 0;
            cell.Colspan = 1;
            cell.BorderColor = new Color(120, 120, 120);
            cell.BorderWidthBottom = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0.5f;
            tbl14.AddCell(cell);
        }
        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl14.AddCell(cell);

        document.Add(tbl14);

        int[] clmwidths112 = { 2,1, 7 };

        PdfPTable tbl15 = new PdfPTable(3);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("NAME OF INSURED", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0;
        cell.BorderWidthTop = 0.5f;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase(":", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0;
        cell.BorderWidthTop = 0.5f;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0;
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0;
        cell.BorderWidthTop = 0.5f;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase("AGE", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase(":", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0;
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl15.AddCell(cell);


        cell = new PdfPCell(new Phrase("HEIGHT & WEIGHT", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase(":", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0;
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl15.AddCell(cell);

        document.Add(tbl15);

        //tbl14
        int[] clmwidths113 = { 1, 19, 20 };

        PdfPTable tbl16 = new PdfPTable(3);

        tbl16.SetWidths(clmwidths113);

        tbl16.WidthPercentage = 100;
        tbl16.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl16.SpacingBefore = 10;
        tbl16.SpacingAfter = 10;
        tbl16.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("DOCTOR’S CERTIFICATE", bodyFont_bold_sml));
        cell.Colspan = 3;
        cell.HorizontalAlignment = 1;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0.5f;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("(NOTE : THIS FORM HASTO BE COMPLETED AND SIGNED BY A REGISTERED DOCTOR WITH MINIMUM  M.B.B.S QUALIFICATIONS CONDUCTING THE TEST)", bodyFont_sml));
        cell.Colspan = 3;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("HISTORY", bodyFont_sml));
        cell.Colspan = 3;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("1.", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("Any past history of disease , operation ,accidents, investigation etc.\n ", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);



        cell = new PdfPCell(new Phrase("2.", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("Result of General Medical Examination\n\n ", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
       // cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("3.", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("Does the attached total reports in your Professional opinion show any abnormalities? If so,  please describe\n ", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("4.", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("Does the abnormality represent a current illness or  disease that may possibly require medical treatment in future?", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
       // cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("5.", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("Does the Insured now or did he/she in the past required medication for his abnormality?\n ", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("6.", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("Please describe  any treatment taken by the insured in the past or being taken  at present?\n ", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("7.", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("Do you consider  that the insured is having good medical or adversely  affecting his /her health/medical condition as per observation?", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("SIGNATURE OF THE DOCTOR\n ", bodyFont_bold_sml));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("QUALIFICATION\n ", bodyFont_bold_sml));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("ADDRESS\n ", bodyFont_bold_sml));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("TELEPHONE NUMBER\n ", bodyFont_bold_sml));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("", bodyFont_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(120, 120, 120);
        //cell.BackgroundColor = new Color(255, 255, 255);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("INSTRUCTIONS", bodyFont_bold_sml));
        cell.Colspan = 3;
        cell.HorizontalAlignment = 1;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);

        cell = new PdfPCell(new Phrase("FOR THE AGENT", bodyFont_bold_sml));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 1;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0.5f;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        cell = new PdfPCell(new Phrase("FOR THE CUSTOMER", bodyFont_bold_sml));
        cell.Colspan = 1;
        cell.HorizontalAlignment = 1;
        cell.BorderColor = new Color(120, 120, 120);
        cell.BackgroundColor = new Color(220, 220, 220);
        cell.BorderWidthBottom = 0.5f;
        cell.BorderWidthTop = 0;
        cell.BorderWidthLeft = 0;
        cell.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell);


        int[] clmwidths114 = { 1, 9 };

        PdfPTable tbl17 = new PdfPTable(2);

        tbl17.SetWidths(clmwidths114);

        tbl17.WidthPercentage = 100;
        tbl17.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl17.SpacingBefore = 10;
        tbl17.SpacingAfter = 10;
        tbl17.DefaultCell.Border = 0;

        #region
        PdfPCell cel1l = new PdfPCell(new Phrase("•", bodyFont_sml));
        cel1l.Colspan = 1;
        cel1l.HorizontalAlignment = 1;
        cel1l.BorderColor = new Color(120, 120, 120);
        //cel1l.BackgroundColor = new Color(220, 220, 220);
        cel1l.BorderWidthBottom = 0;
        cel1l.BorderWidthTop = 0;
        cel1l.BorderWidthLeft = 0.5f;
        cel1l.BorderWidthRight = 0;
        tbl17.AddCell(cel1l);

        cel1l = new PdfPCell(new Phrase("Give  the client the original Medical Report Form and Original Test Reports.", bodyFont_sml));
        cel1l.Colspan = 1;
        cel1l.HorizontalAlignment = 0;
        cel1l.BorderColor = new Color(120, 120, 120);
        //cel1l.BackgroundColor = new Color(220, 220, 220);
        cel1l.BorderWidthBottom = 0;
        cel1l.BorderWidthTop = 0;
        cel1l.BorderWidthLeft = 0;
        cel1l.BorderWidthRight = 0.5f;
        tbl17.AddCell(cel1l);


        cel1l = new PdfPCell(new Phrase("•", bodyFont_sml));
        cel1l.Colspan = 1;
        cel1l.HorizontalAlignment = 1;
        cel1l.BorderColor = new Color(120, 120, 120);
        //cel1l.BackgroundColor = new Color(220, 220, 220);
        cel1l.BorderWidthBottom = 0.5f;
        cel1l.BorderWidthTop = 0;
        cel1l.BorderWidthLeft = 0.5f;
        cel1l.BorderWidthRight = 0;
        tbl17.AddCell(cel1l);

        cel1l = new PdfPCell(new Phrase("Staple  a copy of Medical Report Form and the Test Reports with the accepted proposal Form to be handed over to Sri Lanka Insurance.", bodyFont_sml));
        cel1l.Colspan = 1;
        cel1l.HorizontalAlignment = 0;
        cel1l.BorderColor = new Color(120, 120, 120);
        //cel1l.BackgroundColor = new Color(220, 220, 220);
        cel1l.BorderWidthBottom = 0.5f;
        cel1l.BorderWidthTop = 0;
        cel1l.BorderWidthLeft = 0;
        cel1l.BorderWidthRight = 0.5f;
        tbl17.AddCell(cel1l);
        #endregion
        

        PdfPCell cell34 = new PdfPCell(tbl17);
        cell34.Colspan = 2;
        cell34.HorizontalAlignment = 1;
        cell34.BorderColor = new Color(120, 120, 120);
        //cell34.BackgroundColor = new Color(255, 255, 255);
        cell34.BorderWidthBottom = 0.5f;
        cell34.BorderWidthTop = 0;
        cell34.BorderWidthLeft = 0;
        cell34.BorderWidthRight = 0.5f;
        //tbl16.AddCell(new Phrase(""));
        tbl16.AddCell(cell34);


        int[] clmwidths115 = { 1, 9 };

        PdfPTable tbl18 = new PdfPTable(2);

        tbl18.SetWidths(clmwidths115);

        tbl18.WidthPercentage = 100;
        tbl18.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl18.SpacingBefore = 0;
        tbl18.SpacingAfter = 0;
        tbl18.DefaultCell.Border = 0;
        
        #region
        PdfPCell cel2l = new PdfPCell(new Phrase("•", bodyFont_sml));
        cel2l.Colspan = 1;
        cel2l.HorizontalAlignment = 1;
        cel2l.BorderColor = new Color(120, 120, 120);
        //cel2l.BackgroundColor = new Color(220, 220, 220);
        cel2l.BorderWidthBottom = 0;
        cel2l.BorderWidthTop = 0;
        cel2l.BorderWidthLeft = 0;
        cel2l.BorderWidthRight = 0;
        tbl18.AddCell(cel2l);

        cel2l = new PdfPCell(new Phrase("Keep the original copies of the Medical Report Form and the Test Reports with you.", bodyFont_sml));
        cel2l.Colspan = 1;
        cel2l.HorizontalAlignment = 0;
        cel2l.BorderColor = new Color(120, 120, 120);
        //cel2l.BackgroundColor = new Color(220, 220, 220);
        cel2l.BorderWidthBottom = 0;
        cel2l.BorderWidthTop = 0;
        cel2l.BorderWidthLeft = 0;
        cel2l.BorderWidthRight = 0.5f;
        tbl18.AddCell(cel2l);


        cel2l = new PdfPCell(new Phrase("•", bodyFont_sml));
        cel2l.Colspan = 1;
        cel2l.HorizontalAlignment = 1;
        cel2l.BorderColor = new Color(120, 120, 120);
        //cel2l.BackgroundColor = new Color(220, 220, 220);
        cel2l.BorderWidthBottom = 0.5f;
        cel2l.BorderWidthTop = 0;
        cel2l.BorderWidthLeft = 0;
        cel2l.BorderWidthRight = 0;
        tbl18.AddCell(cel2l);

        cel2l = new PdfPCell(new Phrase("In case of a claim , these originals long with the policy schedule will have to be shown.", bodyFont_sml));
        cel2l.Colspan = 1;
        cel2l.HorizontalAlignment = 0;
        cel2l.BorderColor = new Color(120, 120, 120);
        //cel2l.BackgroundColor = new Color(220, 220, 220);
        cel2l.BorderWidthBottom = 0.5f;
        cel2l.BorderWidthTop = 0;
        cel2l.BorderWidthLeft = 0;
        cel2l.BorderWidthRight = 0.5f;
        tbl18.AddCell(cel2l);
        #endregion
        

        PdfPCell cell45 = new PdfPCell(tbl18);
        cell45.Colspan = 1;
        cell45.HorizontalAlignment = 1;
        cell45.BorderColor = new Color(120, 120, 120);
        //cell45.BackgroundColor = new Color(255, 255, 255);
        cell45.BorderWidthBottom = 0.5f;
        cell45.BorderWidthTop = 0;
        cell45.BorderWidthLeft = 0;
        cell45.BorderWidthRight = 0.5f;
        tbl16.AddCell(cell45);
        



        document.Add(tbl16);

        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=MediPlus_{0}.pdf", "Quotation_Document"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
    }

    public void print_policy(string qid, string polno,  string epf, string ip, bool reprint, string stdate, string enddate, string sum, string refNo)
    {
        bool females_exists = true;

        CustProfile cp = new CustProfile(epf);
        //AMP_Policy_mast aqmas = new AMP_Policy_mast(polno);

        AMP_Quotation_mast qmas = new AMP_Quotation_mast(qid);
        Proposal prop = new Proposal(qid);
        females_exists = qmas.females_exists(qid);

        var qmem = qmas.members.FirstOrDefault(o => o.member_id == qid + "_1");

        Parameters par = new Parameters(qmas.Enrty_Date);

        Document document = new Document(PageSize.A4, 50, 50, 10, 10);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        if (reprint)
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf + " REPRINT", new Font(Font.COURIER, 8));
        else
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        // top & bottom borders on by default 
        header.Border = Rectangle.NO_BORDER;
        // center header
        header.Alignment = 1;
        /*
         * HeaderFooter => add header __before__ opening document
         */
        document.Footer = header;




        Font titleFont1 = FontFactory.GetFont("Times New Roman", 9, Font.BOLD, new Color(0, 0, 0));
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_sml = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold_sml = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);

        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        int rowCount = 0;
        //string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/health_logo.png");

        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.gif");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(25f);
        //logo.SetAbsolutePosition(50, 770);
        //logo.SetAbsolutePosition(260, 760);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        //logo.ScalePercent(25f);
        //logo.SetAbsolutePosition(50, 770);


        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark);

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        document.Add(logo);



        document.Add(new Paragraph("\n\n\n\n\n"));
        //document.Add(new Paragraph("SRI LANKA INSURANCE HEALTH ANNUAL MEDICAL PLAN", titleFont1));

        document.Add(new Paragraph("\n", bodyFont));
        Chunk titch1 = new Chunk("SRI LANKA INSURANCE\nMEDI PLUS PLAN POLICY SCHEDULE", boldTableFont);
        //titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);


        int[] clmwidths111 = { 10, 1, 10 };

        PdfPTable tbl14 = new PdfPTable(3);

        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 20;
        tbl14.SpacingAfter = 10;
        tbl14.DefaultCell.Border = 0;


        tbl14.AddCell(new Phrase("POLICY NO", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(polno, bodyFont));

        tbl14.AddCell(new Phrase("ONLINE REFERENCE NO", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(refNo, bodyFont));

        tbl14.AddCell(new Phrase("PLAN", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qmas.plan, bodyFont));

        tbl14.AddCell(new Phrase("THE INSURED", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qmas.status + " " + qmas.name_1 + " " + qmas.name_2, bodyFont));

        tbl14.AddCell(new Phrase("ADDRESS", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(qmas.add_1, bodyFont));


        if (!String.IsNullOrEmpty(qmas.add_2))
        {
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(qmas.add_2, bodyFont));
        }

        if (!String.IsNullOrEmpty(qmas.add_3))
        {
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(qmas.add_3, bodyFont));
        }

        if (!String.IsNullOrEmpty(qmas.add_4))
        {
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(qmas.add_4, bodyFont));
        }

        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));

        tbl14.AddCell(new Phrase("PERIOD OF INSURANCE", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase("From " + stdate + " To " + enddate, bodyFont));

        DateTime ren = Convert.ToDateTime(stdate).AddYears(1).AddDays(-1);

        tbl14.AddCell(new Phrase("RENEWAL DATE", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(ren.ToString("yyyy/MM/dd"), bodyFont));

        tbl14.AddCell(new Phrase("GEOGRAPHICAL AREA", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase("REPUBLIC OF SRI LANKA", bodyFont));

        tbl14.AddCell(new Phrase("SCHEME", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase("LKR "+ sum, bodyFont));

        int[] clmwidths5 = { 30, 30 };

        PdfPTable tbl151 = new PdfPTable(2);

        tbl151.SetWidths(clmwidths5);

        tbl151.WidthPercentage = 40;
        tbl151.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl151.SpacingBefore = 0;
        tbl151.SpacingAfter = 20;
        tbl151.DefaultCell.Border = 0;

        

        PdfPCell cell = new PdfPCell(new Phrase("(LKR) ", bodyFont));
        cell.HorizontalAlignment = 2;
        cell.Colspan = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("Net Premium", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(qmas.net_premium.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("Administration Fee", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase((qmas.admin_fee + qmas.nbt).ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("Policy Fee", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(qmas.policy_fee.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        /*cell = new PdfPCell(new Phrase("Social Security Contribution", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(qmas.nbt.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);*/

        cell = new PdfPCell(new Phrase("VAT", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(qmas.vat.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("Total Premium", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        Chunk ch1131 = new Chunk(qmas.final_premium.ToString("N2"), bodyFont);
        ch1131.SetUnderline(0.5f, -1.5f);
        ch1131.SetUnderline(0.8f, -3f);
        ch1131.SetUnderline(0.5f, 10f);
        //document.Add(ch1131);

        cell = new PdfPCell(new Phrase(ch1131));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        PdfPCell Outcell = new PdfPCell(tbl151);
        Outcell.HorizontalAlignment = 2;
        Outcell.Colspan = 1;
        Outcell.BorderWidth = 0f;

        tbl14.AddCell(new Phrase("", bodyFont));
        tbl14.AddCell(new Phrase("", bodyFont));
        tbl14.AddCell(Outcell);


        tbl14.AddCell(new Phrase("DATE OF SIGNATURE OF PROPOSAL & DECLARATION", bodyFont));
        tbl14.AddCell(new Phrase(": ", bodyFont));
        tbl14.AddCell(new Phrase(prop.entryDate, bodyFont));


        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));

        DataTable data = null;

        if (qmas.no_persons >= 1)
        {
            

            int[] clmwidths3 = { 1,1, 4, 1 };

            PdfPTable tbl2 = new PdfPTable(4);

            tbl2.SetWidths(clmwidths3);

            tbl2.WidthPercentage = 0.75f;
            tbl2.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl2.SpacingBefore = 0;
            tbl2.SpacingAfter = 0;
            tbl2.DefaultCell.Border = 0;

            PdfPCell celli1 = new PdfPCell();
            celli1 = new PdfPCell(new Phrase("NIC No", whiteFont));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl2.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Passport No.", whiteFont));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl2.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Name of the Life Insured", whiteFont));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl2.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Date of Birth", whiteFont));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl2.AddCell(celli1);

            data = qmas.get_All_members_lst_policy(qid);

            #region
            if (data.Rows.Count > 0)
            {

                foreach (DataRow dr in data.Rows)
                {
                    string name = dr[1].ToString().Trim();
                    string nic = dr[0].ToString().Trim();
                    string dob = dr[2].ToString().Trim();
                    string gender = dr[4].ToString().Trim();
                    string pp_no = dr[5].ToString().Trim();

                    PdfPCell celli = new PdfPCell();

                    if (dr[3].ToString().Trim() == "M")
                    {
                        //tbl2.AddCell(new Phrase(prop.nic, bodyFont));
                        //tbl2.AddCell(new Phrase(prop.fullName, bodyFont));
                        //tbl2.AddCell(new Phrase(dob, bodyFont));

                        

                        celli = new PdfPCell(new Phrase(prop.nic, bodyFont));
                        celli.HorizontalAlignment = 0;
                        celli.BorderColor = new Color(200, 200, 200);
                        tbl2.AddCell(celli);

                        celli = new PdfPCell(new Phrase(prop.passport, bodyFont));
                        celli.HorizontalAlignment = 0;
                        celli.BorderColor = new Color(200, 200, 200);
                        tbl2.AddCell(celli);

                        celli = new PdfPCell(new Phrase(qmas.status + " " + prop.fullName, bodyFont));
                        celli.HorizontalAlignment = 0;
                        celli.BorderColor = new Color(200, 200, 200);
                        tbl2.AddCell(celli);

                        celli = new PdfPCell(new Phrase(dob, bodyFont));
                        celli.HorizontalAlignment = 0;
                        celli.BorderColor = new Color(200, 200, 200);
                        tbl2.AddCell(celli);

                    }
                    else
                    {
                        //tbl2.AddCell(new Phrase(nic, bodyFont));
                        //tbl2.AddCell(new Phrase(name, bodyFont));
                        //tbl2.AddCell(new Phrase(dob, bodyFont));

                        celli = new PdfPCell(new Phrase(nic, bodyFont));
                        celli.HorizontalAlignment = 0;
                        celli.BorderColor = new Color(200, 200, 200);
                        tbl2.AddCell(celli);


                        celli = new PdfPCell(new Phrase(pp_no, bodyFont));
                        celli.HorizontalAlignment = 0;
                        celli.BorderColor = new Color(200, 200, 200);
                        tbl2.AddCell(celli);

                        string ttl = "";

                        if (dr[3].ToString().Trim() == "S")
                        {
                            if (gender.Trim() == "F")
                            {
                                ttl = "Mrs.";
                            }
                            else
                            {
                                ttl = "Mr.";
                            }
                        }
                        else
                        {
                            if (gender.Trim() == "F")
                            {
                                ttl = "Ms.";
                            }
                            else
                            {
                                ttl = "Mstr.";
                            }
                        }

                        celli = new PdfPCell(new Phrase(ttl+" "+name, bodyFont));
                        celli.HorizontalAlignment = 0;
                        celli.BorderColor = new Color(200, 200, 200);
                        tbl2.AddCell(celli);

                        celli = new PdfPCell(new Phrase(dob, bodyFont));
                        celli.HorizontalAlignment = 0;
                        celli.BorderColor = new Color(200, 200, 200);
                        tbl2.AddCell(celli);
                    }


                }
            }
            #endregion
            PdfPCell pp = new PdfPCell(tbl2);
            pp.HorizontalAlignment = 0;
            pp.Colspan = 3;
            pp.BorderWidth = 0;
            tbl14.AddCell(pp);

            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));


            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));


            

            /*pp = new PdfPCell(new Phrase(DateTime.Today.ToString("yyyy/MM/dd"), bodyFont));
            pp.HorizontalAlignment = 0;
            pp.Colspan = 3;
            pp.BorderWidth = 0;
            tbl14.AddCell(pp);          

           pp = new PdfPCell(new Phrase("SRI LANKA INSURANCE CORPORATION LTD", bodyFont_bold));
            pp.HorizontalAlignment = 0;
            pp.Colspan = 3;
            pp.BorderWidth = 0;
            tbl14.AddCell(pp);

            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));

            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));

            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));
            tbl14.AddCell(new Phrase(" ", bodyFont));

            pp = new PdfPCell(new Phrase("MANAGER,", bodyFont_bold));
            pp.HorizontalAlignment = 0;
            pp.Colspan = 3;
            pp.BorderWidth = 0;
            tbl14.AddCell(pp);


            pp = new PdfPCell(new Phrase("GENERAL ACCIDENT DEPARTMENT.", bodyFont));
            pp.HorizontalAlignment = 0;
            pp.Colspan = 3;
            pp.BorderWidth = 0;
            tbl14.AddCell(pp); */



        }


        document.Add(tbl14);

        document.NewPage();

        document.Add(new Paragraph("\n\n\nSLI MEDI PLUS PLAN - BENEFIT TABLE \n\n", boldTableFont));

        int[] clmwidths1110 = { 5, 1, 10 };

        PdfPTable tbl140 = new PdfPTable(3);

        tbl140.SetWidths(clmwidths1110);

        tbl140.WidthPercentage = 100;
        tbl140.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl140.SpacingBefore = 20;
        tbl140.SpacingAfter = 10;
        tbl140.DefaultCell.Border = 0;


        tbl140.AddCell(new Phrase("POLICY NO", bodyFont));
        tbl140.AddCell(new Phrase(": ", bodyFont));
        tbl140.AddCell(new Phrase(polno, bodyFont));

        tbl140.AddCell(new Phrase("THE INSURED", bodyFont));
        tbl140.AddCell(new Phrase(": ", bodyFont));
        tbl140.AddCell(new Phrase(qmas.status + " " + qmas.name_1 + " " + qmas.name_2, bodyFont));


        if (data.Rows.Count > 1)
        {
            int ii = 0;
            foreach (DataRow dr in data.Rows)
            {
                
                string name = dr[1].ToString().Trim();
                string nic = dr[0].ToString().Trim();
                string dob = dr[2].ToString().Trim();
                string gender = dr[4].ToString().Trim();

                if (dr[3].ToString().Trim() == "M")
                {

                }
                else
                {

                    ii++;
                    if (ii == 1)
                    {
                        tbl140.AddCell(new Phrase("DEPENDENTS", bodyFont));
                        tbl140.AddCell(new Phrase(": ", bodyFont));
                        tbl140.AddCell(new Phrase(name, bodyFont));
                    }
                    else
                    {
                        tbl140.AddCell(new Phrase("", bodyFont));
                        tbl140.AddCell(new Phrase("", bodyFont));
                        tbl140.AddCell(new Phrase(name, bodyFont));
                    }
                }
            }
        }
        else
        {
            tbl140.AddCell(new Phrase("DEPENDENTS", bodyFont));
            tbl140.AddCell(new Phrase(": ", bodyFont));
            tbl140.AddCell(new Phrase("NIL", bodyFont));
        }


        tbl140.AddCell(new Phrase("PERIOD OF INSURANCE", bodyFont));
        tbl140.AddCell(new Phrase(": ", bodyFont));
        tbl140.AddCell(new Phrase("From " + stdate + " To " + enddate, bodyFont));

        document.Add(tbl140);

        int[] clmwidths112 = { 3, 30, 20 };

        PdfPTable tbl15 = new PdfPTable(3);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 0;
        tbl15.DefaultCell.Border = 0;



        cell = new PdfPCell(new Phrase("", bodyFont_bold_sml));
        cell.HorizontalAlignment = 0;
        cell.BackgroundColor = new Color(180, 180, 180);
        cell.BorderColor = new Color(200, 200, 200);

        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase("Benefit type", bodyFont_bold_sml));
        cell.HorizontalAlignment = 0;
        cell.BorderColor = new Color(200, 200, 200);
        cell.BackgroundColor = new Color(180, 180, 180);
        tbl15.AddCell(cell);

        cell = new PdfPCell(new Phrase("Annual Benefits", bodyFont_bold_sml));
        cell.HorizontalAlignment = 0;
        cell.BackgroundColor = new Color(180, 180, 180);
        cell.BorderColor = new Color(200, 200, 200);

        tbl15.AddCell(cell);

        // string sql = "select    D.MAINID, D.TITLE, D.CHILDREN, vod, B.LIMIT_AMOUNT , descr  from sligen.amp_benefits B, sligen.amp_benefits_def D " +
        //         " where D.MAINID = B.MAINID and plan_id = '" + qmas.plan.Trim() + "' and (sysdate between B.effect_from and B.effect_to ) order by D.mainid";

        log lg = new log();
        lg.write_log(females_exists.ToString());
        string sql = "";

        if (females_exists)
            sql = "select    D.MAINID, D.TITLE, D.CHILDREN, vod, B.LIMIT_AMOUNT , descr  " +
            " from slic_net.amp_pol_benefits B, slic_net.amp_pol_benefits_def D  where " +
            " D.MAINID = B.MAINID and B.plan_id = D.plan and plan_id = '" + qmas.plan.Trim() + "' and (to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') >=  D.effect_from and to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') < D.effect_to ) " +
            " and (to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') >=  B.effect_from and to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') < B.effect_to )  order by D.mainid";
        else
            sql = "select    D.MAINID, D.TITLE, D.CHILDREN, vod, B.LIMIT_AMOUNT , descr  " +
           " from slic_net.amp_pol_m_benefits B, slic_net.amp_pol_M_benefits_def D  where " +
           " D.MAINID = B.MAINID and B.plan_id = D.plan and plan_id = '" + qmas.plan.Trim() + "' and (to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') >=  D.effect_from and to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') < D.effect_to ) " +
           " and (to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') >=  B.effect_from and to_date('" + qmas.Enrty_Date + "', 'yyyy/mm/dd') < B.effect_to )  order by D.mainid";


        DataSet ds = new DataSet();
        ds = dm.getrow(sql);

        DataTable dt = ds.Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            string main_id = row[0].ToString().Trim();
            string title = row[1].ToString().Trim();
            string child = row[2].ToString().Trim();
            string vod = row[3].ToString().Trim();

            string value = "";
            if (vod == "V")
                value = Convert.ToDouble(row[4].ToString().Trim()).ToString("N2");
            else
                value = row[5].ToString().Trim();


            if (child == "Y")
            {

                cell = new PdfPCell(new Phrase(main_id, bodyFont_bold_sml));
                cell.HorizontalAlignment = 0;
                cell.BorderWidth = 0.1f;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0.5f;

                cell.BorderWidthRight = 0;
                cell.BorderColor = new Color(200, 200, 200);
                tbl15.AddCell(cell);

                cell = new PdfPCell(new Phrase(title, bodyFont_bold_sml));
                cell.HorizontalAlignment = 0;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0.5f;
                cell.BorderColor = new Color(200, 200, 200);
                tbl15.AddCell(cell);

                cell = new PdfPCell(new Phrase(value, bodyFont_bold_sml));
                cell.HorizontalAlignment = 0;
                cell.Border = 1;
                cell.HorizontalAlignment = 0;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0;
                cell.BorderColor = new Color(200, 200, 200);
                cell.BorderWidthRight = 0.5f;
                tbl15.AddCell(cell);
            }
            else
            {

                cell = new PdfPCell(new Phrase(main_id, bodyFont_sml));
                cell.HorizontalAlignment = 0;
                cell.BorderColor = new Color(200, 200, 200);
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0.5f;
                cell.BorderWidthRight = 0;
                tbl15.AddCell(cell);

                cell = new PdfPCell(new Phrase(title, bodyFont_sml));
                cell.HorizontalAlignment = 0;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderColor = new Color(200, 200, 200);
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0.5f;
                tbl15.AddCell(cell);

                cell = new PdfPCell(new Phrase(value, bodyFont_sml));
                cell.HorizontalAlignment = 0;
                cell.Border = 1;
                cell.HorizontalAlignment = 0;
                cell.BorderWidthBottom = 0.5f;
                cell.BorderWidthTop = 0;
                cell.BorderColor = new Color(200, 200, 200);
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0.5f;
                tbl15.AddCell(cell);
            }


        }

        if (females_exists)
        {

            cell = new PdfPCell(new Phrase("*Notes\nMaternity Cover (Both Private and Government Hospitals): Only receivable after two years waiting period for policies of female lives renewed without any break\nConditions Apply", bodyFont_bold_sm));
            cell.HorizontalAlignment = 0;
            cell.Colspan = 3;
            cell.BorderColor = new Color(200, 200, 200);
            cell.BorderWidthBottom = 0.5f;
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0.5f;
            cell.BorderWidthRight = 0.5f;
            tbl15.AddCell(cell);
        }



        document.Add(tbl15);
        document.NewPage();

        document.Add(new Paragraph("\n\n\nImportant\n\n", bodyFont_bold));

        document.Add(new Paragraph("• Hospitalization due to complication in pregnancy / Pregnancy related ailments are covered under abnormal Maternity benefit limit stated above subject to maternity waiting period.\n", bodyFont));
        document.Add(new Paragraph("• Pre & Post hospitalization benefit is limited to 30days prior/after hospitalization.\n This benefit is not applicable to maternity cover.\n\n\n", bodyFont));

        document.Add(new Paragraph("All the Terms, Conditions and Exclusions as per the Sri Lanka insurance Medi Plus Policy. \n\n\n", bodyFont));

        document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont5));
       // document.Add(new Paragraph("SRI LANKA INSURANCE CORPORATION LTD.\n\n\n", bodyFont_bold));

       // document.Add(new Paragraph("MANAGER,\nGENRAL ACCIDENT DEPARTMENT ", bodyFont));


        document.Close();
        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=MediPlus_{0}.pdf", "Policy_Document"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
    }
}