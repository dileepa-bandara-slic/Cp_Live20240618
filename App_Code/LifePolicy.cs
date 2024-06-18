using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

/// <summary>
/// Summary description for LifePolicy
/// </summary>
public class LifePolicy
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);

    public string status { get; private set; }
    public string pmcode { get; private set; }
    public int ComDate { get; private set; }
    public int ComYear { get; private set; }
    public int DueDate { get; private set; }
    public int Term { get; private set; }
    public int sumAssured { get; private set; }
    public int mode { get; private set; }
    public double Premiumamt { get; private set; }
    public string PolicyStatus { get; private set; }
    public string Str_mode { get; private set; }
    public bool dis_hon { get; private set; }

    public bool master_exist { get; private set; }

    public string cus_status { get; private set; }
    public string cus_ini { get; private set; }
    public string cus_surname { get; private set; }
    public string cus_address1 { get; private set; }
    public string cus_address2 { get; private set; }
    public string cus_address3 { get; private set; }
    public string cus_address4 { get; private set; }
    public bool customer_info { get; private set; }

    public double amtpaid100 { get; private set; }

    public DataSet ds_amount_paid = new DataSet();

    public double total_amount_paid { get; private set; }
    public int total_paid_inst { get; set; }
    public int lastDue { get; private set; }
    public int lastDue_All { get; private set; }
    public int maxDueCount { get; private set; }

    public int premast { get; private set; }
    public bool premast_found { get; private set; }

    public bool lapse_found { get; private set; }

    public int MaturityYear { get; private set; }
    public int MaturityDate { get; private set; }

	public LifePolicy()
	{

	}

    public LifePolicy(long policynumber, int taxyear)
    {
        master_exist = false;
        get_status(policynumber);
        get_dis_hon(policynumber);
        get_cus_details(policynumber);
        get_lapse(policynumber);
        get_ledger_details(policynumber, Convert.ToInt32(taxyear.ToString() + "0401"), Convert.ToInt32((taxyear+1).ToString() + "0331"));
        get_max_due_count(policynumber, Convert.ToInt32(taxyear.ToString() + "0401"), Convert.ToInt32((taxyear + 1).ToString() + "0331"));
        get_ledger_details(policynumber);
        DataSet ds = new DataSet();
        string sql = "select pmcom, pmtrm, pmdue, PMSUM,PMMOD,PMPRM,PMCOD from lphs.policymaster   where pmpol = :polNo";

        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds.Clear();
            //cmd.Parameters.AddWithValue("userNam", username);
            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";


            data.SelectCommand.Parameters.Add(oppolNo);
            ds.Clear();
            data.Fill(ds);
        }
        if (ds != null)
        {
            master_exist = true;
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            try
            {
                ComDate = Convert.ToInt32(dt.Rows[0][0].ToString());
                ComYear = Convert.ToInt32(dt.Rows[0][0].ToString().Substring(0,4));
            }
            catch
            {
                ComDate = 0;
            }

            try
            {
                Term = Convert.ToInt32(dt.Rows[0][1].ToString());

                if (Term == 0)
                    Term = 99;

            }
            catch
            {
                Term = 0;
            }
            try
            {
                DueDate = Convert.ToInt32(dt.Rows[0][2].ToString());
            }
            catch
            {
                DueDate = 0;
            }



            try
            {
                sumAssured = Convert.ToInt32(dt.Rows[0][3].ToString());
            }
            catch
            {
                sumAssured = 0;
            }

            try
            {
                mode = Convert.ToInt32(dt.Rows[0][4].ToString());
            }
            catch
            {
                mode = 0;
            }

            try
            {
                Premiumamt = Convert.ToDouble(dt.Rows[0][5].ToString());
            }
            catch
            {
                Premiumamt = 0;
            }

            PolicyStatus = dt.Rows[0][6].ToString();

            try
            {
                MaturityYear = ComYear + Term;
                MaturityDate = Convert.ToInt32(MaturityYear.ToString() + (ComDate.ToString().Substring(4, 2) + "01"));
                

            }
            catch
            {
                MaturityYear = 0;
                MaturityDate = 0;
            }


            switch (mode)
            {
                case 1: Str_mode = "Annual"; break;
                case 2: Str_mode = "Half a Year"; break;
                case 3: Str_mode = "Quarterly"; break;
                case 4: Str_mode = "Monthly"; break;
                case 5: Str_mode = "Single Premium"; break;
            }

        }

    }
    
    public void get_status(long policynumber)
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        string sql = "SELECT PMCOD from LPHS.PREMAST  WHERE  PMPOL = :polNo  ";

        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds.Clear();
            
            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";


            data.SelectCommand.Parameters.Add(oppolNo);
            ds.Clear();
            data.Fill(ds);
            

        }
        premast = ds.Tables[0].Rows.Count;
        if (ds.Tables[0].Rows.Count > 0)
        {
            premast_found = true;
            status = "I";
            pmcode = ds.Tables[0].Rows[0][0].ToString().Trim();
        }
        else
        {
            premast_found = false;
            sql = "Select lpcom from lphs.liflaps  where liflaps.lppol =  :polNo  ";

            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                
                ds1.Clear();

                OracleDataAdapter data = new OracleDataAdapter();
                data.SelectCommand = cmd;

                OracleParameter oppolNo = new OracleParameter();
                oppolNo.DbType = DbType.Int64;
                oppolNo.Value = policynumber;
                oppolNo.ParameterName = "polNo";


                data.SelectCommand.Parameters.Add(oppolNo);
                
                data.Fill(ds1);
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                status = "L";
                pmcode = ds1.Tables[0].Rows[0][0].ToString().Trim();
            }
            else
            {
                status = "";
                pmcode = "";
            }
        }

    }

    public void get_cus_details(long policynumber)
    {
        DataSet ds2 = new DataSet();
        string sql = "SELECT pnsta, pnint, PNSUR, pnad1, pnad2, pnad3, pnad4 from LPHS.PHNAME_  WHERE pnpol = :polNo  ";

        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds2.Clear();

            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";


            data.SelectCommand.Parameters.Add(oppolNo);
            ds2.Clear();
            data.Fill(ds2);
        }

        if (ds2.Tables[0].Rows.Count > 0)
        {
            customer_info = true;

            cus_status =    ds2.Tables[0].Rows[0][0].ToString().Trim();
            cus_ini =       ds2.Tables[0].Rows[0][1].ToString().Trim();
            cus_surname =   ds2.Tables[0].Rows[0][2].ToString().Trim();
            cus_address1 =  ds2.Tables[0].Rows[0][3].ToString().Trim();
            cus_address2 =  ds2.Tables[0].Rows[0][4].ToString().Trim();
            cus_address3 =  ds2.Tables[0].Rows[0][5].ToString().Trim();
            cus_address4 =  ds2.Tables[0].Rows[0][6].ToString().Trim();


        }
        else
        {
            customer_info = false;
        }

    }

    public void get_ledger_details(long policynumber, int stdt, int enddt)
    {
        

        //DataSet ds2 = new DataSet();
        string sql = "SELECT sum(LLPRM),count(LLDAT),max(lldue),llpol from LCLM.LEDGER  WHERE ((LLPOL  = :polNo) AND ( LLDAT BETWEEN :stdt AND :enddt )) group by llpol ";
        //string sql = "SELECT sum(LLPRM),count(LLDAT),max(lldue),llpol from LCLM.LEDGER  WHERE ((LLPOL  = " + policynumber + ") AND ( LLDAT BETWEEN " + stdt + " AND " + enddt + " )) group by llpol ";
        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds_amount_paid.Clear();

            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";

            OracleParameter opstdt = new OracleParameter();
            opstdt.DbType = DbType.Int32;
            opstdt.Value = stdt;
            opstdt.ParameterName = "stdt";

            OracleParameter openddt = new OracleParameter();
            openddt.DbType = DbType.Int32;
            openddt.Value = enddt;
            openddt.ParameterName = "enddt";


            data.SelectCommand.Parameters.Add(oppolNo);
            data.SelectCommand.Parameters.Add(opstdt);
            data.SelectCommand.Parameters.Add(openddt);

            ds_amount_paid.Clear();
            data.Fill(ds_amount_paid);
            
        }
        
        if (ds_amount_paid.Tables[0].Rows.Count > 0)
        {
           

            try
            {
                total_amount_paid = Convert.ToDouble(ds_amount_paid.Tables[0].Rows[0][0].ToString().Trim());
            }
            catch
            {
                total_amount_paid = 0;
            }

            try
            {
                total_paid_inst = Convert.ToInt32(ds_amount_paid.Tables[0].Rows[0][1].ToString().Trim());
            }
            catch
            {
                total_paid_inst = 0;
            }

            try
            {
                lastDue = Convert.ToInt32(ds_amount_paid.Tables[0].Rows[0][2].ToString().Trim());
            }
            catch
            {
                lastDue = 0;
            }
            amtpaid100 = (Math.Round(total_amount_paid, 2) * 100);
            
        }
        else
        {
        }

    }

    public void get_ledger_details(long policynumber)
    {
        DataSet ds2 = new DataSet();
        string sql = "Select max(lldue) from lclm.ledger   where LLPOL  = :polNo ";

        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds2.Clear();

            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";



            data.SelectCommand.Parameters.Add(oppolNo);

            ds2.Clear();
            data.Fill(ds2);
        }

        if (ds2.Tables[0].Rows.Count > 0)
        {

            try
            {
                lastDue_All = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString().Trim());
            }
            catch
            {
                lastDue_All = 0;
            }

        }
        else
        {
        }

    }

    public void get_dis_hon(long policynumber)
    {
        DataSet ds = new DataSet();
        string sql = "select POLNO  from LPAY.DISHONOR_CHEQUE_MAINFRAME    where POLNO = :polNo and DISHONPRO=0  ";

        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds.Clear();

            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";


            data.SelectCommand.Parameters.Add(oppolNo);
            ds.Clear();
            data.Fill(ds);

        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            dis_hon = true;
        }
        else
        {
            dis_hon = false;
        }
    }

    public void get_max_due_count(long policynumber, int stdt, int enddt)
    {
        DataSet ds2 = new DataSet();
        string sql = "select  count(*) from lclm.ledger   where llpol= :polNo and ( lldue||'02' BETWEEN  :stdt AND :enddt )";

        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds2.Clear();

            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";

            OracleParameter opstdt = new OracleParameter();
            opstdt.DbType = DbType.Int32;
            opstdt.Value = stdt;
            opstdt.ParameterName = "stdt";

            OracleParameter openddt = new OracleParameter();
            openddt.DbType = DbType.Int32;
            openddt.Value = enddt;
            openddt.ParameterName = "enddt";

            data.SelectCommand.Parameters.Add(oppolNo);
            data.SelectCommand.Parameters.Add(opstdt);
            data.SelectCommand.Parameters.Add(openddt);

            ds2.Clear();
            data.Fill(ds2);
        }

        if (ds2.Tables[0].Rows.Count > 0)
        {

            try
            {
                maxDueCount = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString().Trim());
            }
            catch
            {
                maxDueCount = 0;
            }

        }
        else
        {
         
        }
    }

    public void get_lapse(long policynumber)
    {
        DataSet ds = new DataSet();
        string sql = "Select LPPOL from lphs.liflaps  WHERE LPPOL = :polNo";

        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds.Clear();

            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";


            data.SelectCommand.Parameters.Add(oppolNo);
            ds.Clear();
            data.Fill(ds);

        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            lapse_found = true;
        }
        else
        {
            lapse_found = false;
        }
    }

    public void print_tax(string epf, string ip, string your_ref, int taxYear, double Tot_Amt_TobePaid, DataTable dt)
    {
        Document document = new Document(PageSize.A4, 50, 50, 10, 10);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        
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
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);


        int rowCount = 0;
        //string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/health_logo.png");




        //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        //logo.ScalePercent(25f);
        //logo.SetAbsolutePosition(50, 770);

        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo_Life.gif");


        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);
        logo.ScalePercent(25f);
        logo.SetAbsolutePosition(260, 750);
       // document.Add(logo);


        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        watermark.SetAbsolutePosition(65, 170);
        //document.Add(watermark);

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        document.Add(logo);

        document.Add(new Paragraph("\n\n\n\n"));
        //document.Add(new Paragraph("SRI LANKA INSURANCE HEALTH ANNUAL MEDICAL PLAN", titleFont1));

        int[] clmwidths111 = { 10, 10, 10 };

        PdfPTable tbl14 = new PdfPTable(3);

        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 100;
        tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl14.SpacingBefore = 10;
        tbl14.SpacingAfter = 20;
        tbl14.DefaultCell.Border = 0;

        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));

        PdfPCell cell = new PdfPCell(new Phrase("Our Ref : " + your_ref, bodyFont));
        cell.HorizontalAlignment = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));

        //cell = new PdfPCell(new Phrase("Our Ref : " + epf, bodyFont));
        //cell.HorizontalAlignment = 2;
        //cell.Border = 0;
        //tbl14.AddCell(cell);

        //tbl14.AddCell(new Phrase(" ", bodyFont));
        //tbl14.AddCell(new Phrase(" ", bodyFont));

        cell = new PdfPCell(new Phrase("Date : " + DateTime.Today.ToString("yyyy/MM/dd"), bodyFont));
        cell.HorizontalAlignment = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));

        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));
        tbl14.AddCell(new Phrase(" ", bodyFont));


        Chunk ch1 = new Chunk("Premium Payment Certificate For Purpose of Income Tax", bodyFont_bold);
        ch1.SetUnderline(0.5f, -1.5f);
        Phrase ph = new Phrase();
        ph.Add(ch1);

        cell = new PdfPCell(ph);
        cell.Colspan = 3;
        cell.Border = 0;
        cell.HorizontalAlignment = 1;
        tbl14.AddCell(cell);

        document.Add(tbl14);
        
        string amtintxt = "";

        if (total_amount_paid != 0)
        {
            string paid = amtpaid100.ToString().Substring(0, (amtpaid100.ToString().Length - 2)) + "." + amtpaid100.ToString().Substring((amtpaid100.ToString().Length - 2), 2);

            ReadAmount txtread = new ReadAmount();
            amtintxt = txtread.readAmount(paid, "Sri Lankan Rupees", "Cts");

        }
        else
            amtintxt = " ";

        document.Add(new Paragraph("This is to certify that " + cus_status + " " + cus_ini + " " + cus_surname + " is the assured under the Life Policy listed in the schedule below during the period " + taxYear.ToString() + "/04/01 TO " + (taxYear + 1).ToString() + "/03/31 an amount of " + amtintxt + " (Rs. " + total_amount_paid.ToString("N2") + ") has been paid by the way of premium.", bodyFont));
        
        if (lapse_found == false)
        {

            if (Tot_Amt_TobePaid != 0)
            {
                double amt100 = double.Parse(Tot_Amt_TobePaid.ToString());
                amt100 = (Math.Round(amt100, 2) * 100);

                int[] clmwidths0 = { 1};

                PdfPTable tbl10 = new PdfPTable(1);

                tbl10.SetWidths(clmwidths0);

                tbl10.WidthPercentage = 100;
                tbl10.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl10.SpacingBefore = 10;
                tbl10.SpacingAfter = 10;
                tbl10.DefaultCell.Border = 0;

                PdfPCell cel0 = new PdfPCell(new Phrase("and", bodyFont));
                cel0.HorizontalAlignment = 1;
                cel0.Border = 0;
                tbl10.AddCell(cel0);
                document.Add(tbl10);

                string amttobepaid1 = amt100.ToString().Substring(0, (amt100.ToString().Length - 2)) + "." + amt100.ToString().Substring((amt100.ToString().Length - 2), 2);
                ReadAmount txtreadtoPay = new ReadAmount();
                string tobePaidText = txtreadtoPay.readAmount(amttobepaid1, "Sri Lankan Rupees", "Cts");

                document.Add(new Paragraph(tobePaidText + "  ( Rs. " + amttobepaid1 + " ) to be payable by the way of premium.", bodyFont));
                 
            }
            else
            {
 
            }
        }
        else
        {
 
        }

        int[] clmwidths = { 10, 11, 11, 13, 10, 5 };

        PdfPTable tbl1 = new PdfPTable(6);

        tbl1.SetWidths(clmwidths);

        tbl1.WidthPercentage = 100;
        tbl1.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl1.SpacingBefore = 20;
        tbl1.SpacingAfter = 10;
        tbl1.DefaultCell.Border = 1;

        PdfPCell cel = new PdfPCell(new Phrase("Policy No.", bodyFont_bold_sm));
        cel.HorizontalAlignment = 1;
        cel.Colspan = 1;
        cel.Border = 0;
        cel.BackgroundColor = new Color(180, 180, 180);
        tbl1.AddCell(cel);

        cel = new PdfPCell(new Phrase("Sum Assured Rs.", bodyFont_bold_sm));
        cel.HorizontalAlignment = 1;
        cel.Colspan = 1;
        cel.Border = 0;
        cel.BackgroundColor = new Color(180, 180, 180);
        cel.BorderColor = new Color(255, 255, 255);
        cel.BorderWidthLeft = 0.5f;
        tbl1.AddCell(cel);

        cel = new PdfPCell(new Phrase("Mode", bodyFont_bold_sm));
        cel.HorizontalAlignment = 1;
        cel.Colspan = 1;
        cel.Border = 0;
        cel.BackgroundColor = new Color(180, 180, 180);
        cel.BorderColor = new Color(255, 255, 255);
        cel.BorderWidthLeft = 0.5f;
        tbl1.AddCell(cel);

        cel = new PdfPCell(new Phrase("Premium Installment Rs.", bodyFont_bold_sm));
        cel.HorizontalAlignment = 1;
        cel.Colspan = 1;
        cel.Border = 0;
        cel.BackgroundColor = new Color(180, 180, 180);
        cel.BorderColor = new Color(255, 255, 255);
        cel.BorderWidthLeft = 0.5f;
        tbl1.AddCell(cel);

        cel = new PdfPCell(new Phrase("No.of Installments", bodyFont_bold_sm));
        cel.HorizontalAlignment = 1;
        cel.Colspan = 1;
        cel.Border = 0;
        cel.BackgroundColor = new Color(180, 180, 180);
        cel.BorderColor = new Color(255, 255, 255);
        cel.BorderWidthLeft = 0.5f;
        tbl1.AddCell(cel);

        cel = new PdfPCell(new Phrase(" ", bodyFont_bold_sm));
        cel.HorizontalAlignment = 1;
        cel.Colspan = 1;
        cel.Border = 0;
        cel.BackgroundColor = new Color(180, 180, 180);
        cel.BorderColor = new Color(255, 255, 255);
        cel.BorderWidthLeft = 0.5f;
        tbl1.AddCell(cel);

        foreach (DataRow dr in dt.Rows)
        {
            cel = new PdfPCell(new Phrase(dr[0].ToString(), bodyFont2));
            cel.HorizontalAlignment = 1;
            cel.Colspan = 1;
            cel.Border = 1;
            cel.BorderColor = new Color(180, 180, 180);
            cel.BorderWidthTop = 0;
            cel.BorderWidthBottom = 0.5f;
            cel.BorderWidthLeft = 0.5f;
            tbl1.AddCell(cel);

            cel = new PdfPCell(new Phrase(dr[1].ToString(), bodyFont2));
            cel.HorizontalAlignment = 1;
            cel.Colspan = 1;
            cel.Border = 1;
            cel.BorderColor = new Color(180, 180, 180);
            cel.BorderWidthTop = 0;
            cel.BorderWidthBottom = 0.5f;
            cel.BorderWidthLeft = 0.5f;
            tbl1.AddCell(cel);

            cel = new PdfPCell(new Phrase(dr[2].ToString(), bodyFont2));
            cel.HorizontalAlignment = 1;
            cel.Colspan = 1;
            cel.Border = 1;
            cel.BorderColor = new Color(180, 180, 180);
            cel.BorderWidthTop = 0;
            cel.BorderWidthBottom = 0.5f;
            cel.BorderWidthLeft = 0.5f;
            tbl1.AddCell(cel);

            cel = new PdfPCell(new Phrase(dr[3].ToString(), bodyFont2));
            cel.HorizontalAlignment = 1;
            cel.Colspan = 1;
            cel.Border = 1;
            cel.BorderColor = new Color(180, 180, 180);
            cel.BorderWidthTop = 0;
            cel.BorderWidthBottom = 0.5f;
            cel.BorderWidthLeft = 0.5f;
            tbl1.AddCell(cel);

            cel = new PdfPCell(new Phrase(dr[4].ToString(), bodyFont2));
            cel.HorizontalAlignment = 1;
            cel.Colspan = 1;
            cel.Border = 1;
            cel.BorderColor = new Color(180, 180, 180);
            cel.BorderWidthTop = 0;
            cel.BorderWidthBottom = 0.5f;
            cel.BorderWidthLeft = 0.5f;
            tbl1.AddCell(cel);

            cel = new PdfPCell(new Phrase(dr[5].ToString(), bodyFont2));
            cel.HorizontalAlignment = 1;
            cel.Colspan = 1;
            cel.Border = 1;
            cel.BorderColor = new Color(180, 180, 180);
            cel.BorderWidthTop = 0;
            cel.BorderWidthBottom = 0.5f;
            cel.BorderWidthLeft = 0.5f;
            cel.BorderWidthRight = 0.5f;
            tbl1.AddCell(cel);
        }

        document.Add(tbl1);

        document.Add(new Paragraph("**    Paid Details", bodyFont));
        document.Add(new Paragraph("*     To be Paid Details", bodyFont));


        document.Add(new Paragraph("\n\n", bodyFont));


        

        //document.Add(new Paragraph("Yours Faithfully,\n\n\n\n", bodyFont));
        //document.Add(new Paragraph("------------------------\n\n", bodyFont));

        //document.Add(new Paragraph("For Life Manager.", bodyFont));
        //document.Add(new Paragraph("Sri Lanka Insurance\n\n", bodyFont));

        if (lapse_found == false)
        {

            if (Tot_Amt_TobePaid != 0)
            {
                
            }
            else
            {
                document.Add(new Paragraph("\n\n\n\n", bodyFont));
            }
        }
        else
        {
            document.Add(new Paragraph("\n\n\n\n", bodyFont));
        }

        document.Add(new Paragraph("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n", bodyFont));

        document.Add(new Paragraph("\n\nNote : This is a computer generated document and requires no signature.\n\n", bodyFont));

        document.Add(new Paragraph("Certificate issued on : "+DateTime.Today.ToString("yyyy/MM/dd"), bodyFont));

        document.Add(new Paragraph(cus_status+" "+cus_ini+" "+cus_surname, bodyFont));
        document.Add(new Paragraph(cus_address1, bodyFont));
        document.Add(new Paragraph(cus_address2, bodyFont));
        document.Add(new Paragraph(cus_address3, bodyFont));
        document.Add(new Paragraph(cus_address4, bodyFont));

        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Tax_{0}.pdf", "Cirtificate"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
 
    }

    public string return_status(long policynumber)
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        string sql = "SELECT PMCOD from LPHS.PREMAST  WHERE  PMPOL = :polNo  ";
        string polStatus = "";
        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {
            ds.Clear();

            OracleDataAdapter data = new OracleDataAdapter();
            data.SelectCommand = cmd;

            OracleParameter oppolNo = new OracleParameter();
            oppolNo.DbType = DbType.Int64;
            oppolNo.Value = policynumber;
            oppolNo.ParameterName = "polNo";


            data.SelectCommand.Parameters.Add(oppolNo);
            ds.Clear();
            data.Fill(ds);


        }
        premast = ds.Tables[0].Rows.Count;
        if (ds.Tables[0].Rows.Count > 0)
        {
            premast_found = true;
            polStatus = "I";
            pmcode = ds.Tables[0].Rows[0][0].ToString().Trim();
        }
        else
        {
            premast_found = false;
            sql = "Select lpcom from lphs.liflaps  where liflaps.lppol =  :polNo  ";

            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {

                ds1.Clear();

                OracleDataAdapter data = new OracleDataAdapter();
                data.SelectCommand = cmd;

                OracleParameter oppolNo = new OracleParameter();
                oppolNo.DbType = DbType.Int64;
                oppolNo.Value = policynumber;
                oppolNo.ParameterName = "polNo";


                data.SelectCommand.Parameters.Add(oppolNo);

                data.Fill(ds1);
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                polStatus = "L";
                pmcode = ds1.Tables[0].Rows[0][0].ToString().Trim();
            }
            else
            {
                polStatus = "";
                pmcode = "";
            }
        }

        return polStatus;

    }

}