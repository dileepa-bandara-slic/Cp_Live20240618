using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Globalization;

/// <summary>
/// Summary description for TRV_print_pdf
/// </summary>
public class TRV_print_pdf
{
    public TRV_print_pdf()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void print_quotation(string qid, string epf, string ip, bool reprint)
    {
        TRV_Prop_mast gtm = new TRV_Prop_mast(qid);
        Document document = new Document(PageSize.A4, 50, 50, 10, 10);
        string agentName = "";
        TRV_Proposal prop = new TRV_Proposal();
        prop.getAgtName(gtm.AGENT_CODE, out agentName);

        List<TRV_Proposal_mem> GT_mem = gtm.members;
        TRV_Proposal pro = new TRV_Proposal();
        //CustProfile cp = new CustProfile(epf);
        TRV_Country coun = new TRV_Country();
        TRV_Quot_Heading quoHead = new TRV_Quot_Heading(qid);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        if (reprint)
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf + " REPRINT", new Font(Font.COURIER, 8));
        else
         //  if (gtm.AGENT_CODE > 0)
           // phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf + " Agency Code : " + gtm.AGENT_CODE.ToString(), new Font(Font.COURIER, 8));
        //else
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf  , new Font(Font.COURIER, 8));

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
        Font underlineFont = FontFactory.GetFont("Times New Roman", 10, Font.UNDERLINE);
        Font underlineAndBoldFont = FontFactory.GetFont("Times New Roman", 10, Font.UNDERLINE | Font.BOLD);

        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/slic_gen_Logo.png");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        //logo.ScalePercent(25f);
        //logo.SetAbsolutePosition(260, 740);
        logo.ScalePercent(25f);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);


        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/Gi_Watermark.gif"));
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark);

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        //document.Add(logo);

        quoHead.UPPER_CONTENT = "We have pleasure in submitting our Quotation for your kind consideration.";

        document.Add(new Paragraph("\n\n\n\n\n\n\n", bodyFont));
        Chunk titch1 = new Chunk("TRAVEL PROTECT QUOTATION", boldTableFont);
        titch1.SetUnderline(0.5f, -1.5f);
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);


        document.Add(new Paragraph("\n", bodyFont));
        document.Add(new Paragraph("Dear Sir/Madam,\n", bodyFont));
        document.Add(new Paragraph(quoHead.UPPER_CONTENT + "\n", bodyFont));

        int[] clmwidths111 = { 8, 1, 20 };

        PdfPTable tbl_1 = new PdfPTable(3);

        tbl_1.SetWidths(clmwidths111);

        tbl_1.WidthPercentage = 100;
        tbl_1.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_1.SpacingBefore = 10;
        tbl_1.SpacingAfter = 10;
        tbl_1.DefaultCell.Border = 0;      

        tbl_1.AddCell(new Phrase("Policy type", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("Travel Protect", bodyFont));

        tbl_1.AddCell(new Phrase("Scheme", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.get_scheme_name(gtm.PLAN,"TPI"), bodyFont));

        tbl_1.AddCell(new Phrase("Quotation Number", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(qid, bodyFont));

        tbl_1.AddCell(new Phrase("Proposer", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.FULL_NAME, bodyFont));

        string ADD1 = ReplaceHtmlFunction(PrepareApostrophe(gtm.ADDRESS1.ToString()).Replace("&amp;", "&").Replace("&nbsp;", ""));
        string ADD2 = ReplaceHtmlFunction(PrepareApostrophe(gtm.ADDRESS2.ToString()).Replace("&amp;", "&").Replace("&nbsp;", ""));
        string ADD3 = ReplaceHtmlFunction(PrepareApostrophe(gtm.ADDRESS3.ToString()).Replace("&amp;", "&").Replace("&nbsp;", ""));
        string ADD4 = ReplaceHtmlFunction(PrepareApostrophe(gtm.ADDRESS4.ToString()).Replace("&amp;", "&").Replace("&nbsp;", ""));
        string proLetter = "";
        //proLetter = proLetter + ADD1 + " ";
        if (!String.IsNullOrEmpty(ADD1))
        {
            proLetter = proLetter + ADD1 + " , ";
            if (!String.IsNullOrEmpty(ADD2))
            {
                proLetter = proLetter + ADD2 + " , ";
                if (!String.IsNullOrEmpty(ADD3))
                {
                    proLetter = proLetter + ADD3 + " , ";
                    if (!String.IsNullOrEmpty(ADD4))
                    {
                        proLetter = proLetter + ADD4 + ".";
                    }
                }
            }
        }
        if (!String.IsNullOrEmpty(ADD1))
        {
            proLetter = proLetter.TrimEnd(',');
        }
        //proLetter = proLetter.Remove(proLetter.LastIndexOf(","));

        tbl_1.AddCell(new Phrase("Proposer Address", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(proLetter, bodyFont));

        tbl_1.AddCell(new Phrase("Contact Number", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.MOBILE_NUMBER, bodyFont));

        tbl_1.AddCell(new Phrase("Issued Date", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.ENTERED_DATE, bodyFont));

        tbl_1.AddCell(new Phrase("Leaving Date", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.DEPART_DATE, bodyFont));

        tbl_1.AddCell(new Phrase("Returning Date", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.RETURN_DATE, bodyFont));
       
        int[] clmwidths_12 = { 1 };

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

        string[] ctryCode = gtm.DESTINATION.Split(',');

        if (gtm.arrDest.Count > 1)
        {
            for (int i = 0; i < gtm.arrDest.Count; i++)
            {
                tbl_12.AddCell(new Phrase(gtm.arrDest[i].ToString() + "  " + (coun.check_schengen(ctryCode[i].ToString()) ? "(Schengen state)" : ""), bodyFont));
            }
        }
        else
        {
            tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.DESTINATION) + "  " + (coun.check_schengen(gtm.DESTINATION) ? "(Schengen state)" : ""), bodyFont));

            if (!String.IsNullOrEmpty(gtm.VISIT_CTRY1))
            {
                tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.VISIT_CTRY1) + "  " + (coun.check_schengen(gtm.VISIT_CTRY1) ? "(Schengen state)" : ""), bodyFont));
            }
            if (!String.IsNullOrEmpty(gtm.VISIT_CTRY2))
            {
                tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.VISIT_CTRY2) + "  " + (coun.check_schengen(gtm.VISIT_CTRY2) ? "(Schengen state)" : ""), bodyFont));

            }

            if (!String.IsNullOrEmpty(gtm.VISIT_CTRY3))
            {
                tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.VISIT_CTRY3) + "  " + (coun.check_schengen(gtm.VISIT_CTRY3) ? "(Schengen state)" : ""), bodyFont));

            }

            if (!String.IsNullOrEmpty(gtm.VISIT_CTRY4))
            {
                tbl_12.AddCell(new Phrase(gtm.get_country_name(gtm.VISIT_CTRY4) + "  " + (coun.check_schengen(gtm.VISIT_CTRY4) ? "(Schengen state)" : ""), bodyFont));

            }
        }


        PdfPCell cellctr = new PdfPCell(tbl_12);
        cellctr.BorderWidth = 0f;

        tbl_1.AddCell(new Phrase("Visiting Countries", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(cellctr);




        tbl_1.AddCell(new Phrase("Sum Insured", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase("USD " + gtm.SUM_INS_USD.ToString("N2"), bodyFont));

        document.Add(tbl_1);

        #region member details
        ////////////////////////////////////////////////////////
        PdfPTable tbl_4 = new PdfPTable(5);
        List<TRV_Proposal_mem> members = gtm.members.OrderBy(s => int.Parse(s.member_id.Split('_')[1])).ToList();

        int memCount = members.Count;


        if (members != null)
        {
            //document.Add(new Paragraph("\nInsureds' Details (Currency type : USD)", bodyFont_bold));

            int[] clmwidths = { 8, 3, 3, 3, 3 };



            tbl_4.SetWidths(clmwidths);

            //tbl_4.WidthPercentage = 80;
            tbl_4.HorizontalAlignment = Element.ALIGN_LEFT;
            tbl_4.SpacingBefore = 10;
            tbl_4.SpacingAfter = 10;
            tbl_4.DefaultCell.Border = 0;
            tbl_4.TotalWidth = 450f;
            tbl_4.LockedWidth = true;

           
            PdfPCell celli1 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

           
            celli1 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
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



            if (memCount > 0 && memCount <= 5)
            {

                document.Add(new Paragraph("Insureds' Details", bodyFont_bold_sm));

                foreach (TRV_Proposal_mem mem in members)
                {

                    i++;
                    mem.name = "Member " + i.ToString();

                    PdfPCell celli = new PdfPCell(new Phrase(mem.name, bodyFont4));
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
                }

                //document.Add(new Paragraph("Insureds' Details", bodyFont));

                document.Add(tbl_4);


                int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                PdfPTable tbl_45 = new PdfPTable(5);
                tbl_45.SetWidths(clmwidths55);

                //tbl_4.WidthPercentage = 80;
                tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl_45.SpacingBefore = 0;
                tbl_45.SpacingAfter = 5;
                tbl_45.DefaultCell.Border = 0;
                tbl_45.TotalWidth = 450f;
                tbl_45.LockedWidth = true;


                PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 0;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);


                cell5i = new PdfPCell(new Phrase(gtm.NET_PREMIUM_USD.ToString("N2"), bodyFont4));
                cell5i.HorizontalAlignment = 2;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                document.Add(tbl_45);

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
                cell2 = new PdfPCell(new Phrase(gtm.NET_PREMIUM_RS.ToString("N2"), bodyFont));
                cell2.HorizontalAlignment = 2;
                cell2.Border = 0;
                tbl_2.AddCell(cell2);

                /* ********** Commented due to adding SSC to Admin fee   **************************************
                tbl_2.AddCell(new Phrase("Admin Fee ", bodyFont));
                tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                cell2 = new PdfPCell(new Phrase(gtm.ADMIN_FEE_RS.ToString("N2"), bodyFont));
                cell2.HorizontalAlignment = 2;
                cell2.Border = 0;
                tbl_2.AddCell(cell2);
                **********************Added Admin Fee and SSC **************************************************/

                tbl_2.AddCell(new Phrase("Admin Fee ", bodyFont));
                tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                cell2 = new PdfPCell(new Phrase((gtm.ADMIN_FEE_RS+gtm.NBT_RS).ToString("N2"), bodyFont));
                cell2.HorizontalAlignment = 2;
                cell2.Border = 0;
                tbl_2.AddCell(cell2);

                tbl_2.AddCell(new Phrase("Policy Fee ", bodyFont));
                tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                cell2 = new PdfPCell(new Phrase(gtm.POLICY_FEE_RS.ToString("N2"), bodyFont));
                cell2.HorizontalAlignment = 2;
                cell2.Border = 0;
                tbl_2.AddCell(cell2);

                /******************************* Removed Since SSC added to Admin fee************************
                 
                tbl_2.AddCell(new Phrase("Social Sec. Cont.", bodyFont));
                tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                cell2 = new PdfPCell(new Phrase(gtm.NBT_RS.ToString("N2"), bodyFont));
                cell2.HorizontalAlignment = 2;
                cell2.Border = 0;
                tbl_2.AddCell(cell2);

                *********************************************************************************************/


                tbl_2.AddCell(new Phrase("VAT", bodyFont));
                tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                cell2 = new PdfPCell(new Phrase(gtm.VAT_RS.ToString("N2"), bodyFont));
                cell2.HorizontalAlignment = 2;
                cell2.Border = 0;
                tbl_2.AddCell(cell2);

                tbl_2.AddCell(new Phrase("Total", bodyFont));
                tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                cell2 = new PdfPCell(new Phrase(gtm.FINAL_PREMIUM_RS.ToString("N2"), bodyFont));
                cell2.HorizontalAlignment = 2;
                cell2.Border = 0;
                tbl_2.AddCell(cell2);                

                tbl_2.WidthPercentage = 50f;
                PdfPCell cell1 = new PdfPCell(tbl_2);
                cell1.HorizontalAlignment = 0;
                cell1.Colspan = 1;
                cell1.Border = 0;

                int[] clmwidthsPre = { 8, 1, 20 };

                PdfPTable tbl_pre = new PdfPTable(3);

                tbl_pre.SetWidths(clmwidthsPre);

                tbl_pre.WidthPercentage = 100;
                tbl_pre.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl_pre.SpacingBefore = 5;
                tbl_pre.SpacingAfter = 5;
                tbl_pre.DefaultCell.Border = 0;


                tbl_pre.AddCell(new Phrase(" ", bodyFont));
                tbl_pre.AddCell(new Phrase(" ", bodyFont));
                tbl_pre.AddCell(new Phrase(" ", bodyFont));

                tbl_pre.AddCell(new Phrase("Premium", bodyFont));
                tbl_pre.AddCell(new Phrase(": ", bodyFont));
                tbl_pre.AddCell(cell1);

                tbl_pre.AddCell(new Phrase(" ", bodyFont));
                tbl_pre.AddCell(new Phrase(" ", bodyFont));
                tbl_pre.AddCell(new Phrase(" ", bodyFont));

                tbl_pre.AddCell(new Phrase(" ", bodyFont));
                tbl_pre.AddCell(new Phrase(" ", bodyFont));
                tbl_pre.AddCell(new Phrase("Today Currency Rate (1 USD) = LKR " + gtm.USD_RATE.ToString("N2"), bodyFont_bold_sm));

                tbl_pre.AddCell(new Phrase(" ", bodyFont));
                tbl_pre.AddCell(new Phrase(" ", bodyFont));
                tbl_pre.AddCell(new Phrase(" ", bodyFont));

                document.Add(tbl_pre);

                document.Add(new Paragraph("* The premium is calculated based  on the exchange rate prevailing on the date hear in. Hence subject to variations of market rate.", bodyFont_bold_sm));

                //document.Add(new Paragraph("* This quotation is valid only for 3 days.", bodyFont_bold_sm));

                document.Add(new Paragraph("\n", bodyFont));

                int[] clmwidths115 = { 8, 1, 20 };

                PdfPTable tbl_5 = new PdfPTable(3);

                tbl_5.SetWidths(clmwidths115);

                tbl_5.WidthPercentage = 100;
                tbl_5.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl_5.SpacingBefore = 10;
                tbl_5.SpacingAfter = 10;
                tbl_5.DefaultCell.Border = 0;

                tbl_5.AddCell(new Phrase("Class of Coverage", bodyFont));
                tbl_5.AddCell(new Phrase(": ", bodyFont));
                tbl_5.AddCell(new Phrase("See Benefit Details", bodyFont));

                tbl_5.AddCell(new Phrase("Excess", bodyFont));
                tbl_5.AddCell(new Phrase(": ", bodyFont));
                tbl_5.AddCell(new Phrase("See Benefit Details", bodyFont));

                tbl_5.AddCell(new Phrase("Special Condition and Exclusion", bodyFont));
                tbl_5.AddCell(new Phrase(": ", bodyFont));
                tbl_5.AddCell(new Phrase("See Benefit Details", bodyFont));

                if (!String.IsNullOrEmpty(quoHead.CONDITIONS))
                {
                    tbl_5.AddCell(new Phrase("Conditions", bodyFont));
                    tbl_5.AddCell(new Phrase(": ", bodyFont));
                    tbl_5.AddCell(new Phrase(quoHead.CONDITIONS, bodyFont));
                }

                document.Add(tbl_5);

                
                document.Add(new Paragraph(quoHead.BOTTOM_CONTENT + "\n", bodyFont));

                document.Add(new Paragraph("\n", bodyFont));
                document.Add(new Paragraph("Yours faithfully,\n\n", bodyFont));
                //document.Add(new Paragraph("Manager,\n", bodyFont));
                //document.Add(new Paragraph("GENERAL ACCIDENT INSURANCE DEPT.\n", bodyFont));

                document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));


                if (gtm.AGENT_CODE > 0)
                {
                     
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + agentName + " )", bodyFont));
                }
            }
            else
            {
                if (memCount > 5 && memCount <= 25)
                {
                    document.NewPage();
                    document.Add(new Paragraph("\n", bodyFont_bold_sm));
                    document.Add(new Paragraph("Quotation Number   : " + qid, bodyFont_bold));
                    document.Add(new Paragraph("Insureds' Details", bodyFont_bold_sm));


                    foreach (TRV_Proposal_mem mem in members)
                    {
                        //mCount++;
                        i++;
                        mem.name = "Member " + i.ToString();
                        PdfPCell celli = new PdfPCell(new Phrase(mem.name, bodyFont4));
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

                    }

                    document.Add(tbl_4);

                    int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                    PdfPTable tbl_45 = new PdfPTable(5);
                    tbl_45.SetWidths(clmwidths55);

                    //tbl_4.WidthPercentage = 80;
                    tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl_45.SpacingBefore = 0;
                    tbl_45.SpacingAfter = 5;
                    tbl_45.DefaultCell.Border = 0;
                    tbl_45.TotalWidth = 450f;
                    tbl_45.LockedWidth = true;


                    PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 0;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);


                    cell5i = new PdfPCell(new Phrase(gtm.NET_PREMIUM_USD.ToString("N2"), bodyFont4));
                    cell5i.HorizontalAlignment = 2;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    document.Add(tbl_45);


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
                    cell2 = new PdfPCell(new Phrase(gtm.NET_PREMIUM_RS.ToString("N2"), bodyFont));
                    cell2.HorizontalAlignment = 2;
                    cell2.Border = 0;
                    tbl_2.AddCell(cell2);

                    /*******************  Commented due to SSC added to Admin Fee*******************
                    tbl_2.AddCell(new Phrase("Admin Fee ", bodyFont));
                    tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2 = new PdfPCell(new Phrase(gtm.ADMIN_FEE_RS.ToString("N2"), bodyFont));
                    cell2.HorizontalAlignment = 2;
                    cell2.Border = 0;
                    tbl_2.AddCell(cell2);
                    ***********************************************************************************/

                    tbl_2.AddCell(new Phrase("Admin Fee ", bodyFont));
                    tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2 = new PdfPCell(new Phrase((gtm.ADMIN_FEE_RS+gtm.NBT_RS).ToString("N2"), bodyFont));
                    cell2.HorizontalAlignment = 2;
                    cell2.Border = 0;
                    tbl_2.AddCell(cell2);

                    tbl_2.AddCell(new Phrase("Policy Fee ", bodyFont));
                    tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2 = new PdfPCell(new Phrase(gtm.POLICY_FEE_RS.ToString("N2"), bodyFont));
                    cell2.HorizontalAlignment = 2;
                    cell2.Border = 0;
                    tbl_2.AddCell(cell2);

                    /**********************  Commented diue to SSC added to Admin Fee********************
                    tbl_2.AddCell(new Phrase("Social Sec. Cont.", bodyFont));
                    tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2 = new PdfPCell(new Phrase(gtm.NBT_RS.ToString("N2"), bodyFont));
                    cell2.HorizontalAlignment = 2;
                    cell2.Border = 0;
                    tbl_2.AddCell(cell2);
                    *************************************************************************************/
                    tbl_2.AddCell(new Phrase("VAT", bodyFont));
                    tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2 = new PdfPCell(new Phrase(gtm.VAT_RS.ToString("N2"), bodyFont));
                    cell2.HorizontalAlignment = 2;
                    cell2.Border = 0;
                    tbl_2.AddCell(cell2);

                    tbl_2.AddCell(new Phrase("Total", bodyFont));
                    tbl_2.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2 = new PdfPCell(new Phrase(gtm.FINAL_PREMIUM_RS.ToString("N2"), bodyFont));
                    cell2.HorizontalAlignment = 2;
                    cell2.Border = 0;
                    tbl_2.AddCell(cell2);

                   
                    tbl_2.WidthPercentage = 50f;
                    PdfPCell cell1 = new PdfPCell(tbl_2);
                    cell1.HorizontalAlignment = 0;
                    cell1.Colspan = 1;
                    cell1.Border = 0;

                    int[] clmwidthsPre = { 8, 1, 20 };

                    PdfPTable tbl_pre = new PdfPTable(3);

                    tbl_pre.SetWidths(clmwidthsPre);

                    tbl_pre.WidthPercentage = 100;
                    tbl_pre.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl_pre.SpacingBefore = 5;
                    tbl_pre.SpacingAfter = 5;
                    tbl_pre.DefaultCell.Border = 0;


                    tbl_pre.AddCell(new Phrase(" ", bodyFont));
                    tbl_pre.AddCell(new Phrase(" ", bodyFont));
                    tbl_pre.AddCell(new Phrase(" ", bodyFont));

                    tbl_pre.AddCell(new Phrase("Premium", bodyFont));
                    tbl_pre.AddCell(new Phrase(": ", bodyFont));
                    tbl_pre.AddCell(cell1);

                    tbl_pre.AddCell(new Phrase(" ", bodyFont));
                    tbl_pre.AddCell(new Phrase(" ", bodyFont));
                    tbl_pre.AddCell(new Phrase(" ", bodyFont));

                    tbl_pre.AddCell(new Phrase(" ", bodyFont));
                    tbl_pre.AddCell(new Phrase(" ", bodyFont));
                    tbl_pre.AddCell(new Phrase("Today Currency Rate (1 USD) = LKR " + gtm.USD_RATE.ToString("N2"), bodyFont_bold_sm));

                    tbl_pre.AddCell(new Phrase(" ", bodyFont));
                    tbl_pre.AddCell(new Phrase(" ", bodyFont));
                    tbl_pre.AddCell(new Phrase(" ", bodyFont));

                    document.Add(tbl_pre);

                    document.Add(new Paragraph("* The premium is calculated based  on the exchange rate prevailing on the date hear in. Hence subject to variations of market rate.", bodyFont_bold_sm));

                    //document.Add(new Paragraph("* This quotation is valid only for 3 days.", bodyFont_bold_sm));

                    document.Add(new Paragraph("\n", bodyFont));

                    int[] clmwidths115 = { 8, 1, 20 };

                    PdfPTable tbl_5 = new PdfPTable(3);

                    tbl_5.SetWidths(clmwidths115);

                    tbl_5.WidthPercentage = 100;
                    tbl_5.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl_5.SpacingBefore = 10;
                    tbl_5.SpacingAfter = 10;
                    tbl_5.DefaultCell.Border = 0;

                    tbl_5.AddCell(new Phrase("Class of Coverage", bodyFont));
                    tbl_5.AddCell(new Phrase(": ", bodyFont));
                    tbl_5.AddCell(new Phrase("See Benefit Details", bodyFont));

                    tbl_5.AddCell(new Phrase("Excess", bodyFont));
                    tbl_5.AddCell(new Phrase(": ", bodyFont));
                    tbl_5.AddCell(new Phrase("See Benefit Details", bodyFont));

                    tbl_5.AddCell(new Phrase("Special Condition and Exclusion", bodyFont));
                    tbl_5.AddCell(new Phrase(": ", bodyFont));
                    tbl_5.AddCell(new Phrase("See Benefit Details", bodyFont));

                    if (!String.IsNullOrEmpty(quoHead.CONDITIONS))
                    {
                        tbl_5.AddCell(new Phrase("Conditions", bodyFont));
                        tbl_5.AddCell(new Phrase(": ", bodyFont));
                        tbl_5.AddCell(new Phrase(quoHead.CONDITIONS, bodyFont));
                    }

                    document.Add(tbl_5);

                    document.Add(new Paragraph(quoHead.BOTTOM_CONTENT + "\n", bodyFont));

                    document.Add(new Paragraph("\n", bodyFont));
                    document.Add(new Paragraph("Yours faithfully,\n\n", bodyFont));
                    //document.Add(new Paragraph("Manager,\n", bodyFont));
                    //document.Add(new Paragraph("GENERAL ACCIDENT INSURANCE DEPT.\n", bodyFont));
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.\n", bodyFont));


                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + agentName + " )", bodyFont));
                    }
                }

                //if (memCount <= 55)
                if (memCount > 25 && memCount <= 55)
                {
                    document.NewPage();
                    document.Add(new Paragraph("\n", bodyFont_bold_sm));
                    document.Add(new Paragraph("Quotation Number   : " + qid, bodyFont_bold));
                    document.Add(new Paragraph("Insureds' Details", bodyFont_bold_sm));


                    foreach (TRV_Proposal_mem mem in members)
                    {
                        //mCount++;
                        i++;

                        
                        PdfPCell celli = new PdfPCell(new Phrase(mem.name, bodyFont4));
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

                    }

                    //document.Add(new Paragraph("Insureds' Details", bodyFont));

                    document.Add(tbl_4);

                    int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                    PdfPTable tbl_45 = new PdfPTable(5);
                    tbl_45.SetWidths(clmwidths55);

                    //tbl_4.WidthPercentage = 80;
                    tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl_45.SpacingBefore = 0;
                    tbl_45.SpacingAfter = 5;
                    tbl_45.DefaultCell.Border = 0;
                    tbl_45.TotalWidth = 450f;
                    tbl_45.LockedWidth = true;


                    PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 0;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);


                    cell5i = new PdfPCell(new Phrase(gtm.NET_PREMIUM_USD.ToString("N2"), bodyFont4));
                    cell5i.HorizontalAlignment = 2;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    document.Add(tbl_45);

                    document.NewPage();

                    int[] clmwidths_2a = { 5, 2, 3 };

                    PdfPTable tbl_2a = new PdfPTable(3);

                    tbl_2a.SetWidths(clmwidths_2a);

                    //tbl_2.WidthPercentage = 0.50f;
                    tbl_2a.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl_2a.SpacingBefore = 0;
                    tbl_2a.SpacingAfter = 0;
                    tbl_2a.DefaultCell.Border = 0;
                    //tbl_2.WidthPercentage = 40;
                    tbl_2a.TotalWidth = 200f;
                    tbl_2a.LockedWidth = true;

                    PdfPCell cell2a = new PdfPCell();
                    cell2a.HorizontalAlignment = 2;
                    //cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell2a.Border = 0;

                    tbl_2a.AddCell(new Phrase("Basic Premium ", bodyFont));
                    tbl_2a.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2a = new PdfPCell(new Phrase(gtm.NET_PREMIUM_RS.ToString("N2"), bodyFont));
                    cell2a.HorizontalAlignment = 2;
                    cell2a.Border = 0;
                    tbl_2a.AddCell(cell2a);

                    /************************* Commented due to SSC added to Admin Fee****************
                    tbl_2a.AddCell(new Phrase("Admin Fee ", bodyFont));
                    tbl_2a.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2a = new PdfPCell(new Phrase(gtm.ADMIN_FEE_RS.ToString("N2"), bodyFont));
                    cell2a.HorizontalAlignment = 2;
                    cell2a.Border = 0;
                    tbl_2a.AddCell(cell2a);
                    ***********************************************************************************/
                    tbl_2a.AddCell(new Phrase("Admin Fee ", bodyFont));
                    tbl_2a.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2a = new PdfPCell(new Phrase((gtm.ADMIN_FEE_RS+gtm.NBT_RS).ToString("N2"), bodyFont));
                    cell2a.HorizontalAlignment = 2;
                    cell2a.Border = 0;
                    tbl_2a.AddCell(cell2a);


                    tbl_2a.AddCell(new Phrase("Policy Fee ", bodyFont));
                    tbl_2a.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2a = new PdfPCell(new Phrase(gtm.POLICY_FEE_RS.ToString("N2"), bodyFont));
                    cell2a.HorizontalAlignment = 2;
                    cell2a.Border = 0;
                    tbl_2a.AddCell(cell2a);

                    /***************** Commented due to SSC added to Admin Feee************
                    tbl_2a.AddCell(new Phrase("Social Sec. Cont.", bodyFont));
                    tbl_2a.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2a = new PdfPCell(new Phrase(gtm.NBT_RS.ToString("N2"), bodyFont));
                    cell2a.HorizontalAlignment = 2;
                    cell2a.Border = 0;
                    tbl_2a.AddCell(cell2a);
                    *************************************************************************/

                    tbl_2a.AddCell(new Phrase("VAT", bodyFont));
                    tbl_2a.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2a = new PdfPCell(new Phrase(gtm.VAT_RS.ToString("N2"), bodyFont));
                    cell2a.HorizontalAlignment = 2;
                    cell2a.Border = 0;
                    tbl_2a.AddCell(cell2a);

                    tbl_2a.AddCell(new Phrase("Total", bodyFont));
                    tbl_2a.AddCell(new Phrase("LKR   ", bodyFont));
                    cell2a = new PdfPCell(new Phrase(gtm.FINAL_PREMIUM_RS.ToString("N2"), bodyFont));
                    cell2a.HorizontalAlignment = 2;
                    cell2a.Border = 0;
                    tbl_2a.AddCell(cell2a);
                   
                    tbl_2a.WidthPercentage = 50f;
                    PdfPCell cell1a = new PdfPCell(tbl_2a);
                    cell1a.HorizontalAlignment = 0;
                    cell1a.Colspan = 1;
                    cell1a.Border = 0;

                    int[] clmwidthsPrea = { 8, 1, 20 };

                    PdfPTable tbl_prea = new PdfPTable(3);

                    tbl_prea.SetWidths(clmwidthsPrea);

                    tbl_prea.WidthPercentage = 100;
                    tbl_prea.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl_prea.SpacingBefore = 5;
                    tbl_prea.SpacingAfter = 5;
                    tbl_prea.DefaultCell.Border = 0;


                    tbl_prea.AddCell(new Phrase(" ", bodyFont));
                    tbl_prea.AddCell(new Phrase(" ", bodyFont));
                    tbl_prea.AddCell(new Phrase(" ", bodyFont));

                    tbl_prea.AddCell(new Phrase("Premium", bodyFont));
                    tbl_prea.AddCell(new Phrase(": ", bodyFont));
                    tbl_prea.AddCell(cell1a);

                    tbl_prea.AddCell(new Phrase(" ", bodyFont));
                    tbl_prea.AddCell(new Phrase(" ", bodyFont));
                    tbl_prea.AddCell(new Phrase(" ", bodyFont));

                    tbl_prea.AddCell(new Phrase(" ", bodyFont));
                    tbl_prea.AddCell(new Phrase(" ", bodyFont));
                    tbl_prea.AddCell(new Phrase("Today Currency Rate (1 USD) = LKR " + gtm.USD_RATE.ToString("N2"), bodyFont_bold_sm));

                    tbl_prea.AddCell(new Phrase(" ", bodyFont));
                    tbl_prea.AddCell(new Phrase(" ", bodyFont));
                    tbl_prea.AddCell(new Phrase(" ", bodyFont));

                    document.Add(tbl_prea);

                    document.Add(new Paragraph("* The premium is calculated based  on the exchange rate prevailing on the date hear in. Hence subject to variations of market rate.", bodyFont_bold_sm));

                    //document.Add(new Paragraph("* This quotation is valid only for 3 days.", bodyFont_bold_sm));

                    document.Add(new Paragraph("\n", bodyFont));

                    int[] clmwidths115a = { 8, 1, 20 };

                    PdfPTable tbl_5a = new PdfPTable(3);

                    tbl_5a.SetWidths(clmwidths115a);

                    tbl_5a.WidthPercentage = 100;
                    tbl_5a.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl_5a.SpacingBefore = 10;
                    tbl_5a.SpacingAfter = 10;
                    tbl_5a.DefaultCell.Border = 0;

                    tbl_5a.AddCell(new Phrase("Class of Coverage", bodyFont));
                    tbl_5a.AddCell(new Phrase(": ", bodyFont));
                    tbl_5a.AddCell(new Phrase("See Benefit Details", bodyFont));

                    tbl_5a.AddCell(new Phrase("Excess", bodyFont));
                    tbl_5a.AddCell(new Phrase(": ", bodyFont));
                    tbl_5a.AddCell(new Phrase("See Benefit Details", bodyFont));

                    tbl_5a.AddCell(new Phrase("Special Condition and Exclusion", bodyFont));
                    tbl_5a.AddCell(new Phrase(": ", bodyFont));
                    tbl_5a.AddCell(new Phrase("See Benefit Details", bodyFont));

                    if (!String.IsNullOrEmpty(quoHead.CONDITIONS))
                    {
                        tbl_5a.AddCell(new Phrase("Conditions", bodyFont));
                        tbl_5a.AddCell(new Phrase(": ", bodyFont));
                        tbl_5a.AddCell(new Phrase(quoHead.CONDITIONS, bodyFont));
                    }

                    document.Add(tbl_5a);

                    document.Add(new Paragraph(quoHead.BOTTOM_CONTENT + "\n", bodyFont));

                    document.Add(new Paragraph("\n", bodyFont));
                    document.Add(new Paragraph("Yours faithfully,\n\n", bodyFont));
                    //document.Add(new Paragraph("Manager,\n", bodyFont));
                    //document.Add(new Paragraph("GENERAL ACCIDENT INSURANCE DEPT.\n", bodyFont));
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.\n", bodyFont));


                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + agentName + " )", bodyFont));
                    }

                }
                if (memCount > 55)
                {
                    //int mRemain = memCount % 40;
                    ////int remaCount = 0;
                    ////for (int rCount = 0; rCount < remaCount + 40; rCount++)
                    ////{
                    ///
                    int remainder = members.Count % 55;

                    for (int rCount = 0; rCount < members.Count; rCount = rCount + 55)
                    {

                        PdfPTable tbl_memHe = new PdfPTable(5);
                        int[] clmwidthsMemHe = { 8, 3, 3, 3, 3 };



                        tbl_memHe.SetWidths(clmwidthsMemHe);

                        //tbl_4.WidthPercentage = 80;
                        tbl_memHe.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_memHe.SpacingBefore = 10;
                        tbl_memHe.SpacingAfter = 0;
                        tbl_memHe.DefaultCell.Border = 0;
                        tbl_memHe.TotalWidth = 450f;
                        tbl_memHe.LockedWidth = true;

                        PdfPTable tbl_mem = new PdfPTable(5);
                        int[] clmwidthsMem = { 8, 3, 3, 3, 3 };



                        tbl_mem.SetWidths(clmwidthsMem);

                        //tbl_4.WidthPercentage = 80;
                        tbl_mem.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_mem.SpacingBefore = 0;
                        tbl_mem.SpacingAfter = 5;
                        tbl_mem.DefaultCell.Border = 0;
                        tbl_mem.TotalWidth = 450f;
                        tbl_mem.LockedWidth = true;

                        for (int k = rCount; k < members.Count; k = k + 1)
                        {
                            i++;



                            if (i % 55 == 0)
                            {
                                PdfPCell celli = new PdfPCell(new Phrase(members[k].name, bodyFont4));
                                celli.HorizontalAlignment = 0;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);

                                //celli = new PdfPCell(new Phrase(members[k].memType_desc, bodyFont4));
                                //celli.HorizontalAlignment = 1;
                                //celli.BorderColor = new Color(200, 200, 200);
                                //tbl_mem.AddCell(celli);

                                celli = new PdfPCell(new Phrase((members[k].gender.Trim().Equals("M") ? "Male" : (members[k].gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                                celli.HorizontalAlignment = 1;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);

                                celli = new PdfPCell(new Phrase(members[k].ppno, bodyFont4));
                                celli.HorizontalAlignment = 1;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);

                                celli = new PdfPCell(new Phrase(members[k].dob, bodyFont4));
                                celli.HorizontalAlignment = 1;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);


                                celli = new PdfPCell(new Phrase(members[k].base_amount_usd.ToString("N2"), bodyFont4));
                                celli.HorizontalAlignment = 2;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);
                                break;
                            }
                            else
                            {

                                //////PdfPCell celli = new PdfPCell(new Phrase("Member " + i, bodyFont4));
                                //////celli.HorizontalAlignment = 0;
                                //////celli.BorderColor = new Color(200, 200, 200);
                                //////tbl_4.AddCell(celli);



                                PdfPCell celli = new PdfPCell(new Phrase(members[k].name, bodyFont4));
                                celli.HorizontalAlignment = 0;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);

                                //celli = new PdfPCell(new Phrase(members[k].memType_desc, bodyFont4));
                                //celli.HorizontalAlignment = 1;
                                //celli.BorderColor = new Color(200, 200, 200);
                                //tbl_mem.AddCell(celli);

                                celli = new PdfPCell(new Phrase((members[k].gender.Trim().Equals("M") ? "Male" : (members[k].gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                                celli.HorizontalAlignment = 1;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);

                                celli = new PdfPCell(new Phrase(members[k].ppno, bodyFont4));
                                celli.HorizontalAlignment = 1;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);

                                celli = new PdfPCell(new Phrase(members[k].dob, bodyFont4));
                                celli.HorizontalAlignment = 1;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);


                                celli = new PdfPCell(new Phrase(members[k].base_amount_usd.ToString("N2"), bodyFont4));
                                celli.HorizontalAlignment = 2;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);

                            }

                            //}


                        }

                        if (i <= members.Count + 1)
                        {
                            document.NewPage();
                            document.Add(new Paragraph("\n", bodyFont_bold_sm));
                            document.Add(new Paragraph("Quotation Number   : " + qid, bodyFont_bold));

                            document.Add(new Paragraph("Insureds' Details", bodyFont));

                            PdfPCell celli2 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2); 

                            celli2 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            celli2 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            celli2 = new PdfPCell(new Phrase("Date of Birth", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            celli2 = new PdfPCell(new Phrase("Premium (USD)", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            document.Add(tbl_memHe);
                            document.Add(tbl_mem);
                            //int mCount = 0;
                        }

                    }

                    int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                    PdfPTable tbl_45 = new PdfPTable(5);
                    tbl_45.SetWidths(clmwidths55);

                    //tbl_4.WidthPercentage = 80;
                    tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl_45.SpacingBefore = 0;
                    tbl_45.SpacingAfter = 5;
                    tbl_45.DefaultCell.Border = 0;
                    tbl_45.TotalWidth = 450f;
                    tbl_45.LockedWidth = true;


                    PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 0;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);


                    cell5i = new PdfPCell(new Phrase(gtm.NET_PREMIUM_USD.ToString("N2"), bodyFont4));
                    cell5i.HorizontalAlignment = 2;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    document.Add(tbl_45);

                    if (remainder <= 25)
                    {
                        int[] clmwidths_2b = { 5, 2, 3 };

                        PdfPTable tbl_2b = new PdfPTable(3);

                        tbl_2b.SetWidths(clmwidths_2b);

                        //tbl_2.WidthPercentage = 0.50f;
                        tbl_2b.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_2b.SpacingBefore = 0;
                        tbl_2b.SpacingAfter = 0;
                        tbl_2b.DefaultCell.Border = 0;
                        //tbl_2.WidthPercentage = 40;
                        tbl_2b.TotalWidth = 200f;
                        tbl_2b.LockedWidth = true;

                        PdfPCell cell2b = new PdfPCell();
                        cell2b.HorizontalAlignment = 2;
                        //cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell2b.Border = 0;

                        tbl_2b.AddCell(new Phrase("Basic Premium ", bodyFont));
                        tbl_2b.AddCell(new Phrase("LKR   ", bodyFont));
                        cell2b = new PdfPCell(new Phrase(gtm.NET_PREMIUM_RS.ToString("N2"), bodyFont));
                        cell2b.HorizontalAlignment = 2;
                        cell2b.Border = 0;
                        tbl_2b.AddCell(cell2b);

                        /***************** Commented due to SSC added to Admin Feee************
                       tbl_2b.AddCell(new Phrase("Admin Fee ", bodyFont));
                       tbl_2b.AddCell(new Phrase("LKR   ", bodyFont));
                       cell2b = new PdfPCell(new Phrase(gtm.ADMIN_FEE_RS.ToString("N2"), bodyFont));
                       cell2b.HorizontalAlignment = 2;
                       cell2b.Border = 0;
                       tbl_2b.AddCell(cell2b);
                       **********************************************************************/

                        tbl_2b.AddCell(new Phrase("Admin Fee ", bodyFont));
                        tbl_2b.AddCell(new Phrase("LKR   ", bodyFont));
                        cell2b = new PdfPCell(new Phrase((gtm.ADMIN_FEE_RS+gtm.NBT_RS).ToString("N2"), bodyFont));
                        cell2b.HorizontalAlignment = 2;
                        cell2b.Border = 0;
                        tbl_2b.AddCell(cell2b);


                        tbl_2b.AddCell(new Phrase("Policy Fee ", bodyFont));
                       tbl_2b.AddCell(new Phrase("LKR   ", bodyFont));
                       cell2b = new PdfPCell(new Phrase(gtm.POLICY_FEE_RS.ToString("N2"), bodyFont));
                       cell2b.HorizontalAlignment = 2;
                       cell2b.Border = 0;
                       tbl_2b.AddCell(cell2b);

                        /***************** Commented due to SSC added to Admin Feee************
                       tbl_2b.AddCell(new Phrase("Social Sec. Cont.", bodyFont));
                       tbl_2b.AddCell(new Phrase("LKR   ", bodyFont));
                       cell2b = new PdfPCell(new Phrase(gtm.NBT_RS.ToString("N2"), bodyFont));
                       cell2b.HorizontalAlignment = 2;
                       cell2b.Border = 0;
                       tbl_2b.AddCell(cell2b);
                       **********************************************************************/

                       tbl_2b.AddCell(new Phrase("VAT", bodyFont));
                       tbl_2b.AddCell(new Phrase("LKR   ", bodyFont));
                       cell2b = new PdfPCell(new Phrase(gtm.VAT_RS.ToString("N2"), bodyFont));
                       cell2b.HorizontalAlignment = 2;
                       cell2b.Border = 0;
                       tbl_2b.AddCell(cell2b);

                       tbl_2b.AddCell(new Phrase("Total", bodyFont));
                       tbl_2b.AddCell(new Phrase("LKR   ", bodyFont));
                       cell2b = new PdfPCell(new Phrase(gtm.FINAL_PREMIUM_RS.ToString("N2"), bodyFont));
                       cell2b.HorizontalAlignment = 2;
                       cell2b.Border = 0;
                       tbl_2b.AddCell(cell2b);

                       //if (memCount > 20 && memCount <= 30)
                       //{
                       //    document.NewPage();
                       //}

                       tbl_2b.WidthPercentage = 50f;
                       PdfPCell cell1b = new PdfPCell(tbl_2b);
                       cell1b.HorizontalAlignment = 0;
                       cell1b.Colspan = 1;
                       cell1b.Border = 0;

                       int[] clmwidthsPreb = { 8, 1, 20 };

                       PdfPTable tbl_preb = new PdfPTable(3);

                       tbl_preb.SetWidths(clmwidthsPreb);

                       tbl_preb.WidthPercentage = 100;
                       tbl_preb.HorizontalAlignment = Element.ALIGN_CENTER;
                       tbl_preb.SpacingBefore = 5;
                       tbl_preb.SpacingAfter = 5;
                       tbl_preb.DefaultCell.Border = 0;


                       tbl_preb.AddCell(new Phrase(" ", bodyFont));
                       tbl_preb.AddCell(new Phrase(" ", bodyFont));
                       tbl_preb.AddCell(new Phrase(" ", bodyFont));

                       tbl_preb.AddCell(new Phrase("Premium", bodyFont));
                       tbl_preb.AddCell(new Phrase(": ", bodyFont));
                       tbl_preb.AddCell(cell1b);

                       tbl_preb.AddCell(new Phrase(" ", bodyFont));
                       tbl_preb.AddCell(new Phrase(" ", bodyFont));
                       tbl_preb.AddCell(new Phrase(" ", bodyFont));

                       tbl_preb.AddCell(new Phrase(" ", bodyFont));
                       tbl_preb.AddCell(new Phrase(" ", bodyFont));
                       tbl_preb.AddCell(new Phrase("Today Currency Rate (1 USD) = LKR " + gtm.USD_RATE.ToString("N2"), bodyFont_bold_sm));

                       tbl_preb.AddCell(new Phrase(" ", bodyFont));
                       tbl_preb.AddCell(new Phrase(" ", bodyFont));
                       tbl_preb.AddCell(new Phrase(" ", bodyFont));

                       document.Add(tbl_preb);

                       document.Add(new Paragraph("* The premium is calculated based  on the exchange rate prevailing on the date hear in. Hence subject to variations of market rate.", bodyFont_bold_sm));

                       //document.Add(new Paragraph("* This quotation is valid only for 3 days.", bodyFont_bold_sm));

                       document.Add(new Paragraph("\n", bodyFont));

                       int[] clmwidths115b = { 8, 1, 20 };

                       PdfPTable tbl_5b = new PdfPTable(3);

                       tbl_5b.SetWidths(clmwidths115b);

                       tbl_5b.WidthPercentage = 100;
                       tbl_5b.HorizontalAlignment = Element.ALIGN_CENTER;
                       tbl_5b.SpacingBefore = 10;
                       tbl_5b.SpacingAfter = 10;
                       tbl_5b.DefaultCell.Border = 0;

                       tbl_5b.AddCell(new Phrase("Class of Coverage", bodyFont));
                       tbl_5b.AddCell(new Phrase(": ", bodyFont));
                       tbl_5b.AddCell(new Phrase("See Benefit Details", bodyFont));

                       tbl_5b.AddCell(new Phrase("Excess", bodyFont));
                       tbl_5b.AddCell(new Phrase(": ", bodyFont));
                       tbl_5b.AddCell(new Phrase("See Benefit Details", bodyFont));

                       tbl_5b.AddCell(new Phrase("Special Condition and Exclusion", bodyFont));
                       tbl_5b.AddCell(new Phrase(": ", bodyFont));
                       tbl_5b.AddCell(new Phrase("See Benefit Details", bodyFont));

                       if (!String.IsNullOrEmpty(quoHead.CONDITIONS))
                       {
                           tbl_5b.AddCell(new Phrase("Conditions", bodyFont));
                           tbl_5b.AddCell(new Phrase(": ", bodyFont));
                           tbl_5b.AddCell(new Phrase(quoHead.CONDITIONS, bodyFont));
                       }

                       document.Add(tbl_5b);

                       document.Add(new Paragraph(quoHead.BOTTOM_CONTENT + "\n", bodyFont));

                       document.Add(new Paragraph("\n", bodyFont));
                       document.Add(new Paragraph("Yours faithfully,\n\n", bodyFont));
                       //document.Add(new Paragraph("Manager,\n", bodyFont));
                       //document.Add(new Paragraph("GENERAL ACCIDENT INSURANCE DEPT.\n", bodyFont));
                       document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                       document.Add(new Paragraph("This is a computer generated document. No signature is required.\n", bodyFont));


                       if (gtm.AGENT_CODE > 0)
                       {
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph(" ", bodyFont));
                           document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + agentName + " )", bodyFont));
                       }
                   }
                   else
                   {
                       document.NewPage();
                       int[] clmwidths_2c = { 5, 2, 3 };

                       PdfPTable tbl_2c = new PdfPTable(3);

                       tbl_2c.SetWidths(clmwidths_2c);

                       //tbl_2.WidthPercentage = 0.50f;
                       tbl_2c.HorizontalAlignment = Element.ALIGN_LEFT;
                       tbl_2c.SpacingBefore = 0;
                       tbl_2c.SpacingAfter = 0;
                       tbl_2c.DefaultCell.Border = 0;
                       //tbl_2.WidthPercentage = 40;
                       tbl_2c.TotalWidth = 200f;
                       tbl_2c.LockedWidth = true;

                       PdfPCell cell2c = new PdfPCell();
                       cell2c.HorizontalAlignment = 2;
                       //cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                       cell2c.Border = 0;

                       tbl_2c.AddCell(new Phrase("Basic Premium ", bodyFont));
                       tbl_2c.AddCell(new Phrase("LKR   ", bodyFont));
                       cell2c = new PdfPCell(new Phrase(gtm.NET_PREMIUM_RS.ToString("N2"), bodyFont));
                       cell2c.HorizontalAlignment = 2;
                       cell2c.Border = 0;
                       tbl_2c.AddCell(cell2c);

                        /***************** Commented due to SSC added to Admin Feee************
                      tbl_2c.AddCell(new Phrase("Admin Fee ", bodyFont));
                      tbl_2c.AddCell(new Phrase("LKR   ", bodyFont));
                      cell2c = new PdfPCell(new Phrase(gtm.ADMIN_FEE_RS.ToString("N2"), bodyFont));
                      cell2c.HorizontalAlignment = 2;
                      cell2c.Border = 0;
                      tbl_2c.AddCell(cell2c);
                      **********************************************************************/

                        tbl_2c.AddCell(new Phrase("Admin Fee ", bodyFont));
                        tbl_2c.AddCell(new Phrase("LKR   ", bodyFont));
                        cell2c = new PdfPCell(new Phrase((gtm.ADMIN_FEE_RS+gtm.NBT_RS).ToString("N2"), bodyFont));
                        cell2c.HorizontalAlignment = 2;
                        cell2c.Border = 0;
                        tbl_2c.AddCell(cell2c);

                        tbl_2c.AddCell(new Phrase("Policy Fee ", bodyFont));
                      tbl_2c.AddCell(new Phrase("LKR   ", bodyFont));
                      cell2c = new PdfPCell(new Phrase(gtm.POLICY_FEE_RS.ToString("N2"), bodyFont));
                      cell2c.HorizontalAlignment = 2;
                      cell2c.Border = 0;
                        tbl_2c.AddCell(cell2c);

                        /*
                      tbl_2c.AddCell(new Phrase("Social Sec. Cont.", bodyFont));
                      tbl_2c.AddCell(new Phrase("LKR   ", bodyFont));
                      cell2c = new PdfPCell(new Phrase(gtm.NBT_RS.ToString("N2"), bodyFont));
                      cell2c.HorizontalAlignment = 2;
                      cell2c.Border = 0;
                      tbl_2c.AddCell(cell2c);
                      */

                      tbl_2c.AddCell(new Phrase("VAT", bodyFont));
                      tbl_2c.AddCell(new Phrase("LKR   ", bodyFont));
                      cell2c = new PdfPCell(new Phrase(gtm.VAT_RS.ToString("N2"), bodyFont));
                      cell2c.HorizontalAlignment = 2;
                      cell2c.Border = 0;
                      tbl_2c.AddCell(cell2c);

                      tbl_2c.AddCell(new Phrase("Total", bodyFont));
                      tbl_2c.AddCell(new Phrase("LKR   ", bodyFont));
                      cell2c = new PdfPCell(new Phrase(gtm.FINAL_PREMIUM_RS.ToString("N2"), bodyFont));
                      cell2c.HorizontalAlignment = 2;
                      cell2c.Border = 0;
                      tbl_2c.AddCell(cell2c);

                      tbl_2c.WidthPercentage = 50f;
                      PdfPCell cell1c = new PdfPCell(tbl_2c);
                      cell1c.HorizontalAlignment = 0;
                      cell1c.Colspan = 1;
                      cell1c.Border = 0;

                      int[] clmwidthsPrec = { 8, 1, 20 };

                      PdfPTable tbl_prec = new PdfPTable(3);

                      tbl_prec.SetWidths(clmwidthsPrec);

                      tbl_prec.WidthPercentage = 100;
                      tbl_prec.HorizontalAlignment = Element.ALIGN_CENTER;
                      tbl_prec.SpacingBefore = 5;
                      tbl_prec.SpacingAfter = 5;
                      tbl_prec.DefaultCell.Border = 0;


                      tbl_prec.AddCell(new Phrase(" ", bodyFont));
                      tbl_prec.AddCell(new Phrase(" ", bodyFont));
                      tbl_prec.AddCell(new Phrase(" ", bodyFont));

                      tbl_prec.AddCell(new Phrase("Premium", bodyFont));
                      tbl_prec.AddCell(new Phrase(": ", bodyFont));
                      tbl_prec.AddCell(cell1c);

                      tbl_prec.AddCell(new Phrase(" ", bodyFont));
                      tbl_prec.AddCell(new Phrase(" ", bodyFont));
                      tbl_prec.AddCell(new Phrase(" ", bodyFont));

                      tbl_prec.AddCell(new Phrase(" ", bodyFont));
                      tbl_prec.AddCell(new Phrase(" ", bodyFont));
                      tbl_prec.AddCell(new Phrase("Today Currency Rate (1 USD) = LKR " + gtm.USD_RATE.ToString("N2"), bodyFont_bold_sm));

                      tbl_prec.AddCell(new Phrase(" ", bodyFont));
                      tbl_prec.AddCell(new Phrase(" ", bodyFont));
                      tbl_prec.AddCell(new Phrase(" ", bodyFont));

                      document.Add(tbl_prec);

                      document.Add(new Paragraph("* The premium is calculated based  on the exchange rate prevailing on the date hear in. Hence subject to variations of market rate.", bodyFont_bold_sm));

                      //document.Add(new Paragraph("* This quotation is valid only for 3 days.", bodyFont_bold_sm));

                      document.Add(new Paragraph("\n", bodyFont));

                      int[] clmwidths115c = { 8, 1, 20 };

                      PdfPTable tbl_5c = new PdfPTable(3);

                      tbl_5c.SetWidths(clmwidths115c);

                      tbl_5c.WidthPercentage = 100;
                      tbl_5c.HorizontalAlignment = Element.ALIGN_CENTER;
                      tbl_5c.SpacingBefore = 10;
                      tbl_5c.SpacingAfter = 10;
                      tbl_5c.DefaultCell.Border = 0;

                      tbl_5c.AddCell(new Phrase("Class of Coverage", bodyFont));
                      tbl_5c.AddCell(new Phrase(": ", bodyFont));
                      tbl_5c.AddCell(new Phrase("See Benefit Details", bodyFont));

                      tbl_5c.AddCell(new Phrase("Excess", bodyFont));
                      tbl_5c.AddCell(new Phrase(": ", bodyFont));
                      tbl_5c.AddCell(new Phrase("See Benefit Details", bodyFont));

                      tbl_5c.AddCell(new Phrase("Special Condition and Exclusion", bodyFont));
                      tbl_5c.AddCell(new Phrase(": ", bodyFont));
                      tbl_5c.AddCell(new Phrase("See Benefit Details", bodyFont));

                      if (!String.IsNullOrEmpty(quoHead.CONDITIONS))
                      {
                          tbl_5c.AddCell(new Phrase("Conditions", bodyFont));
                          tbl_5c.AddCell(new Phrase(": ", bodyFont));
                          tbl_5c.AddCell(new Phrase(quoHead.CONDITIONS, bodyFont));
                      }

                      document.Add(tbl_5c);

                      document.Add(new Paragraph(quoHead.BOTTOM_CONTENT + "\n", bodyFont));

                      document.Add(new Paragraph("\n", bodyFont));
                      document.Add(new Paragraph("Yours faithfully,\n\n", bodyFont));
                      //document.Add(new Paragraph("Manager,\n", bodyFont));
                      //document.Add(new Paragraph("GENERAL ACCIDENT INSURANCE DEPT.\n", bodyFont));
                      document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                      document.Add(new Paragraph("This is a computer generated document. No signature is required.\n", bodyFont));



                      if (gtm.AGENT_CODE > 0)
                      {
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph(" ", bodyFont));
                          document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + agentName + " )", bodyFont));
                      }
                  }

              }
          }

          #endregion            

      }   


      document.NewPage();

      document.Add(new Paragraph("\n\n", bodyFont));
      Chunk titchTitle = new Chunk("SPECIFIED TRIP COVER", FontFactory.GetFont("Times New Roman", 10, Font.BOLD, Color.BLACK));
      // titchTitle.SetUnderline(0.5f, -1.5f);
      Paragraph titletitle = new Paragraph(titchTitle);
      titletitle.SetAlignment("Center");
      document.Add(titletitle);

      int[] clmwidth1 = { 8, 1, 9 };

      PdfPTable tbl_Quote = new PdfPTable(3);

      tbl_Quote.SetWidths(clmwidth1);

      tbl_Quote.WidthPercentage = 100;
      tbl_Quote.HorizontalAlignment = Element.ALIGN_CENTER;
      tbl_Quote.SpacingBefore = 5;
      tbl_Quote.SpacingAfter = 5;
      tbl_Quote.DefaultCell.Border = 0;

      tbl_Quote.AddCell(new Phrase("QUOTATION NUMBER", bodyFont));
      tbl_Quote.AddCell(new Phrase(": ", bodyFont));
      tbl_Quote.AddCell(new Phrase(gtm.refID, bodyFont));

      tbl_Quote.AddCell(new Phrase("SCHEME", bodyFont));
      tbl_Quote.AddCell(new Phrase(": ", bodyFont));
      tbl_Quote.AddCell(new Phrase(gtm.get_scheme_name(gtm.PLAN,"TPI"), bodyFont));

      tbl_Quote.AddCell(new Phrase("CURRENCY TYPE", bodyFont));
      tbl_Quote.AddCell(new Phrase(": ", bodyFont));
      tbl_Quote.AddCell(new Phrase("USD", bodyFont));

      tbl_Quote.AddCell(new Phrase("  ", bodyFont));
      tbl_Quote.AddCell(new Phrase("  ", bodyFont));
      tbl_Quote.AddCell(new Phrase("  ", bodyFont));

      document.Add(tbl_Quote);
      TRV_Benefits gtBen = new TRV_Benefits(gtm.PLAN);

      if (gtBen.DTproduct.Rows.Count > 0)
      {
          int[] clmwidths100 = { 10, 4, 4 };

          PdfPTable tbl_header = new PdfPTable(3);

          tbl_header.SetWidths(clmwidths100);

          tbl_header.WidthPercentage = 100;
          tbl_header.HorizontalAlignment = Element.ALIGN_CENTER;
          tbl_header.SpacingBefore = 2;
          tbl_header.SpacingAfter = 0;
          tbl_header.DefaultCell.Border = 0;

          tbl_header.AddCell(new Phrase("BENEFIT", boldTableFont));
          tbl_header.AddCell(new Phrase("SUM_INSURED", boldTableFont));
          tbl_header.AddCell(new Phrase("EXCESS", boldTableFont));

          document.Add(tbl_header);

          int[] clmwidths1115 = { 10, 4, 4 };

          PdfPTable tbl_15 = new PdfPTable(3);


          tbl_15.SetWidths(clmwidths1115);

          tbl_15.WidthPercentage = 100;
          tbl_15.HorizontalAlignment = Element.ALIGN_CENTER;
          tbl_15.SpacingBefore = 5;
          tbl_15.SpacingAfter = 5;
          tbl_15.DefaultCell.Border = 0;



          //tbl_1.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont));

          for (int k = 0; k < gtBen.DTproduct.Rows.Count; k++)
          {
              //5 % 4
              string code = gtBen.DTproduct.Rows[k]["code"].ToString();
              if (code.Equals("1000") || code.Equals("2000") || code.Equals("3000") || code.Equals("4000") || code.Equals("5000") || code.Equals("6000"))
              {
                  tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString().ToUpper(), bodyFont_bold));
                  tbl_15.AddCell(new Phrase(" ", bodyFont_bold));
                  tbl_15.AddCell(new Phrase(" ", bodyFont_bold));
              }
              else
              {
                  string sum_insured = gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString().Trim();
                  if (!sum_insured.Equals("NA"))
                  {
                      tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString(), bodyFont));
                      tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString(), bodyFont));
                      tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["EXCESS"].ToString(), bodyFont));
                  }


              }
          }


          document.Add(tbl_15);
      }

      int[] clmwidthsReInsu = { 1 };

      PdfPTable tbl_reIns = new PdfPTable(1);

      tbl_reIns.SetWidths(clmwidthsReInsu);

      //tbl_2.WidthPercentage = 0.50f;
      tbl_reIns.HorizontalAlignment = Element.ALIGN_LEFT;
      tbl_reIns.SpacingBefore = 5;
      tbl_reIns.SpacingAfter = 0;
      tbl_reIns.DefaultCell.Border = 0;
      //tbl_2.WidthPercentage = 40;
      tbl_reIns.TotalWidth = 500f;
      tbl_reIns.LockedWidth = true;

      DateTime EntryDate = DateTime.ParseExact(gtm.ENTERED_DATE, "yyyy/MM/dd", CultureInfo.InvariantCulture);

      TRV_TPA_Company tpaCom = new TRV_TPA_Company(EntryDate);
      //TRV_TPA_Company tpaCom = new TRV_TPA_Company(System.DateTime.Now.Date);
      //tbl_12.AddCell(new Phrase("Sri Lanka", bodyFont));


      if (!String.IsNullOrEmpty(tpaCom.COMPANY))
      {
          tbl_reIns.AddCell(new Phrase("Contact Details -:", underlineAndBoldFont));
          if (!String.IsNullOrEmpty(tpaCom.COMPANY))
          {
              tbl_reIns.AddCell(new Phrase("    " + tpaCom.COMPANY, bodyFont_bold));
          }
          if (!String.IsNullOrEmpty(tpaCom.ADDRESS_1))
          {
              tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_1 + "    " + tpaCom.ADDRESS_2, bodyFont_bold));
          }
          /*if (!String.IsNullOrEmpty(tpaCom.ADDRESS_2))
          {
              tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_2, bodyFont_bold));
          }*/
                        if (!String.IsNullOrEmpty(tpaCom.ADDRESS_3))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_3, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_4))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_4, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_5))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_5, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.TELE_NO))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.TELE_NO + "   " + tpaCom.EMAIL, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.FAX_NO))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.FAX_NO, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.TOLL_FREE))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.TOLL_FREE, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.SLIC_HELP))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.SLIC_HELP, bodyFont));
            }
            /* if (!String.IsNullOrEmpty(tpaCom.EMAIL))
             {
                 tbl_reIns.AddCell(new Phrase("    " + tpaCom.EMAIL, bodyFont_bold));
             }*/
        }
        document.Add(tbl_reIns);


        DateTime CurrentDate = DateTime.ParseExact("2020/02/03", "yyyy/MM/dd", CultureInfo.InvariantCulture);
        DateTime rateChangeDate = DateTime.ParseExact("2022/01/01", "yyyy/MM/dd", CultureInfo.InvariantCulture);

        if (reprint)
        {
            if (EntryDate < rateChangeDate)
            {
                Chunk specNotes = new Chunk("  This Insurance is backed by Munich Re", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
                //titchTitle.SetUnderline(0.5f, -1.5f);
                Paragraph specNote = new Paragraph(specNotes);
                document.Add(specNote);
            }
        }


        //document.Add(new Paragraph("\n", bodyFont));
        Chunk spec = new Chunk("Special notes :", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph specP = new Paragraph(spec);
        document.Add(specP);

        List lstSpeNodes = new List(List.UNORDERED, 10f);

        lstSpeNodes.SetListSymbol("\u2022");

        lstSpeNodes.IndentationLeft = 20f;

        ListItem itemSpeNo1 = new ListItem("Pre - existing Medical Conditions are excluded", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        itemSpeNo1.SetAlignment("ALIGN_JUSTIFIED");

        lstSpeNodes.Add(itemSpeNo1);


        //Chunk specWord = new Chunk("  -   Pre-existing Medical Conditions are excluded", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        ////titchTitle.SetUnderline(0.5f, -1.5f);
        //Paragraph specPWord = new Paragraph(specWord);
        //document.Add(specPWord);

        if (reprint)
        {
            if (EntryDate >= CurrentDate)
            {
                if (EntryDate < rateChangeDate)
                {

                    ListItem itemSpeNo2 = new ListItem("Any claim arising directly or indirectly as a result of the Corona Novel virus/outbreak is excluded", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
                    itemSpeNo2.SetAlignment("ALIGN_JUSTIFIED");

                    lstSpeNodes.Add(itemSpeNo2);

                }
                else
                {

                    ListItem itemSpeNo3 = new ListItem("The policy includes Medical expenses incurred overseas and Trip postponement/ Cancellation owing to testing positive for COVID-19 subject to terms, conditions, and exclusions under the policy.- (Insured should have obtained booster dose vaccine for COVID-19 OR PCR obtained within 72 hours prior to departure, testing negative).", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
                    itemSpeNo3.SetAlignment("ALIGN_JUSTIFIED");

                    lstSpeNodes.Add(itemSpeNo3);


                    ListItem itemSpeNo4 = new ListItem("Countries of citizenship are not covered.", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
                    itemSpeNo4.SetAlignment("ALIGN_JUSTIFIED");

                    lstSpeNodes.Add(itemSpeNo4);


                }
            }
        }
        else
        {

            ListItem itemSpeNo3 = new ListItem("The policy includes Medical expenses incurred overseas and Trip postponement/ Cancellation owing to testing positive for COVID-19 subject to terms, conditions, and exclusions under the policy.- (Insured should have obtained booster dose vaccine for COVID-19 OR PCR obtained within 72 hours prior to departure, testing negative).", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
            itemSpeNo3.SetAlignment("ALIGN_JUSTIFIED");

            lstSpeNodes.Add(itemSpeNo3);


            ListItem itemSpeNo4 = new ListItem("Countries of citizenship are not covered.", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
            itemSpeNo4.SetAlignment("ALIGN_JUSTIFIED");

            lstSpeNodes.Add(itemSpeNo4);


        }

        document.Add(lstSpeNodes);

        document.Close();

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=TRV_{0}.pdf", "Quotation_Document"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();

    }

    private string PrepareApostrophe(string str)
    {
        string newStr = "";
        bool pZero = false, pEnd = false, pMiddle = false;
        if (str.IndexOf("'") < 0) return str;
        pZero = str.IndexOf("'") == 0 ? true : false;
        pEnd = str.LastIndexOf("'") + 1 == str.Length ? true : false;
        pMiddle = str.Substring(1, str.Length - 2).LastIndexOf("'") > 0 ? true : false;


        newStr = pZero && pMiddle && pEnd ? "'||chr(39)||'" + str.Substring(1, str.Length - 2).Replace("'", "'||chr(39)||'") + "'||chr(39)|| '"
            : !pZero && pMiddle && pEnd ? str.Substring(0, (str.Length - 1)).Replace("'", "'||chr(39)||'") + "'||chr(39)|| '" : pZero && !pMiddle && pEnd ? "'||chr(39)||'" + str.Substring(1, str.Length - 2) + "'||chr(39)|| '"
            : pZero && pMiddle && !pEnd ? "'||chr(39)||'" + str.Substring(1, str.Length - 1).Replace("'", "'||chr(39)||'")
            : pZero && !pMiddle && !pEnd ? "'||chr(39)||'" + str.Substring(1, str.Length - 1)
            : !pZero && !pMiddle && pEnd ? str.Substring(0, str.Length - 1) + "'||chr(39)|| '"
            : !pZero && pMiddle && !pEnd ? str.Substring(0, str.Length).Replace("'", "'||chr(39)||'") : str;
        return newStr;
    }

    private string CheckSingleQuotes(string name)
    {
        if (name.Contains("'"))
        {
            //if (countQuotes(name) == 1)
            //    name = name.Replace("'", "''");

            name = name.Replace("'||chr(39)||", "");
        }
        return name;
    }

    private string ReplaceHtmlFunction(string replaceString)
    {
        string name = replaceString.Replace("&#33", "!").Replace("&#34;", "\"").Replace("&#35;", "#").Replace("&#36;", "$").Replace("&#37;", "%").Replace("&#38;", "&").Replace("&#39;", "\'").Replace("&#40;", "(").Replace("&#41;", ")").Replace("&#42;", "*").Replace("&#43;", "+").Replace("&#44;", ",").Replace("&#45;", "-").Replace("&#46;", ".").Replace("&#47;", "/").Replace("&quot;", "\"");

        return name;
    }

    #region Policy Schedule New
    public void print_policy(string poliID, string epf, string ip, bool reprint)
    {
        TRV_Policy_mast gtm = new TRV_Policy_mast(poliID, "TPI");//,"TPI" 
       
        List<TRV_Policy_mem> GT_mem = gtm.members;
        string AgentName = "";
        TRV_Proposal prop = new TRV_Proposal();
        prop.getAgtName(gtm.AGENT_CODE,out AgentName);

        Document document = new Document(PageSize.A4, 50, 50, 30, 5);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        if (reprint)
            //if (gtm.AGENT_CODE > 0)
             //   phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf + " Agency Code : " + gtm.AGENT_CODE.ToString(), new Font(Font.COURIER, 8));
           // else
                phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));
        else
           // if (gtm.AGENT_CODE > 0)
           // phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf + " Agency Code : " + gtm.AGENT_CODE.ToString(), new Font(Font.COURIER, 8));
       // else
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

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_white_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD, new Color(255, 255, 255));
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font underlineAndBoldFont = FontFactory.GetFont("Times New Roman", 9, Font.UNDERLINE | Font.BOLD);


       
        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/slic_gen_Logo.png");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        
        logo.ScalePercent(25f);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);


        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/Gi_Watermark.gif"));
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark); 

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        //document.Add(logo); 


        document.Add(new Paragraph("\n\n\n\n\n\n\n", bodyFont));
        Chunk titch1 = new Chunk("TRAVEL PROTECT INSURANCE\n", boldTableFont);
        //titch1.SetUnderline(0.5f, -1.5f); 
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        Chunk head1 = new Chunk("POLICY SCHEDULE\n", boldTableFont);
        //titch1.SetUnderline(0.5f, -1.5f); 
        Paragraph titleHead = new Paragraph(head1);
        titleHead.SetAlignment("Center");
        document.Add(titleHead);

        #region visiting countries
        /////////////////////////////////////////////// 

        Country coun = new Country();
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

        string[] ctryCode = gtm.destination.Split(',');

        if (gtm.arrDest.Count > 1)
        {

            for (int i = 0; i < gtm.arrDest.Count; i++)
            {
                // tbl_12.AddCell(new Phrase(gtm.arrDest[i].ToString() + "  " + (coun.check_schengen(ctryCode[i].ToString()) ? "(Schengen state)" : ""), bodyFont)); 

                if (i == 0)
                {
                    celli = new PdfPCell(new Phrase(gtm.get_country_name("LK"), bodyFont));
                    celli.HorizontalAlignment = 0;
                    celli.BorderColor = new Color(200, 200, 200);
                    tbl_3.AddCell(celli);

                    celli = new PdfPCell(new Phrase(gtm.arrDest[i].ToString() + "  " + (coun.check_schengen(ctryCode[i].ToString()) ? "(Schengen State)" : ""), bodyFont));

                    celli.HorizontalAlignment = 0;
                    celli.BorderColor = new Color(200, 200, 200);
                    tbl_3.AddCell(celli);
                }
                else
                {
                    celli = new PdfPCell(new Phrase("", bodyFont));
                    celli.HorizontalAlignment = 0;
                    celli.BorderColor = new Color(200, 200, 200);
                    tbl_3.AddCell(celli);

                    celli = new PdfPCell(new Phrase(gtm.arrDest[i].ToString() + "  " + (coun.check_schengen(ctryCode[i].ToString()) ? "(Schengen State)" : ""), bodyFont));

                    celli.HorizontalAlignment = 0;
                    celli.BorderColor = new Color(200, 200, 200);
                    tbl_3.AddCell(celli);
                }
            }
        }

        else
        {
            celli = new PdfPCell(new Phrase(gtm.get_country_name("LK"), bodyFont));
            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);

            celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.destination) + "  " + (coun.check_schengen(gtm.destination) ? "(Schengen State)" : ""), bodyFont));

            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);


            if (!String.IsNullOrEmpty(gtm.visitCntry1))
            {

                celli = new PdfPCell(new Phrase("", bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);

                celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry1) + "  " + (coun.check_schengen(gtm.visitCntry1) ? "(Schengen State)" : ""), bodyFont));

                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);
            }



            if (!String.IsNullOrEmpty(gtm.visitCntry2))
            {
                celli = new PdfPCell(new Phrase("", bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);

                celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry2) + "  " + (coun.check_schengen(gtm.visitCntry2) ? "(Schengen State)" : ""), bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);
            }
            if (!String.IsNullOrEmpty(gtm.visitCntry3))
            {
                celli = new PdfPCell(new Phrase("", bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);

                celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry3) + "  " + (coun.check_schengen(gtm.visitCntry3) ? "(Schengen State)" : ""), bodyFont));

                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);
            }
            if (!String.IsNullOrEmpty(gtm.visitCntry4))
            {
                celli = new PdfPCell(new Phrase("", bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);

                celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry4) + "  " + (coun.check_schengen(gtm.visitCntry4) ? "(Schengen State)" : ""), bodyFont));

                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);
            }
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
        tbl_1.SpacingBefore = 5;
        tbl_1.SpacingAfter = 5;
        tbl_1.DefaultCell.Border = 0;


        tbl_1.AddCell(new Phrase("Policy No.", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(poliID, bodyFont));

        

        tbl_1.AddCell(new Phrase("Scheme", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont));
        
        tbl_1.AddCell(new Phrase("Date of Departure", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        DateTime dDate = DateTime.ParseExact(gtm.departDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

        tbl_1.AddCell(new Phrase(dDate.ToString("dd/MM/yyyy"), bodyFont));

        tbl_1.AddCell(new Phrase("Date of Returning", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));

        DateTime rDate = DateTime.ParseExact(gtm.returnDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);


        tbl_1.AddCell(new Phrase(rDate.ToString("dd/MM/yyyy"), bodyFont));


        tbl_1.AddCell(new Phrase("Insured's Name", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.title + " " + gtm.fullName, bodyFont));


        tbl_1.AddCell(new Phrase("Address of the insured", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.getFullAddress2(), bodyFont));         

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

        document.Add(tbl_1);

        Chunk titch12 = new Chunk("For Benefits/Exclusions, Please refer schedule overleaf\n\n", bodyFont_bold);
        //titch1.SetUnderline(0.5f, -1.5f); 
        Paragraph titleLine12 = new Paragraph(titch12);
        //titleLine2.SetAlignment("Center"); 
        document.Add(titleLine12);      

        List<TRV_Policy_mem> members = gtm.members.OrderBy(s => int.Parse(s.member_id.Split('_')[1])).ToList();


        int memCount = members.Count;
        PdfPTable tbl_4 = new PdfPTable(5);
        int counrtycount = ctryCode.Length;
        if (members != null)
        {                       
            #region Create Table Header
            int[] clmwidths = { 8, 3, 3, 3, 3 };

            tbl_4.SetWidths(clmwidths);

            //tbl_4.WidthPercentage = 80; 
            tbl_4.HorizontalAlignment = Element.ALIGN_LEFT;
            tbl_4.SpacingBefore = 5;
            tbl_4.SpacingAfter = 5;
            tbl_4.DefaultCell.Border = 0;
            tbl_4.TotalWidth = 450f;
            tbl_4.LockedWidth = true;           

            PdfPCell celli1 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1); 

            celli1 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
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

            #endregion

            int i = 0;
            if ((memCount + counrtycount) <= 34)
            {
                #region Create Member Table
                foreach (TRV_Policy_mem mem in members)
                {

                    i++;                    

                    PdfPCell celli_1 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                    celli_1.HorizontalAlignment = 0;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    celli_1 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    celli_1 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                    celli_1 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);


                    celli_1 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                    celli_1.HorizontalAlignment = 2;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);
                }

                document.Add(new Paragraph("Insureds' Details", bodyFont));

                document.Add(tbl_4);

                int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                PdfPTable tbl_45 = new PdfPTable(5);
                tbl_45.SetWidths(clmwidths55);

                //tbl_4.WidthPercentage = 80; 
                tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl_45.SpacingBefore = 0;
                tbl_45.SpacingAfter = 1;
                tbl_45.DefaultCell.Border = 0;
                tbl_45.TotalWidth = 450f;
                tbl_45.LockedWidth = true;


                PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 0;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);


                cell5i = new PdfPCell(new Phrase(gtm.netPremium_usd.ToString("N2"), bodyFont4));
                cell5i.HorizontalAlignment = 2;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                document.Add(tbl_45);

                #endregion                
                
                if ((memCount + counrtycount) <= 25)
                {
                    #region Signature in Same Page

                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                    }

                    #endregion

                }
                else
                {
                    #region Signature in Next Page

                    document.NewPage();
                    ////document.Add(new Paragraph("\n", bodyFont_bold_sm)); 
                    document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));
                    document.Add(new Paragraph("Insureds' Details", bodyFont));

                    document.Add(new Paragraph(" ", bodyFont));

                    document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                    }

                    #endregion

                }
            }
            else
            {
                 
                #region Create Member Table in Next Page
                int remMemCnt = 0;

                int[] clmwidths_23 = { 8, 3, 3, 3, 3 };
                PdfPTable tbl_6 = new PdfPTable(5);
                tbl_6.SetWidths(clmwidths_23);

                int[] clmwidths_24 = { 8, 3, 3, 3, 3 };
                PdfPTable tbl_7 = new PdfPTable(5);
                tbl_7.SetWidths(clmwidths_24);

                int page1cnt = 0;
                int page2cnt = 0;
                int page3cnt = 0;
                int page4cnt = 0;


                foreach (TRV_Policy_mem mem in members)
                {

                    remMemCnt++;
                    if (remMemCnt <= 32)
                    {
                        page1cnt++;
                        PdfPCell celli_2 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                        celli_2.HorizontalAlignment = 0;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);

                        celli1 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                        celli1.HorizontalAlignment = 1;
                        celli1.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli1);

                        celli1 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                        celli1.HorizontalAlignment = 1;
                        celli1.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli1);

                        DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                        celli1 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                        celli1.HorizontalAlignment = 1;
                        celli1.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli1);


                        celli1 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                        celli1.HorizontalAlignment = 2;
                        celli1.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli1);
                    }
                    else if (remMemCnt > 32)
                    {
                        page2cnt++;
                        if (page2cnt == 1)
                        {
                            #region Create Table Header for Remaining Members in Next Page
                            if (remMemCnt == 33)
                            {
                                document.Add(new Paragraph("Insureds' Details", bodyFont));
                                document.Add(tbl_4);
                            }
                             

                            document.NewPage();
                            document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));
                            document.Add(new Paragraph("Insureds' Details", bodyFont));

                            //int[] clmwidths_22 = { 8, 3, 3, 3, 3 };
                            //PdfPTable tbl_5 = new PdfPTable(5);
                            //tbl_5.SetWidths(clmwidths_22);

                            //tbl_4.WidthPercentage = 80; 
                            tbl_6.HorizontalAlignment = Element.ALIGN_LEFT;
                            tbl_6.SpacingBefore = 5;
                            tbl_6.SpacingAfter = 5;
                            tbl_6.DefaultCell.Border = 0;
                            tbl_6.TotalWidth = 450f;
                            tbl_6.LockedWidth = true;

                            PdfPCell celli_12 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);


                            celli_12 = new PdfPCell(new Phrase("Date of Birth", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Premium (USD)", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);


                            //document.Add(tbl_6);
                            #endregion
                        }

                        #region Table Data
                        tbl_6.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_6.SpacingBefore = 5;
                        tbl_6.SpacingAfter = 5;
                        tbl_6.DefaultCell.Border = 0;
                        tbl_6.TotalWidth = 450f;
                        tbl_6.LockedWidth = true;

                        PdfPCell celli_2 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                        celli_2.HorizontalAlignment = 0;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);

                        celli_2 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);

                        celli_2 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);

                        DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                        celli_2 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);


                        celli_2 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                        celli_2.HorizontalAlignment = 2;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);

                        if (page2cnt == 55)
                        {
                            page2cnt = 0;
                            document.Add(tbl_6);
                            tbl_6.DeleteBodyRows();
                        }
                        else if (page2cnt > 0 && remMemCnt == members.Count)
                        {
                            document.Add(tbl_6);
                        }
                        #endregion

                    }
                    /*
                    else if (remMemCnt > 32 && remMemCnt < 80)
                    {
                        page2cnt++;
                        if (remMemCnt == 33)
                        {
                           
                            #region Create Table Header for Remaining Members in Next Page

                            document.Add(new Paragraph("Insureds' Details", bodyFont));

                            document.Add(tbl_4);


                            document.NewPage();
                            document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));
                            document.Add(new Paragraph("Insureds' Details", bodyFont));

                            //int[] clmwidths_22 = { 8, 3, 3, 3, 3 };
                            //PdfPTable tbl_5 = new PdfPTable(5);
                            //tbl_5.SetWidths(clmwidths_22);

                            //tbl_4.WidthPercentage = 80; 
                            tbl_6.HorizontalAlignment = Element.ALIGN_LEFT;
                            tbl_6.SpacingBefore = 5;
                            tbl_6.SpacingAfter = 5;
                            tbl_6.DefaultCell.Border = 0;
                            tbl_6.TotalWidth = 450f;
                            tbl_6.LockedWidth = true;

                            PdfPCell celli_12 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);


                            celli_12 = new PdfPCell(new Phrase("Date of Birth", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Premium (USD)", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_6.AddCell(celli_12);


                            //document.Add(tbl_6);
                            #endregion

                        }

                        tbl_6.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_6.SpacingBefore = 5;
                        tbl_6.SpacingAfter = 5;
                        tbl_6.DefaultCell.Border = 0;
                        tbl_6.TotalWidth = 450f;
                        tbl_6.LockedWidth = true;

                        PdfPCell celli_2 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                        celli_2.HorizontalAlignment = 0;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);

                        celli_2 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);

                        celli_2 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);

                        DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                        celli_2 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);


                        celli_2 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                        celli_2.HorizontalAlignment = 2;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_6.AddCell(celli_2);


                    }
                    else if (remMemCnt > 79 && remMemCnt < 130)
                    {
                        page3cnt++;
                        if (remMemCnt == 80)
                        {

                            #region Create Table Header for Remaining Members in Next Page

                            document.Add(new Paragraph("Insureds' Details", bodyFont));

                            document.Add(tbl_6);


                            document.NewPage();
                            document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));
                            document.Add(new Paragraph("Insureds' Details", bodyFont));

                            //int[] clmwidths_22 = { 8, 3, 3, 3, 3 };
                            //PdfPTable tbl_5 = new PdfPTable(5);
                            //tbl_5.SetWidths(clmwidths_22);

                            //tbl_4.WidthPercentage = 80; 
                            tbl_7.HorizontalAlignment = Element.ALIGN_LEFT;
                            tbl_7.SpacingBefore = 5;
                            tbl_7.SpacingAfter = 5;
                            tbl_7.DefaultCell.Border = 0;
                            tbl_7.TotalWidth = 450f;
                            tbl_7.LockedWidth = true;

                            PdfPCell celli_12 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_7.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_7.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_7.AddCell(celli_12);


                            celli_12 = new PdfPCell(new Phrase("Date of Birth", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_7.AddCell(celli_12);

                            celli_12 = new PdfPCell(new Phrase("Premium (USD)", bodyFont4_white_bold));
                            celli_12.HorizontalAlignment = 1;
                            celli_12.BackgroundColor = new Color(180, 180, 180);
                            celli_12.BorderColor = new Color(200, 200, 200);
                            tbl_7.AddCell(celli_12);


                            //document.Add(tbl_7);
                            #endregion

                        }

                        tbl_7.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_7.SpacingBefore = 5;
                        tbl_7.SpacingAfter = 5;
                        tbl_7.DefaultCell.Border = 0;
                        tbl_7.TotalWidth = 450f;
                        tbl_7.LockedWidth = true;

                        PdfPCell celli_2 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                        celli_2.HorizontalAlignment = 0;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_7.AddCell(celli_2);

                        celli_2 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_7.AddCell(celli_2);

                        celli_2 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_7.AddCell(celli_2);

                        DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                        celli_2 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_7.AddCell(celli_2);


                        celli_2 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                        celli_2.HorizontalAlignment = 2;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_7.AddCell(celli_2);
                    }*/
                }

                //document.Add(new Paragraph("Insureds' Details", bodyFont));
                document.Add(tbl_7);


                int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                PdfPTable tbl_45 = new PdfPTable(5);
                tbl_45.SetWidths(clmwidths55);

                //tbl_4.WidthPercentage = 80; 
                tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl_45.SpacingBefore = 0;
                tbl_45.SpacingAfter = 1;
                tbl_45.DefaultCell.Border = 0;
                tbl_45.TotalWidth = 450f;
                tbl_45.LockedWidth = true;


                PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 0;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);


                cell5i = new PdfPCell(new Phrase(gtm.netPremium_usd.ToString("N2"), bodyFont4));
                cell5i.HorizontalAlignment = 2;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                document.Add(tbl_45);

                #endregion


                if ((page2cnt>0 && page2cnt <= 40) && members.Count==remMemCnt)
                {
                    #region Signature in Same Page

                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                    }

                    #endregion

                }
                else
                {
                    #region Signature in Next Page

                    document.NewPage();
                    ////document.Add(new Paragraph("\n", bodyFont_bold_sm)); 
                    document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));
                    document.Add(new Paragraph("Insureds' Details", bodyFont));

                    document.Add(new Paragraph(" ", bodyFont));

                    document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                    }

                    #endregion

                }

            }

            /*
            if (memCount > 0 && memCount <= 15)
            {


                foreach (TRV_Policy_mem mem in members)
                {

                    i++;


                    PdfPCell celli_1 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                    celli_1.HorizontalAlignment = 0;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    celli_1 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    celli_1 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                    celli_1 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);


                    celli_1 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                    celli_1.HorizontalAlignment = 2;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);
                }

                document.Add(new Paragraph("Insureds' Details", bodyFont));

                document.Add(tbl_4);

                int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                PdfPTable tbl_45 = new PdfPTable(5);
                tbl_45.SetWidths(clmwidths55);

                //tbl_4.WidthPercentage = 80; 
                tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl_45.SpacingBefore = 0;
                tbl_45.SpacingAfter = 1;
                tbl_45.DefaultCell.Border = 0;
                tbl_45.TotalWidth = 450f;
                tbl_45.LockedWidth = true;


                PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 0;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);


                cell5i = new PdfPCell(new Phrase(gtm.netPremium_usd.ToString("N2"), bodyFont4));
                cell5i.HorizontalAlignment = 2;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                document.Add(tbl_45);
                document.Add(new Paragraph(" ", bodyFont));

                document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                document.Add(new Paragraph(" ", bodyFont));
                document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                document.Add(new Paragraph(" ", bodyFont));
                document.Add(new Paragraph(" ", bodyFont));
                document.Add(new Paragraph(" ", bodyFont));
                document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                if (gtm.AGENT_CODE > 0)
                {
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                }

            }
            else
            {

                if (memCount <= 50)
                {
                    //document.NewPage();
                    ////document.Add(new Paragraph("\n", bodyFont_bold_sm)); 
                    document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));
                    document.Add(new Paragraph("Insureds' Details", bodyFont));


                    foreach (TRV_Policy_mem mem in members)
                    {
                        //mCount++; 
                        i++;

                        //PdfPCell celli = new PdfPCell(new Phrase("Member " + i, bodyFont4)); 
                        //celli.HorizontalAlignment = 0; 
                        //celli.BorderColor = new Color(200, 200, 200); 
                        //tbl_4.AddCell(celli); 

                        PdfPCell celli_2 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                        celli_2.HorizontalAlignment = 0;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);

                        //celli_2 = new PdfPCell(new Phrase(mem.memType_desc, bodyFont4)); 
                        //celli_2.HorizontalAlignment = 1; 
                        //celli_2.BorderColor = new Color(200, 200, 200); 
                        //tbl_4.AddCell(celli_2); 

                        celli_2 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);


                        celli_2 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);

                        DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                        celli_2 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);


                        celli_2 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                        celli_2.HorizontalAlignment = 2;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);

                    }

                    // document.Add(new Paragraph("Insureds' Details", bodyFont)); 

                    document.Add(tbl_4);


                    int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                    PdfPTable tbl_45 = new PdfPTable(5);
                    tbl_45.SetWidths(clmwidths55);

                    //tbl_4.WidthPercentage = 80; 
                    tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl_45.SpacingBefore = 0;
                    tbl_45.SpacingAfter = 1;
                    tbl_45.DefaultCell.Border = 0;
                    tbl_45.TotalWidth = 450f;
                    tbl_45.LockedWidth = true;


                    PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 0;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);


                    cell5i = new PdfPCell(new Phrase(gtm.netPremium_usd.ToString("N2"), bodyFont4));
                    cell5i.HorizontalAlignment = 2;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    document.Add(tbl_45);

                    //document.Add(new Paragraph(" ", bodyFont)); 


                    document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                    //document.Add(new Paragraph(" ", bodyFont)); 
                    document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                    }
                }
                else
                {
                    //int mRemain = memCount % 40; 
                    ////int remaCount = 0; 
                    ////for (int rCount = 0; rCount < remaCount + 40; rCount++) 
                    ////{ 
                    /// 


                    for (int rCount = 0; rCount < members.Count; rCount = rCount + 50)
                    {

                        PdfPTable tbl_memHe = new PdfPTable(5);
                        int[] clmwidthsMemHe = { 8, 3, 3, 3, 3 };



                        tbl_memHe.SetWidths(clmwidthsMemHe);

                        //tbl_4.WidthPercentage = 80; 
                        tbl_memHe.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_memHe.SpacingBefore = 10;
                        tbl_memHe.SpacingAfter = 0;
                        tbl_memHe.DefaultCell.Border = 0;
                        tbl_memHe.TotalWidth = 450f;
                        tbl_memHe.LockedWidth = true;

                        PdfPTable tbl_mem = new PdfPTable(5);
                        int[] clmwidthsMem = { 8, 3, 3, 3, 3 };



                        tbl_mem.SetWidths(clmwidthsMem);

                        //tbl_4.WidthPercentage = 80; 
                        tbl_mem.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_mem.SpacingBefore = 0;
                        tbl_mem.SpacingAfter = 5;
                        tbl_mem.DefaultCell.Border = 0;
                        tbl_mem.TotalWidth = 450f;
                        tbl_mem.LockedWidth = true;

                        for (int k = rCount; k < members.Count; k = k + 1)
                        {
                            i++;

                            if (i % 50 == 0)
                            {
                                PdfPCell celli_3 = new PdfPCell(new Phrase(members[k].title + " " + members[k].name, bodyFont4));
                                celli_3.HorizontalAlignment = 0;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);

                                //celli_3 = new PdfPCell(new Phrase(members[k].memType_desc, bodyFont4)); 
                                //celli_3.HorizontalAlignment = 1; 
                                //celli_3.BorderColor = new Color(200, 200, 200); 
                                //tbl_mem.AddCell(celli_3); 

                                celli_3 = new PdfPCell(new Phrase((members[k].gender.Trim().Equals("M") ? "Male" : (members[k].gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                                celli_3.HorizontalAlignment = 1;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);


                                celli_3 = new PdfPCell(new Phrase(members[k].ppno, bodyFont4));
                                celli_3.HorizontalAlignment = 1;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);

                                DateTime dobDate = DateTime.ParseExact(members[k].dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                                celli_3 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                                celli_3.HorizontalAlignment = 1;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);


                                celli_3 = new PdfPCell(new Phrase(members[k].base_amount_usd.ToString("N2"), bodyFont4));
                                celli_3.HorizontalAlignment = 2;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);
                                break;
                            }
                            else
                            {

                                //////PdfPCell celli = new PdfPCell(new Phrase("Member " + i, bodyFont4)); 
                                //////celli.HorizontalAlignment = 0; 
                                //////celli.BorderColor = new Color(200, 200, 200); 
                                //////tbl_4.AddCell(celli); 



                                PdfPCell celli_4 = new PdfPCell(new Phrase(members[k].title + " " + members[k].name, bodyFont4));
                                celli_4.HorizontalAlignment = 0;
                                celli_4.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_4);

                                //celli_4 = new PdfPCell(new Phrase(members[k].memType_desc, bodyFont4)); 
                                //celli_4.HorizontalAlignment = 1; 
                                //celli_4.BorderColor = new Color(200, 200, 200); 
                                //tbl_mem.AddCell(celli_4); 

                                celli = new PdfPCell(new Phrase((members[k].gender.Trim().Equals("M") ? "Male" : (members[k].gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                                celli.HorizontalAlignment = 1;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);


                                celli_4 = new PdfPCell(new Phrase(members[k].ppno, bodyFont4));
                                celli_4.HorizontalAlignment = 1;
                                celli_4.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_4);


                                DateTime dobDate = DateTime.ParseExact(members[k].dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);


                                celli_4 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                                celli_4.HorizontalAlignment = 1;
                                celli_4.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_4);


                                celli_4 = new PdfPCell(new Phrase(members[k].base_amount_usd.ToString("N2"), bodyFont4));
                                celli_4.HorizontalAlignment = 2;
                                celli_4.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_4);

                            }

                            //} 


                        }

                        if (i <= members.Count + 1)
                        {
                            document.NewPage();
                            // document.Add(new Paragraph("\n", bodyFont_bold_sm)); 
                            document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));

                            document.Add(new Paragraph("Insureds' Details", bodyFont));

                            PdfPCell celli2 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            //celli2 = new PdfPCell(new Phrase("Category", bodyFont4_white_bold)); 
                            //celli2.HorizontalAlignment = 1; 
                            //celli2.BackgroundColor = new Color(180, 180, 180); 
                            //celli2.BorderColor = new Color(200, 200, 200); 
                            //tbl_memHe.AddCell(celli2); 

                            celli2 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            celli2 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);



                            celli2 = new PdfPCell(new Phrase("Date of Birth", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            celli2 = new PdfPCell(new Phrase("Premium (USD)", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            document.Add(tbl_memHe);
                            document.Add(tbl_mem);
                            //int mCount = 0; 
                        }

                    }

                    int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                    PdfPTable tbl_45 = new PdfPTable(5);
                    tbl_45.SetWidths(clmwidths55);

                    //tbl_4.WidthPercentage = 80; 
                    tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl_45.SpacingBefore = 0;
                    tbl_45.SpacingAfter = 2;
                    tbl_45.DefaultCell.Border = 0;
                    tbl_45.TotalWidth = 450f;
                    tbl_45.LockedWidth = true;


                    PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 0;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);


                    cell5i = new PdfPCell(new Phrase(gtm.netPremium_usd.ToString("N2"), bodyFont4));
                    cell5i.HorizontalAlignment = 2;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    document.Add(tbl_45);

                    //document.Add(new Paragraph(" ", bodyFont)); 

                    document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                    //document.Add(new Paragraph(" ", bodyFont)); 
                    document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    //document.Add(new Paragraph(" ", bodyFont)); 
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                    }
                }
            }*/

        }



        document.NewPage();

        #region claim sheet


        //document.Add(new Paragraph("\n", bodyFont)); 
        Chunk titchTitle = new Chunk("BENEFITS SCHEDULE", FontFactory.GetFont("Times New Roman", 10, Font.BOLD, Color.BLACK));
        titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph titletitle = new Paragraph(titchTitle);
        titletitle.SetAlignment("Center");
        document.Add(titletitle);

        int[] clmwidth1 = { 4, 1, 8 };

        PdfPTable tbl_Quote = new PdfPTable(3);

        tbl_Quote.SetWidths(clmwidth1);

        tbl_Quote.WidthPercentage = 100;
        tbl_Quote.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_Quote.SpacingBefore = 5;
        tbl_Quote.SpacingAfter = 5;
        tbl_Quote.DefaultCell.Border = 0;

        tbl_Quote.AddCell(new Phrase("Scheme", bodyFont));
        tbl_Quote.AddCell(new Phrase(": ", bodyFont));
        tbl_Quote.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont));

        tbl_Quote.AddCell(new Phrase("Policy Number", bodyFont));
        tbl_Quote.AddCell(new Phrase(": ", bodyFont));
        tbl_Quote.AddCell(new Phrase(poliID, bodyFont));

        //document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont)); 

        tbl_Quote.AddCell(new Phrase("Currency Type", bodyFont));
        tbl_Quote.AddCell(new Phrase(": ", bodyFont));
        tbl_Quote.AddCell(new Phrase("USD", bodyFont));

        //tbl_Quote.AddCell(new Phrase("  ", bodyFont)); 
        //tbl_Quote.AddCell(new Phrase("  ", bodyFont)); 
        //tbl_Quote.AddCell(new Phrase("  ", bodyFont)); 

        document.Add(tbl_Quote);
        TRV_Benefits gtBen = new TRV_Benefits(gtm.plan);

        if (gtBen.DTproduct.Rows.Count > 0)
        {
            int[] clmwidths100 = { 10, 4, 4 };

            PdfPTable tbl_header = new PdfPTable(3);

            tbl_header.SetWidths(clmwidths100);

            tbl_header.WidthPercentage = 100;
            tbl_header.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl_header.SpacingBefore = 2;
            tbl_header.SpacingAfter = 0;
            tbl_header.DefaultCell.Border = 0;

            //tbl_header.AddCell(new Phrase("BENEFIT", boldTableFont)); 
            //tbl_header.AddCell(new Phrase("SUM_INSURED", boldTableFont)); 
            //tbl_header.AddCell(new Phrase("EXCESS", boldTableFont)); 

            PdfPCell cellHead1 = new PdfPCell(new Phrase("Summary of Benefits", boldTableFont));

            cellHead1.HorizontalAlignment = 1;
            cellHead1.BorderWidth = 0f;
            cellHead1.Padding = 2;
            cellHead1.BorderColor = new Color(180, 180, 180);
            cellHead1.BorderWidthLeft = 0.5f;
            cellHead1.BorderWidthTop = 0.5f;

            tbl_header.AddCell(cellHead1);

            PdfPCell cellHead2 = new PdfPCell(new Phrase("Sum_Insured", boldTableFont));
            cellHead2.HorizontalAlignment = 2;
            cellHead2.BorderWidth = 0f;
            cellHead2.Padding = 2;
            cellHead2.BorderColor = new Color(180, 180, 180);
            cellHead2.BorderWidthTop = 0.5f;
            cellHead2.BorderWidthLeft = 0.5f;

            tbl_header.AddCell(cellHead2);

            cellHead2 = new PdfPCell(new Phrase("Deductible", boldTableFont));
            cellHead2.HorizontalAlignment = 2;
            cellHead2.BorderWidth = 0f;
            cellHead2.Padding = 2;
            cellHead2.BorderColor = new Color(180, 180, 180);
            cellHead2.BorderWidthTop = 0.5f;
            cellHead2.BorderWidthRight = 0.5f;


            tbl_header.AddCell(cellHead2);

            document.Add(tbl_header);

            int[] clmwidths1115 = { 2, 8, 4, 4 };

            PdfPTable tbl_15 = new PdfPTable(4);


            tbl_15.SetWidths(clmwidths1115);

            tbl_15.WidthPercentage = 100;
            //tbl_15.HorizontalAlignment = Element.ALIGN_CENTER; 
            tbl_15.HorizontalAlignment = Element.ALIGN_CENTER;
            // tbl_15.SpacingBefore = 5; 
            tbl_15.SpacingAfter = 5;
            tbl_15.DefaultCell.Border = 0;



            //tbl_1.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont)); 
            string HeaderCode = "";
            int count = 0;
            for (int k = 0; k < gtBen.DTproduct.Rows.Count; k++)
            {
                //5 % 4 
                string code = gtBen.DTproduct.Rows[k]["code"].ToString();
                if (code.Equals("1000") || code.Equals("2000") || code.Equals("3000") || code.Equals("4000") || code.Equals("5000") || code.Equals("6000"))
                {
                    //tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString().ToUpper(), bodyFont_bold)); 
                    //tbl_15.AddCell(new Phrase(" ", bodyFont_bold)); 
                    //tbl_15.AddCell(new Phrase(" ", bodyFont_bold)); 
                    count = 0;
                    string setCode = code.TrimEnd('0');
                    HeaderCode = setCode;
                    PdfPCell cellCode = new PdfPCell(new Phrase(setCode.ToString(), bodyFont));
                    cellCode.HorizontalAlignment = 0;
                    cellCode.BorderColor = new Color(180, 180, 180);
                    cellCode.BackgroundColor = new Color(180, 180, 180);
                    cellCode.BorderWidth = 0f;
                    cellCode.Padding = 2;
                    cellCode.BorderWidthTop = 0.5f;
                    cellCode.BorderWidthLeft = 0.5f;

                    tbl_15.AddCell(cellCode);

                    PdfPCell cellTBA = new PdfPCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString(), bodyFont));
                    cellTBA.HorizontalAlignment = 0;
                    cellTBA.BorderColor = new Color(180, 180, 180);
                    cellTBA.BackgroundColor = new Color(180, 180, 180);
                    cellTBA.BorderWidth = 0f;
                    cellTBA.Padding = 2;
                    cellTBA.BorderWidthTop = 0.5f;
                    cellTBA.BorderWidthLeft = 0.5f;

                    tbl_15.AddCell(cellTBA);

                    PdfPCell cellTBA1 = new PdfPCell(new Phrase(" ", bodyFont));
                    cellTBA1.HorizontalAlignment = 2;
                    cellTBA1.BorderColor = new Color(180, 180, 180);
                    cellTBA1.BackgroundColor = new Color(180, 180, 180);
                    cellTBA1.BorderWidth = 0f;
                    cellTBA1.Padding = 2;
                    cellTBA1.BorderWidthTop = 0.5f;
                    cellTBA1.BorderWidthLeft = 0.5f;

                    tbl_15.AddCell(cellTBA1);

                    cellTBA1 = new PdfPCell(new Phrase(" ", bodyFont));
                    cellTBA1.HorizontalAlignment = 2;
                    cellTBA1.BorderColor = new Color(180, 180, 180);
                    cellTBA1.BackgroundColor = new Color(180, 180, 180);
                    cellTBA1.BorderWidth = 0f;
                    cellTBA1.Padding = 2;
                    cellTBA1.BorderWidthTop = 0.5f;
                    cellTBA1.BorderWidthRight = 0.5f;

                    tbl_15.AddCell(cellTBA1);
                }
                else
                {
                    //code = gtBen.DTproduct.Rows[k]["code"].ToString(); 
                    string sum_insured = gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString().Trim();
                    if (!sum_insured.Equals("NA"))
                    {
                        //ben = gtBen.DTproduct.Rows[k]["benefit"].ToString(); 
                        //sum_ins = gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString(); 
                        //excess = gtBen.DTproduct.Rows[k]["EXCESS"].ToString(); 
                        //tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString(), bodyFont)); 

                        //tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString(), bodyFont)); 
                        //tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["EXCESS"].ToString(), bodyFont)); 
                        ////count++; 
                        ////string grdcode = HeaderCode + "." + count.ToString(); 
                        /// 
                        string grdcode = "";
                        if (gtBen.DTproduct.Rows[k]["benefit"].ToString().Trim() == "Single article limit" || gtBen.DTproduct.Rows[k]["benefit"].ToString().Trim() == "Single article limit for Home Safety cover")
                        {
                            grdcode = HeaderCode + "." + count.ToString() + " A ";
                        }
                        else
                        {
                            count++;
                            grdcode = HeaderCode + "." + count.ToString();
                        }

                        PdfPCell cellCode = new PdfPCell(new Phrase(grdcode.ToString(), bodyFont));
                        cellCode.HorizontalAlignment = 0;
                        cellCode.BorderColor = new Color(180, 180, 180);
                        //cellCode.BackgroundColor = new Color(180, 180, 180); 
                        cellCode.BorderWidth = 0f;
                        cellCode.Padding = 2;
                        cellCode.BorderWidthLeft = 0.5f;

                        tbl_15.AddCell(cellCode);

                        PdfPCell cellTBA = new PdfPCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString(), bodyFont));
                        cellTBA.HorizontalAlignment = 0;
                        cellTBA.BorderColor = new Color(180, 180, 180);
                        cellTBA.BorderWidth = 0f;
                        cellTBA.Padding = 2;
                        cellTBA.BorderWidthLeft = 0.5f;

                        tbl_15.AddCell(cellTBA);

                        PdfPCell cellTBA1 = new PdfPCell(new Phrase(gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString(), bodyFont));
                        cellTBA1.HorizontalAlignment = 2;
                        cellTBA1.BorderWidth = 0f;
                        cellTBA1.BorderColor = new Color(180, 180, 180);
                        cellTBA1.Padding = 2;
                        cellTBA1.BorderWidthLeft = 0.5f;

                        tbl_15.AddCell(cellTBA1);

                        cellTBA1 = new PdfPCell(new Phrase(gtBen.DTproduct.Rows[k]["EXCESS"].ToString(), bodyFont));
                        cellTBA1.HorizontalAlignment = 2;
                        cellTBA1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellTBA1.BorderColor = new Color(180, 180, 180);
                        cellTBA1.BorderWidth = 0f;
                        cellTBA1.Padding = 2;
                        cellTBA1.BorderWidthRight = 0.5f;

                        tbl_15.AddCell(cellTBA1);
                    }


                }
            }
            //tbl_15.AddCell(gtBen.DTproduct.ToString()); 
            //tbl_15.AddCell("Row 1, Col 2"); 
            //tbl_15.AddCell("Row 1, Col 3"); 

            document.Add(tbl_15);
        }
        
        int[] clmwidthsReInsu = { 1 };

        PdfPTable tbl_reIns = new PdfPTable(1);

        tbl_reIns.SetWidths(clmwidthsReInsu);

        //tbl_2.WidthPercentage = 0.50f;
        tbl_reIns.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_reIns.SpacingBefore = 5;
        tbl_reIns.SpacingAfter = 0;
        tbl_reIns.DefaultCell.Border = 0;
        ////tbl_2.WidthPercentage = 40;
        tbl_reIns.TotalWidth = 450f;
        tbl_reIns.LockedWidth = true;

        //TRV_TPA_Company tpaCom = new TRV_TPA_Company(System.DateTime.Now.Date);
        //tbl_12.AddCell(new Phrase("Sri Lanka", bodyFont)); 
        DateTime EntryDate = DateTime.ParseExact(gtm.Enrty_Date, "yyyy/MM/dd", CultureInfo.InvariantCulture);
        TRV_TPA_Company tpaCom = new TRV_TPA_Company(EntryDate);

        if (!String.IsNullOrEmpty(tpaCom.COMPANY))
        {
            tbl_reIns.AddCell(new Phrase("Contact Details of Third Party Assistance service -:", underlineAndBoldFont));
            if (!String.IsNullOrEmpty(tpaCom.COMPANY))
            {
                tbl_reIns.AddCell(new Phrase(tpaCom.COMPANY, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_1))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_1 + " " + tpaCom.ADDRESS_2, bodyFont_bold));
            }
            /* if (!String.IsNullOrEmpty(tpaCom.ADDRESS_2))
             {
                 tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_2  , bodyFont_bold));
             }*/
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_3))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_3, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_4))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_4, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_5))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_5, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.TELE_NO))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.TELE_NO + "    " + tpaCom.EMAIL, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.FAX_NO))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.FAX_NO, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.TOLL_FREE))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.TOLL_FREE, bodyFont_bold));
            }
            /*
             if (!String.IsNullOrEmpty(tpaCom.EMAIL))
             {
                 tbl_reIns.AddCell(new Phrase(tpaCom.EMAIL, bodyFont_bold));
             }*/
            if (!String.IsNullOrEmpty(tpaCom.SLIC_HELP))
            {
                tbl_reIns.AddCell(new Paragraph("\n", bodyFont));
                tbl_reIns.AddCell(new Phrase(tpaCom.SLIC_HELP, bodyFont));
            }
        }
        document.Add(tbl_reIns);


        DateTime CurrentDate = DateTime.ParseExact("2020/02/03", "yyyy/MM/dd", CultureInfo.InvariantCulture);

        DateTime rateChangeDate = DateTime.ParseExact("2022/01/01", "yyyy/MM/dd", CultureInfo.InvariantCulture);

        if (reprint)
        {
            if (EntryDate < rateChangeDate)
            {
                Chunk specNotes = new Chunk("  This Insurance is backed by Munich Re", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
                //titchTitle.SetUnderline(0.5f, -1.5f);
                Paragraph specNote = new Paragraph(specNotes);
                document.Add(specNote);
            }
        }

        document.Add(new Paragraph("\n", bodyFont));
        Chunk spec = new Chunk("Special notes :", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph specP = new Paragraph(spec);
        document.Add(specP);

        List lstSpeNodes = new List(List.UNORDERED, 10f);

        lstSpeNodes.SetListSymbol("\u2022");

        lstSpeNodes.IndentationLeft = 20f;

        ListItem itemSpeNo1 = new ListItem("Pre - existing Medical Conditions are excluded", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        itemSpeNo1.SetAlignment("ALIGN_JUSTIFIED");

        lstSpeNodes.Add(itemSpeNo1);

        if (reprint)
        {
            if (EntryDate >= CurrentDate)
            {
                if (EntryDate < rateChangeDate)
                {
                    
                    ListItem itemSpeNo2 = new ListItem("Any claim arising directly or indirectly as a result of the Corona Novel virus/outbreak is excluded", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
                    itemSpeNo2.SetAlignment("ALIGN_JUSTIFIED");

                    lstSpeNodes.Add(itemSpeNo2);
                }
                else
                {
                    ListItem itemSpeNo3 = new ListItem("The policy includes Medical expenses incurred overseas and Trip postponement/ Cancellation owing to testing positive for COVID-19 subject to terms, conditions, and exclusions under the policy.-(Insured should have obtained booster dose vaccine for COVID-19 OR PCR obtained within 72 hours prior to departure, testing negative).", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
                    itemSpeNo3.SetAlignment("ALIGN_JUSTIFIED");

                    lstSpeNodes.Add(itemSpeNo3);

                    ListItem itemSpeNo4 = new ListItem("Countries of citizenship are not covered.", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
                    itemSpeNo4.SetAlignment("ALIGN_JUSTIFIED");

                    lstSpeNodes.Add(itemSpeNo4);

                }
            }
        }
        else
        {
            ListItem itemSpeNo3 = new ListItem("The policy includes Medical expenses incurred overseas and Trip postponement/ Cancellation owing to testing positive for COVID-19 subject to terms, conditions, and exclusions under the policy.-(Insured should have obtained booster dose vaccine for COVID-19 OR PCR obtained within 72 hours prior to departure, testing negative).", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
            itemSpeNo3.SetAlignment("ALIGN_JUSTIFIED");

            lstSpeNodes.Add(itemSpeNo3);


            ListItem itemSpeNo4 = new ListItem("Countries of citizenship are not covered.", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
            itemSpeNo4.SetAlignment("ALIGN_JUSTIFIED");

            lstSpeNodes.Add(itemSpeNo4);

        }

        document.Add(lstSpeNodes);

        #endregion

        if (EntryDate >= rateChangeDate)
        {

            document.NewPage();

            document.Add(new Paragraph("\n", bodyFont));
            //document.Add(new Paragraph("\n", bodyFont));

            Chunk tiEndTitle = new Chunk("SRI LANKA INSURANCE CORPORATION GENERAL LTD.", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
            //tiEndTitle.SetUnderline(0.5f, -1.5f);
            Paragraph tititleEndo = new Paragraph(tiEndTitle);
            tititleEndo.SetAlignment("Center");
            document.Add(tititleEndo);

            Chunk tiEndSubTitle = new Chunk("ATTACHING TO AND FORMING PART OF POLICY NUMBER :  " + gtm.polNO, FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
            // tiEndSubTitle.SetUnderline(0.5f, -1.5f);
            Paragraph tititleEndoSub = new Paragraph(tiEndSubTitle);
            tititleEndoSub.SetAlignment("Center");
            document.Add(tititleEndoSub);

            document.Add(new Paragraph("\n", bodyFont));

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP1 = new Chunk("It is hereby noted and agreed that the health benefit section of the insured’s policy has been extended to provide the necessary and reasonable cost for treatment of COVID-19 up to the limit as stated in the schedule subject to the following terms, conditions and limits:", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP1 = new Paragraph(specEndoP1);
            endoP1.SetAlignment("ALIGN_JUSTIFIED");
            document.Add(endoP1);

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP2 = new Chunk("The insured is covered for COVID-19 on testing positive, only for medically necessary OPD/ Outdoor treatments (initial doctor consultation charges and related drugs for 14 days only) and/or Medically necessary hospital admission.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP2 = new Paragraph(specEndoP2);
            endoP2.SetAlignment("ALIGN_JUSTIFIED");
            document.Add(endoP2);

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP3 = new Chunk("The insured must comply with all local laws and rules of social distancing, personal protective equipment (PPE) and restricted activities as advised and/ or mandated by the Sri Lanka, the insured’s destination and all countries the insured passes through during the course of the Insured’s trip;", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP3 = new Paragraph(specEndoP3);
            endoP3.SetAlignment("ALIGN_JUSTIFIED");
            document.Add(endoP3);

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP4 = new Chunk("Above is subject to following exclusions;", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP4 = new Paragraph(specEndoP4);
            endoP4.SetAlignment("ALIGN_JUSTIFIED");
            document.Add(endoP4);

            List list = new List(List.ORDERED, 20f);

            list.SetListSymbol("\u2022");

            list.IndentationLeft = 20f;

            ListItem item1 = new ListItem("COVID-19 medical expenses claims if the insured is not vaccinated with booster dose of the COVID-19 vaccination OR unable to provide a negative pre-departure COVID-19 diagnostic test such as PCR obtained from a testing organization approved by government or local public health authorities, and certification of this approval from the testing organization no earlier than within 72 hours before departing for his trip.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            item1.SetAlignment("ALIGN_JUSTIFIED");

            list.Add(item1);

            ListItem item2 = new ListItem("Claims related to mandatory COVID-19 diagnostic tests that the insured person is required to take for the trip, such as pre-departure tests and post-arrival tests at destination and after return to Sri Lanka.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            item2.SetAlignment("ALIGN_JUSTIFIED");

            list.Add(item2);

            ListItem item3 = new ListItem("Claims if the policy has not been purchased before the insured has left Sri Lanka.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            item3.SetAlignment("ALIGN_JUSTIFIED");

            list.Add(item3);

            ListItem item4 = new ListItem("Claims in case the insured person is less than 30 days old.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            item4.SetAlignment("ALIGN_JUSTIFIED");

            list.Add(item4);

            ListItem item5 = new ListItem("The Insured is not covered for costs and fees directly or indirectly incurred as a consequence of isolation and/ or quarantine. Such costs and fees would include but not be limited to: “fees and expenses for diagnosis, medical visits, hospitalization, lodging, attendant care, and any other fees or expenses associated directly or indirectly with the isolation and/ or quarantine and that are not as a result of a emergency medical treatment for COVID-19”;.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            item5.SetAlignment("ALIGN_JUSTIFIED");

            list.Add(item5);

            ListItem item6 = new ListItem("The Company will NOT be responsible for any costs or expenses if the insured suffers from any serious or chronic pre-existing health issues. Serious or chronic pre-existing health issues are conditions that are likely to increase the severity of COVID-19 infection, including but not limited to chronic heart, kidney or liver disease, diabetes and chronic lung and respiratory illnesses.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            item6.SetAlignment("ALIGN_JUSTIFIED");

            list.Add(item6);

            ListItem item7 = new ListItem("The company will NOT be responsible for post COVID-19 any sickness situations and related testing.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            item7.SetAlignment("ALIGN_JUSTIFIED");

            list.Add(item7);

            //document.Add(paragraph);
            document.Add(list);

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP5 = new Chunk("Further, it is hereby noted and agreed that the events for claiming under Trip Cancellation/ Postponement under Travel Inconvenience Benefit section of the policy has been extended to include policy holder and/or Travel companions and/or Family member/s testing positive for COVID-19 within 30 days, prior to travel start date.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP5 = new Paragraph(specEndoP5);
            document.Add(endoP5);

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP6 = new Chunk("Above is subject to following exclusions;", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP6 = new Paragraph(specEndoP6);
            document.Add(endoP6);

            List listEx = new List(List.ORDERED, 20f);

            listEx.SetListSymbol("\u2022");

            listEx.IndentationLeft = 20f;

            ListItem itemEx1 = new ListItem("Trip cancellation solely due to epidemic or pandemic related travel advisories issued by governments, health authorities or the World Health Organization, by or for destination country or origin country.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            itemEx1.SetAlignment("ALIGN_JUSTIFIED");

            listEx.Add(itemEx1);

            ListItem itemEx2 = new ListItem("Trip cancellation resulting solely from border closures, quarantine or other government orders, advisories, regulations or directives.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            itemEx2.SetAlignment("ALIGN_JUSTIFIED");

            listEx.Add(itemEx2);

            ListItem itemEx3 = new ListItem("Trip cancellations if you cancel your trip because of disinclination to travel, change of mind or fear of traveling.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            itemEx3.SetAlignment("ALIGN_JUSTIFIED");

            listEx.Add(itemEx3);

            ListItem itemEx4 = new ListItem("Trip cancellation if the hotel, travel agent or any other provider of travel and/ or accommodation has offered a voucher or credit or re-booking of the Trip for cancellation refund or compensation.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            itemEx4.SetAlignment("ALIGN_JUSTIFIED");

            listEx.Add(itemEx4);

            ListItem itemEx5 = new ListItem("Traveling against a medical practitioner’s or doctor’s advise, or any claim arising from you acting in a way that goes against the advise of a medical practitioner or doctor.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            itemEx5.SetAlignment("ALIGN_JUSTIFIED");

            listEx.Add(itemEx5);

            ListItem itemEx6 = new ListItem("Refundable bookings.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            itemEx6.SetAlignment("ALIGN_JUSTIFIED");

            listEx.Add(itemEx6);

            //document.Add(paragraph);
            document.Add(listEx);

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP7 = new Chunk("This endorsement should be read in conjunction with the policy document. All other terms and conditions remain unchanged", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP7 = new Paragraph(specEndoP7);
            document.Add(endoP7);

            document.Add(new Paragraph("\n", bodyFont));
            document.Add(new Paragraph("\n", bodyFont));

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP8 = new Chunk("_ _ _ _ _ _ _ _ _ _ _ _ _ _", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP8 = new Paragraph(specEndoP8);
            document.Add(endoP8);

            //document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP9 = new Chunk("Authorized Officer", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP9 = new Paragraph(specEndoP9);
            document.Add(endoP9);

            //document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP10 = new Chunk("Sri Lanka Insurance Corporation General (Ltd).", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP10 = new Paragraph(specEndoP10);
            document.Add(endoP10);


        }

        #endregion

        document.Close();
        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=TRV_{0}.pdf", "Policy_Document"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
    }
    

    #region Policy Schedule Old
    public void print_policy_old(string poliID, string epf, string ip, bool reprint)
    {
        TRV_Policy_mast gtm = new TRV_Policy_mast(poliID, "TPI");//,"TPI" 

        List<TRV_Policy_mem> GT_mem = gtm.members;
        string AgentName = "";
        TRV_Proposal prop = new TRV_Proposal();
        prop.getAgtName(gtm.AGENT_CODE, out AgentName);

        Document document = new Document(PageSize.A4, 50, 50, 30, 5);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        if (reprint)
            //if (gtm.AGENT_CODE > 0)
            //   phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf + " Agency Code : " + gtm.AGENT_CODE.ToString(), new Font(Font.COURIER, 8));
            // else
            phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));
        else
            // if (gtm.AGENT_CODE > 0)
            // phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf + " Agency Code : " + gtm.AGENT_CODE.ToString(), new Font(Font.COURIER, 8));
            // else
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

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_white_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD, new Color(255, 255, 255));
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font underlineAndBoldFont = FontFactory.GetFont("Times New Roman", 9, Font.UNDERLINE | Font.BOLD);



        int rowCount = 0;
        string root = System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/slic_gen_Logo.png");




        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);

        logo.ScalePercent(25f);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);


        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/Gi_Watermark.gif"));
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark); 

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        //document.Add(logo); 


        document.Add(new Paragraph("\n\n\n\n\n\n\n", bodyFont));
        Chunk titch1 = new Chunk("TRAVEL PROTECT INSURANCE\n", boldTableFont);
        //titch1.SetUnderline(0.5f, -1.5f); 
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        Chunk head1 = new Chunk("POLICY SCHEDULE\n", boldTableFont);
        //titch1.SetUnderline(0.5f, -1.5f); 
        Paragraph titleHead = new Paragraph(head1);
        titleHead.SetAlignment("Center");
        document.Add(titleHead);

        #region visiting countries
        /////////////////////////////////////////////// 

        Country coun = new Country();
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

        string[] ctryCode = gtm.destination.Split(',');

        if (gtm.arrDest.Count > 1)
        {

            for (int i = 0; i < gtm.arrDest.Count; i++)
            {
                // tbl_12.AddCell(new Phrase(gtm.arrDest[i].ToString() + "  " + (coun.check_schengen(ctryCode[i].ToString()) ? "(Schengen state)" : ""), bodyFont)); 

                if (i == 0)
                {
                    celli = new PdfPCell(new Phrase(gtm.get_country_name("LK"), bodyFont));
                    celli.HorizontalAlignment = 0;
                    celli.BorderColor = new Color(200, 200, 200);
                    tbl_3.AddCell(celli);

                    celli = new PdfPCell(new Phrase(gtm.arrDest[i].ToString() + "  " + (coun.check_schengen(ctryCode[i].ToString()) ? "(Schengen State)" : ""), bodyFont));

                    celli.HorizontalAlignment = 0;
                    celli.BorderColor = new Color(200, 200, 200);
                    tbl_3.AddCell(celli);
                }
                else
                {
                    celli = new PdfPCell(new Phrase("", bodyFont));
                    celli.HorizontalAlignment = 0;
                    celli.BorderColor = new Color(200, 200, 200);
                    tbl_3.AddCell(celli);

                    celli = new PdfPCell(new Phrase(gtm.arrDest[i].ToString() + "  " + (coun.check_schengen(ctryCode[i].ToString()) ? "(Schengen State)" : ""), bodyFont));

                    celli.HorizontalAlignment = 0;
                    celli.BorderColor = new Color(200, 200, 200);
                    tbl_3.AddCell(celli);
                }
            }
        }

        else
        {
            celli = new PdfPCell(new Phrase(gtm.get_country_name("LK"), bodyFont));
            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);

            celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.destination) + "  " + (coun.check_schengen(gtm.destination) ? "(Schengen State)" : ""), bodyFont));

            celli.HorizontalAlignment = 0;
            celli.BorderColor = new Color(200, 200, 200);
            tbl_3.AddCell(celli);


            if (!String.IsNullOrEmpty(gtm.visitCntry1))
            {

                celli = new PdfPCell(new Phrase("", bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);

                celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry1) + "  " + (coun.check_schengen(gtm.visitCntry1) ? "(Schengen State)" : ""), bodyFont));

                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);
            }



            if (!String.IsNullOrEmpty(gtm.visitCntry2))
            {
                celli = new PdfPCell(new Phrase("", bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);

                celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry2) + "  " + (coun.check_schengen(gtm.visitCntry2) ? "(Schengen State)" : ""), bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);
            }
            if (!String.IsNullOrEmpty(gtm.visitCntry3))
            {
                celli = new PdfPCell(new Phrase("", bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);

                celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry3) + "  " + (coun.check_schengen(gtm.visitCntry3) ? "(Schengen State)" : ""), bodyFont));

                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);
            }
            if (!String.IsNullOrEmpty(gtm.visitCntry4))
            {
                celli = new PdfPCell(new Phrase("", bodyFont));
                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);

                celli = new PdfPCell(new Phrase(gtm.get_country_name(gtm.visitCntry4) + "  " + (coun.check_schengen(gtm.visitCntry4) ? "(Schengen State)" : ""), bodyFont));

                celli.HorizontalAlignment = 0;
                celli.BorderColor = new Color(200, 200, 200);
                tbl_3.AddCell(celli);
            }
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
        tbl_1.AddCell(new Phrase(poliID, bodyFont));



        tbl_1.AddCell(new Phrase("Scheme", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont));

        tbl_1.AddCell(new Phrase("Date of Departure", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        DateTime dDate = DateTime.ParseExact(gtm.departDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

        tbl_1.AddCell(new Phrase(dDate.ToString("dd/MM/yyyy"), bodyFont));

        tbl_1.AddCell(new Phrase("Date of Returning", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));

        DateTime rDate = DateTime.ParseExact(gtm.returnDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);


        tbl_1.AddCell(new Phrase(rDate.ToString("dd/MM/yyyy"), bodyFont));


        tbl_1.AddCell(new Phrase("Insured's Name", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.title + " " + gtm.fullName, bodyFont));


        tbl_1.AddCell(new Phrase("Address of the insured", bodyFont));
        tbl_1.AddCell(new Phrase(": ", bodyFont));
        tbl_1.AddCell(new Phrase(gtm.getFullAddress2(), bodyFont));

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

        document.Add(tbl_1);

        Chunk titch12 = new Chunk("For Benefits/Exclusions, Please refer schedule overleaf\n\n", bodyFont_bold);
        //titch1.SetUnderline(0.5f, -1.5f); 
        Paragraph titleLine12 = new Paragraph(titch12);
        //titleLine2.SetAlignment("Center"); 
        document.Add(titleLine12);

        List<TRV_Policy_mem> members = gtm.members.OrderBy(s => int.Parse(s.member_id.Split('_')[1])).ToList();


        int memCount = members.Count;
        PdfPTable tbl_4 = new PdfPTable(5);

        if (members != null)
        {

            //document.Add(new Paragraph("\nInsureds' Details (Currency type : USD)", bodyFont_bold)); 

            int[] clmwidths = { 8, 3, 3, 3, 3 };



            tbl_4.SetWidths(clmwidths);

            //tbl_4.WidthPercentage = 80; 
            tbl_4.HorizontalAlignment = Element.ALIGN_LEFT;
            tbl_4.SpacingBefore = 10;
            tbl_4.SpacingAfter = 10;
            tbl_4.DefaultCell.Border = 0;
            tbl_4.TotalWidth = 450f;
            tbl_4.LockedWidth = true;



            PdfPCell celli1 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);


            celli1 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
            celli1.HorizontalAlignment = 1;
            celli1.BackgroundColor = new Color(180, 180, 180);
            celli1.BorderColor = new Color(200, 200, 200);
            tbl_4.AddCell(celli1);

            celli1 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
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



            if (memCount > 0 && memCount <= 15)
            {


                foreach (TRV_Policy_mem mem in members)
                {

                    i++;


                    PdfPCell celli_1 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                    celli_1.HorizontalAlignment = 0;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    celli_1 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    celli_1 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);

                    DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                    celli_1 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                    celli_1.HorizontalAlignment = 1;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);


                    celli_1 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                    celli_1.HorizontalAlignment = 2;
                    celli_1.BorderColor = new Color(200, 200, 200);
                    tbl_4.AddCell(celli_1);
                }

                document.Add(new Paragraph("Insureds' Details", bodyFont));

                document.Add(tbl_4);

                int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                PdfPTable tbl_45 = new PdfPTable(5);
                tbl_45.SetWidths(clmwidths55);

                //tbl_4.WidthPercentage = 80; 
                tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl_45.SpacingBefore = 0;
                tbl_45.SpacingAfter = 1;
                tbl_45.DefaultCell.Border = 0;
                tbl_45.TotalWidth = 450f;
                tbl_45.LockedWidth = true;


                PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 0;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                cell5i.HorizontalAlignment = 1;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);


                cell5i = new PdfPCell(new Phrase(gtm.netPremium_usd.ToString("N2"), bodyFont4));
                cell5i.HorizontalAlignment = 2;
                cell5i.BorderColor = new Color(200, 200, 200);
                tbl_45.AddCell(cell5i);

                document.Add(tbl_45);
                document.Add(new Paragraph(" ", bodyFont));

                document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                document.Add(new Paragraph(" ", bodyFont));
                document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                document.Add(new Paragraph(" ", bodyFont));
                document.Add(new Paragraph(" ", bodyFont));
                document.Add(new Paragraph(" ", bodyFont));
                document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).\n", bodyFont));
                document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                if (gtm.AGENT_CODE > 0)
                {
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                }

            }
            else
            {

                if (memCount <= 50)
                {
                    document.NewPage();
                    //document.Add(new Paragraph("\n", bodyFont_bold_sm)); 
                    document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));
                    document.Add(new Paragraph("Insureds' Details", bodyFont));


                    foreach (TRV_Policy_mem mem in members)
                    {
                        //mCount++; 
                        i++;

                        //PdfPCell celli = new PdfPCell(new Phrase("Member " + i, bodyFont4)); 
                        //celli.HorizontalAlignment = 0; 
                        //celli.BorderColor = new Color(200, 200, 200); 
                        //tbl_4.AddCell(celli); 

                        PdfPCell celli_2 = new PdfPCell(new Phrase(mem.title + " " + mem.name, bodyFont4));
                        celli_2.HorizontalAlignment = 0;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);

                        //celli_2 = new PdfPCell(new Phrase(mem.memType_desc, bodyFont4)); 
                        //celli_2.HorizontalAlignment = 1; 
                        //celli_2.BorderColor = new Color(200, 200, 200); 
                        //tbl_4.AddCell(celli_2); 

                        celli_2 = new PdfPCell(new Phrase((mem.gender.Trim().Equals("M") ? "Male" : (mem.gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);


                        celli_2 = new PdfPCell(new Phrase(mem.ppno, bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);

                        DateTime dobDate = DateTime.ParseExact(mem.dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                        celli_2 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                        celli_2.HorizontalAlignment = 1;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);


                        celli_2 = new PdfPCell(new Phrase(mem.base_amount_usd.ToString("N2"), bodyFont4));
                        celli_2.HorizontalAlignment = 2;
                        celli_2.BorderColor = new Color(200, 200, 200);
                        tbl_4.AddCell(celli_2);

                    }

                    // document.Add(new Paragraph("Insureds' Details", bodyFont)); 

                    document.Add(tbl_4);


                    int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                    PdfPTable tbl_45 = new PdfPTable(5);
                    tbl_45.SetWidths(clmwidths55);

                    //tbl_4.WidthPercentage = 80; 
                    tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl_45.SpacingBefore = 0;
                    tbl_45.SpacingAfter = 1;
                    tbl_45.DefaultCell.Border = 0;
                    tbl_45.TotalWidth = 450f;
                    tbl_45.LockedWidth = true;


                    PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 0;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);


                    cell5i = new PdfPCell(new Phrase(gtm.netPremium_usd.ToString("N2"), bodyFont4));
                    cell5i.HorizontalAlignment = 2;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    document.Add(tbl_45);

                    //document.Add(new Paragraph(" ", bodyFont)); 


                    document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                    //document.Add(new Paragraph(" ", bodyFont)); 
                    document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                    }
                }
                else
                {
                    //int mRemain = memCount % 40; 
                    ////int remaCount = 0; 
                    ////for (int rCount = 0; rCount < remaCount + 40; rCount++) 
                    ////{ 
                    /// 

                    for (int rCount = 0; rCount < members.Count; rCount = rCount + 50)
                    {

                        PdfPTable tbl_memHe = new PdfPTable(5);
                        int[] clmwidthsMemHe = { 8, 3, 3, 3, 3 };

                        tbl_memHe.SetWidths(clmwidthsMemHe);

                        //tbl_4.WidthPercentage = 80; 
                        tbl_memHe.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_memHe.SpacingBefore = 10;
                        tbl_memHe.SpacingAfter = 0;
                        tbl_memHe.DefaultCell.Border = 0;
                        tbl_memHe.TotalWidth = 450f;
                        tbl_memHe.LockedWidth = true;

                        PdfPTable tbl_mem = new PdfPTable(5);
                        int[] clmwidthsMem = { 8, 3, 3, 3, 3 };

                        tbl_mem.SetWidths(clmwidthsMem);

                        //tbl_4.WidthPercentage = 80; 
                        tbl_mem.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl_mem.SpacingBefore = 0;
                        tbl_mem.SpacingAfter = 5;
                        tbl_mem.DefaultCell.Border = 0;
                        tbl_mem.TotalWidth = 450f;
                        tbl_mem.LockedWidth = true;

                        for (int k = rCount; k < members.Count; k = k + 1)
                        {
                            i++;

                            if (i % 50 == 0)
                            {
                                PdfPCell celli_3 = new PdfPCell(new Phrase(members[k].title + " " + members[k].name, bodyFont4));
                                celli_3.HorizontalAlignment = 0;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);

                                //celli_3 = new PdfPCell(new Phrase(members[k].memType_desc, bodyFont4)); 
                                //celli_3.HorizontalAlignment = 1; 
                                //celli_3.BorderColor = new Color(200, 200, 200); 
                                //tbl_mem.AddCell(celli_3); 

                                celli_3 = new PdfPCell(new Phrase((members[k].gender.Trim().Equals("M") ? "Male" : (members[k].gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                                celli_3.HorizontalAlignment = 1;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);


                                celli_3 = new PdfPCell(new Phrase(members[k].ppno, bodyFont4));
                                celli_3.HorizontalAlignment = 1;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);

                                DateTime dobDate = DateTime.ParseExact(members[k].dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                                celli_3 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                                celli_3.HorizontalAlignment = 1;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);


                                celli_3 = new PdfPCell(new Phrase(members[k].base_amount_usd.ToString("N2"), bodyFont4));
                                celli_3.HorizontalAlignment = 2;
                                celli_3.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_3);
                                break;
                            }
                            else
                            {

                                //////PdfPCell celli = new PdfPCell(new Phrase("Member " + i, bodyFont4)); 
                                //////celli.HorizontalAlignment = 0; 
                                //////celli.BorderColor = new Color(200, 200, 200); 
                                //////tbl_4.AddCell(celli); 

                                PdfPCell celli_4 = new PdfPCell(new Phrase(members[k].title + " " + members[k].name, bodyFont4));
                                celli_4.HorizontalAlignment = 0;
                                celli_4.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_4);

                                //celli_4 = new PdfPCell(new Phrase(members[k].memType_desc, bodyFont4)); 
                                //celli_4.HorizontalAlignment = 1; 
                                //celli_4.BorderColor = new Color(200, 200, 200); 
                                //tbl_mem.AddCell(celli_4); 

                                celli = new PdfPCell(new Phrase((members[k].gender.Trim().Equals("M") ? "Male" : (members[k].gender.Trim().Equals("F") ? "Female" : "Other")), bodyFont4));
                                celli.HorizontalAlignment = 1;
                                celli.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli);


                                celli_4 = new PdfPCell(new Phrase(members[k].ppno, bodyFont4));
                                celli_4.HorizontalAlignment = 1;
                                celli_4.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_4);


                                DateTime dobDate = DateTime.ParseExact(members[k].dob, "yyyy/MM/dd", CultureInfo.InvariantCulture);


                                celli_4 = new PdfPCell(new Phrase(dobDate.ToString("dd/MM/yyyy"), bodyFont4));
                                celli_4.HorizontalAlignment = 1;
                                celli_4.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_4);


                                celli_4 = new PdfPCell(new Phrase(members[k].base_amount_usd.ToString("N2"), bodyFont4));
                                celli_4.HorizontalAlignment = 2;
                                celli_4.BorderColor = new Color(200, 200, 200);
                                tbl_mem.AddCell(celli_4);

                            }

                            //} 


                        }

                        if (i <= members.Count + 1)
                        {
                            document.NewPage();
                            // document.Add(new Paragraph("\n", bodyFont_bold_sm)); 
                            document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont));

                            document.Add(new Paragraph("Insureds' Details", bodyFont));

                            PdfPCell celli2 = new PdfPCell(new Phrase("Member", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            //celli2 = new PdfPCell(new Phrase("Category", bodyFont4_white_bold)); 
                            //celli2.HorizontalAlignment = 1; 
                            //celli2.BackgroundColor = new Color(180, 180, 180); 
                            //celli2.BorderColor = new Color(200, 200, 200); 
                            //tbl_memHe.AddCell(celli2); 

                            celli2 = new PdfPCell(new Phrase("Gender", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            celli2 = new PdfPCell(new Phrase("Passport No", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);



                            celli2 = new PdfPCell(new Phrase("Date of Birth", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            celli2 = new PdfPCell(new Phrase("Premium (USD)", bodyFont4_white_bold));
                            celli2.HorizontalAlignment = 1;
                            celli2.BackgroundColor = new Color(180, 180, 180);
                            celli2.BorderColor = new Color(200, 200, 200);
                            tbl_memHe.AddCell(celli2);

                            document.Add(tbl_memHe);
                            document.Add(tbl_mem);
                            //int mCount = 0; 
                        }

                    }

                    int[] clmwidths55 = { 8, 3, 3, 3, 3 };

                    PdfPTable tbl_45 = new PdfPTable(5);
                    tbl_45.SetWidths(clmwidths55);

                    //tbl_4.WidthPercentage = 80; 
                    tbl_45.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl_45.SpacingBefore = 0;
                    tbl_45.SpacingAfter = 2;
                    tbl_45.DefaultCell.Border = 0;
                    tbl_45.TotalWidth = 450f;
                    tbl_45.LockedWidth = true;


                    PdfPCell cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 0;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    cell5i = new PdfPCell(new Phrase("TOTAL", bodyFont4));
                    cell5i.HorizontalAlignment = 1;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);


                    cell5i = new PdfPCell(new Phrase(gtm.netPremium_usd.ToString("N2"), bodyFont4));
                    cell5i.HorizontalAlignment = 2;
                    cell5i.BorderColor = new Color(200, 200, 200);
                    tbl_45.AddCell(cell5i);

                    document.Add(tbl_45);

                    //document.Add(new Paragraph(" ", bodyFont)); 

                    document.Add(new Paragraph("In witness whereof the Undersigned being duly authorized by the Company and on behalf of the Company has hereunder set his(their) hand(s).", bodyFont));
                    //document.Add(new Paragraph(" ", bodyFont)); 
                    document.Add(new Paragraph("Issued Date     " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"), bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    document.Add(new Paragraph(" ", bodyFont));
                    //document.Add(new Paragraph(" ", bodyFont)); 
                    document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).", bodyFont));
                    document.Add(new Paragraph("This is a computer generated document. No signature is required.", bodyFont));

                    if (gtm.AGENT_CODE > 0)
                    {
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph(" ", bodyFont));
                        document.Add(new Paragraph("Service Code :" + gtm.AGENT_CODE + " (" + AgentName + " )", bodyFont));
                    }
                }
            }

        }



        document.NewPage();

        #region claim sheet


        //document.Add(new Paragraph("\n", bodyFont)); 
        Chunk titchTitle = new Chunk("BENEFITS SCHEDULE", FontFactory.GetFont("Times New Roman", 10, Font.BOLD, Color.BLACK));
        titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph titletitle = new Paragraph(titchTitle);
        titletitle.SetAlignment("Center");
        document.Add(titletitle);

        int[] clmwidth1 = { 4, 1, 8 };

        PdfPTable tbl_Quote = new PdfPTable(3);

        tbl_Quote.SetWidths(clmwidth1);

        tbl_Quote.WidthPercentage = 100;
        tbl_Quote.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl_Quote.SpacingBefore = 10;
        tbl_Quote.SpacingAfter = 5;
        tbl_Quote.DefaultCell.Border = 0;

        tbl_Quote.AddCell(new Phrase("Scheme", bodyFont));
        tbl_Quote.AddCell(new Phrase(": ", bodyFont));
        tbl_Quote.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont));

        tbl_Quote.AddCell(new Phrase("Policy Number", bodyFont));
        tbl_Quote.AddCell(new Phrase(": ", bodyFont));
        tbl_Quote.AddCell(new Phrase(poliID, bodyFont));

        //document.Add(new Paragraph("Policy Number   : " + poliID, bodyFont)); 

        tbl_Quote.AddCell(new Phrase("Currency Type", bodyFont));
        tbl_Quote.AddCell(new Phrase(": ", bodyFont));
        tbl_Quote.AddCell(new Phrase("USD", bodyFont));

        //tbl_Quote.AddCell(new Phrase("  ", bodyFont)); 
        //tbl_Quote.AddCell(new Phrase("  ", bodyFont)); 
        //tbl_Quote.AddCell(new Phrase("  ", bodyFont)); 

        document.Add(tbl_Quote);
        TRV_Benefits gtBen = new TRV_Benefits(gtm.plan);

        if (gtBen.DTproduct.Rows.Count > 0)
        {
            int[] clmwidths100 = { 10, 4, 4 };

            PdfPTable tbl_header = new PdfPTable(3);

            tbl_header.SetWidths(clmwidths100);

            tbl_header.WidthPercentage = 100;
            tbl_header.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl_header.SpacingBefore = 2;
            tbl_header.SpacingAfter = 0;
            tbl_header.DefaultCell.Border = 0;

            //tbl_header.AddCell(new Phrase("BENEFIT", boldTableFont)); 
            //tbl_header.AddCell(new Phrase("SUM_INSURED", boldTableFont)); 
            //tbl_header.AddCell(new Phrase("EXCESS", boldTableFont)); 

            PdfPCell cellHead1 = new PdfPCell(new Phrase("Summary of Benefits", boldTableFont));

            cellHead1.HorizontalAlignment = 1;
            cellHead1.BorderWidth = 0f;
            cellHead1.Padding = 2;
            cellHead1.BorderColor = new Color(180, 180, 180);
            cellHead1.BorderWidthLeft = 0.5f;
            cellHead1.BorderWidthTop = 0.5f;

            tbl_header.AddCell(cellHead1);

            PdfPCell cellHead2 = new PdfPCell(new Phrase("Sum_Insured", boldTableFont));
            cellHead2.HorizontalAlignment = 2;
            cellHead2.BorderWidth = 0f;
            cellHead2.Padding = 2;
            cellHead2.BorderColor = new Color(180, 180, 180);
            cellHead2.BorderWidthTop = 0.5f;
            cellHead2.BorderWidthLeft = 0.5f;

            tbl_header.AddCell(cellHead2);

            cellHead2 = new PdfPCell(new Phrase("Deductible", boldTableFont));
            cellHead2.HorizontalAlignment = 2;
            cellHead2.BorderWidth = 0f;
            cellHead2.Padding = 2;
            cellHead2.BorderColor = new Color(180, 180, 180);
            cellHead2.BorderWidthTop = 0.5f;
            cellHead2.BorderWidthRight = 0.5f;


            tbl_header.AddCell(cellHead2);

            document.Add(tbl_header);

            int[] clmwidths1115 = { 2, 8, 4, 4 };

            PdfPTable tbl_15 = new PdfPTable(4);


            tbl_15.SetWidths(clmwidths1115);

            tbl_15.WidthPercentage = 100;
            //tbl_15.HorizontalAlignment = Element.ALIGN_CENTER; 
            tbl_15.HorizontalAlignment = Element.ALIGN_CENTER;
            // tbl_15.SpacingBefore = 5; 
            tbl_15.SpacingAfter = 5;
            tbl_15.DefaultCell.Border = 0;



            //tbl_1.AddCell(new Phrase(gtm.get_scheme_name(gtm.plan), bodyFont)); 
            string HeaderCode = "";
            int count = 0;
            for (int k = 0; k < gtBen.DTproduct.Rows.Count; k++)
            {
                //5 % 4 
                string code = gtBen.DTproduct.Rows[k]["code"].ToString();
                if (code.Equals("1000") || code.Equals("2000") || code.Equals("3000") || code.Equals("4000") || code.Equals("5000") || code.Equals("6000"))
                {
                    //tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString().ToUpper(), bodyFont_bold)); 
                    //tbl_15.AddCell(new Phrase(" ", bodyFont_bold)); 
                    //tbl_15.AddCell(new Phrase(" ", bodyFont_bold)); 
                    count = 0;
                    string setCode = code.TrimEnd('0');
                    HeaderCode = setCode;
                    PdfPCell cellCode = new PdfPCell(new Phrase(setCode.ToString(), bodyFont));
                    cellCode.HorizontalAlignment = 0;
                    cellCode.BorderColor = new Color(180, 180, 180);
                    cellCode.BackgroundColor = new Color(180, 180, 180);
                    cellCode.BorderWidth = 0f;
                    cellCode.Padding = 2;
                    cellCode.BorderWidthTop = 0.5f;
                    cellCode.BorderWidthLeft = 0.5f;

                    tbl_15.AddCell(cellCode);

                    PdfPCell cellTBA = new PdfPCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString(), bodyFont));
                    cellTBA.HorizontalAlignment = 0;
                    cellTBA.BorderColor = new Color(180, 180, 180);
                    cellTBA.BackgroundColor = new Color(180, 180, 180);
                    cellTBA.BorderWidth = 0f;
                    cellTBA.Padding = 2;
                    cellTBA.BorderWidthTop = 0.5f;
                    cellTBA.BorderWidthLeft = 0.5f;

                    tbl_15.AddCell(cellTBA);

                    PdfPCell cellTBA1 = new PdfPCell(new Phrase(" ", bodyFont));
                    cellTBA1.HorizontalAlignment = 2;
                    cellTBA1.BorderColor = new Color(180, 180, 180);
                    cellTBA1.BackgroundColor = new Color(180, 180, 180);
                    cellTBA1.BorderWidth = 0f;
                    cellTBA1.Padding = 2;
                    cellTBA1.BorderWidthTop = 0.5f;
                    cellTBA1.BorderWidthLeft = 0.5f;

                    tbl_15.AddCell(cellTBA1);

                    cellTBA1 = new PdfPCell(new Phrase(" ", bodyFont));
                    cellTBA1.HorizontalAlignment = 2;
                    cellTBA1.BorderColor = new Color(180, 180, 180);
                    cellTBA1.BackgroundColor = new Color(180, 180, 180);
                    cellTBA1.BorderWidth = 0f;
                    cellTBA1.Padding = 2;
                    cellTBA1.BorderWidthTop = 0.5f;
                    cellTBA1.BorderWidthRight = 0.5f;

                    tbl_15.AddCell(cellTBA1);
                }
                else
                {
                    //code = gtBen.DTproduct.Rows[k]["code"].ToString(); 
                    string sum_insured = gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString().Trim();
                    if (!sum_insured.Equals("NA"))
                    {
                        //ben = gtBen.DTproduct.Rows[k]["benefit"].ToString(); 
                        //sum_ins = gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString(); 
                        //excess = gtBen.DTproduct.Rows[k]["EXCESS"].ToString(); 
                        //tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString(), bodyFont)); 

                        //tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString(), bodyFont)); 
                        //tbl_15.AddCell(new Phrase(gtBen.DTproduct.Rows[k]["EXCESS"].ToString(), bodyFont)); 
                        ////count++; 
                        ////string grdcode = HeaderCode + "." + count.ToString(); 
                        /// 
                        string grdcode = "";
                        if (gtBen.DTproduct.Rows[k]["benefit"].ToString().Trim() == "Single article limit" || gtBen.DTproduct.Rows[k]["benefit"].ToString().Trim() == "Single article limit for Home Safety cover")
                        {
                            grdcode = HeaderCode + "." + count.ToString() + " A ";
                        }
                        else
                        {
                            count++;
                            grdcode = HeaderCode + "." + count.ToString();
                        }

                        PdfPCell cellCode = new PdfPCell(new Phrase(grdcode.ToString(), bodyFont));
                        cellCode.HorizontalAlignment = 0;
                        cellCode.BorderColor = new Color(180, 180, 180);
                        //cellCode.BackgroundColor = new Color(180, 180, 180); 
                        cellCode.BorderWidth = 0f;
                        cellCode.Padding = 2;
                        cellCode.BorderWidthLeft = 0.5f;

                        tbl_15.AddCell(cellCode);

                        PdfPCell cellTBA = new PdfPCell(new Phrase(gtBen.DTproduct.Rows[k]["benefit"].ToString(), bodyFont));
                        cellTBA.HorizontalAlignment = 0;
                        cellTBA.BorderColor = new Color(180, 180, 180);
                        cellTBA.BorderWidth = 0f;
                        cellTBA.Padding = 2;
                        cellTBA.BorderWidthLeft = 0.5f;

                        tbl_15.AddCell(cellTBA);

                        PdfPCell cellTBA1 = new PdfPCell(new Phrase(gtBen.DTproduct.Rows[k]["SUM_INSURED"].ToString(), bodyFont));
                        cellTBA1.HorizontalAlignment = 2;
                        cellTBA1.BorderWidth = 0f;
                        cellTBA1.BorderColor = new Color(180, 180, 180);
                        cellTBA1.Padding = 2;
                        cellTBA1.BorderWidthLeft = 0.5f;

                        tbl_15.AddCell(cellTBA1);

                        cellTBA1 = new PdfPCell(new Phrase(gtBen.DTproduct.Rows[k]["EXCESS"].ToString(), bodyFont));
                        cellTBA1.HorizontalAlignment = 2;
                        cellTBA1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellTBA1.BorderColor = new Color(180, 180, 180);
                        cellTBA1.BorderWidth = 0f;
                        cellTBA1.Padding = 2;
                        cellTBA1.BorderWidthRight = 0.5f;

                        tbl_15.AddCell(cellTBA1);
                    }


                }
            }
            //tbl_15.AddCell(gtBen.DTproduct.ToString()); 
            //tbl_15.AddCell("Row 1, Col 2"); 
            //tbl_15.AddCell("Row 1, Col 3"); 

            document.Add(tbl_15);
        }

        int[] clmwidthsReInsu = { 1 };

        PdfPTable tbl_reIns = new PdfPTable(1);

        tbl_reIns.SetWidths(clmwidthsReInsu);

        //tbl_2.WidthPercentage = 0.50f; 
        tbl_reIns.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl_reIns.SpacingBefore = 10;
        tbl_reIns.SpacingAfter = 0;
        tbl_reIns.DefaultCell.Border = 0;
        ////tbl_2.WidthPercentage = 40; 
        tbl_reIns.TotalWidth = 450f;
        tbl_reIns.LockedWidth = true;

        TRV_TPA_Company tpaCom = new TRV_TPA_Company(System.DateTime.Now.Date);
        //tbl_12.AddCell(new Phrase("Sri Lanka", bodyFont)); 

        if (!String.IsNullOrEmpty(tpaCom.COMPANY))
        {
            tbl_reIns.AddCell(new Phrase("Contact Details of Third Party Assistance service -:", underlineAndBoldFont));
            if (!String.IsNullOrEmpty(tpaCom.COMPANY))
            {
                tbl_reIns.AddCell(new Phrase(tpaCom.COMPANY, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_1))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_1, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_2))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_2, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_3))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_3, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_4))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_4, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.ADDRESS_5))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.ADDRESS_5, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.TELE_NO))
            {
                tbl_reIns.AddCell(new Phrase(tpaCom.TELE_NO, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.FAX_NO))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.FAX_NO, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.TOLL_FREE))
            {
                tbl_reIns.AddCell(new Phrase("    " + tpaCom.TOLL_FREE, bodyFont_bold));
            }

            if (!String.IsNullOrEmpty(tpaCom.EMAIL))
            {
                tbl_reIns.AddCell(new Phrase(tpaCom.EMAIL, bodyFont_bold));
            }
            if (!String.IsNullOrEmpty(tpaCom.SLIC_HELP))
            {
                tbl_reIns.AddCell(new Paragraph("\n", bodyFont));
                tbl_reIns.AddCell(new Phrase(tpaCom.SLIC_HELP, bodyFont_bold));
            }
        }
        document.Add(tbl_reIns);

        //document.Add(new Paragraph("\n", bodyFont)); 
        Chunk spec = new Chunk("Special notes :", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f); 
        Paragraph specP = new Paragraph(spec);
        document.Add(specP);

        Chunk specWord = new Chunk("  Pre-existing Medical Conditions are excluded", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f); 
        Paragraph specPWord = new Paragraph(specWord);
        document.Add(specPWord);

        Chunk specNotes = new Chunk("  This Insurance is backed by Munich Re", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f); 
        Paragraph specNote = new Paragraph(specNotes);
        document.Add(specNote);

        #endregion

        document.Close();
        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=TRV_{0}.pdf", "Policy_Document"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
    }

    #endregion
}

 