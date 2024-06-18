using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Print_pdf_Life
/// </summary>
public class Print_pdf_Life
{
	public Print_pdf_Life()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void print_receipt(LifePayment pro, string epf, string ip, GridView gv)
    {

        Document document = new Document(PageSize.A4, 50, 50, 50, 50);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        header.Border = Rectangle.NO_BORDER;
        header.Alignment = 1;
        document.Footer = header;
        document.Open();



        Font titleFont = FontFactory.GetFont("Times New Roman", 14, Font.BOLD, new Color(0, 0, 0));
        Font titleFont2 = FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new Color(0, 0, 0));
        Font titleFont3 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new Color(0, 0, 0));
        Font headerFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);


        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/logoLife.png");


        // document.Open();

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
        //logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
       // watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);



        document.Add(logo);


        document.Add(new Paragraph("\n\n\n"));

        Paragraph titleLine = new Paragraph("Sri Lanka Insurance Corporation Life Limited", titleFont3);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        Paragraph titleLine2 = new Paragraph("Online Payment Receipt", titleFont);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);


        //document.Add(new Paragraph(drop_br(pro.getFullAddress()), bodyFont));
        document.Add(new Paragraph("Dear Customer,", bodyFont));

        document.Add(new Paragraph("\n"));

        string line2 = "We have received your payment of Rs. " + pro.amount.ToString("N2") + " under the reference no:  " + pro.receiptNo + " regarding premium payment for Life policy number "+pro.polNum+".\n";
        document.Add(new Paragraph(line2, bodyFont));

        document.Add(new Paragraph("Policy Details:", titleFont2));

        int[] clmwidths112 = { 1, 4 };

        PdfPTable tbl15 = new PdfPTable(2);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;

        tbl15.AddCell(new Phrase("Policy Number: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.polNum, bodyFont));

        tbl15.AddCell(new Phrase("Customer Name: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.custName, bodyFont));

        tbl15.AddCell(new Phrase(" ", bodyFont));
        tbl15.AddCell(new Phrase(" ", bodyFont));

        
        

        if (gv.Rows.Count > 0)
        {

            int[] clmwidths119 = { 1, 1, 1 };

            PdfPTable tbl19 = new PdfPTable(3);

            tbl19.SetWidths(clmwidths119);

            tbl19.WidthPercentage = 100;
            tbl19.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl19.SpacingBefore = 10;
            tbl19.SpacingAfter = 10;
            tbl19.DefaultCell.Border = 0;


            PdfPCell cellTBA = new PdfPCell(new Phrase(gv.HeaderRow.Cells[0].Text, headerFont));
            cellTBA.HorizontalAlignment = 1;
            cellTBA.BackgroundColor = new Color(180, 180, 180);
            cellTBA.BorderColor = new Color(200, 200, 200);
            cellTBA.BorderWidthTop = 0.5f;
            cellTBA.BorderWidthBottom = 0.5f;
            cellTBA.BorderWidthLeft = 0.5f;
            cellTBA.BorderWidthRight = 0f;
            tbl19.AddCell(cellTBA);

            cellTBA = new PdfPCell(new Phrase(gv.HeaderRow.Cells[1].Text, headerFont));
            cellTBA.HorizontalAlignment = 1;
            cellTBA.BackgroundColor = new Color(180, 180, 180);
            cellTBA.BorderColor = new Color(200, 200, 200); 
            cellTBA.BorderWidthTop = 0.5f;
            cellTBA.BorderWidthBottom = 0.5f;
            cellTBA.BorderWidthLeft = 0.5f;
            cellTBA.BorderWidthRight = 0f;
            tbl19.AddCell(cellTBA);

            cellTBA = new PdfPCell(new Phrase(gv.HeaderRow.Cells[2].Text, headerFont));
            cellTBA.HorizontalAlignment = 1;
            cellTBA.BackgroundColor = new Color(180, 180, 180);
            cellTBA.BorderColor = new Color(200, 200, 200);
            cellTBA.BorderWidthTop = 0.5f;
            cellTBA.BorderWidthBottom = 0.5f;
            cellTBA.BorderWidthLeft = 0.5f;
            cellTBA.BorderWidthRight = 0.5f;
            tbl19.AddCell(cellTBA);

            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    cellTBA = new PdfPCell(new Phrase(row.Cells[0].Text, bodyFont));
                    cellTBA.HorizontalAlignment = 2;
                    cellTBA.BorderColor = new Color(200, 200, 200);
                    cellTBA.BorderWidthTop = 0f;
                    cellTBA.BorderWidthBottom = 0.5f;
                    cellTBA.BorderWidthLeft = 0.5f;
                    cellTBA.BorderWidthRight = 0f;
                    tbl19.AddCell(cellTBA);

                    cellTBA = new PdfPCell(new Phrase(row.Cells[1].Text, bodyFont));
                    cellTBA.HorizontalAlignment = 2;
                    cellTBA.BorderColor = new Color(200, 200, 200);
                    cellTBA.BorderWidthTop = 0f;
                    cellTBA.BorderWidthBottom = 0.5f;
                    cellTBA.BorderWidthLeft = 0.5f;
                    cellTBA.BorderWidthRight = 0f;
                    tbl19.AddCell(cellTBA);

                    cellTBA = new PdfPCell(new Phrase(row.Cells[2].Text, bodyFont));
                    cellTBA.HorizontalAlignment = 2;
                    cellTBA.BorderColor = new Color(200, 200, 200);
                    cellTBA.BorderWidthTop = 0f;
                    cellTBA.BorderWidthBottom = 0.5f;
                    cellTBA.BorderWidthLeft = 0.5f;
                    cellTBA.BorderWidthRight = 0.5f;
                    tbl19.AddCell(cellTBA);
                }
            }

            PdfPCell cellTBA2 = new PdfPCell(tbl19);
            cellTBA2.HorizontalAlignment = 1;
            cellTBA2.Colspan = 2;
            tbl15.AddCell(cellTBA2);
        }

        tbl15.AddCell(new Phrase("Total Dues amount: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.duesAmt.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase("Deposits: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.deposits.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase("Paid Dues amount: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.paidDuesAmt.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase("Additional amount: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.addtAmt.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase(" ", bodyFont));
        tbl15.AddCell(new Phrase(" ", bodyFont));

        tbl15.AddCell(new Phrase("Date of Payment: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.entryDate, bodyFont));

        
        document.Add(tbl15);

        document.Add(new Paragraph("Premium receipt will be posted to the above address in due course.", bodyFont));
        document.Add(new Paragraph("Thanking you,", bodyFont));
        document.Add(new Paragraph("Sri Lanka Insurance Life.", bodyFont));



        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Online_Payment_{0}.pdf", "Receipt"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();




    }

    public void print_receipt_Loan(LifePayment pro, string epf, string ip)
    {

        Document document = new Document(PageSize.A4, 50, 50, 50, 50);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        header.Border = Rectangle.NO_BORDER;
        header.Alignment = 1;
        document.Footer = header;
        document.Open();



        Font titleFont = FontFactory.GetFont("Times New Roman", 14, Font.BOLD, new Color(0, 0, 0));
        Font titleFont2 = FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new Color(0, 0, 0));
        Font titleFont3 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new Color(0, 0, 0));
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont_B = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);


        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/logoLife.png");


        //document.Open();

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
        //logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);



        document.Add(logo);


        document.Add(new Paragraph("\n\n\n"));

        Paragraph titleLine = new Paragraph("Sri Lanka Insurance Corporation Life Limited", titleFont3);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        Paragraph titleLine2 = new Paragraph("Online Payment Receipt", titleFont);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);


        //document.Add(new Paragraph(drop_br(pro.address), bodyFont));
        document.Add(new Paragraph("Dear Customer,", bodyFont));

        document.Add(new Paragraph("\n"));

        string line2 = "We have received your payment of Rs. " + pro.amount.ToString("N2") + " under the reference no:  " + pro.receiptNo + " regarding Loan payment for loan number " + pro.loanNo + " under the policy number "+pro.polNum+".\n";
        document.Add(new Paragraph(line2, bodyFont));

        document.Add(new Paragraph("Policy Details:", titleFont2));

        int[] clmwidths112 = { 1, 4 };

        PdfPTable tbl15 = new PdfPTable(2);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;

        tbl15.AddCell(new Phrase("Policy Number: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.polNum, bodyFont));
        
        tbl15.AddCell(new Phrase("Customer Name: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.custName, bodyFont));

        tbl15.AddCell(new Phrase(" ", bodyFont));
        tbl15.AddCell(new Phrase(" ", bodyFont));

        tbl15.AddCell(new Phrase("Date of Payment: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.entryDate, bodyFont));

       

        document.Add(tbl15);

       // document.Add(new Paragraph("Subject to there being no claim from the date of Renewal to the date of online payment.", bodyFont_B));
        document.Add(new Paragraph("Loan Payment Receipt will be posted to the above address in due course.", bodyFont));
        document.Add(new Paragraph("Thanking you,", bodyFont));
        document.Add(new Paragraph("Sri Lanka Insurance Life.", bodyFont));



        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Online_{0}.pdf", "Renewal_Receipt"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();




    }

    private string drop_br(string inpt)
    {
        string outpt = "";

        outpt = inpt.Replace("<br/>", "\n");

        return outpt;
    }


    public void print_policy_addition_request(CustProfile customer, string policyNo, string ip)
    {
        string[] arr = new string[8];

        List<string> plo_chars_ar = new List<string>();
        int i = 0;

        for (; i < policyNo.Trim().Length; i++)
        {
            arr[i] = policyNo[i].ToString();
        }
        for (; i < 8; i++)
        {
            arr[i] = " ";
        }


        Document document = new Document(PageSize.A4, 50, 50, 50, 30);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + customer.O_username, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        header.Border = Rectangle.NO_BORDER;
        header.Alignment = 1;
        document.Footer = header;
        document.Open();

        Font titleFont = FontFactory.GetFont("Times New Roman", 14, Font.BOLD, new Color(0, 0, 0));
        Font titleFont2 = FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new Color(0, 0, 0));
        Font titleFont3 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new Color(0, 0, 0));
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont_sml = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont_B = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);


        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/logoLife.png");

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
        //logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);

        document.Add(logo);

        document.Add(new Paragraph("\n\n\n"));

        Paragraph titleLine = new Paragraph("Request to view life policy details", titleFont2);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);
        document.Add(new Paragraph("\n\n"));
        document.Add(new Paragraph("The Manager\nLife PHS\nSri Lanka Insurance Life\n\n\nDear Sir,", bodyFont));

        PdfPTable tbl15 = new PdfPTable(2);

        int[] clmwidths112 = { 2, 5 };
        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;




        tbl15.AddCell(new Phrase("Name of the Customer", bodyFont));
        tbl15.AddCell(new Phrase(customer.O_firstName + " " + customer.O_lastName, bodyFont));

        tbl15.AddCell(new Phrase("Address", bodyFont));
        string address = customer.O_addrss1 + ", " + customer.O_addrss2 + (String.IsNullOrEmpty(customer.O_addrss3) ? "" : ", " + customer.O_addrss3) + (String.IsNullOrEmpty(customer.O_addrss4) ? "" : ", " + customer.O_addrss4) + (String.IsNullOrEmpty(customer.O_cityTown) ? "" : ", " + customer.O_cityTown) + (String.IsNullOrEmpty(customer.O_postCode) ? "" : ", " + customer.O_postCode) + ".";
        tbl15.AddCell(new Phrase(address, bodyFont));

        tbl15.AddCell(new Phrase("Contact No(s)", bodyFont));
        string phones = customer.O_mobileNumber + (String.IsNullOrEmpty(customer.O_homeNumber) ? "" : @" / " + customer.O_homeNumber) + (String.IsNullOrEmpty(customer.O_officeNumber) ? "" : @" / " + customer.O_officeNumber);
        tbl15.AddCell(new Phrase(phones, bodyFont));

        tbl15.AddCell(new Phrase("Email", bodyFont));
        tbl15.AddCell(new Phrase(customer.O_email, bodyFont));

        tbl15.AddCell(new Phrase("NIC Number", bodyFont));

        PdfPTable tbl16 = new PdfPTable(10);

        int[] clmwidths111 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        tbl16.SetWidths(clmwidths111);

        tbl16.TotalWidth = 200F;

        tbl16.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl16.SpacingBefore = 10;
        tbl16.SpacingAfter = 10;
        tbl16.DefaultCell.Border = 0;
        tbl16.LockedWidth = true;



        PdfPCell cell = new PdfPCell();
        cell.HorizontalAlignment = 0;
        cell.BorderWidth = 0.5F;
        cell.FixedHeight = 10f;

        PdfPCell bcell = new PdfPCell();
        bcell.HorizontalAlignment = 0;
        bcell.BorderWidth = 0F;
        bcell.FixedHeight = 10f;
        bcell.Colspan = 8;


        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);
        //tbl16.AddCell(cell);


        //tbl15.AddCell(tbl16);
        tbl15.AddCell(new Paragraph(customer.O_nicNo, bodyFont));

        document.Add(tbl15);

        document.Add(new Paragraph("Please allow me to view the policy information of below policy/s", bodyFont));


        PdfPTable tbl17 = new PdfPTable(8);

        int[] clmwidths110 = { 1, 1, 1, 1, 1, 1, 1, 1 };
        tbl17.SetWidths(clmwidths110);

        tbl17.TotalWidth = 160F;

        tbl17.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl17.SpacingBefore = 10;
        tbl17.SpacingAfter = 10;
        tbl17.DefaultCell.Border = 0;
        tbl17.LockedWidth = true;

        cell.FixedHeight = 20f;
        bcell = new PdfPCell(new Phrase(" ", bodyFont));
        bcell.FixedHeight = 10f;
        bcell.Colspan = 8;
        bcell.BorderWidth = 0F;


        PdfPCell icell = new PdfPCell();

        for (int k = 0; k < 8; k++)
        {
            icell = new PdfPCell(new Phrase(arr[k], bodyFont));
            icell.VerticalAlignment = Element.ALIGN_MIDDLE;
            icell.HorizontalAlignment = Element.ALIGN_CENTER;
            icell.BorderWidth = 0.5F;
            icell.FixedHeight = 10f;
            icell.Padding = 0;
            icell.FixedHeight = 20f;
            tbl17.AddCell(icell);

        }

        //tbl17.AddCell(cell);
        //tbl17.AddCell(cell);
        //tbl17.AddCell(cell);
        //tbl17.AddCell(cell);
        //tbl17.AddCell(cell);
        //tbl17.AddCell(cell);
        //tbl17.AddCell(cell);


        tbl17.AddCell(bcell);



        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);

        tbl17.AddCell(bcell);

        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);

        tbl17.AddCell(bcell);



        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);

        tbl17.AddCell(bcell);

        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);
        tbl17.AddCell(cell);


        document.Add(tbl17);

        document.Add(new Paragraph("This request is made under the terms, conditions and indemnity of my original Sri Lanka Insurance Life User Account.", bodyFont));
        document.Add(new Paragraph("\n\n\nSignature of the User : .............................................  Date : .............................................\n\n\n\n", bodyFont));

        Paragraph split = new Paragraph("\n\n-------------------------------------------------------------------------- FOR OFFICE USE ONLY --------------------------------------------------------------------------\n\n\n\n", bodyFont_sml);
        split.SetAlignment("Center");
        document.Add(split);

        document.Add(new Paragraph("                  .............................................                                                   .............................................\n", bodyFont));
        document.Add(new Paragraph("                  Customer Data Validated by                                                                       Date", bodyFont));

        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.pdf", "Policy_request"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
    }

    public int GeneratePolicyRevivalPDF(int polNo, string polHolderName, string polHolderNIC, string mobileNumber, bool OR_checked, bool SR_checked, string emailAdrs)
    {
        int ret = 0;
        Document document = new Document(PageSize.A4, 50, 50, 25, 25);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);

        Phrase phrase;

        //if (reprint)
        //    phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));
        //else
        phrase = new Phrase(DateTime.Now.ToString() + "ip  epf", new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        // top & bottom borders on by default 
        header.Border = Rectangle.NO_BORDER;
        // center header
        header.Alignment = 1;
        /*
         * HeaderFooter => add header __before__ opening document
         */
        document.Footer = header;

        // Open the Document for writing
        document.Open();

        Font titleFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLDITALIC, new Color(0, 0, 0));
        Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(0, 0, 0));
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.gif"));
        // logo.SetDpi(300, 300);
        logo.ScalePercent(25f);
        //logo.SetAbsolutePosition(260, 740);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);
        document.Add(logo);

        //iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        ////watermark.ScalePercent(10f);
        ////watermark.SetAbsolutePosition(76, 190);
        //watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark);

        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));

        string rev_type = "";
        if (OR_checked)
        {
            rev_type = "OR";
        }
        else if (SR_checked)
        {
            rev_type = "SR";
        }

        Paragraph titleLine = new Paragraph("(Onilne Policy Revival Request) - " + rev_type, titleFont);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        document.Add(new Paragraph("\n"));

        document.Add(new Paragraph("Policy Details:"));
        //document.Add(new Paragraph("\n"));

        int[] clmwidths = { 35, 65 };

        PdfPTable tbl1 = new PdfPTable(2);

        tbl1.SetWidths(clmwidths);

        tbl1.WidthPercentage = 100;
        tbl1.HorizontalAlignment = 0;
        tbl1.SpacingBefore = 15;
        tbl1.SpacingAfter = 10;
        tbl1.DefaultCell.Border = 0;

        tbl1.AddCell(new Phrase("Policy Number", boldTableFont));
        tbl1.AddCell(new Phrase(polNo.ToString(), bodyFont));

        tbl1.AddCell(new Phrase("Policy Holder's Name", bodyFont));
        tbl1.AddCell(new Phrase(polHolderName, bodyFont));

        tbl1.AddCell(new Phrase("Policy Holder's NIC", bodyFont));
        tbl1.AddCell(new Phrase(polHolderNIC, bodyFont));

        tbl1.AddCell(new Phrase("Mobile Number", bodyFont));
        tbl1.AddCell(new Phrase(mobileNumber, bodyFont));

        tbl1.AddCell(new Phrase("Email Address", bodyFont));
        tbl1.AddCell(new Phrase(emailAdrs, bodyFont));

        document.Add(tbl1);

        string revivalTypeStr = "";
        if (OR_checked)
        {
            revivalTypeStr = "Activate Policy by paying Arrears of Premiums.";
        }
        if (SR_checked)
        {
            revivalTypeStr = "Activate under Special Revival by Extending Commencement Date.";
        }
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("**" + revivalTypeStr));

        writer.CloseStream = false;

        document.Close();

        output.Position = 0;

        Db_Email email = new Db_Email();
        //email.send_app_email("slic_phsweb@srilankainsurance.com", "Online policy Revival Request", "text message", "<p>Dear Life PHS,<br/><br/>Onilne policy revival request form is attached with. <br/>Please do the needful.", output, "policy revival request.pdf", null);
        email.send_app_email("phs@srilankainsurance.com", "Online policy Revival Request", "text message", "<p>Dear Life PHS,<br/><br/>Onilne policy revival request form is attached with. <br/>Please do the needful.", output, "policy revival request.pdf", null);

        output.Close();
        ret = 1;
        return ret;
    }

    public void print_receipt_Revival(LifePayment pro, string epf, string ip)
    {

        Document document = new Document(PageSize.A4, 50, 50, 50, 50);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        header.Border = Rectangle.NO_BORDER;
        header.Alignment = 1;
        document.Footer = header;
        document.Open();

        Font titleFont = FontFactory.GetFont("Times New Roman", 14, Font.BOLD, new Color(0, 0, 0));
        Font titleFont2 = FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new Color(0, 0, 0));
        Font titleFont3 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new Color(0, 0, 0));
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont_B = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font bodyFontSmall = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/logoLife.png");

        //document.Open();

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
        //logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);

        document.Add(logo);

        document.Add(new Paragraph("\n\n\n"));

        Paragraph titleLine = new Paragraph("Sri Lanka Insurance Corporation Life Limited", titleFont3);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        Paragraph titleLine2 = new Paragraph("Online Payment Receipt", titleFont);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);

        CustProfile customer = new CustProfile(pro.username);

        document.Add(new Paragraph(pro.custName, bodyFont));
        document.Add(new Paragraph(drop_br(customer.O_addrss1 + "\n" + customer.O_addrss2 + "\n" + customer.O_addrss3 + "\n" + customer.O_addrss4), bodyFont));
        document.Add(new Paragraph("\n"));

        document.Add(new Paragraph("Dear Valued Customer,", bodyFont));

        document.Add(new Paragraph("\n"));

        string line2 = "We have received your payment of Rs. " + pro.amount.ToString("N2") + " under the reference no:  " + pro.receiptNo + " for the policy revival as follows.\n\n";
        document.Add(new Paragraph(line2, bodyFont));

        document.Add(new Paragraph("Policy Details:", titleFont2));

        int[] clmwidths112 = { 1, 4 };

        PdfPTable tbl15 = new PdfPTable(2);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;

        tbl15.AddCell(new Phrase("Policy Number: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.polNum, bodyFont));

        tbl15.AddCell(new Phrase("Policy Holder's Name: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.custName, bodyFont));

        tbl15.AddCell(new Phrase("Paid Amount: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.amount.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase(" ", bodyFont));
        tbl15.AddCell(new Phrase(" ", bodyFont));

        tbl15.AddCell(new Phrase("Date of Payment: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.entryDate, bodyFont));

        document.Add(tbl15);

        // document.Add(new Paragraph("Subject to there being no claim from the date of Renewal to the date of online payment.", bodyFont_B));
        document.Add(new Paragraph("Deposit Receipt will be posted to the above address in due course.", bodyFont));
        document.Add(new Paragraph("\n\n"));
        document.Add(new Paragraph("Thanking you,", bodyFont));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("Sri Lanka Insurance Life.", bodyFont));
        document.Add(new Paragraph("\n\n\n"));
        document.Add(new Paragraph("This is a computer generated letter and no signature required.", bodyFontSmall));

        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Online_{0}.pdf", "Renewal_Receipt"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();




    }


    public void print_receipt_Proposal(LifePayment pro, string epf, string ip)
    {

        Document document = new Document(PageSize.A4, 50, 50, 50, 50);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        header.Border = Rectangle.NO_BORDER;
        header.Alignment = 1;
        document.Footer = header;
        document.Open();

        Font titleFont = FontFactory.GetFont("Times New Roman", 14, Font.BOLD, new Color(0, 0, 0));
        Font titleFont2 = FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new Color(0, 0, 0));
        Font titleFont3 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new Color(0, 0, 0));
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont_B = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font bodyFontSmall = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/logoLife.png");

        //document.Open();

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
        //logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);

        document.Add(logo);

        document.Add(new Paragraph("\n\n\n"));

        Paragraph titleLine = new Paragraph("Sri Lanka Insurance Corporation Life Limited", titleFont3);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        Paragraph titleLine2 = new Paragraph("Online Payment Receipt", titleFont);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);

        CustProfile customer = new CustProfile(pro.username);

        document.Add(new Paragraph(pro.custName, bodyFont));
        document.Add(new Paragraph(drop_br(customer.O_addrss1 + "\n" + customer.O_addrss2 + "\n" + customer.O_addrss3 + "\n" + customer.O_addrss4), bodyFont));
        document.Add(new Paragraph("\n"));

        document.Add(new Paragraph("Dear Valued Customer,", bodyFont));

        document.Add(new Paragraph("\n"));

        string line2 = "We have received your payment of Rs. " + pro.amount.ToString("N2") + " under the reference no:  " + pro.receiptNo + " for the new proposal as follows.\n\n";
        document.Add(new Paragraph(line2, bodyFont));

        document.Add(new Paragraph("Proposal Details:", titleFont2));

        int[] clmwidths112 = { 1, 4 };

        PdfPTable tbl15 = new PdfPTable(2);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;

        tbl15.AddCell(new Phrase("Proposal Number: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.polNum, bodyFont));

        tbl15.AddCell(new Phrase("Customer's Name: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.custName, bodyFont));

        tbl15.AddCell(new Phrase("Paid Amount: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.amount.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase(" ", bodyFont));
        tbl15.AddCell(new Phrase(" ", bodyFont));

        tbl15.AddCell(new Phrase("Date of Payment: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.entryDate, bodyFont));

        document.Add(tbl15);

        // document.Add(new Paragraph("Subject to there being no claim from the date of Renewal to the date of online payment.", bodyFont_B));
        document.Add(new Paragraph("Deposit Receipt will be posted to the above address in due course.", bodyFont));
        document.Add(new Paragraph("\n\n"));
        document.Add(new Paragraph("Thanking you,", bodyFont));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("Sri Lanka Insurance Life.", bodyFont));
        document.Add(new Paragraph("\n\n\n"));
        document.Add(new Paragraph("This is a computer generated letter and no signature required.", bodyFontSmall));

        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Online_{0}.pdf", "Renewal_Receipt"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();

    }

    public void print_receipt_PolFee(LifePayment pro, string epf, string ip)
    {

        Document document = new Document(PageSize.A4, 50, 50, 50, 50);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + epf, new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        header.Border = Rectangle.NO_BORDER;
        header.Alignment = 1;
        document.Footer = header;
        document.Open();

        Font titleFont = FontFactory.GetFont("Times New Roman", 14, Font.BOLD, new Color(0, 0, 0));
        Font titleFont2 = FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new Color(0, 0, 0));
        Font titleFont3 = FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new Color(0, 0, 0));
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont_B = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font bodyFontSmall = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/logoLife.png");

        //document.Open();

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
        //logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);

        document.Add(logo);

        document.Add(new Paragraph("\n\n\n"));

        Paragraph titleLine = new Paragraph("Sri Lanka Insurance Corporation Life Limited", titleFont3);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);

        Paragraph titleLine2 = new Paragraph("Online Payment Receipt", titleFont);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);

        CustProfile customer = new CustProfile(pro.username);

        document.Add(new Paragraph(pro.custName, bodyFont));
        document.Add(new Paragraph(drop_br(customer.O_addrss1 + "\n" + customer.O_addrss2 + "\n" + customer.O_addrss3 + "\n" + customer.O_addrss4), bodyFont));
        document.Add(new Paragraph("\n"));

        document.Add(new Paragraph("Dear Valued Customer,", bodyFont));

        document.Add(new Paragraph("\n"));

        string line2 = "We have received your payment (Policy Fee) of Rs. " + pro.amount.ToString("N2") + " under the reference no:  " + pro.receiptNo + " for the a new proposal as follows.\n\n";
        document.Add(new Paragraph(line2, bodyFont));

        document.Add(new Paragraph("Proposal Details:", titleFont2));

        int[] clmwidths112 = { 1, 4 };

        PdfPTable tbl15 = new PdfPTable(2);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;

        tbl15.AddCell(new Phrase("Proposal Number: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.polNum, bodyFont));

        tbl15.AddCell(new Phrase("Customer's Name: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.custName, bodyFont));

        tbl15.AddCell(new Phrase("Paid Amount: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.amount.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase(" ", bodyFont));
        tbl15.AddCell(new Phrase(" ", bodyFont));

        tbl15.AddCell(new Phrase("Date of Payment: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.entryDate, bodyFont));

        document.Add(tbl15);

        // document.Add(new Paragraph("Subject to there being no claim from the date of Renewal to the date of online payment.", bodyFont_B));
        document.Add(new Paragraph("Deposit Receipt will be posted to the above address in due course.", bodyFont));
        document.Add(new Paragraph("\n\n"));
        document.Add(new Paragraph("Thanking you,", bodyFont));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("Sri Lanka Insurance Life.", bodyFont));
        document.Add(new Paragraph("\n\n\n"));
        document.Add(new Paragraph("This is a computer generated letter and no signature required.", bodyFontSmall));

        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Online_{0}.pdf", "Renewal_Receipt"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();




    }

    
}