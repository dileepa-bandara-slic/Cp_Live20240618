using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using iTextSharp.text;
using System.IO;
//using iTextSharp.text.pdf;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;


/// <summary>
/// Summary description for Print_pdf
/// </summary>
public class Print_pdf
{


    public Print_pdf()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void print_receipt(Proposal pro , string epf, string ip)
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
        Font bodyFont_bold = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);


        string root = System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/slic_gen_Logo.png");


       // document.Open();

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
        //logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);


        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/Gi_Watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);
        
        
        //document.Add(logo);


        document.Add(new Paragraph("\n"));
        
        Paragraph titleLine = new Paragraph("Sri Lanka Insurance Corporation General (Ltd)", titleFont3);
        titleLine.SetAlignment("Center");
       // document.Add(titleLine);

        Paragraph titleLine2 = new Paragraph("Online Payment Confirmation", titleFont);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);


        //document.Add(new Paragraph(drop_br(pro.getFullAddress()), bodyFont));

        document.Add(new Paragraph("\n\n"));

        document.Add(new Paragraph("Dear Customer,\n\n", bodyFont));

        string line2 = "Your payment of Rs. "+pro.annualPremium.ToString("N2")+" under the reference no:  "+pro.refNo+" regarding premium payment for "+pro.product_Name + " (Plan - " + pro.plan + ") policy has been received.\n";
        document.Add(new Paragraph(line2, bodyFont));

        document.Add(new Paragraph("\nPolicy Details:", bodyFont_bold));

        int[] clmwidths112 = { 1,4 };

        PdfPTable tbl15 = new PdfPTable(2);

        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;

        tbl15.AddCell(new Phrase("Policy No: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.policy_no, bodyFont));

        tbl15.AddCell(new Phrase("Policy Type: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.product_Name + " (Plan - " + pro.plan + ")", bodyFont));

        if (pro.refNo.Contains("G/999/TPI/"))
        {
            tbl15.AddCell(new Phrase("Sum Assured (USD): ", bodyFont));
            tbl15.AddCell(new Phrase(pro.sumAssured.ToString("N2"), bodyFont));
        }
        else
        {
            tbl15.AddCell(new Phrase("Sum Assured (Rs.): ", bodyFont));
            tbl15.AddCell(new Phrase(pro.sumAssured.ToString("N2"), bodyFont));
        }
        /*
        tbl15.AddCell(new Phrase("Sum Assured (Rs.): ", bodyFont));
        tbl15.AddCell(new Phrase(pro.sumAssured.ToString("N2"), bodyFont));*/

        tbl15.AddCell(new Phrase("Customer Name: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.fullName, bodyFont));

        tbl15.AddCell(new Phrase("Premium (Rs.): ", bodyFont));
        tbl15.AddCell(new Phrase(pro.annualPremium.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase("Cover period:", bodyFont));
        tbl15.AddCell(new Phrase(pro.getCoverPeriod(), bodyFont));

        tbl15.AddCell(new Phrase(" ", bodyFont));
        tbl15.AddCell(new Phrase(" ", bodyFont));

        tbl15.AddCell(new Phrase("Date of Payment:", bodyFont));
        tbl15.AddCell(new Phrase(pro.entryDate, bodyFont));

        document.Add(tbl15);

        if ((pro.refNo.Contains("G/999/GTI/")) || (pro.refNo.Contains("G/999/TPI/")))
        {
            document.Add(new Paragraph("Your payment receipt will be posted to the registered address in due course.", bodyFont));
            document.Add(new Paragraph("If you are required with a physical policy document, please contact us.", bodyFont));
        }
        else if (pro.refNo.Contains("G/999/MP/")) // product change from AMP
        {
            //document.Add(new Paragraph("Policy schedule will be posted to the above address in due course.", bodyFont));
            document.Add(new Paragraph("Medi Plus card will be sent by post to the given address within 15 working days.", bodyFont));
        }

        document.Add(new Paragraph("The Policy is valid only if the bank transfer is successful.", bodyFont));
        document.Add(new Paragraph("\n\nThanking you,", bodyFont));
        document.Add(new Paragraph("Sri Lanka Insurance Corporation General (Ltd).", bodyFont));



        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Online_Payment_{0}.pdf", "Receipt"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
        

       
 
    }

    public void print_receipt(Renewal pro, string epf, string ip)
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


        string root = System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/slic_gen_Logo.png");


        //document.Open();

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
       // logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/Gi_Watermark.gif"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);



        //document.Add(logo);


        document.Add(new Paragraph("\n\n"));

        //Paragraph titleLine = new Paragraph("Sri Lanka Insurance Corporation", titleFont3);
        //titleLine.SetAlignment("Center");
        //document.Add(titleLine);

        Paragraph titleLine2 = new Paragraph("Online Payment Confirmation", titleFont);
        titleLine2.SetAlignment("Center");
        document.Add(titleLine2);


        //document.Add(new Paragraph(drop_br(pro.address), bodyFont));

        document.Add(new Paragraph("\n\n"));
        document.Add(new Paragraph("Dear Customer,", bodyFont));

        document.Add(new Paragraph("\n"));

        string line2 = "Your payment of Rs. " + pro.amount.ToString("N2") + " under the reference no:  " + pro.receiptNo + " regarding premium payment for " + pro.polTypName + " policy has been received.\n";
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
        tbl15.AddCell(new Phrase(pro.polNum , bodyFont));

        tbl15.AddCell(new Phrase("Sum Assured (Rs.): ", bodyFont));
        tbl15.AddCell(new Phrase(pro.sumAssurd.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase("Customer Name: ", bodyFont));
        tbl15.AddCell(new Phrase(pro.custName, bodyFont));

        tbl15.AddCell(new Phrase("Premium (Rs.): ", bodyFont));
        tbl15.AddCell(new Phrase(pro.amount.ToString("N2"), bodyFont));

        tbl15.AddCell(new Phrase("Cover period:", bodyFont));

        if (pro.dept == "M")
            tbl15.AddCell(new Phrase("From: " + pro.startDate + " to: " + pro.endDate, bodyFont));
        else
            tbl15.AddCell(new Phrase("Starts From: " + pro.startDate, bodyFont));

        tbl15.AddCell(new Phrase(" ", bodyFont));
        tbl15.AddCell(new Phrase(" ", bodyFont));

        tbl15.AddCell(new Phrase("Date of Payment:", bodyFont));
        tbl15.AddCell(new Phrase(pro.entryDate, bodyFont));

        document.Add(tbl15);

        document.Add(new Paragraph("Please note that if any claim is made prior to the renewal date of the policy, there will be a revision in the premium paid.", bodyFont_B));
        document.Add(new Paragraph("Renewal receipt will be posted to the address in the policy document in due course.", bodyFont));
        document.Add(new Paragraph("Renewal is valid only if the bank transfer is successful.", bodyFont));
        document.Add(new Paragraph("Thanking you,", bodyFont));
        document.Add(new Paragraph("Sri Lanka Insurance.", bodyFont));



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


        Document document = new Document(PageSize.A4, 50, 50, 50,30);

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


        string root = System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/slic_gen_Logo.png");

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(50f);
        //logo.SetAbsolutePosition(260, 750);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/General/GenImages/slic_gen_Logo.png"));
        //watermark.SetAbsolutePosition(65, 170);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);
        
        document.Add(logo);
        
        document.Add(new Paragraph("\n\n\n"));

        Paragraph titleLine = new Paragraph("Request to view life policy details", titleFont2);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);
        document.Add(new Paragraph("\n\n"));
        document.Add(new Paragraph("The Manager\nLife PHS\nSri Lanka Insurance\n\n\nDear Sir,", bodyFont));

        PdfPTable tbl15 = new PdfPTable(2);

        int[] clmwidths112 = { 2, 5 };
        tbl15.SetWidths(clmwidths112);

        tbl15.WidthPercentage = 100;
        tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl15.SpacingBefore = 10;
        tbl15.SpacingAfter = 10;
        tbl15.DefaultCell.Border = 0;
        

        

        tbl15.AddCell(new Phrase("Name of the Customer", bodyFont));
        tbl15.AddCell(new Phrase(customer.O_firstName+" "+customer.O_lastName, bodyFont));

        tbl15.AddCell(new Phrase("Address", bodyFont));
        string address = customer.O_addrss1 + ", " + customer.O_addrss2 + (String.IsNullOrEmpty(customer.O_addrss3) ? "" : ", " + customer.O_addrss3) + (String.IsNullOrEmpty(customer.O_addrss4) ? "" : ", " + customer.O_addrss4) + (String.IsNullOrEmpty(customer.O_cityTown) ? "" : ", " + customer.O_cityTown) + (String.IsNullOrEmpty(customer.O_postCode) ? "" : ", " + customer.O_postCode)+".";
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

        document.Add(new Paragraph("This request is made under the terms, conditions and indemnity of my original Sri Lanka Insurance User Account.", bodyFont));
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
    
}