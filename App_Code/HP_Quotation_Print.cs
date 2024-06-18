using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

/// <summary>
/// Summary description for HP_Quotation_Print
/// </summary>
public class HP_Quotation_Print
{
	public HP_Quotation_Print()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void print_quotation(string qid, string epf, string ip, bool reprint)
    {
        //GT_Proposal_mast gtm = new GT_Proposal_mast(qid);
        Document document = new Document(PageSize.A4, 50, 20, 50, 20);
        //List<GT_Proposal_mem> GT_mem = gtm.members;
        Proposal pro = new Proposal(qid);
        CustProfile cp = new CustProfile(pro.userName);

        HP_Quotation hq = new HP_Quotation(qid);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        if (reprint)
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf , new Font(Font.COURIER, 8));
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
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);

        Font bodyFont_sml = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold_sml = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.gif");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(25f);
        //logo.SetAbsolutePosition(260, 740);
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


        document.Add(new Paragraph("\n\n\n\n", bodyFont));

        int[] clmwidths_1 = { 1 };

        PdfPTable tbl_1 = new PdfPTable(1);

        tbl_1.SetWidths(clmwidths_1);

        tbl_1.WidthPercentage = 100;
        tbl_1.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_1.SpacingBefore = 10;
        tbl_1.SpacingAfter = 5;
        tbl_1.DefaultCell.Border = 0;

        tbl_1.AddCell(new Phrase(DateTime.Today.ToString("yyyy/MM/dd"), bodyFont));
        tbl_1.AddCell(new Phrase(cp.O_title + " " + cp.O_firstName + " " + cp.O_lastName, bodyFont));

        tbl_1.AddCell(new Phrase(pro.Address1.Trim(), bodyFont));
        if (!String.IsNullOrEmpty(pro.Address2.Trim()))
            tbl_1.AddCell(new Phrase(pro.Address2.Trim(), bodyFont));
        if (!String.IsNullOrEmpty(pro.Address3.Trim()))
            tbl_1.AddCell(new Phrase(pro.Address3.Trim(), bodyFont));
        if (!String.IsNullOrEmpty(pro.Address4.Trim()))
            tbl_1.AddCell(new Phrase(pro.Address4.Trim(), bodyFont));
              

        document.Add(tbl_1);

        document.Add(new Paragraph("Dear Sir/Madam,", bodyFont));
        
        Chunk titch1 = new Chunk("QUOTATION - HOME PROTECT LITE", boldTableFont);
        titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);


        int[] clmwidths_2 = { 10, 1 ,40 };

        PdfPTable tbl_2 = new PdfPTable(3);

        tbl_2.SetWidths(clmwidths_2);

        tbl_2.WidthPercentage = 100;
        tbl_2.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_2.SpacingBefore = 20;
        tbl_2.SpacingAfter = 10;
        tbl_2.DefaultCell.Border = 0;

        PdfPCell cell = new PdfPCell(new Phrase("We have pleasure in submitting our Quotation for your kind consideration. ", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 3;
        cell.BorderWidth = 0f;
        tbl_2.AddCell(cell);

        tbl_2.AddCell(new Phrase(" ", bodyFont_bold));
        tbl_2.AddCell(new Phrase("  ", bodyFont));
        tbl_2.AddCell(new Phrase(" ", bodyFont));

        tbl_2.AddCell(new Phrase("Type", bodyFont_bold));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase("Home Insurance \"First Loss\" Cover ", bodyFont));

        tbl_2.AddCell(new Phrase("Quotation No", bodyFont_bold));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(qid.ToUpper(), bodyFont));

        tbl_2.AddCell(new Phrase("Issued Date", bodyFont_bold));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(pro.entryDate, bodyFont));

        tbl_2.AddCell(new Phrase("Proposer", bodyFont_bold));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(pro.assignee, bodyFont));

        tbl_2.AddCell(new Phrase("Risk Location", bodyFont_bold));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(pro.locAddress1 + (!String.IsNullOrEmpty(pro.locAddress2.Trim()) ? ", " + pro.locAddress2 : " ") + (!String.IsNullOrEmpty(pro.locAddress3) ? ", " + pro.locAddress3 : " ") + (!String.IsNullOrEmpty(pro.locAddress4) ? ", " + pro.locAddress4 : ""), bodyFont));

        tbl_2.AddCell(new Phrase("Occupation/Business", bodyFont_bold));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(pro.profession, bodyFont_bold));

        tbl_2.AddCell(new Phrase("Period of Insurance", bodyFont_bold));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase("12 months (date to be agreed)", bodyFont));

        tbl_2.AddCell(new Phrase(" ", bodyFont_bold));
        tbl_2.AddCell(new Phrase("  ", bodyFont));
        tbl_2.AddCell(new Phrase(" ", bodyFont));

        document.Add(tbl_2);

        int[] clmwidths_3 = { 2, 50 };

        PdfPTable tbl_3 = new PdfPTable(2);

        tbl_3.SetWidths(clmwidths_3);

        tbl_3.WidthPercentage = 100;
        tbl_3.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_3.SpacingBefore = 0;
        tbl_3.SpacingAfter = 10;
        tbl_3.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("Coverage", bodyFont_bold));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.BorderWidth = 0f;
        tbl_3.AddCell(cell);

        cell = new PdfPCell(new Phrase("*", bodyFont_bold));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Colspan = 1;
        cell.BorderWidth = 0f;
        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Fire & Lighting", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Malicious Damage by any person ", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Explosion  ", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Natural perils including Cyclone/ Storm/ Tempest/ Floods/Earthquake, tsunami etc", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Electrical short circuiting cover(Limited to Rs.25,000.00 only.) ", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Bursting or Overflowing of Water Tanks, Apparatus or pipes  ", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Aircraft Damage ", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Impact Damage", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Burglary involving forcible and violent entry/exit (for contents only) Damage to the building caused by burglary or house breaking(Limited to Rs.25,000.00 only.)", bodyFont));

        document.Add(tbl_3);

        int[] clmwidths_4 = { 9,1 ,3 };

        PdfPTable tbl_4 = new PdfPTable(3);

        tbl_4.SetWidths(clmwidths_4);

        tbl_4.WidthPercentage = 60;
        tbl_4.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_4.SpacingBefore = 0;
        tbl_4.SpacingAfter = 10;
        tbl_4.DefaultCell.Border = 0;

        tbl_4.AddCell(new Phrase("Extensions", bodyFont_bold));
        tbl_4.AddCell(new Phrase(" ", bodyFont));
        tbl_4.AddCell(new Phrase("  ", bodyFont));

        tbl_4.AddCell(new Phrase("Removal of Debris", bodyFont));
        tbl_4.AddCell(new Phrase(":  Rs.", bodyFont));

        cell = new PdfPCell(new Phrase("25,000.00", bodyFont_bold));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.BorderWidth = 0f;
        tbl_4.AddCell(cell);

        tbl_4.AddCell(new Phrase("Professional fees for reinstating the building", bodyFont));
        tbl_4.AddCell(new Phrase(":  Rs.", bodyFont));

        cell = new PdfPCell(new Phrase("25,000.00", bodyFont_bold));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.BorderWidth = 0f;
        tbl_4.AddCell(cell);

        tbl_4.AddCell(new Phrase(" ", bodyFont_bold));
        tbl_4.AddCell(new Phrase(" ", bodyFont));
        tbl_4.AddCell(new Phrase("  ", bodyFont));

        tbl_4.AddCell(new Phrase("Interests Covered & Sum(s) insured", bodyFont_bold));
        tbl_4.AddCell(new Phrase(" ", bodyFont));
        tbl_4.AddCell(new Phrase("  ", bodyFont));


        tbl_4.AddCell(new Phrase("Total Sum to be insured on building", bodyFont_bold));
        tbl_4.AddCell(new Phrase(":  Rs.", bodyFont));

        cell = new PdfPCell(new Phrase(hq.sumInsured.ToString("N2"), bodyFont_bold));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.BorderWidth = 0f;
        tbl_4.AddCell(cell);


        tbl_4.AddCell(new Phrase("Total Sum to be insured on Contents", bodyFont_bold));
        tbl_4.AddCell(new Phrase(":  Rs.", bodyFont));

        cell = new PdfPCell(new Phrase(hq.sumInsured_Cont.ToString("N2"), bodyFont_bold));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.BorderWidth = 0f;
        tbl_4.AddCell(cell);

        document.Add(tbl_4);


        ////////////////////////////////

        //document.Add(new Phrase("Premium Calculation", bodyFont_bold));
        int[] clmwidths5 = { 30, 30 };

        PdfPTable tbl151 = new PdfPTable(2);

        tbl151.SetWidths(clmwidths5);

        tbl151.WidthPercentage = 40;
        tbl151.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl151.SpacingBefore = 0;
        tbl151.SpacingAfter = 10;
        tbl151.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("Premium Calculation (Rs.)", bodyFont_bold));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);



        cell = new PdfPCell(new Phrase("Net Premium", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(hq.baseAnuPrem.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("Administration Fee", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(pro.adminFee.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("Policy Fee", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(pro.polFee.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("NBT", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(pro.NBT.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("VAT", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase(pro.VAT.ToString("N2"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        cell = new PdfPCell(new Phrase("Total Premium", bodyFont));
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl151.AddCell(cell);

        Chunk ch1131 = new Chunk(pro.annualPremium.ToString("N2"), bodyFont);
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
        document.NewPage();

        tbl_3 = new PdfPTable(2);

        tbl_3.SetWidths(clmwidths_3);

        tbl_3.WidthPercentage = 100;
        tbl_3.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_3.SpacingBefore = 0;
        tbl_3.SpacingAfter = 20;
        tbl_3.DefaultCell.Border = 0;

        cell = new PdfPCell(new Phrase("Special/Additional Conditions ", bodyFont_bold));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.BorderWidth = 0f;
        tbl_3.AddCell(cell);

        cell = new PdfPCell(new Phrase("*", bodyFont_bold));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Colspan = 1;
        cell.BorderWidth = 0f;
        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("The building should be used as a private dwelling house and no other business purposes or domestic industries", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("The construction should be bricks/concrete/cement block walls and roofed with asbestos and/or tiles", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("The premises should be secure against Burglary", bodyFont));

        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("The premises should be in a good state of repair", bodyFont));

        tbl_3.AddCell(new Phrase(" ", bodyFont));
        tbl_3.AddCell(new Phrase(" ", bodyFont));

        cell = new PdfPCell(new Phrase("Excess/Deductibles Applicable ", bodyFont_bold));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.BorderWidth = 0f;
        tbl_3.AddCell(cell);


        cell = new PdfPCell(new Phrase("1.", bodyFont));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Colspan = 1;
        cell.BorderWidth = 0f;
        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("10% with a minimum of Rs.5,000.00 - of each and every claim in respect of Malicious Damage claims ", bodyFont));

        cell = new PdfPCell(new Phrase("2.", bodyFont));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Colspan = 1;
        cell.BorderWidth = 0f;
        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("10% or Rs.10,000.00 - whichever is higher on each and every claim in respect of Natural perils claims ", bodyFont));

        cell = new PdfPCell(new Phrase("3.", bodyFont));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Colspan = 1;
        cell.BorderWidth = 0f;
        tbl_3.AddCell(cell);
        tbl_3.AddCell(new Phrase("Rs.1,000.00 - in respect of All other claims. ", bodyFont));

        document.Add(tbl_3);

        document.Add(new Paragraph("Validity Period	:    30 Days", bodyFont_bold));
        
        document.Add(new Paragraph("We trust the above would meet with your requirements and await your advice to proceed.If you need further clarifications feel free to contact us.\n", bodyFont));
        document.Add(new Paragraph("\nAssuring you of our best services at all times.", bodyFont));
        document.Add(new Paragraph("Best Regards.\n\n\n\n", bodyFont));
        document.Add(new Paragraph("................................", bodyFont));
        document.Add(new Paragraph("Authorized Officer, ", bodyFont));
        document.Add(new Paragraph("General Accident Insurance Dep, ", bodyFont_bold));
        document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd). ", bodyFont_bold));

        document.Close();

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=HP_{0}.pdf", "Quotation_Document"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();

    }

    public void print_policy(string qid, string polno , string epf, string ip, bool reprint)
    {
        Document document = new Document(PageSize.A4, 45, 45, 50, 25);
        //List<GT_Proposal_mem> GT_mem = gtm.members;
        Proposal pro = new Proposal(qid);
        CustProfile cp = new CustProfile(pro.userName);

        HP_Quotation hq = new HP_Quotation(qid);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        if (reprint)
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));
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
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);

        Font bodyFont_sml = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold_sml = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.gif");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(25f);
       // logo.SetAbsolutePosition(260, 740);
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


        document.Add(new Paragraph("\n\n\n\n", bodyFont));


        Chunk titch1 = new Chunk("Policy Schedule\nFirst Loss Home Insurance Policy", boldTableFont);
        titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);


        int[] clmwidths_2 = { 10, 1, 40 };

        PdfPTable tbl_2 = new PdfPTable(3);

        tbl_2.SetWidths(clmwidths_2);

        tbl_2.WidthPercentage = 100;
        tbl_2.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_2.SpacingBefore = 20;
        tbl_2.SpacingAfter = 10;
        tbl_2.DefaultCell.Border = 0;

        tbl_2.AddCell(new Phrase("Policy Number", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(polno.Trim(), bodyFont));

        tbl_2.AddCell(new Phrase("Online Reference No", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(qid.Trim(), bodyFont));

        tbl_2.AddCell(new Phrase("Customer Id", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(" ", bodyFont));

        tbl_2.AddCell(new Phrase("Name of the Insured", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(pro.fullName, bodyFont));

        tbl_2.AddCell(new Phrase("Address", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(pro.locAddress1 + (!String.IsNullOrEmpty(pro.locAddress2.Trim()) ? ", " + pro.locAddress2 : " ") + (!String.IsNullOrEmpty(pro.locAddress3) ? ", " + pro.locAddress3 : " ") + (!String.IsNullOrEmpty(pro.locAddress4) ? ", " + pro.locAddress4 : ""), bodyFont));

        tbl_2.AddCell(new Phrase("The Premises", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase("Private House", bodyFont));

        tbl_2.AddCell(new Phrase("Situated at ", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(pro.locAddress1 + (!String.IsNullOrEmpty(pro.locAddress2.Trim()) ? ", " + pro.locAddress2 : " ") + (!String.IsNullOrEmpty(pro.locAddress3) ? ", " + pro.locAddress3 : " ") + (!String.IsNullOrEmpty(pro.locAddress4) ? ", " + pro.locAddress4 : ""), bodyFont));

        tbl_2.AddCell(new Phrase("", bodyFont));
        tbl_2.AddCell(new Phrase("", bodyFont));
        tbl_2.AddCell(new Phrase("But excluding any garden or yard and any outbuilding not communicating with main building", bodyFont));

        tbl_2.AddCell(new Phrase("Period of Insurance", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase("(a) From " + pro.comenmentDate + " To" + pro.endDate, bodyFont));

        tbl_2.AddCell(new Phrase("", bodyFont));
        tbl_2.AddCell(new Phrase("", bodyFont));
        tbl_2.AddCell(new Phrase("Commencing and expiring at 4:00 p.m. standard time.", bodyFont));

        tbl_2.AddCell(new Phrase("", bodyFont));
        tbl_2.AddCell(new Phrase("", bodyFont));
        tbl_2.AddCell(new Phrase("(b) Any subsequent period for which the Insured shall pay and the Corporation shall agree to accept a renewal premium", bodyFont));

        tbl_2.AddCell(new Phrase("Currency Type", bodyFont));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase("LKR", bodyFont));

        tbl_2.AddCell(new Phrase("Premium Rs.", bodyFont_bold));
        tbl_2.AddCell(new Phrase(": ", bodyFont));
        tbl_2.AddCell(new Phrase(hq.baseAnuPrem.ToString("N2") + " (All inclusive Rs. " + pro.annualPremium.ToString("N2")+")", bodyFont));

        document.Add(tbl_2);
        document.Add(new Paragraph("\n\nTHE PROPERTY INSURED", bodyFont_bold));

        int[] clmwidths_0 = { 4,1 };

        PdfPTable tbl_0 = new PdfPTable(2);

        tbl_0.SetWidths(clmwidths_0);

        tbl_0.WidthPercentage = 100;
        tbl_0.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_0.SpacingBefore = 20;
        tbl_0.SpacingAfter = 10;
        tbl_0.DefaultCell.Border = 0;

        Phrase ph1 = new Phrase();
        ph1.Add(new Chunk("The Building : ",bodyFont_bold));
        ph1.Add(new Chunk("(The building including landlords fixture and fittings, boundary wall, fences, gates excluding Air Conditioners therein.)\n\n", bodyFont));

        PdfPCell cell = new PdfPCell(ph1);
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Sum Insured (Rs.)", bodyFont_bold));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 1;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Full reinstatement value of the building ", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase(hq.sumInsured.ToString("N2"), bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);


        cell = new PdfPCell(new Phrase(" ", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        Phrase ph2 = new Phrase();
        ph2.Add(new Chunk("The Contents : ", bodyFont_bold));
        ph2.Add(new Chunk("Household Goods including air conditioner (Except after mentioned). The Property of the Proposer or member of the Proposer's Family normally residing with the Proposer.\n\n", bodyFont));

        cell = new PdfPCell(ph2);
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Sum Insured (Rs.)", bodyFont_bold));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 1;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Full reinstatement value of the content ", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase(hq.sumInsured_Cont.ToString("N2"), bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);


        cell = new PdfPCell(new Phrase("The insurance on Contents does not cover any part of the structure or ceiling of the Buildings, nor any property to be insured under buildings, nor does it cover jewellery, gold, gems & personal effects, other valuables, and antiques , Works of arts, Professional Equipments, Deeds, Bonds, Bills of Exchange, Promissory Notes, Manuscripts, Medals, Coins, Motor Vehicles and Accessories and the like.\n\n", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Extensions", bodyFont_bold));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 1;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Sum Insured (Rs.)", bodyFont_bold));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 1;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Removal of Debris", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("25,000.00", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Professional fees for reinstating the building", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("25,000.00", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Burglary involving forcible and violent entry/exit (for contents only) Damage to the building caused by burglary or house breaking", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("25,000.00", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Electrical short circuiting", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("25,000.00", bodyFont));
        cell.BorderColor = new Color(200, 200, 200);
        cell.Colspan = 1;
        cell.HorizontalAlignment = 2;
        cell.BorderWidth = 0.5f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", bodyFont_bold));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Excess/Deductibles", bodyFont_bold));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("Intermediary ", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("    1. 10% with a minimum of Rs.5,000/- of each and every claim in respect of Malicious Damage claims", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("    2. 10% or Rs.10,000.00 - whichever is higher on each and every claim in respect of Natural perils claims ", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("    3. Rs.1,000.00 - of each and every claim in respect of All other claims", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);


        //cell = new PdfPCell(new Phrase("  ", bodyFont));
        //cell.Colspan = 2;
        //cell.HorizontalAlignment = 0;
        //cell.BorderWidth = 0f;
        //tbl_0.AddCell(cell);


        //cell = new PdfPCell(new Phrase("In witness whereof this Policy has been signed at Head Office , Thursday, March 9, 2017 ", bodyFont));
        //cell.Colspan = 2;
        //cell.HorizontalAlignment = 0;
        //cell.BorderWidth = 0f;
        //tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", bodyFont));
        cell.Colspan = 2;
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0f;
        tbl_0.AddCell(cell);

        document.Add(tbl_0);


        document.Add(new Paragraph("................................", bodyFont));
        document.Add(new Paragraph("Authorized Officer, ", bodyFont));
        document.Add(new Paragraph("General Accident Insurance Dep, ", bodyFont_bold));
        document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd). ", bodyFont_bold));

        document.Close();

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=HIP_{0}.pdf", "Policy_Schedule"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();

    }
}