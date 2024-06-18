using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for TRV_PropConfirm
/// </summary>
public class TRV_PropConfirm
{
    public string quotNo { get; set; }
    public string destination { get; set; }
    public string departdate { get; set; }
    public string returndate { get; set; }
    public string visitcntr1 { get; set; }
    public string visitcntr2 { get; set; }
    public string visitcntr3 { get; set; }
    public string visitcntr4 { get; set; }
    public string travelPurpose { get; set; }
    public string cntcName { get; set; }
    public string cntcadd1 { get; set; }
    public string cntcadd2 { get; set; }
    public string cntcadd3 { get; set; }
    public string cntcadd4 { get; set; }
    public string cntcno1 { get; set; }
    public string cntcno2 { get; set; }
    public string trvtype { get; set; }
    public string fullName { get; set; }
    public string add1 { get; set; }
    public string add2 { get; set; }
    public string add3 { get; set; }
    public string add4 { get; set; }
    public string  mobileNo { get; set; }
    public string plan { get; set; }
    public string title { get; set; }
    public int noofPerson { get; set; } 
    public double netPrmUSD { get; set; }
    public double adminFee { get; set; }
    public double polFee { get; set; }
    public double nbtAmt { get; set; }
    public double vatAmt { get; set; }
    public double finalPrmRS { get; set; }
    public string enteredDate { get; set; }
    public double netPrmRs { get; set; }
    public double noofDays { get; set; }
    public double suminsUSD { get; set; }
    public double usdRate { get; set; }
    public string ipAddress { get; set; }
    public int agtcode  { get; set; }
    public string currType { get; set; }
    public string polType { get; set; }
    public string polsDate { get; set; }
    public string poleDate { get; set; }
    public double finalPrmUSD { get; set; }
    public string entryUser { get; set; }
    public double discRate { get; set; }
    public double discAmt { get; set; }
    public double taxesexp { get; set; }
    public string email { get; set; }
    public string nic { get; set; }
    public string username { get; set; }
    public string passportno { get; set; }

    public string polno { get; set; }

    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public TRV_PropConfirm()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool confirm_TRV_proposal(TRV_PropConfirm propcnf, GridView gvMembers,DataTable dtMembers)
    {
        bool returnValue = false;
        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        OracleCommand cmd = oconn.CreateCommand();
        OracleTransaction trans = oconn.BeginTransaction();
        cmd.Transaction = trans;
        try
        {
            
            string adrs1 = "";
            string adrs2 = "";
            string adrs3 = "";
            string adrs4 = "";

            propcnf.add1 = add1.Replace("<br/>&nbsp;&nbsp", "#");
            string[] adrsArray = new string[4];
            adrsArray = propcnf.add1.Split('#');
            if (adrsArray.Length > 0)
            {
                adrs1 = adrsArray[0];
            }
            
           if (adrsArray.Length > 1)
            {
                adrs2 = adrsArray[1];
            }
            if (adrsArray.Length > 2)
            {
                adrs3 = adrsArray[2];
            }
            if (adrsArray.Length > 3)
            {
                adrs4 = adrsArray[3];
            }
            if (!String.IsNullOrEmpty(propcnf.add2))
            {
                adrs2 = propcnf.add2;
            }
            if (!String.IsNullOrEmpty(propcnf.add3))
            {
                adrs3 = propcnf.add3;
            }
            if (!String.IsNullOrEmpty(propcnf.add4))
            {
                adrs4 = propcnf.add4;
            }

            using (cmd)
            {


                #region Update PropMast
                string instPropDetails = "Insert into SLIGEN.TRV_PROP_MAST(REFNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3,VISIT_CTRY4," +
                                                                    " TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4," +
                                                                    "CONTCT_NO1,CONTCT_NO2,TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4," +
                                                                    " MOBILE_NUMBER ,PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE,NET_PREMIUM_USD,ADMIN_FEE_RS,POLICY_FEE_RS," +
                                                                    "NBT_RS,VAT_RS,FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE," +
                                                                    "FINAL_PREMIUM_USD,ENTERED_BY,AGENT_CODE,CURR_TYPE,BRANCH,DISC_RATE,DISC_AMNT," +
                                                                    " POL_TYPE, POL_SDATE,POL_EDATE,IP_ADDRESS)" +
                                                                " VALUES( :refNo, :dest, :deptdate,:retdate,  :visitcntry1 , :visitcntry2, :visitcntry3 , :visitcntry4," +
                                                                "  :travelprpose, :cntcname, :cntcadd1, :cntcadd2, :cntcadd3, :cntcadd4," +
                                                                "  :cntcno1, :cntcno2, :trvtype, :fullName,:add1, :add2, :add3, :add4, " +
                                                                "   :mobileNo , :plan , :title, :noofPerson, 0,:netPrmUSD, :adminFee, :polFee," +
                                                                "   :nbtAmt, :vatAmt, :finalPrmRS, :taxesexp,sysdate, :netPrmRs, :noofDays, :suminsUSD, :usdRate," +
                                                                "   :finalPrmUSD, :entryUser, :agtcode , :currType, 999 , :discRate, :discAmt," +
                                                                "   :polType, :polsdate, :poledate, :ipAddress)";//, :deptdate, :retdate



                cmd.CommandText = instPropDetails;
                
                OracleParameter oRefNo = new OracleParameter();
                oRefNo.DbType = DbType.String;
                oRefNo.Value = propcnf.quotNo;
                oRefNo.ParameterName = "refNo";
               
                OracleParameter oDest = new OracleParameter();
                oRefNo.DbType = DbType.String;
                oDest.Value = propcnf.destination;
                oDest.ParameterName = "dest";

                OracleParameter oDeptdate = new OracleParameter();
                oDeptdate.DbType = DbType.DateTime;
                oDeptdate.Value = DateTime.Parse(propcnf.departdate.ToString()).ToString("yyyy-MM-dd");
                oDeptdate.ParameterName = "deptdate";

                OracleParameter oRetndate = new OracleParameter();
                oRetndate.DbType = DbType.DateTime;
                oRetndate.Value = DateTime.Parse(propcnf.returndate.ToString()).ToString("yyyy-MM-dd");
                oRetndate.ParameterName = "retdate";
 
                OracleParameter oVisitcntr1 = new OracleParameter();
                oVisitcntr1.DbType = DbType.String;
                oVisitcntr1.Value = propcnf.visitcntr1;
                oVisitcntr1.ParameterName = "visitcntry1";


                OracleParameter oVisitcntr2 = new OracleParameter();
                oVisitcntr2.DbType = DbType.String;
                oVisitcntr2.Value = propcnf.visitcntr2;
                oVisitcntr2.ParameterName = "visitcntry2";


                OracleParameter oVisitcntr3 = new OracleParameter();
                oVisitcntr3.DbType = DbType.String;
                oVisitcntr3.Value = propcnf.visitcntr3;
                oVisitcntr3.ParameterName = "visitcntry3";


                OracleParameter oVisitcntr4 = new OracleParameter();
                oVisitcntr4.DbType = DbType.String;
                oVisitcntr4.Value = propcnf.visitcntr4;
                oVisitcntr4.ParameterName = "visitcntry4";

                OracleParameter oTravelPrpose = new OracleParameter();
                oTravelPrpose.DbType = DbType.String;
                oTravelPrpose.Value = propcnf.travelPurpose;
                oTravelPrpose.ParameterName = "travelprpose";

                OracleParameter oCntcName = new OracleParameter();
                oCntcName.DbType = DbType.String;
                oCntcName.Value = propcnf.cntcName;
                oCntcName.ParameterName = "cntcname";

                OracleParameter oCntctAdd1 = new OracleParameter();
                oCntctAdd1.DbType = DbType.String;
                oCntctAdd1.Value = propcnf.cntcadd1;
                oCntctAdd1.ParameterName = "cntcadd1";

                OracleParameter oCntctAdd2 = new OracleParameter();
                oCntctAdd2.DbType = DbType.String;
                oCntctAdd2.Value = propcnf.cntcadd2;
                oCntctAdd2.ParameterName = "cntcadd2";

                OracleParameter oCntctAdd3 = new OracleParameter();
                oCntctAdd3.DbType = DbType.String;
                oCntctAdd3.Value = propcnf.cntcadd3;
                oCntctAdd3.ParameterName = "cntcadd3";

                OracleParameter oCntctAdd4 = new OracleParameter();
                oCntctAdd4.DbType = DbType.String;
                oCntctAdd4.Value = propcnf.cntcadd4;
                oCntctAdd4.ParameterName = "cntcadd4";

                OracleParameter oCntctNo1 = new OracleParameter();
                oCntctNo1.DbType = DbType.String;
                oCntctNo1.Value = propcnf.cntcno1;
                oCntctNo1.ParameterName = "cntcno1";

                OracleParameter oCntctNo2 = new OracleParameter();
                oCntctNo2.DbType = DbType.String;
                oCntctNo2.Value = propcnf.cntcno2;
                oCntctNo2.ParameterName = "cntcno2";

                OracleParameter otrvType = new OracleParameter();
                otrvType.DbType = DbType.String;
                otrvType.Value = propcnf.trvtype;
                otrvType.ParameterName = "trvtype";

                OracleParameter ofullName = new OracleParameter();
                ofullName.DbType = DbType.String;
                ofullName.Value = propcnf.fullName;
                ofullName.ParameterName = "fullName";

                OracleParameter oadd1 = new OracleParameter();
                oadd1.DbType = DbType.String;
                oadd1.Value = adrs1;
                oadd1.ParameterName = "add1";

                OracleParameter oadd2 = new OracleParameter();
                oadd2.DbType = DbType.String;
                oadd2.Value =adrs2;
                oadd2.ParameterName = "add2";

                OracleParameter oadd3 = new OracleParameter();
                oadd3.DbType = DbType.String;
                oadd3.Value = adrs3;
                oadd3.ParameterName = "add3";

                OracleParameter oadd4 = new OracleParameter();
                oadd4.DbType = DbType.String;
                oadd4.Value = adrs4;
                oadd4.ParameterName = "add4";

                OracleParameter omobileNo = new OracleParameter();
                omobileNo.DbType = DbType.String;
                omobileNo.Value = propcnf.mobileNo;
                omobileNo.ParameterName = "mobileNo";

                OracleParameter oplan = new OracleParameter();
                oplan.DbType = DbType.String;
                oplan.Value = propcnf.plan;
                oplan.ParameterName = "plan";

                OracleParameter otitle = new OracleParameter();
                otitle.DbType = DbType.String;
                otitle.Value = propcnf.title;
                otitle.ParameterName = "title";


                OracleParameter onoofPersons = new OracleParameter();
                onoofPersons.DbType = DbType.Int32;
                onoofPersons.Value = propcnf.noofPerson;
                onoofPersons.ParameterName = "noofPerson";

                //NET_PREMIUM_USD
                OracleParameter onetPrmUSD = new OracleParameter();
                onetPrmUSD.DbType = DbType.Double;
                onetPrmUSD.Value = propcnf.netPrmUSD;
                onetPrmUSD.ParameterName = "netPrmUSD";

                //ADMIN_FEE_RS
                OracleParameter oadminFee = new OracleParameter();
                oadminFee.DbType = DbType.Double;
                oadminFee.Value = propcnf.adminFee;
                oadminFee.ParameterName = "adminFee";

                //POLICY_FEE_RS
                OracleParameter opolFee = new OracleParameter();
                opolFee.DbType = DbType.Double;
                opolFee.Value = propcnf.polFee;
                opolFee.ParameterName = "polFee";

                //NBT_RS
                OracleParameter onbtAmt = new OracleParameter();
                onbtAmt.DbType = DbType.Double;
                onbtAmt.Value = propcnf.nbtAmt;
                onbtAmt.ParameterName = "nbtAmt";

                //VAT_RS
                OracleParameter ovatAmt = new OracleParameter();
                ovatAmt.DbType = DbType.Double;
                ovatAmt.Value = propcnf.vatAmt;
                ovatAmt.ParameterName = "vatAmt";

                //FINAL_PREMIUM_RS
                OracleParameter ofinalPrmRs = new OracleParameter();
                ofinalPrmRs.DbType = DbType.Double;
                ofinalPrmRs.Value = propcnf.finalPrmRS;
                ofinalPrmRs.ParameterName = "finalPrmRS";

                //				TAXES_EXPENSES_RS
                OracleParameter otaxexp = new OracleParameter();
                otaxexp.DbType = DbType.Double;
                otaxexp.Value = propcnf.taxesexp;
                otaxexp.ParameterName = "taxesexp";

                //				NET_PREMIUM_RS
                OracleParameter onetPrmRs = new OracleParameter();
                onetPrmRs.DbType = DbType.Double;
                onetPrmRs.Value = propcnf.netPrmRs;
                onetPrmRs.ParameterName = "netPrmRs";

                //NO_OF_DAYS
                OracleParameter onofDays = new OracleParameter();
                onofDays.DbType = DbType.Int64;
                onofDays.Value = propcnf.noofDays;
                onofDays.ParameterName = "noofDays";

                //SUM_INS_USD
                OracleParameter osumInsUSD = new OracleParameter();
                osumInsUSD.DbType = DbType.Double;
                osumInsUSD.Value = propcnf.suminsUSD;
                osumInsUSD.ParameterName = "suminsUSD";

                //USD_RATE
                OracleParameter oUSDRate = new OracleParameter();
                oUSDRate.DbType = DbType.Double;
                oUSDRate.Value = propcnf.usdRate;
                oUSDRate.ParameterName = "usdRate";

                //FINAL_PREMIUM_USD
                OracleParameter ofinalPrmUsd = new OracleParameter();
                ofinalPrmUsd.DbType = DbType.Double;
                ofinalPrmUsd.Value = propcnf.finalPrmUSD;
                ofinalPrmUsd.ParameterName = "finalPrmUSD";

                //ENTERED_BY
                OracleParameter oenteredBy = new OracleParameter();
                oenteredBy.DbType = DbType.String;
                oenteredBy.Value = propcnf.username;// propcnf.entryUser;
                oenteredBy.ParameterName = "entryUser";

                //AGENT_CODE
                OracleParameter oagtCode = new OracleParameter();
                oagtCode.DbType = DbType.UInt64 ;
                oagtCode.Value = propcnf.agtcode;
                oagtCode.ParameterName = "agtcode";

                //CURR_TYPE
                OracleParameter ocurrType = new OracleParameter();
                ocurrType.DbType = DbType.String;
                ocurrType.Value = propcnf.currType;
                ocurrType.ParameterName = "currType";
               
                //DISC_RATE
                OracleParameter odiscRate = new OracleParameter();
                odiscRate.DbType = DbType.Double;
                odiscRate.Value = propcnf.discRate;
                odiscRate.ParameterName = "discRate";

                //DISC_AMNT
                OracleParameter odiscAmt = new OracleParameter();
                odiscAmt.DbType = DbType.Double;
                odiscAmt.Value = propcnf.discAmt;
                odiscAmt.ParameterName = "discAmt";

                //POL_TYPE
                OracleParameter opolType = new OracleParameter();
                opolType.DbType = DbType.String;
                opolType.Value = propcnf.polType;
                opolType.ParameterName = "polType";

                ////POL_SDATE
                OracleParameter opolSdate = new OracleParameter();
                opolSdate.DbType = DbType.DateTime;
                opolSdate.Value = DateTime.Parse(propcnf.departdate.ToString()).ToString("yyyy-MM-dd");
                opolSdate.ParameterName = "polsdate";

                //POL_EDATE
                OracleParameter opolEdate = new OracleParameter();
                opolEdate.DbType = DbType.DateTime;
                opolEdate.Value = DateTime.Parse(propcnf.returndate.ToString()).ToString("yyyy-MM-dd");
                opolEdate.ParameterName = "poledate";

                //POL_EDATE
                OracleParameter oIPaddress = new OracleParameter();
                oIPaddress.DbType = DbType.String;
                oIPaddress.Value = propcnf.ipAddress;
                oIPaddress.ParameterName = "ipAddress";
                
                cmd.Parameters.Add(oRefNo);
                cmd.Parameters.Add(oDest);
                cmd.Parameters.Add(oDeptdate);
                cmd.Parameters.Add(oRetndate);
                cmd.Parameters.Add(oVisitcntr1);
                cmd.Parameters.Add(oVisitcntr2);
                cmd.Parameters.Add(oVisitcntr3);
                cmd.Parameters.Add(oVisitcntr4);
                cmd.Parameters.Add(oTravelPrpose);
                cmd.Parameters.Add(oCntcName);
                cmd.Parameters.Add(oCntctAdd1);
                cmd.Parameters.Add(oCntctAdd2);
                cmd.Parameters.Add(oCntctAdd3);
                cmd.Parameters.Add(oCntctAdd4);
                cmd.Parameters.Add(oCntctNo1);
                cmd.Parameters.Add(oCntctNo2); 
                cmd.Parameters.Add(otrvType);
                cmd.Parameters.Add(ofullName);
                cmd.Parameters.Add(oadd1);
                cmd.Parameters.Add(oadd2);
                cmd.Parameters.Add(oadd3);
                cmd.Parameters.Add(oadd4);
                cmd.Parameters.Add(omobileNo);
                cmd.Parameters.Add(oplan);
                cmd.Parameters.Add(otitle);
                cmd.Parameters.Add(onoofPersons);
                cmd.Parameters.Add(onetPrmUSD);
                cmd.Parameters.Add(oadminFee);
                cmd.Parameters.Add(opolFee);
                cmd.Parameters.Add(onbtAmt);
                cmd.Parameters.Add(ovatAmt);
                cmd.Parameters.Add(ofinalPrmRs);
                cmd.Parameters.Add(otaxexp);
                cmd.Parameters.Add(onetPrmRs);
                cmd.Parameters.Add(onofDays);
                cmd.Parameters.Add(osumInsUSD);
                cmd.Parameters.Add(oUSDRate);
                cmd.Parameters.Add(ofinalPrmUsd);
                cmd.Parameters.Add(oenteredBy);
                cmd.Parameters.Add(oagtCode);
                cmd.Parameters.Add(ocurrType);
                cmd.Parameters.Add(odiscRate);
                cmd.Parameters.Add(odiscAmt);
                cmd.Parameters.Add(opolType);
                cmd.Parameters.Add(opolSdate);
                cmd.Parameters.Add(opolEdate);
                cmd.Parameters.Add(oIPaddress);/**/



                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                #endregion

                #region Update Prop Member

                foreach (GridViewRow row in gvMembers.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string memId = dtMembers.Rows[row.RowIndex].ItemArray[1].ToString();
                        string memName = ((TextBox)row.FindControl("txtName")).Text.Trim();
                        string memPP = ((TextBox)row.FindControl("txtPP")).Text.Trim();
                        string memTitle = ((DropDownList)row.FindControl("ddTitle")).SelectedValue.Trim();
                        string Memtype = dtMembers.Rows[row.RowIndex].ItemArray[11].ToString();
                       string DOB= dtMembers.Rows[row.RowIndex].ItemArray[4].ToString();

                        string updateMember = "Update SLIGEN.TRV_QUOT_MEM_DETAILS" +
                                              " set NAME = :name," +
                                              " PP_NO = :ppNo," +
                                              " TITLE = :title ," +
                                              " DOB= :dob "+
                                              " where REF_NO = :refNo" +
                                              " and MEM_ID = :memId";

                        cmd.CommandText = updateMember;

                        OracleParameter oRefNo2 = new OracleParameter();
                        oRefNo2.DbType = DbType.String;
                        oRefNo2.Value = propcnf.quotNo;
                        oRefNo2.ParameterName = "refNo";

                        OracleParameter oMemId = new OracleParameter();
                        oMemId.DbType = DbType.String;
                        oMemId.Value = memId;
                        oMemId.ParameterName = "memId";

                        OracleParameter oMemName = new OracleParameter();
                        oMemName.DbType = DbType.String;
                        oMemName.Value = memName;
                        oMemName.ParameterName = "name";

                        OracleParameter oPPNum = new OracleParameter();
                        oPPNum.DbType = DbType.String;
                        oPPNum.Value = memPP;
                        oPPNum.ParameterName = "ppNo";

                        OracleParameter oMemTitle = new OracleParameter();
                        oMemTitle.DbType = DbType.String;
                        oMemTitle.Value = memTitle;
                        oMemTitle.ParameterName = "title";

                        OracleParameter oMemDOB = new OracleParameter();
                        oMemDOB.DbType = DbType.Date;
                        oMemDOB.Value = DOB;
                        oMemDOB.ParameterName = "dob";


                        cmd.Parameters.Add(oRefNo2);
                        cmd.Parameters.Add(oMemId);
                        cmd.Parameters.Add(oMemName);
                        cmd.Parameters.Add(oPPNum);
                        cmd.Parameters.Add(oMemTitle);
                        cmd.Parameters.Add(oMemDOB);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        
                    }
                }
              

                string insertPropMem = "insert into sligen.trv_prop_mem_details (select * from sligen.trv_quot_mem_details where ref_no='" + quotNo + "')";
                cmd.CommandText = insertPropMem;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                #endregion

                

                #region Generate Policy No
                int newMaxNo = 0;
                TRV_PolRef polData = new TRV_PolRef();
                int lastTwoDigit = Convert.ToInt32(System.DateTime.Now.ToString("yy"));
                var maxNo = polData.GetMaxSeqNo(Convert.ToInt32(999), lastTwoDigit, "TPI");
                newMaxNo = maxNo + 1;

                bool genState = polData.UpdateMaxSeqNo(Convert.ToInt32(999), lastTwoDigit, newMaxNo, "TPI");

                if (genState == true)
                {
                    TRV_PolRef refNo = new TRV_PolRef(Convert.ToInt32(999), lastTwoDigit, "TPI");
                    polno = refNo.SUFX1 + refNo.SUFX2 + refNo.YEAR + refNo.BRCD.ToString().PadLeft(3, '0') + refNo.SEQ.ToString().PadLeft(7, '0');
                }


                string PolMast = "INSERT INTO SLIGEN.TRV_POL_MAST (POLNO,DESTINATION,DEPART_DATE,RETURN_DATE,VISIT_CTRY1,VISIT_CTRY2,VISIT_CTRY3," +
                                 "VISIT_CTRY4,TRAVEL_PURPOSE,CONTCT_NAME,CONTCT_ADRS1,CONTCT_ADRS2,CONTCT_ADRS3,CONTCT_ADRS4,CONTCT_NO1," +
                                 " TRV_TYPE,FULL_NAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,MOBILE_NUMBER," +
                                 " PLAN,TITLE,NUM_OF_PERSONS,GROUP_DISC_RATE, NET_PREMIUM_USD, ADMIN_FEE_RS,POLICY_FEE_RS,NBT_RS,VAT_RS," +
                                 " FINAL_PREMIUM_RS,TAXES_EXPENSES_RS,ENTERED_DATE,NET_PREMIUM_RS,NO_OF_DAYS,SUM_INS_USD,USD_RATE," +
                                 "FINAL_PREMIUM_USD,IP_ADDRESS,ENTERED_BY,AGENT_CODE,DEBTOR_CODE,CURR_TYPE,SLI_NO1," +
                                 "SLI_NO2,REF_NO,DISC_RATE,DISC_AMNT,POL_TYPE,POL_SDATE,POL_EDATE,ENT_BRANCH)" +
                                 "VALUES( :polno, :destination, :departDate, :returnDate, :visitcnt1, :visitcnt2, :visitcnt3, :visitcnt4," +
                                 " :trvlPurpose, :cntname, :cntadrs1, :cntadrs2, :cntadrs3, :cntadrs4, :cntno1," +
                                 "  :trvtype, :fullname, :adrs1, :adrs2, :adrs3, :adrs4, :mobileno," +
                                 " :plan, :title, :noofperson, 0 , :netprmusd, :adminfee, :polfee, :nbtrs , :vatrs," +
                                 " :finalprmrs, :taxesexp,sysdate, :netprmrs, :noofdays, :sumins, :usdrate," +
                                 " :finalprmusd, :ipaddress, :enteredby, :agtcode, 0,'LKR',  0," +
                                 " 0, :refno, :discrate, :discamt, :poltype, :polsdate, :poledate,999)";

                cmd.CommandText = PolMast;

                OracleParameter opolno = new OracleParameter();
                opolno.DbType = DbType.String;
                opolno.Value = polno;
                opolno.ParameterName = "polno";

                OracleParameter odistination = new OracleParameter();
                odistination.DbType = DbType.String;
                odistination.Value = destination;
                odistination.ParameterName = "destination";
                
                OracleParameter oDepturedate = new OracleParameter();
                oDepturedate.DbType = DbType.DateTime;
                oDepturedate.Value = DateTime.Parse(propcnf.departdate.ToString()).ToString("yyyy-MM-dd");
                oDepturedate.ParameterName = "departDate";

                OracleParameter oReturndate = new OracleParameter();
                oReturndate.DbType = DbType.DateTime;
                oReturndate.Value = DateTime.Parse(propcnf.returndate.ToString()).ToString("yyyy-MM-dd");
                oReturndate.ParameterName = "returnDate";

                OracleParameter ovisitCntry1 = new OracleParameter();
                ovisitCntry1.DbType = DbType.String;
                ovisitCntry1.Value = propcnf.visitcntr1;
                ovisitCntry1.ParameterName = "visitcnt1";

                OracleParameter ovisitCntry2 = new OracleParameter();
                ovisitCntry2.DbType = DbType.String;
                ovisitCntry2.Value = propcnf.visitcntr2;
                ovisitCntry2.ParameterName = "visitcnt2";

                OracleParameter ovisitCntry3 = new OracleParameter();
                ovisitCntry3.DbType = DbType.String;
                ovisitCntry3.Value = propcnf.visitcntr3;
                ovisitCntry3.ParameterName = "visitcnt3";

                OracleParameter ovisitCntry4 = new OracleParameter();
                ovisitCntry4.DbType = DbType.String;
                ovisitCntry4.Value = propcnf.visitcntr4;
                ovisitCntry4.ParameterName = "visitcnt4";

                OracleParameter otravlpurps = new OracleParameter();
                otravlpurps.DbType = DbType.String;
                otravlpurps.Value = propcnf.travelPurpose;
                otravlpurps.ParameterName = "trvlPurpose";

                OracleParameter ocntname = new OracleParameter();
                ocntname.DbType = DbType.String;
                ocntname.Value = propcnf.cntcName;
                ocntname.ParameterName = "cntname";

                OracleParameter ocntadrs1 = new OracleParameter();
                ocntadrs1.DbType = DbType.String;
                ocntadrs1.Value = propcnf.cntcadd1;
                ocntadrs1.ParameterName = "cntadrs1";

                OracleParameter ocntadrs2 = new OracleParameter();
                ocntadrs2.DbType = DbType.String;
                ocntadrs2.Value = propcnf.cntcadd2;
                ocntadrs2.ParameterName = "cntadrs2";

                OracleParameter ocntadrs3 = new OracleParameter();
                ocntadrs3.DbType = DbType.String;
                ocntadrs3.Value = propcnf.cntcadd3;
                ocntadrs3.ParameterName = "cntadrs3";

                OracleParameter ocntadrs4 = new OracleParameter();
                ocntadrs4.DbType = DbType.String;
                ocntadrs4.Value = propcnf.cntcadd4;
                ocntadrs4.ParameterName = "cntadrs4";


                OracleParameter ocntno1 = new OracleParameter();
                ocntno1.DbType = DbType.String;
                ocntno1.Value = propcnf.cntcno1;
                ocntno1.ParameterName = "cntno1";
                
                OracleParameter otrvtype = new OracleParameter();
                otrvtype.DbType = DbType.String;
                otrvtype.Value = propcnf.trvtype;
                otrvtype.ParameterName = "trvtype";

                OracleParameter ofullname = new OracleParameter();
                ofullname.DbType = DbType.String;
                ofullname.Value = propcnf.fullName;
                ofullname.ParameterName = "fullname";

                OracleParameter opadrs1 = new OracleParameter();
                opadrs1.DbType = DbType.String;
                opadrs1.Value = adrs1;
                opadrs1.ParameterName = "adrs1";

                OracleParameter opadrs2 = new OracleParameter();
                opadrs2.DbType = DbType.String;
                opadrs2.Value = adrs2;
                opadrs2.ParameterName = "adrs2";

                OracleParameter opadrs3 = new OracleParameter();
                opadrs3.DbType = DbType.String;
                opadrs3.Value = adrs3;
                opadrs3.ParameterName = "adrs3";

                OracleParameter opadrs4 = new OracleParameter();
                opadrs4.DbType = DbType.String;
                opadrs4.Value = adrs4;
                opadrs4.ParameterName = "adrs4";

                OracleParameter opmobileno = new OracleParameter();
                opmobileno.DbType = DbType.String;
                opmobileno.Value = propcnf.mobileNo;
                opmobileno.ParameterName = "mobileno";

                OracleParameter opplan = new OracleParameter();
                opplan.DbType = DbType.String;
                opplan.Value = propcnf.plan;
                opplan.ParameterName = "plan";

                OracleParameter optitle = new OracleParameter();
                optitle.DbType = DbType.String;
                optitle.Value = propcnf.title;
                optitle.ParameterName = "title";

                OracleParameter onoofperson = new OracleParameter();
                onoofperson.DbType = DbType.Int64;
                onoofperson.Value = propcnf.noofPerson;
                onoofperson.ParameterName = "noofperson";

                OracleParameter opnetprmusd = new OracleParameter();
                opnetprmusd.DbType = DbType.Double;
                opnetprmusd.Value = propcnf.netPrmUSD;
                opnetprmusd.ParameterName = "netprmusd";


                OracleParameter opadminfee = new OracleParameter();
                opadminfee.DbType = DbType.Double;
                opadminfee.Value = propcnf.adminFee;
                opadminfee.ParameterName = "adminfee";

                OracleParameter oppolfee = new OracleParameter();
                oppolfee.DbType = DbType.Double;
                oppolfee.Value = propcnf.polFee;
                oppolfee.ParameterName = "polfee";

                OracleParameter opnbtrs = new OracleParameter();
                opnbtrs.DbType = DbType.Double;
                opnbtrs.Value = propcnf.nbtAmt;
                opnbtrs.ParameterName = "nbtrs";

                OracleParameter opvatrs = new OracleParameter();
                opvatrs.DbType = DbType.Double;
                opvatrs.Value = propcnf.vatAmt;
                opvatrs.ParameterName = "vatrs";

                OracleParameter opfinalprmrs = new OracleParameter();
                opfinalprmrs.DbType = DbType.Double;
                opfinalprmrs.Value = propcnf.finalPrmRS;
                opfinalprmrs.ParameterName = "finalprmrs";

                OracleParameter optaxesexp = new OracleParameter();
                optaxesexp.DbType = DbType.Double;
                optaxesexp.Value = propcnf.taxesexp;
                optaxesexp.ParameterName = "taxesexp";                

                OracleParameter opnetprmrs = new OracleParameter();
                opnetprmrs.DbType = DbType.Double;
                opnetprmrs.Value = propcnf.netPrmRs;
                opnetprmrs.ParameterName = "netprmrs";

                OracleParameter opnoofdays = new OracleParameter();
                opnoofdays.DbType = DbType.Int64;
                opnoofdays.Value = propcnf.noofDays;
                opnoofdays.ParameterName = "noofdays";

                OracleParameter opsumins = new OracleParameter();
                opsumins.DbType = DbType.Double;
                opsumins.Value = propcnf.suminsUSD;
                opsumins.ParameterName = "sumins";

                OracleParameter opusdrate = new OracleParameter();
                opusdrate.DbType = DbType.Double;
                opusdrate.Value = propcnf.usdRate;
                opusdrate.ParameterName = "usdrate";

                OracleParameter opfinalprmusd = new OracleParameter();
                opfinalprmusd.DbType = DbType.Double;
                opfinalprmusd.Value = propcnf.finalPrmUSD;
                opfinalprmusd.ParameterName = "finalprmusd";

                OracleParameter opipaddress = new OracleParameter();
                opipaddress.DbType = DbType.String;
                opipaddress.Value = propcnf.ipAddress;
                opipaddress.ParameterName = "ipaddress";

                OracleParameter openteredby = new OracleParameter();
                openteredby.DbType = DbType.String;
                openteredby.Value = propcnf.username;
                openteredby.ParameterName = "enteredby";

                OracleParameter opagtcode = new OracleParameter();
                opagtcode.DbType = DbType.Int64;
                opagtcode.Value = propcnf.agtcode;
                opagtcode.ParameterName = "agtcode";

                OracleParameter oprefno = new OracleParameter();
                oprefno.DbType = DbType.String;
                oprefno.Value = propcnf.quotNo;
                oprefno.ParameterName = "refno";

                OracleParameter opdiscrate = new OracleParameter();
                opdiscrate.DbType = DbType.Double;
                opdiscrate.Value = propcnf.discRate;
                opdiscrate.ParameterName = "discrate";

                OracleParameter opdiscamt = new OracleParameter();
                opdiscamt.DbType = DbType.Double;
                opdiscamt.Value = propcnf.discAmt;
                opdiscamt.ParameterName = "discamt";

                OracleParameter oppoltype = new OracleParameter();
                oppoltype.DbType = DbType.String;
                oppoltype.Value = propcnf.polType;
                oppoltype.ParameterName = "poltype";

                ////POL_SDATE
                OracleParameter oppolsdate = new OracleParameter();
                oppolsdate.DbType = DbType.DateTime;
                oppolsdate.Value = DateTime.Parse(propcnf.departdate.ToString()).ToString("yyyy-MM-dd");
                oppolsdate.ParameterName = "polsdate";

                //POL_EDATE
                OracleParameter oppoledate = new OracleParameter();
                oppoledate.DbType = DbType.DateTime;
                oppoledate.Value = DateTime.Parse(propcnf.returndate.ToString()).ToString("yyyy-MM-dd");
                oppoledate.ParameterName = "poledate";


                cmd.Parameters.Add(opolno);
                cmd.Parameters.Add(odistination);
                cmd.Parameters.Add(oDepturedate);
                cmd.Parameters.Add(oReturndate);
                cmd.Parameters.Add(ovisitCntry1);
                cmd.Parameters.Add(ovisitCntry2);
                cmd.Parameters.Add(ovisitCntry3);
                cmd.Parameters.Add(ovisitCntry4);
                cmd.Parameters.Add(otravlpurps);
                cmd.Parameters.Add(ocntname);
                cmd.Parameters.Add(ocntadrs1);
                cmd.Parameters.Add(ocntadrs2);
                cmd.Parameters.Add(ocntadrs3);
                cmd.Parameters.Add(ocntadrs4);
                cmd.Parameters.Add(ocntno1);
                cmd.Parameters.Add(otrvtype);
                cmd.Parameters.Add(ofullname);
                cmd.Parameters.Add(opadrs1);
                cmd.Parameters.Add(opadrs2);
                cmd.Parameters.Add(opadrs3);
                cmd.Parameters.Add(opadrs4);
                cmd.Parameters.Add(opmobileno);
                cmd.Parameters.Add(opplan);
                cmd.Parameters.Add(optitle);
                cmd.Parameters.Add(onoofperson);
                cmd.Parameters.Add(opnetprmusd);
                cmd.Parameters.Add(opadminfee);
                cmd.Parameters.Add(oppolfee);
                cmd.Parameters.Add(opnbtrs);
                cmd.Parameters.Add(opvatrs);
                cmd.Parameters.Add(opfinalprmrs);
                cmd.Parameters.Add(optaxesexp);
                cmd.Parameters.Add(opnetprmrs);
                cmd.Parameters.Add(opnoofdays);
                cmd.Parameters.Add(opsumins);
                cmd.Parameters.Add(opusdrate);
                cmd.Parameters.Add(opfinalprmusd);
                cmd.Parameters.Add(opipaddress);
                cmd.Parameters.Add(openteredby);
                cmd.Parameters.Add(opagtcode);
                cmd.Parameters.Add(oprefno);
                cmd.Parameters.Add(opdiscrate);
                cmd.Parameters.Add(opdiscamt);
                cmd.Parameters.Add(oppoltype);
                cmd.Parameters.Add(oppolsdate);
                cmd.Parameters.Add(oppoledate); /**/

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                #endregion

                #region Update Pol Member

                string insertPolMem = "insert into sligen.trv_pol_mem_details " +
                                      "(select '"+polno+ "','" + polno + "'||'_'||substr(mem_id,length((ref_no))+2,((length(MEM_ID)-length(ref_no))-1 )) as mem_id," +
                                      " MEM_TYPE, gender,dob,age,name, pp_no, title, base_amount_usd,sysdate from sligen.trv_prop_mem_details " +
                                      " where ref_no = '"+quotNo+"')";
                cmd.CommandText = insertPolMem;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();


                #endregion

                #region Update Proposal Details

                string instTRVPropDetails = "Insert into SLIC_NET.PROPOSAL_DETAILS(POL_TYPE, REF_NO, FULL_NAME, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, " +
                                                                                " MOBILE_NUMBER, HOME_NUMBER, OFFICE_NUMBER, EMAIL, NIC, LOC_ADRS1, " +
                                                                                " LOC_ADRS2, LOC_ADRS3, LOC_ADRS4, ASSIGNEE, DAMAGED_BEFORE, REJCTED_BEFORE, " +
                                                                                " REJCT_REASON, PLAN, SUM_ASSURD, ANU_PREMIUM, ADMIN_FEE, POL_FEE, NBT, VAT, COM_DATE, " +
                                                                                " STATUS, ENTRY_DATE, USERNAME, TITLE, PRODUCT_ID, END_DATE, PASSPORT_NO,POLICY_NUMBER)" +
                                                        " VALUES(:polTyp, :refNo, :fullNam, :padrs1, :padrs2, :padrs3, :padrs4, :mobNo, :homNo, :ofcNo, :email, " +
                                                        " :nic, :lcAdrs1, :lcAdrs2, :lcAdrs3, :lcAdrs4, null, :dmgBefore, :rejBefore, null, :plan, :sumAssurd, " +
                                                        " :anuPrem, :admnFee, :polFee, :nbt, :vat, :comDate, :status, sysdate, :username, :title, :prodId, :endDate, :ppno, :polno)";


                cmd.CommandText = instTRVPropDetails;


                OracleParameter oPolTyp = new OracleParameter();
                //oPolTyp.DbType = DbType.String;
                oPolTyp.Value = "G";
                oPolTyp.ParameterName = "polTyp";

                OracleParameter opRefNo = new OracleParameter();
                //oRefNo.DbType = DbType.String;
                opRefNo.Value = propcnf.quotNo;
                opRefNo.ParameterName = "refNo";

                OracleParameter oName = new OracleParameter();
                //oName.DbType = DbType.String;
                oName.Value = propcnf.fullName;
                oName.ParameterName = "fullNam";

                OracleParameter oAdrs1 = new OracleParameter();
                //oAdrs1.DbType = DbType.String;
                oAdrs1.Value = adrs1;
                oAdrs1.ParameterName = "padrs1";

                OracleParameter oAdrs2 = new OracleParameter();
                //oAdrs2.DbType = DbType.String;
                oAdrs2.Value = adrs2;
                oAdrs2.ParameterName = "padrs2";

                OracleParameter oAdrs3 = new OracleParameter();
                //oAdrs3.DbType = DbType.String;
                oAdrs3.Value = adrs3;
                oAdrs3.ParameterName = "padrs3";

                OracleParameter oAdrs4 = new OracleParameter();
                //oAdrs4.DbType = DbType.String;
                oAdrs4.Value = adrs4;
                oAdrs4.ParameterName = "padrs4";

                OracleParameter oMobNo = new OracleParameter();
                //oMobNo.DbType = DbType.String;
                oMobNo.Value = propcnf.mobileNo;
                oMobNo.ParameterName = "mobNo";

                OracleParameter oHomNo = new OracleParameter();
                //oHomNo.DbType = DbType.String;
                oHomNo.Value = "0000000000";
                oHomNo.ParameterName = "homNo";

                OracleParameter oOfcNo = new OracleParameter();
                //oOfcNo.DbType = DbType.String;
                oOfcNo.Value = "0000000000";
                oOfcNo.ParameterName = "ofcNo";

                OracleParameter oEmail = new OracleParameter();
                //oEmail.DbType = DbType.String;
                oEmail.Value = propcnf.email;
                oEmail.ParameterName = "email";

                OracleParameter oNic = new OracleParameter();
                //oNic.DbType = DbType.String;
                oNic.Value = (String.IsNullOrEmpty(propcnf.nic) ? "0000000000" : propcnf.nic);
                oNic.ParameterName = "nic";

                OracleParameter oLcAdrs1 = new OracleParameter();
                //oLcAdrs1.DbType = DbType.String;
                oLcAdrs1.Value = "Not applicable";
                oLcAdrs1.ParameterName = "lcAdrs1";

                OracleParameter oLcAdrs2 = new OracleParameter();
                //oLcAdrs2.DbType = DbType.String;
                oLcAdrs2.Value = "Not applicable";
                oLcAdrs2.ParameterName = "lcAdrs2";

                OracleParameter oLcAdrs3 = new OracleParameter();
                //oLcAdrs3.DbType = DbType.String;
                oLcAdrs3.Value = "Not applicable";
                oLcAdrs3.ParameterName = "lcAdrs3";

                OracleParameter oLcAdrs4 = new OracleParameter();
                oLcAdrs4.DbType = DbType.String;
                oLcAdrs4.Value = "Not applicable";
                oLcAdrs4.ParameterName = "lcAdrs4";

                OracleParameter oDmgBefore = new OracleParameter();
                //oDmgBefore.DbType = DbType.String;
                oDmgBefore.Value = "N";
                oDmgBefore.ParameterName = "dmgBefore";

                OracleParameter oRejBefore = new OracleParameter();
                oRejBefore.DbType = DbType.String;
                oRejBefore.Value = "N";
                oRejBefore.ParameterName = "rejBefore";

                OracleParameter oPlan = new OracleParameter();
                //oPlan.DbType = DbType.String;
                oPlan.Value = plan;
                oPlan.ParameterName = "plan";

                OracleParameter oSumAssurd = new OracleParameter();
                oSumAssurd.DbType = DbType.Double;
                oSumAssurd.Value = propcnf.suminsUSD;
                oSumAssurd.ParameterName = "sumAssurd";

                OracleParameter oAnuPrem = new OracleParameter();
                oAnuPrem.DbType = DbType.Double;
                oAnuPrem.Value = propcnf.finalPrmRS;
                oAnuPrem.ParameterName = "anuPrem";

                OracleParameter oAdmnFee = new OracleParameter();
                oAdmnFee.DbType = DbType.Double;
                oAdmnFee.Value = propcnf.adminFee;
                oAdmnFee.ParameterName = "admnFee";

                OracleParameter oPolFee = new OracleParameter();
                oPolFee.DbType = DbType.Double;
                oPolFee.Value = propcnf.polFee;
                oPolFee.ParameterName = "polFee";

                OracleParameter oNbt = new OracleParameter();
                oNbt.DbType = DbType.Double;
                oNbt.Value = 0;
                oNbt.ParameterName = "nbt";

                OracleParameter oVat = new OracleParameter();
                oVat.DbType = DbType.Double;
                oVat.Value = 0;
                oVat.ParameterName = "vat";

                OracleParameter oComDate = new OracleParameter();
                oComDate.DbType = DbType.Date;
                oComDate.Value = DateTime.Parse(propcnf.departdate.ToString()).ToString("yyyy-MM-dd"); ;
                oComDate.ParameterName = "comDate";

                OracleParameter oStatus = new OracleParameter();
                oStatus.DbType = DbType.String;
                oStatus.Value = "P";
                oStatus.ParameterName = "status";

                OracleParameter oUsername = new OracleParameter();
                oUsername.Value = username;
                oUsername.ParameterName = "username";

                OracleParameter oTitle = new OracleParameter();
                //oTitle.DbType = DbType.String;
                oTitle.Value = title;
                oTitle.ParameterName = "title";

                OracleParameter oProdId = new OracleParameter();
                //oProdId.DbType = DbType.String;
                oProdId.Value = "TPI";
                oProdId.ParameterName = "prodId";

                OracleParameter oEndDate = new OracleParameter();
                oEndDate.DbType = DbType.Date;
                oEndDate.Value = DateTime.Parse(propcnf.returndate.ToString()).ToString("yyyy-MM-dd"); ;
                oEndDate.ParameterName = "endDate";

                OracleParameter oppno = new OracleParameter();
                oppno.Value = propcnf.passportno;
                oppno.ParameterName = "ppno";

                OracleParameter oppolno = new OracleParameter();
                oppolno.Value = polno;
                oppolno.ParameterName = "polno";

                cmd.Parameters.Add(oPolTyp);
                cmd.Parameters.Add(oRefNo);
                cmd.Parameters.Add(oName);
                cmd.Parameters.Add(oAdrs1);
                cmd.Parameters.Add(oAdrs2);
                cmd.Parameters.Add(oAdrs3);
                cmd.Parameters.Add(oAdrs4);
                cmd.Parameters.Add(oMobNo);
                cmd.Parameters.Add(oHomNo);
                cmd.Parameters.Add(oOfcNo);
                cmd.Parameters.Add(oEmail);
                cmd.Parameters.Add(oNic);
                cmd.Parameters.Add(oLcAdrs1);
                cmd.Parameters.Add(oLcAdrs2);
                cmd.Parameters.Add(oLcAdrs3);
                cmd.Parameters.Add(oLcAdrs4);
                cmd.Parameters.Add(oDmgBefore);
                cmd.Parameters.Add(oRejBefore);
                cmd.Parameters.Add(oPlan);
                cmd.Parameters.Add(oSumAssurd);
                cmd.Parameters.Add(oAnuPrem);
                cmd.Parameters.Add(oAdmnFee);
                cmd.Parameters.Add(oPolFee);
                cmd.Parameters.Add(oNbt);
                cmd.Parameters.Add(oVat);
                cmd.Parameters.Add(oComDate);
                cmd.Parameters.Add(oStatus);
                //cmd.Parameters.Add(oEntryDate);
                cmd.Parameters.Add(oUsername);
                cmd.Parameters.Add(oTitle);
                cmd.Parameters.Add(oProdId);
                cmd.Parameters.Add(oEndDate);
                cmd.Parameters.Add(oppno);
                cmd.Parameters.Add(oppolno);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                #endregion


                trans.Commit();

                returnValue = true;

            }

        }
        catch (Exception e)
        {
            trans.Rollback();
            log logger = new log();
            logger.write_log("Failed at confirm_GT_proposal: " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }


}