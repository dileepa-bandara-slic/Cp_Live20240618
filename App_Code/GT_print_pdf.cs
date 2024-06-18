using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
/// <summary>
/// Summary description for GT_print_pdf
/// </summary>
public class GT_print_pdf
{
	public GT_print_pdf()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void print_quotation(string qid, string epf, string ip, bool reprint)
    {
        GT_Proposal_mast gtm = new GT_Proposal_mast(qid);
        Document document = new Document(PageSize.A4, 50, 50, 10, 10);
        List<GT_Proposal_mem> GT_mem = gtm.members;
        Proposal pro = new Proposal();
        CustProfile cp = new CustProfile(epf);
        Country coun = new Country();

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
        Font bodyFont4_white_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD, new Color(255, 255, 255));
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.gif");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(25f);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);


        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark);

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        document.Add(logo);


        document.Add(new Paragraph("\n\n\n\n\n\n\n\n", bodyFont));
        Chunk titch1 = new Chunk("QUOTATION - GLOBE TROTTER INSURANCE", boldTableFont);
        titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);


        document.Add(new Paragraph("\n\n\n", bodyFont));
        document.Add(new Paragraph("Dear Sir/Madam,\n\n", bodyFont));
        document.Add(new Paragraph("We have pleasure in submitting our Quotation for your kind consideration. \n", bodyFont));



        #region member details
        ////////////////////////////////////////////////////////
        PdfPTable tbl_4 = new PdfPTable(5);
        List<GT_Proposal_mem> members = gtm.members;

        if (members != null)
        {

            //document.Add(new Paragraph("\nInsureds' Details (Currency type : USD)", bodyFont_bold));

            int[] clmwidths = { 3, 3, 3, 3, 3 };

           

            tbl_4.SetWidths(clmwidths);

            //tbl_4.WidthPercentage = 80;
            tbl_4.HorizontalAlignment = Element.ALIGN_LEFT;
            tbl_4.SpacingBefore = 10;
            tbl_4.SpacingAfter = 10;
            tbl_4.DefaultCell.Border = 0;
            tbl_4.TotalWidth = 396f;
            tbl_4.LockedWidth = true;

            //PdfPCell celli1 = new PdfPCell(new Phrase("Member ID", bodyFont4_white_bold));
            //celli1.HorizontalAlignment = 1;
            //celli1.BackgroundColor = new Color(180, 180, 180);
            //celli1.BorderColor = new Color(200, 200, 200);
            //tbl_4.AddCell(celli1);

            PdfPCell celli1 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Category", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Date of Birth", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Premium (USD)", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            int i = 0;

            foreach (GT_Proposal_mem mem in members)
            {

                i++;

                PdfPCell celli = new PdfPCell(new Phrase("Member " + i, bodyFont4));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);

                celli = new PdfPCell(new Phrase(mem.memType_desc, bodyFont4));
                celli.HorizontalAlignment = 1;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);

                celli = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                celli.HorizontalAlignment = 1;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);

                celli = new PdfPCell(new Phrase(mem.dob, bodyFont4));
                celli.HorizontalAlignment = 1;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);


                celli = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                celli.HorizontalAlignment = 2;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);
            }

            //document.Add(tbl_4);
        }

        ////////////////////////////////////////////////////////////
        #endregion



        int[] clmwidths111 = { 8, 1, 20 };

        PdfPTable tbl_1 = new PdfPTable(3);

        tbl_1.SetWidths(clmwidths111);

        tbl_1.WidthPercentage = 100;
        tbl_1.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_1.SpacingBefore = 10;
        tbl_1.SpacingAfter = 10;
        tbl_1.DefaultCell.Border = 0;


        tbl_1.AddCell(new Phrase("Customer Name in full", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(cp.O_title+" "+cp.O_firstName+" "+cp.O_lastName, bodyFont));

        tbl_1.AddCell(new Phrase("Policy type", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("Globe Trotter Insurance", bodyFont));

        tbl_1.AddCell(new Phrase("Scheme", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont));

        tbl_1.AddCell(new Phrase("Quotation Number", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(qid, bodyFont));

        tbl_1.AddCell(new Phrase("Issued Date", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.Enrty_Date, bodyFont));

        tbl_1.AddCell(new Phrase("Leaving Date", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.departDate, bodyFont));

        tbl_1.AddCell(new Phrase("Returning Date", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.returnDate, bodyFont));

        //tbl_1.AddCell(new Phrase("Insured's Date of Birth", bodyFont));
        //tbl_1.AddCell(new Phrase(": ", bodyFont));
        //tbl_1.AddCell(new Phrase(GT_mem[0].dob, bodyFont));

        tbl_1.AddCell(new Phrase("Destination", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.get_country_name(gtm.destination) + "  " + (coun.check_schengen(gtm.destination)?"(Schengen Country)":""), bodyFont));

        tbl_1.AddCell(new Phrase("Sum Insured", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("USD "+gtm.sumIns_usd.ToString("N2"), bodyFont));



        PdfPCell ppc = new PdfPCell();
        ppc.Colspan = 3;
        ppc.Border = 0;
        ppc.AddElement(new Phrase("Insureds' Details (Currency type : USD)", bodyFont));
        tbl_1.AddCell(ppc);

        ppc = new PdfPCell(tbl_4);
        ppc.Colspan = 3;
        ppc.Border = 0;
        tbl_1.AddCell(ppc);

        int[] clmwidths_2 = { 5, 2, 3 };

        PdfPTable tbl_2 = new PdfPTable(3);

        tbl_2.SetWidths(clmwidths_2);

        //tbl_2.WidthPercentage = 0.50f;
        tbl_2.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_2.SpacingBefore = 0;
        tbl_2.SpacingAfter = 0;
        tbl_2.DefaultCell.Border = 0;
        //tbl_2.WidthPercentage = 40;
        tbl_2.TotalWidth = 200f;
        tbl_2.LockedWidth = true;

        PdfPCell cell2 = new PdfPCell();
        cell2.HorizontalAlignment = 2;
        //cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell2.Border = 0;

        tbl_2.AddCell(new Phrase("Basic Premium ", bodyFont));
        tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
        cell2 = new PdfPCell(new Phrase(gtm.netPremium_rs.ToString("N2"), bodyFont));
        cell2.HorizontalAlignment = 2;
        cell2.Border = 0;
        tbl_2.AddCell(cell2);

        tbl_2.AddCell(new Phrase("Admin Fee ", bodyFont));
        tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
        cell2 = new PdfPCell(new Phrase(gtm.adminFee_rs.ToString("N2"), bodyFont));
        cell2.HorizontalAlignment = 2;
        cell2.Border = 0;
        tbl_2.AddCell(cell2);

        tbl_2.AddCell(new Phrase("Policy Fee ", bodyFont));
        tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
        cell2 = new PdfPCell(new Phrase(gtm.policyFee_rs.ToString("N2"), bodyFont));
        cell2.HorizontalAlignment = 2;
        cell2.Border = 0;
        tbl_2.AddCell(cell2);

        tbl_2.AddCell(new Phrase("NBT", bodyFont));
        tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
        cell2 = new PdfPCell(new Phrase(gtm.nbt_rs.ToString("N2"), bodyFont));
        cell2.HorizontalAlignment = 2;
        cell2.Border = 0;
        tbl_2.AddCell(cell2);

        tbl_2.AddCell(new Phrase("VAT", bodyFont));
        tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
        cell2 = new PdfPCell(new Phrase(gtm.vat_rs.ToString("N2"), bodyFont));
        cell2.HorizontalAlignment = 2;
        cell2.Border = 0;
        tbl_2.AddCell(cell2);

        tbl_2.AddCell(new Phrase("Total", bodyFont));
        tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
        cell2 = new PdfPCell(new Phrase(gtm.finalPremium_rs.ToString("N2"), bodyFont));
        cell2.HorizontalAlignment = 2;
        cell2.Border = 0;
        tbl_2.AddCell(cell2);



        tbl_2.WidthPercentage = 50f;
        PdfPCell cell1 = new PdfPCell(tbl_2);
        cell1.HorizontalAlignment = 0;
        cell1.Colspan = 1;
        cell1.Border = 0;

        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));

        tbl_1.AddCell(new Phrase("Premium", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(cell1);


        int[] clmwidths_12 = { 1};

        PdfPTable tbl_12 = new PdfPTable(1);

        tbl_12.SetWidths(clmwidths_12);

        //tbl_2.WidthPercentage = 0.50f;
        tbl_12.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_12.SpacingBefore = 0;
        tbl_12.SpacingAfter = 0;
        tbl_12.DefaultCell.Border = 0;
        //tbl_2.WidthPercentage = 40;
        tbl_12.TotalWidth = 200f;
        tbl_12.LockedWidth = true;


        //tbl_12.AddCell(new Phrase("Sri Lanka", bodyFont));
        tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.visitCntry1), bodyFont));

        if (!String.IsNullOrEmpty(gtm.visitCntry2))
        {
            //tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.visitCntry1), bodyFont));
            tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.visitCntry2), bodyFont));
        }

        if (!String.IsNullOrEmpty(gtm.visitCntry3))
        {
            //tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.visitCntry2), bodyFont));
            tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.visitCntry3), bodyFont));
        }

        if (!String.IsNullOrEmpty(gtm.visitCntry4))
        {
            //tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.visitCntry3), bodyFont));
            tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.visitCntry4), bodyFont));
        }


        cell1 = new PdfPCell(tbl_12);
        cell1.BorderWidth = 0f;

        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase("Today Currency Rate (1 USD) = LKR " + pro.getDollarRate_Opn().ToString("N2"), bodyFont_bold_sm));

        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));

        tbl_1.AddCell(new Phrase("Visiting Countries", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(cell1);

        

        //tbl_1.AddCell(new Phrase(" ", bodyFont));
        //tbl_1.AddCell(new Phrase(" ", bodyFont));
        //tbl_1.AddCell(new Phrase(" ", bodyFont));

        /*tbl_1.AddCell(new Phrase("Class of Coverage", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("See Benefit Details" , bodyFont));

        tbl_1.AddCell(new Phrase("Excess", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("See Benefit Details", bodyFont));

        tbl_1.AddCell(new Phrase("Special Conditoin and Exclusion", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("See Benefit Details", bodyFont));*/

        document.Add(tbl_1);


        document.Add(new Paragraph("* The premium is calculated based  on the exchange rate prevailing on the date hear in. Hence subject to variations of market rate.", bodyFont_bold_sm));

        document.Add(new Paragraph("* This quotation is valid only for 3 days.", bodyFont_bold_sm));

        //document.Add(new Paragraph("\nWe trust the above would meet with your requirements and await your advice to proceed.If you need further clarifications feel free to contact us.", bodyFont));

        
        /* Changes 2016/09/15
        document.Add(new Paragraph("\nIf you need further clarifications feel free to contact us.", bodyFont));

        document.Add(new Paragraph("\n\n\nYours faithfully,\n", bodyFont));
        document.Add(new Paragraph("Manager,", bodyFont));
        document.Add(new Paragraph("GENERAL ACCIDENT INSURANCE DEPT.", bodyFont));
        document.Add(new Paragraph("SRI LANKA INSURANCE CORPORATION LIMITED.", bodyFont));
        */
        document.Add(new Paragraph("\n", bodyFont));

        int[] clmwidths_3 = { 11, 5, 4};

        PdfPTable tbl_3 = new PdfPTable(3);

        tbl_3.SetWidths(clmwidths_3);

        tbl_3.WidthPercentage = 80;
        tbl_3.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_3.SpacingBefore = 0;
        tbl_3.SpacingAfter = 0;
        tbl_3.DefaultCell.Border = 0;



        GT_Benefits gtt = new GT_Benefits(gtm.plan);

        PdfPCell cellTBA = new PdfPCell(new Phrase("Benefits", bodyFont4_white_bold));
        cellTBA.HorizontalAlignment = 1;
        cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
        cellTBA.BorderWidth = 0f;
        cellTBA.BackgroundColor = new Color(180, 180, 180);
        cellTBA.BorderColor = new Color(System.Drawing.Color.LightGray);
        cellTBA.BorderWidthRight = 0f;
        cellTBA.BorderWidthLeft = 0.5f;
        cellTBA.BorderWidthTop = 0.5f;
        cellTBA.BorderWidthBottom = 0.5f;
        cellTBA.Padding = 5;

        tbl_3.AddCell(cellTBA);

        cellTBA = new PdfPCell(new Phrase("Sum Insured (USD)", bodyFont4_white_bold));
        cellTBA.HorizontalAlignment = 1;
        cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
        cellTBA.BorderWidth = 0f;
        cellTBA.BorderColor = new Color(System.Drawing.Color.LightGray);
        cellTBA.BackgroundColor = new Color(180, 180, 180);
        cellTBA.BorderWidthRight = 0f;
        cellTBA.BorderWidthLeft = 0.5f;
        cellTBA.BorderWidthTop = 0.5f;
        cellTBA.BorderWidthBottom = 0.5f;
        cellTBA.Padding = 5;

        tbl_3.AddCell(cellTBA);

        cellTBA = new PdfPCell(new Phrase("Excess (USD)", bodyFont4_white_bold));
        cellTBA.HorizontalAlignment = 1;
        cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
        cellTBA.BorderWidth = 0f;
        cellTBA.BackgroundColor = new Color(180, 180, 180);
        cellTBA.BorderColor = new Color(System.Drawing.Color.LightGray);
        cellTBA.BorderWidthRight = 0.5f;
        cellTBA.BorderWidthLeft = 0.5f;
        cellTBA.BorderWidthTop = 0.5f;
        cellTBA.BorderWidthBottom = 0.5f;
        cellTBA.Padding = 5;

        tbl_3.AddCell(cellTBA);


        foreach (DataRow dr in gtt.DTproduct.Rows)
        {
            cellTBA = new PdfPCell(new Phrase(dr[0].ToString(), bodyFont));
            cellTBA.HorizontalAlignment = 0;
            cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellTBA.BorderWidth = 0f;
            cellTBA.BorderColor = new Color(System.Drawing.Color.LightGray);
            cellTBA.BorderWidthRight = 0f;
            cellTBA.BorderWidthLeft = 0.5f;
            cellTBA.BorderWidthTop = 0f;
            cellTBA.BorderWidthBottom = 0.5f;
            cellTBA.Padding = 2;

            tbl_3.AddCell(cellTBA);

            cellTBA = new PdfPCell(new Phrase(dr[1].ToString(), bodyFont));
            cellTBA.HorizontalAlignment = 2;
            cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellTBA.BorderWidth = 0f;
            cellTBA.BorderColor = new Color(System.Drawing.Color.LightGray);
            cellTBA.BorderWidthRight = 0f;
            cellTBA.BorderWidthLeft = 0.5f;
            cellTBA.BorderWidthTop = 0f;
            cellTBA.BorderWidthBottom = 0.5f;
            cellTBA.Padding = 2;

            tbl_3.AddCell(cellTBA);

            cellTBA = new PdfPCell(new Phrase(dr[2].ToString(), bodyFont));
            cellTBA.HorizontalAlignment = 2;
            cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellTBA.BorderWidth = 0f;
            cellTBA.BorderColor = new Color(System.Drawing.Color.LightGray);
            cellTBA.BorderWidthRight = 0.5f;
            cellTBA.BorderWidthLeft = 0.5f;
            cellTBA.BorderWidthTop = 0f;
            cellTBA.BorderWidthBottom = 0.5f;
            cellTBA.Padding = 2;

            tbl_3.AddCell(cellTBA);
        }

        document.Add(tbl_3);
        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=GT_{0}.pdf", "Quotation_Document"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
    }

    public void print_policy(string qid, string polno, string epf, string ip, bool reprint)
    {
        GT_Proposal_mast gtm = new GT_Proposal_mast(qid);
        Document document = new Document(PageSize.A4, 50, 50, 50, 25);
        List<GT_Proposal_mem> GT_mem = gtm.members;
        Proposal pro = new Proposal(qid);

        CustProfile customer = new CustProfile(epf.Trim());

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
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont_bold = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);

        Font bodyFont_sml = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold_sml = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_white_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD, new Color(255, 255, 255));
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("/Images/slic_logo.gif");

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(25f);
        //logo.SetAbsolutePosition(260, 740);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark);

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        document.Add(logo);

        #region
        
        document.Add(new Paragraph("\n\n\n\n", bodyFont));
        Chunk titch1 = new Chunk("GLOBE TROTTER INSURANCE\nPOLICY SCHEDULE\n\n", boldTableFont);
        //titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);


        #region visiting countries
        ///////////////////////////////////////////////

        int[] clmwidths_3 = { 1, 1 };

        PdfPTable tbl_3 = new PdfPTable(2);

        tbl_3.SetWidths(clmwidths_3);

        //tbl_2.WidthPercentage = 0.50f;
        tbl_3.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_3.SpacingBefore = 0;
        tbl_3.SpacingAfter = 0;
        tbl_3.DefaultCell.Border = 1;
        //tbl_2.WidthPercentage = 40;
        tbl_3.TotalWidth = 250f;
        tbl_3.LockedWidth = true;

        PdfPCell celli = new PdfPCell(new Phrase("From", whiteFont));
        celli.HorizontalAlignment = 1;
        celli.BackgroundColor = new Color(180, 180, 180);
        celli.BorderColor = new Color(200, 200, 200);
        tbl_3.AddCell(celli);

        celli = new PdfPCell(new Phrase("To", whiteFont));
        celli.HorizontalAlignment = 1;
        celli.BackgroundColor = new Color(180, 180, 180);
        celli.BorderColor = new Color(200, 200, 200);
        tbl_3.AddCell(celli);


        celli = new PdfPCell(new Phrase(gtm.get_country_name("LK"), bodyFont));
        celli.HorizontalAlignment = 0;
        celli.BorderColor = new Color(200, 200, 200);
        tbl_3.AddCell(celli);

        celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry1), bodyFont));
        celli.HorizontalAlignment = 0;
        celli.BorderColor = new Color(200, 200, 200);
        tbl_3.AddCell(celli);



        if (!String.IsNullOrEmpty(gtm.visitCntry2))
        {
            celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry1), bodyFont));
            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);

            celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry2), bodyFont));
            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);
        }
        if (!String.IsNullOrEmpty(gtm.visitCntry3))
        {
            celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry2), bodyFont));
            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);

            celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry3), bodyFont));
            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);
        }
        if (!String.IsNullOrEmpty(gtm.visitCntry4))
        {
            celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry3), bodyFont));
            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);

            celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry4), bodyFont));
            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);
        }

        PdfPCell cell11 = new PdfPCell(tbl_3);
        cell11.HorizontalAlignment = 0;
        cell11.Colspan = 1;
        cell11.Border = 0;

        ///////////////////////////////////////////////
        #endregion

        int[] clmwidths111 = { 9, 1, 20 };

        PdfPTable tbl_1 = new PdfPTable(3);

        tbl_1.SetWidths(clmwidths111);

        tbl_1.WidthPercentage = 100;
        tbl_1.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_1.SpacingBefore = 10;
        tbl_1.SpacingAfter = 10;
        tbl_1.DefaultCell.Border = 0;

        
        tbl_1.AddCell(new Phrase("Policy No.", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(pro.policy_no, bodyFont));

        //if (qid.Trim() != polno.Trim())
        //{
        //    tbl_1.AddCell(new Phrase("Policy Number", bodyFont));
        //    tbl_1.AddCell(new Phrase(": ", bodyFont));
        //    tbl_1.AddCell(new Phrase(polno, bodyFont));
        //}

        tbl_1.AddCell(new Phrase("Scheme", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont));

        tbl_1.AddCell(new Phrase("Customer Address", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(pro.getFullAddress2(), bodyFont));

        tbl_1.AddCell(new Phrase("Leaving Date", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.departDate, bodyFont));

        tbl_1.AddCell(new Phrase("Returning Date", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.returnDate, bodyFont));

        tbl_1.AddCell(new Phrase("Destination", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase( gtm.get_country_name(gtm.destination), bodyFont));

        tbl_1.AddCell(new Phrase("Insured's Name", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(GT_mem[0].title+ " " + pro.fullName, bodyFont));

        tbl_1.AddCell(new Phrase("Insured's Date of Birth", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(GT_mem[0].dob, bodyFont));

        tbl_1.AddCell(new Phrase("Passport Number", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(GT_mem[0].ppno, bodyFont));

        tbl_1.AddCell(new Phrase("Gender", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(GT_mem[0].genderDesc, bodyFont));

        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));

        tbl_1.AddCell(new Phrase("Visiting Countries", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(cell11);

        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));
        tbl_1.AddCell(new Phrase(" ", bodyFont));

        tbl_1.AddCell(new Phrase("Sum Insured", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("USD " + gtm.sumIns_usd.ToString("N2"), bodyFont));

        tbl_1.AddCell(new Phrase("Net Premium", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("USD " + gtm.netPremium_usd.ToString("N2"), bodyFont));

        tbl_1.AddCell(new Phrase("Class of cover", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("See Benefit Details", bodyFont));

        tbl_1.AddCell(new Phrase("Excess", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("See Benefit Details", bodyFont));

        tbl_1.AddCell(new Phrase("Special Conditions & Exclusions", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("See Benefit Details", bodyFont));

        document.Add(tbl_1);

        #region member details
        ////////////////////////////////////////////////////////

        List<GT_Proposal_mem> members = gtm.members;

        if (members != null)
        {

            document.Add(new Paragraph("\nInsureds' Details (Currency type : USD)", bodyFont_bold));

            int[] clmwidths = { 7, 2 ,2 , 2, 2 };

            PdfPTable tbl_4 = new PdfPTable(5);

            tbl_4.SetWidths(clmwidths);

            tbl_4.WidthPercentage = 100;
            tbl_4.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl_4.SpacingBefore = 10;
            tbl_4.SpacingAfter = 0;
            tbl_4.DefaultCell.Border = 0;


            //PdfPCell celli1 = new PdfPCell(new Phrase("Member ID", bodyFont4_white_bold));
            //celli1.HorizontalAlignment = 1;
            //celli1.BackgroundColor = new Color(180, 180, 180);
            //celli1.BorderColor = new Color(200, 200, 200);
            //tbl_4.AddCell(celli1);

            PdfPCell celli1 = new PdfPCell(new Phrase("Name", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Passport No.", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Date of Birth", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Premium", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            int i = 0;

            foreach (GT_Proposal_mem mem in members)
            {
                //celli = new PdfPCell(new Phrase(mem.member_id, bodyFont4));
                //celli.HorizontalAlignment = 0;
                //celli.BorderColor = new Color(200, 200, 200);
                //tbl_4.AddCell(celli);

                i++;

                celli = new PdfPCell(new Phrase(i.ToString()+". "+mem.title + " " + mem.name, bodyFont4));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);

                

                celli = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                celli.HorizontalAlignment = 1;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);

                celli = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                celli.HorizontalAlignment = 1;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);

                celli = new PdfPCell(new Phrase(mem.dob, bodyFont4));
                celli.HorizontalAlignment = 1;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);

                

                celli = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                celli.HorizontalAlignment = 2;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_4.AddCell(celli);

                //tbl_4.AddCell(new Phrase(mem.member_id, bodyFont));
                //tbl_4.AddCell(new Phrase(mem.title +" "+mem.name, bodyFont));
                //tbl_4.AddCell(new Phrase(mem.ppno, bodyFont));
                //tbl_4.AddCell(new Phrase(mem.gender, bodyFont));
                //tbl_4.AddCell(new Phrase(mem.dob, bodyFont));
                //tbl_4.AddCell(new Phrase(mem.age.ToString(), bodyFont));
                //tbl_4.AddCell(new Phrase(mem.member_type, bodyFont));
            }

            document.Add(tbl_4);
        }

        ////////////////////////////////////////////////////////////
        #endregion

        document.NewPage();

        Chunk titch12 = new Chunk("SPECIFIED TRIP COVER\n\n", bodyFont_bold_sml);
        //titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine2 = new Paragraph(titch12);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);

        int[] clmwidths112 = { 8, 1, 20 };

        PdfPTable tbl_2 = new PdfPTable(3);

        tbl_2.SetWidths(clmwidths112);

        tbl_2.WidthPercentage = 100;
        tbl_2.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_2.SpacingBefore = 10;
        tbl_2.SpacingAfter = 10;
        tbl_2.DefaultCell.Border = 0;

        tbl_2.AddCell(new Phrase("Reference No.", bodyFont_sml));
        tbl_2.AddCell(new Phrase(": ", bodyFont_sml));
        tbl_2.AddCell(new Phrase(qid, bodyFont_sml));

        tbl_2.AddCell(new Phrase("Scheme", bodyFont_sml));
        tbl_2.AddCell(new Phrase(": ", bodyFont_sml));
        tbl_2.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont_sml));

        tbl_2.AddCell(new Phrase("Currency Type", bodyFont_sml));
        tbl_2.AddCell(new Phrase(": ", bodyFont_sml));
        tbl_2.AddCell(new Phrase("USD", bodyFont_sml));

        document.Add(tbl_2);

        GT_Benefits gtt = new GT_Benefits(gtm.plan);

        int[] clmwidths_5 = { 11, 5, 3 };

        PdfPTable tbl_5 = new PdfPTable(3);

        tbl_5.SetWidths(clmwidths_5);

        tbl_5.WidthPercentage = 100;
        tbl_5.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_5.SpacingBefore = 15;
        tbl_5.SpacingAfter = 20;
        tbl_5.DefaultCell.Border = 0;

        PdfPCell cellTBA = new PdfPCell(new Phrase("BENEFITS", bodyFont_bold_sm));
        cellTBA.HorizontalAlignment = 0;
        cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
        cellTBA.BorderWidth = 0f;
        cellTBA.BorderColor = new Color(System.Drawing.Color.White);
        cellTBA.Padding = 5;

        tbl_5.AddCell(cellTBA);

        cellTBA = new PdfPCell(new Phrase("SUM INSURED (USD)", bodyFont_bold_sm));
        cellTBA.HorizontalAlignment = 2;
        cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
        cellTBA.BorderWidth = 0f;
        cellTBA.Padding = 5;

        tbl_5.AddCell(cellTBA);

        cellTBA = new PdfPCell(new Phrase("EXCESS (USD)", bodyFont_bold_sm));
        cellTBA.HorizontalAlignment = 2;
        cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
        cellTBA.BorderWidth = 0f;
        cellTBA.Padding = 5;

        tbl_5.AddCell(cellTBA);


        foreach (DataRow dr in gtt.DTproduct.Rows)
        {
            cellTBA = new PdfPCell(new Phrase(dr[0].ToString(), bodyFont_sml));
            cellTBA.HorizontalAlignment = 0;
            cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellTBA.BorderWidth = 0f;
            cellTBA.Padding = 2;

            tbl_5.AddCell(cellTBA);

            cellTBA = new PdfPCell(new Phrase(dr[1].ToString(), bodyFont_sml));
            cellTBA.HorizontalAlignment = 2;
            cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellTBA.BorderWidth = 0f;
            cellTBA.Padding = 2;

            tbl_5.AddCell(cellTBA);

            cellTBA = new PdfPCell(new Phrase(dr[2].ToString(), bodyFont_sml));
            cellTBA.HorizontalAlignment = 2;
            cellTBA.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellTBA.BorderWidth = 0f;
            cellTBA.Padding = 2;

            tbl_5.AddCell(cellTBA);
        }

        document.Add(tbl_5);


        Chunk titch14 = new Chunk("Contact Details -:", bodyFont_bold_sm);
        titch14.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine4 = new Paragraph(titch14);
        //titleLine4.SetAlignment("Center");
        document.Add(titleLine4);



        document.Add(new Paragraph("     Paramount Healthcare Management Pvt. Ltd.", bodyFont_bold_sm));
        document.Add(new Paragraph("     Travel Health Dept.", bodyFont_bold_sm));
        document.Add(new Paragraph("     401 - 402 Sumer Plaza,", bodyFont_bold_sm));
        document.Add(new Paragraph("     Marol Maroshi Road, Marol,", bodyFont_bold_sm));
        document.Add(new Paragraph("     Andheri (East)", bodyFont_bold_sm));
        document.Add(new Paragraph("     Mumbai 400 059.", bodyFont_bold_sm));
        document.Add(new Paragraph("     Tel : +91 22 4000 4207/4219", bodyFont_bold_sm));
        document.Add(new Paragraph("     Fax : +91 22 4000 4280", bodyFont_bold_sm));
        document.Add(new Paragraph("     USA Toll Free : 001 866 978 5205", bodyFont_bold_sm));
        document.Add(new Paragraph("     Dedicated Helpline Number for SLIC members : +91 22 4090 8314", bodyFont_bold_sm));
        document.Add(new Paragraph("     Email Id : travelhealth@paramount.healthcare", bodyFont_bold_sm));
        document.Add(new Paragraph("     Whatsapp No +91 7718806681 (the message should include following details)", bodyFont_bold_sm));
        document.Add(new Paragraph("          Insured Name:", bodyFont_bold_sm));
        document.Add(new Paragraph("          Insurance Co. Name:", bodyFont_bold_sm));
        document.Add(new Paragraph("          Insurance Policy No.:", bodyFont_bold_sm));
        document.Add(new Paragraph("          Country Where Traveled:", bodyFont_bold_sm));
        document.Add(new Paragraph("          Type of Assistance Required:", bodyFont_bold_sm));
        document.Add(new Paragraph("          Email id of insured:", bodyFont_bold_sm));

        #endregion

        document.NewPage();

        #region claim sheet

        document.Add(new Paragraph("ATTACHMENT ON CLAIMS PROCEDURE", bodyFont));

        string root2 = System.Web.HttpContext.Current.Server.MapPath("/Images/gt_claim.png");
        iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance(root2);
        logo2.ScalePercent(35f);
        logo2.SetAbsolutePosition(350, 710);
        document.Add(logo2);

        document.Add(new Paragraph("\n\n\n\n\nAs per Policy terms and conditions: General Conditions: Point no: 5 Claim procedure: Insured should immediately inform PARAMOUNT Healthcare Management (Pvt.) Ltd. about the loss and provide PHM with the necessary details.\n", bodyFont));


        int[] clmwidths115 = { 2, 1, 18 };

        PdfPTable tbl_15 = new PdfPTable(3);

        tbl_15.SetWidths(clmwidths115);

        tbl_15.WidthPercentage = 100;
        tbl_15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_15.SpacingBefore = 20;
        tbl_15.SpacingAfter = 10;
        tbl_15.DefaultCell.Border = 0;


        Chunk titch15 = new Chunk("Claim Procedure in respect of:", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");

        


        PdfPCell cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);



        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("(a)", bodyFont));
        tbl_15.AddCell(new Phrase("Procedure in the event of accident or Illness:", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("(b)", bodyFont));
        tbl_15.AddCell(new Phrase("Procedure in case of loss of baggage or passport:", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("(c)", bodyFont));
        tbl_15.AddCell(new Phrase("Procedure in case of financial emergency:", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("(d)", bodyFont));
        tbl_15.AddCell(new Phrase("Procedure in case of hijacking:", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("", bodyFont_sml));
        tbl_15.AddCell(new Phrase("REFER GENERAL CONDITIONS (APPLICABLE TO WHOLE OF THE POLICY): 5. (a) (b) (c) & (d)", bodyFont_bold_sml));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));

        titch15 = new Chunk("For Claims settlement:", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");

        cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);


        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("(a)", bodyFont));
        tbl_15.AddCell(new Phrase("Direct Payment:", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("(b)", bodyFont));
        tbl_15.AddCell(new Phrase("Reimbursement:", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("REFER GENERAL CONDITIONS (APPLICABLE TO WHOLE OF THE POLICY): 6. (a) & (b)", bodyFont_bold_sml));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));


        titch15 = new Chunk("Obligations:", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");
        cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);


        Phrase ph = new Phrase();
        ph.Add(new Chunk("Claims Intimation: ", bodyFont_bold));
        ph.Add(new Chunk("Not later than one month after completion of the treatment or transportation home, or in the event of death, after transportation of mortal remains/burial.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("i.", bodyFont_sml));
        tbl_15.AddCell(ph);

        ph = new Phrase();
        ph.Add(new Chunk("Further documentation / medical examination: ", bodyFont_bold));
        ph.Add(new Chunk("requested by PARAMOUNT Healthcare Management (Pvt.) Ltd.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("ii.", bodyFont_sml));
        tbl_15.AddCell(ph);


        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));

        titch15 = new Chunk("MANDATORY DOCUMENTS REQUIRED FOR EACH CLAIM:", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");

        cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Claim Form duly perfected.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original policy document.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original air ticket – e-ticket and boarding pass.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Copy of Passport, immigration stampings for all the sectors.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));

        titch15 = new Chunk("Health cover", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");

        cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);


        ph = new Phrase();
        ph.Add(new Chunk("Medical reports from the treating doctor mentioning the diagnosis, its duration, treatment medication prescribe for the same ", bodyFont));
        ph.Add(new Chunk("in English.", bodyFont_bold));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(ph);

        ph = new Phrase();
        ph.Add(new Chunk("Original Bills / Invoices, Receipts, Prescriptions, along with the payment proof/receipts from the provider for the services rendered by them ", bodyFont));
        ph.Add(new Chunk("in English.", bodyFont_bold));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(ph);

        ph = new Phrase();
        ph.Add(new Chunk("All Original investigation reports related to the treatment ", bodyFont));
        ph.Add(new Chunk("in English.", bodyFont_bold));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(ph);


        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));


        tbl_15.AddCell(new Phrase("Note : ", bodyFont_bold_sm));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));


        

        cell15 = new PdfPCell(new Phrase("Please note that the conditions of policy prevail/supersede this attachment and the attachment does not form part of the policy issued to the Insured.\nAs the Original documents are required by PHM, the Insured may keep a duplicate set of documents for their reference.", bodyFont_sml));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);

        document.Add(tbl_15);
        document.NewPage();

        document.Add(new Paragraph("ATTACHMENT ON CLAIMS PROCEDURE\n\n", bodyFont));

        tbl_15 = new PdfPTable(3);

        tbl_15.SetWidths(clmwidths115);

        tbl_15.WidthPercentage = 100;
        tbl_15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_15.SpacingBefore = 10;
        tbl_15.SpacingAfter = 10;
        tbl_15.DefaultCell.Border = 0;


        titch15 = new Chunk("Delay of baggage / Loss of Baggage", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");

        cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original Property Irregularity Report (PIR) issued by the airlines.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Letter from airline stating the baggage is permanently loss and compensation received thereof.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Description of item lost in the baggage along with relevant bills.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Letter from the airline if any compensation is paid towards settlement of claim for delay of baggage of Baggage delivery receipt.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Emergency purchase Bills/receipts toiletries and medicines (for delay in baggage).", bodyFont));


        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));


        titch15 = new Chunk("Loss of Passport:", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");

        cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original Police report in English.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Brief Description of incident.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original receipts for obtaining a duplicate or new passport.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Copies of Duplicate or new passport.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original Bills and receipts of incidental expenses if any.", bodyFont));


        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));


        titch15 = new Chunk("Financial Emergency:", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");

        cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original Police report in English.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Brief Description of the incident.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));
        tbl_15.AddCell(new Phrase(" ", bodyFont));

        titch15 = new Chunk("Hijack Distress Allowance:", bodyFont_bold);
        titch15.SetUnderline(0.5f, -1.5f);
        titleLine15 = new Paragraph(titch15);
        titleLine15.SetAlignment("Left");

        cell15 = new PdfPCell(new Phrase(titleLine15));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original Police Report in English.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Original Airline report.", bodyFont));

        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase("*", bodyFont));
        tbl_15.AddCell(new Phrase("Media TV Coverage Report.", bodyFont));



        cell15 = new PdfPCell(new Phrase(" ", bodyFont));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);
        tbl_15.AddCell(cell15);


        tbl_15.AddCell(new Phrase("Note : ", bodyFont_bold_sm));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));
        tbl_15.AddCell(new Phrase(" ", bodyFont_sml));

        
        cell15 = new PdfPCell(new Phrase("Please note that the conditions of policy prevail/supersede this attachment and the attachment does not form part of the policy issued to the Insured.\nAs the Original documents are required by PHM, the Insured may keep a duplicate set of documents for their reference.", bodyFont_sml));
        cell15.Colspan = 3;
        cell15.HorizontalAlignment = 0;
        cell15.VerticalAlignment = Element.ALIGN_LEFT;
        cell15.BorderWidth = 0f;
        cell15.BorderColor = new Color(System.Drawing.Color.White);
        cell15.Padding = 5;

        tbl_15.AddCell(cell15);


        document.Add(tbl_15);
        



        #endregion

        document.Close();
        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=GT_{0}.pdf", "Policy_Document"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
    }
}