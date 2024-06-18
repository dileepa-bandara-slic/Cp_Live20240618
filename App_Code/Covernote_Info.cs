using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for Covernote_Info
/// </summary>
public class Covernote_Info
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

    public string Cus_name { get; private set; }
    public double SA { get; private set; }
    public string policy_start_date { get; private set; }
    public string policy_end_date { get; private set; }

    public string rcc_tag { get; private set; }
    public string tc_tag { get; private set; }
    public string vehicleNo { get; private set; }
    public int cylinder_capacity { get; private set; }
    public string address1 { get; private set; }
    public string address2 { get; private set; }
    public string address3 { get; private set; }
    public string address4 { get; private set; }

    public string province { get; private set; }


	public Covernote_Info()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Covernote_Info(string polno)
    {
        string getPolicyDetls = "Select  a.NAME, a.PMSUM AS SUM_ASSURD, to_char(a.PMDST, 'yyyy/mm/dd') AS START_DATE," +
                                                    " to_char(a.PMDEX, 'yyyy/mm/dd') AS END_DATE, a.PMVE2 AS VEHI_NUM, a.PMPRM AS PREMIUM" +
                                                    " from SLIC_CNOTE.PAYFLE_CNOTE_RENEWAL a" +
                                                    " where a.PMDEX = (Select max(PMDEX) from SLIC_CNOTE.PAYFLE_CNOTE_RENEWAL where PMPOL = :polNo)" +
                                                    " and a.PMPOL = :polNo2";
        bool i = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            using (OracleCommand cmd2 = new OracleCommand(getPolicyDetls, oconn))
            {
                cmd2.Parameters.AddWithValue("polNo", polno);
                cmd2.Parameters.AddWithValue("polNo2", polno);
                OracleDataReader polDetReader = cmd2.ExecuteReader();
                while (polDetReader.Read())
                {
                    i = true;
                    if (!polDetReader.IsDBNull(0))
                    {
                        Cus_name = polDetReader.GetString(0);
                    }
                    if (!polDetReader.IsDBNull(1))
                    {
                        SA = Convert.ToDouble(polDetReader.GetDouble(1));
                    }

                    policy_start_date = polDetReader.GetString(2);
                    policy_end_date = polDetReader.GetString(3);

                    
                }
            }
        }
        catch(Exception e)
        {
            log lg = new log();
            lg.write_log("Covernote details info : "+e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        if (i)
            get_other_info(polno, policy_end_date);

    }

    private void get_other_info(string polno, string expire_date)
    {

        bool recordFound = false;
        string getPolicyDetls = "select rcc_premium, tc_premium, vehical_number, cylinder_capacity, address1, address2, address3, address4, province from  mcomp.mc_renewal_notice where policy_number = :polNum and policy_end_date = to_date( :expDate, 'yyyy/mm/dd')";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            using (OracleCommand cmd2 = new OracleCommand(getPolicyDetls, oconn))
            {
                cmd2.Parameters.AddWithValue("polNum", polno);
                cmd2.Parameters.AddWithValue("expDate", expire_date);

                OracleDataReader polDetReader = cmd2.ExecuteReader();

                while (polDetReader.Read())
                {
                    recordFound = true;
                    if (!polDetReader.IsDBNull(0))
                    {
                        if (polDetReader.GetDouble(0) > 0)
                        {
                            rcc_tag = "Y";
                        }
                        else
                        {
                            rcc_tag = "N";
                        }
                    }
                    else
                    {
                        rcc_tag = "N";
                    }


                    if (!polDetReader.IsDBNull(1))
                    {
                        if (polDetReader.GetDouble(1) > 0)
                        {
                            tc_tag = "Y";
                        }
                        else
                        {
                            tc_tag = "N";
                        }
                    }
                    else
                    {
                        tc_tag = "N";
                    }

                    vehicleNo = polDetReader.GetString(2);

                    if (!polDetReader.IsDBNull(3))
                    {
                        try
                        {
                            cylinder_capacity = polDetReader.GetInt32(3);
                        }
                        catch
                        {
                            cylinder_capacity = 0;
                        }
                    }
                    else
                    {
                        cylinder_capacity = 0;
                    }

                    address1 = polDetReader.GetString(4);
                    address2 = polDetReader.GetString(5);
                    address3 = polDetReader.GetString(6);
                    address4 = polDetReader.GetString(7);

                    province = polDetReader.GetString(8);
                }
            }

            if (!recordFound)
            {
                DateTime startDate = DateTime.ParseExact(expire_date, "yyyy/mm/dd", System.Globalization.CultureInfo.InvariantCulture).AddDays(1).AddYears(-1);
                int policy_year = Convert.ToInt16(startDate.ToString("yyyy"));

                string getPolDetails2 = "select MVPROVINCE, MVVCC, MVVEHINO from  mcomp.mcactuvdet where MVPOLNO = :polNum and MVYEAR = :polYear";

                using (OracleCommand cmd3 = new OracleCommand(getPolDetails2, oconn))
                {
                    cmd3.Parameters.AddWithValue("polNum", polno);
                    cmd3.Parameters.AddWithValue("polYear", policy_year);

                    OracleDataReader polDet2Reader = cmd3.ExecuteReader();

                    while (polDet2Reader.Read())
                    {
                        if (!polDet2Reader.IsDBNull(0))
                        {
                            province = polDet2Reader.GetString(0);
                        }

                        if (!polDet2Reader.IsDBNull(1))
                        {
                            try
                            {
                                cylinder_capacity = polDet2Reader.GetInt32(1);
                            }
                            catch
                            {
                                cylinder_capacity = 0;
                            }
                        }
                        else
                        {
                            cylinder_capacity = 0;
                        }
                        if (!polDet2Reader.IsDBNull(2))
                        {
                            vehicleNo = polDet2Reader.GetString(2);
                        }

                    }
                }

                string getPolDetails3 = "select PDRCC, PDTC from mcomp.MCACTUPDET where PDREFER = :polNum and PDYEAR = :polYear";

                using (OracleCommand cmd4 = new OracleCommand(getPolDetails3, oconn))
                {
                    cmd4.Parameters.AddWithValue("polNum", polno);
                    cmd4.Parameters.AddWithValue("polYear", policy_year);

                    OracleDataReader polDet3Reader = cmd4.ExecuteReader();

                    while (polDet3Reader.Read())
                    {
                        if (!polDet3Reader.IsDBNull(0))
                        {
                            if (polDet3Reader.GetDouble(0) > 0)
                            {
                                rcc_tag = "Y";
                            }
                            else
                            {
                                rcc_tag = "N";
                            }
                        }
                        else
                        {
                            rcc_tag = "N";
                        }


                        if (!polDet3Reader.IsDBNull(1))
                        {
                            if (polDet3Reader.GetDouble(1) > 0)
                            {
                                tc_tag = "Y";
                            }
                            else
                            {
                                tc_tag = "N";
                            }
                        }
                        else
                        {
                            tc_tag = "N";
                        }

                    }
                }

                string getPolDetails4 = "select pmad1, pmad2, pmad3, pmad4 from genpay.payfle where pmpol = :polNum and pmdex = to_date(:expDate, 'yyyy/mm/dd')";

                using (OracleCommand cmd5 = new OracleCommand(getPolDetails4, oconn))
                {
                    cmd5.Parameters.AddWithValue("polNum", polno);
                    cmd5.Parameters.AddWithValue("expDate", expire_date);

                    OracleDataReader polDet4Reader = cmd5.ExecuteReader();

                    while (polDet4Reader.Read())
                    {
                        address1 = polDet4Reader.GetString(0);
                        address2 = polDet4Reader.GetString(1);
                        address3 = polDet4Reader.GetString(2);
                        address4 = polDet4Reader.GetString(3);

                    }
                }

            }
        }
        catch (Exception e)
        {
            log lg = new log();
            lg.write_log("Covernote details info : " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

    }


}