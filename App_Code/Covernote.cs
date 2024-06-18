using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;
using System.Collections;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Net.Mail;

/// <summary>
/// Summary description for covernote
/// </summary>
public class Covernote
{
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
    public OracleCommand cmd = new OracleCommand(); 

	public Covernote()
	{

	}

    public Covernote(string cno)
    {
        cn_id = cno.Trim();
        try
        {
            conn.Open();
            string sql = "SELECT MO.RISK_CVR, MO.TEROR_CVR, MO.FLOOD_CVR, MA.SUM_ASSURED, MA.MANUAL_REC_NO, MA.MANUAL_REC_AMOUNT, MA.BRANCH, MA.DURATION1,  MO.VEH_NO_PROVINCE ||' ' || MO.VEHICLE_NO AS VEHICLE_NO , MO.CHASSIS_NO, MA.POLICY_NO,"+
                        " MA.PROPOSAL_NO,  MA.COMMENCEMENT_DATE AS COMDATE, MA.COMMENCEMENT_TIME AS COMTIME, MA.COMMENCEMENT_DATE || ' ' ||MA.COMMENCEMENT_TIME AS COMMENCEMENT_DATE, MA.EXPIRE_DATE  || ' ' ||MA.COMMENCEMENT_TIME AS EXPIRE_DATE, MA.INSURED_NAME1, MA.STATUS," +
                        " MA.CUSTOMER_ADD_1, MA.CUSTOMER_ADD_2, MA.CUSTOMER_ADD_3, MA.CUSTOMER_ADD_4, MA.BUSINESS_TYPE, MA.INFORMED_USER_NAME, MA.ENTRY_EPF, MO.COVER_TYP AS CVR_TYP , PT.POLICY_TYPE_NAME, MO.MAKE, MO.CYLINDR_CAP, MO.PURPOSE, MA.DEL_TAG,to_char(MA.ENTRY_DATE,'yyyy-mm-dd') as ENTRY_DATE,"+
                        " MA.INFORMED_USER_CODE,to_char(MA.MANUAL_REC_DATE,'yyyy-mm-dd') as MANUAL_REC_DATE ,ASSN_NAME,MA.CVR_ISS_PLACE,MA.BUSINESS_AGENT_CODE,MO.VEHICLE_TYPE,MA.NIC_BREGNO,MA.MANUAL_CN_NO,MA.EDIT_EPF, to_char(MA.EDIT_DATE,'yyyy-MM-dd')  as Del_date, MA.CAN_INFBY, MA.CAN_RSN, MA.LOC_ID,MA.CONTACT_NO" +
                        " FROM SLIC_CNOTE.MASTR_FILE MA, SLIC_CNOTE.MOTOR_FILE MO,  SLIC_CNOTE.POLICY_TYPE PT WHERE MA.REFNO = MO.REF_NO AND  upper(trim(PT.POLICY_TYPE_NAME)) = UPPER(trim(MO.POLICY_TYPE)) AND MA.REFNO = :ref_no ";

            
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = conn;

            OracleParameter cno_01 = new OracleParameter();
            cno_01.Value = cno;
            cno_01.ParameterName = "ref_no";

            cmd.Parameters.Add(cno_01);
            cmd.ExecuteReader();

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                vehicleNO = reader["VEHICLE_NO"].ToString().Trim() ;
                chassisNo = reader["CHASSIS_NO"].ToString();
                policyNo = reader["POLICY_NO"].ToString();
                proposalNo = reader["PROPOSAL_NO"].ToString();
                toDate = reader["EXPIRE_DATE"].ToString();
                insuredName1 = reader["INSURED_NAME1"].ToString();
                status = reader["STATUS"].ToString();
                fromDate = reader["COMMENCEMENT_DATE"].ToString();
                add1 = reader["CUSTOMER_ADD_1"].ToString();
                add2 = reader["CUSTOMER_ADD_2"].ToString();
                add3 = reader["CUSTOMER_ADD_3"].ToString();
                add4 = reader["CUSTOMER_ADD_4"].ToString();
                contact_no = reader["CONTACT_NO"].ToString();
                businesType = reader["BUSINESS_TYPE"].ToString();
                entryEPF = reader["ENTRY_EPF"].ToString();
                informedUser = reader["INFORMED_USER_NAME"].ToString();
                coverType = reader["CVR_TYP"].ToString();
                policyType = reader["POLICY_TYPE_NAME"].ToString();
                make = reader["MAKE"].ToString();
                cc = reader["CYLINDR_CAP"].ToString();
                purpose = reader["PURPOSE"].ToString();
                com_date = reader["COMDATE"].ToString();
                com_time = reader["COMTIME"].ToString();
                duration = reader["DURATION1"].ToString();
                branch = Convert.ToInt32(reader["BRANCH"].ToString());
                recAmount = Convert.ToDouble(reader["MANUAL_REC_AMOUNT"].ToString());
                recNo = reader["MANUAL_REC_NO"].ToString();

                rc = reader["RISK_CVR"].ToString();
                tc = reader["TEROR_CVR"].ToString();
                fc = reader["FLOOD_CVR"].ToString();

                string[] arr = fromDate.Split(' ');
                com_date_only = arr[0];

                string[] arr2 = toDate.Split(' ');
                exp_date_only = arr2[0];

                sumAssured = Convert.ToDouble(reader["SUM_ASSURED"].ToString());

                deletion_tag = reader["DEL_TAG"].ToString();
                Entry_Date = reader["ENTRY_DATE"].ToString();
                informedUserCode = reader["INFORMED_USER_CODE"].ToString();
                manRctDate = reader["MANUAL_REC_DATE"].ToString();
                assgName = reader["ASSN_NAME"].ToString();
                CvrIssPlace = reader["CVR_ISS_PLACE"].ToString();
                busAgent = reader["BUSINESS_AGENT_CODE"].ToString();
                vehType = reader["VEHICLE_TYPE"].ToString();
                NICRegNo = reader["NIC_BREGNO"].ToString();
                mnCvrnoteNo = reader["MANUAL_CN_NO"].ToString();
                CanBy = reader["EDIT_EPF"].ToString();
                CanOn = reader["Del_date"].ToString();
                InfToCan = reader["CAN_INFBY"].ToString();
                CanRSN = reader["CAN_RSN"].ToString();
                LocCode = reader["LOC_ID"].ToString(); 
            }
            pendin_list = get_pnd_rsns(cn_id);
        }
        catch
        { }
        finally
        {
            conn.Close();
        }

    }

    public void GetPostalAdd(string cno,string POcode)
    {
        cn_id = cno.Trim();
        try
        {
            conn.Open();
            string sql = "SELECT  L.LOCATION_NAME,U.DEBTO_CODE FROM SLIC_CNOTE.CNOTE_INTERNET_LOCATIONS L, SLIC_CNOTE.CNOTE_INTERNET_USER U " +
                            " WHERE  L.LOCATION_CODE= U.LOCATION_CODE AND  L.COMPANY_CODE='P' AND L.LOCATION_CODE='" + POcode + "'";

            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = conn;

            cmd.ExecuteReader();

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LocationName = reader["LOCATION_NAME"].ToString();
                DebtCd =int.Parse( reader["DEBTO_CODE"].ToString());
            }


            string sql2 = "select po_add1, po_add2, po_add3, po_add4,PROMO_NIC from slic_cnote.postal_detail where refno='" + cno + "'";

            OracleCommand cmd2 = new OracleCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;
            cmd2.Connection = conn;

            cmd2.ExecuteReader();

            OracleDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                Po_Add1 = reader2["po_add1"].ToString();
                Po_Add2 =  reader2["po_add2"].ToString();
                Po_Add3 = reader2["po_add3"].ToString();
                Po_Add4 = reader2["po_add4"].ToString();
                promo_NIC = reader2["PROMO_NIC"].ToString();
            }


        }
        catch
        { 
        
        }
        finally
        {
            conn.Close();
        } 
    }

    #region GTRs

    public string deletion_tag { get; set; }
    public string entryEPF { get; set; }
    public string informedUser { get; set; }
    public string informedUserCode { get; set; }
    public string businesType { get; set; }
    public string add4 { get; set; }
    public string add3 { get; set; }
    public string add2 { get; set; }
    public string add1 { get; set; }
    public string insuredName1 { get; set; }
    public string status { get; set; }
    public string toDate { get; set; }
    public string proposalNo { get; set; }
    public string policyNo { get; set; }
    public string cn_id { get; set; }
    public string vehicleNO { get; set; }
    public string fromDate { get; set; }
    public string policyType { get; set; }
    public string coverType { get; set; }
    public ArrayList pendin_list { get; set; }
    public string make { get; set; }
    public string cc { get; set; }
    public string purpose { get; set; }
    public string com_date { get; set; }
    public string com_time { get; set; }
    public string duration { get; set; }
    public string chassisNo { get; set; }
    public int branch { get; set; }
    public double recAmount { get; set; }
    public string recNo { get; set; }
    public string rc { get; set; }
    public string tc { get; set; }
    public string fc { get; set; }
    public string com_date_only { get; set; }
    public string exp_date_only { get; set; }
    public string com_time_only { get; set; }
    public double sumAssured { get; set; }
    public string Entry_Date { get; set; }
    public string manRctDate { get; set; }
    public string assgName { get; set; }
    public string CvrIssPlace { get; set; }
    public string busAgent { get; set; }
    public string vehType { get; set; }
    public string NICRegNo { get; set; }
    public string mnCvrnoteNo { get; set; }
    public string CanBy { get; set; }
    public string InfToCan { get; set; }
    public string CanOn { get; set; }
    public string CanRSN { get; set; }
    public string LocationName { get; set; }
    public int DebtCd { get; set; }
    public string  LocCode { get; set; }
    public string Po_Add1 { get; set; }
    public string Po_Add2 { get; set; }
    public string Po_Add3 { get; set; }
    public string Po_Add4 { get; set; }
    public string promo_NIC { get; set; }
    public string contact_no { get; set; }

    #endregion

    public bool CNExists(string id)
    {
        bool result = false;

        string sql = "SELECT REFNO FROM SLIC_CNOTE.MASTR_FILE WHERE REFNO = :rfno ";
        conn.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.Connection = conn;

        OracleParameter cno_01 = new OracleParameter();
        cno_01.Value = id;
        cno_01.ParameterName = "rfno";

        cmd.Parameters.Add(cno_01);
        cmd.ExecuteReader();

        OracleDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            result = true;
            break;
        }
        conn.Close();
        return result;
    }


    public string insertCN(int brno, string comDate, int duration, string expDate, string insName1, string status, string ManualCN, string manualRec, 
        string entryEpf, string entryIP, string poCode, string preRefNo, string dept, string infUserType, string infUserCode, int BusAgtCode, 
        string assignmnt, string addr1, string addr2, string addr3, string addr4, string bustype, string infUserName, string isudPlz, string polNo, 
        string proNo, string comTym, string vhNo, string chNo, int cc, string vType, string pTyp , string purpos, string mak, double sumAssurd,
        string rc, string tc, string cvrTyp, string vhProvnz, string pnding, double ManRecAmt, string ManRecDt) // The cool way
    {
        string ref_num = "";


        bool result = false;
        int i = 0;
        string sql = "SLIC_CNOTE.INSERT_TCN";

        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("branchNo", OracleType.Number).Value = brno;
            cmd.Parameters.Add("comDate", OracleType.VarChar).Value = comDate;
            cmd.Parameters.Add("duration", OracleType.Number).Value = duration;
            cmd.Parameters.Add("expDate", OracleType.VarChar).Value = expDate;
            cmd.Parameters.Add("insName1", OracleType.VarChar).Value = insName1;
            cmd.Parameters.Add("status", OracleType.VarChar).Value = status;
            cmd.Parameters.Add("manualCN", OracleType.VarChar).Value = ManualCN;
            cmd.Parameters.Add("manualRec", OracleType.VarChar).Value = manualRec;
            cmd.Parameters.Add("entryEPF", OracleType.VarChar).Value = entryEpf;
            cmd.Parameters.Add("entryIP", OracleType.VarChar).Value = entryIP;
            cmd.Parameters.Add("poCode", OracleType.VarChar).Value = poCode;
            cmd.Parameters.Add("prevRefNo", OracleType.VarChar).Value = preRefNo;
            cmd.Parameters.Add("deptCode", OracleType.VarChar).Value = dept;
            cmd.Parameters.Add("infUsrType", OracleType.VarChar).Value = infUserType;
            cmd.Parameters.Add("infUsrCode", OracleType.VarChar).Value = infUserCode;
            cmd.Parameters.Add("busyAgtCode", OracleType.Number).Value = BusAgtCode;
            
            cmd.Parameters.Add("assgn", OracleType.VarChar).Value = assignmnt;
            cmd.Parameters.Add("addr1", OracleType.VarChar).Value = addr1;
            cmd.Parameters.Add("addr2", OracleType.VarChar).Value = addr2;
            cmd.Parameters.Add("addr3", OracleType.VarChar).Value = addr3;
            cmd.Parameters.Add("addr4", OracleType.VarChar).Value = addr4;
            cmd.Parameters.Add("buzType", OracleType.VarChar).Value = bustype;
            cmd.Parameters.Add("infUsrName", OracleType.VarChar).Value = infUserName;
            cmd.Parameters.Add("issuedPlz", OracleType.VarChar).Value = isudPlz;
       

            cmd.Parameters.Add("polNo", OracleType.VarChar).Value = polNo;

            cmd.Parameters.Add("propNo", OracleType.VarChar).Value = proNo;
            cmd.Parameters.Add("comTime", OracleType.VarChar).Value = comTym;
            cmd.Parameters.Add("vehNo", OracleType.VarChar).Value = vhNo;
            cmd.Parameters.Add("chassNo", OracleType.VarChar).Value = chNo;
            cmd.Parameters.Add("cc", OracleType.Number).Value = cc;
            cmd.Parameters.Add("vehType", OracleType.VarChar).Value = vType;
            cmd.Parameters.Add("polType", OracleType.VarChar).Value = pTyp;


            cmd.Parameters.Add("purpose", OracleType.VarChar).Value = purpos;
            cmd.Parameters.Add("make", OracleType.VarChar).Value = mak;
            cmd.Parameters.Add("sumAssurd", OracleType.Double).Value = sumAssurd;
            cmd.Parameters.Add("rcc", OracleType.VarChar).Value = rc;
            cmd.Parameters.Add("tc", OracleType.VarChar).Value = tc;
            cmd.Parameters.Add("cvr_Type", OracleType.VarChar).Value = cvrTyp;
            cmd.Parameters.Add("veh_pro", OracleType.VarChar).Value = vhProvnz;
            cmd.Parameters.Add("pendin_rsn", OracleType.VarChar).Value = pnding;

            cmd.Parameters.Add("refno", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            ref_num = cmd.Parameters["refno"].Value.ToString();
            log lg = new log();
            lg.write_log(ref_num.ToString());

        }
        catch (Exception rr)
        {
            string d = rr.ToString();
            log lg = new log();
            lg.write_log(rr.ToString());

        }
        finally
        {
            conn.Close();

        }
        


        return ref_num;
    }



    public string insertCN(int brno, string comDate, int duration, string expDate, string insName1, string status, string ManualCN, string manualRec,
        string entryEpf, string entryIP, string poCode, string preRefNo, string dept, string infUserType, string infUserCode, int BusAgtCode,
        string assignmnt, string addr1, string addr2, string addr3, string addr4, string bustype, string infUserName, string isudPlz, string polNo,
        string proNo, string comTym, string vhNo, string chNo, int cc, string vType, string pTyp, string purpos, string mak, double sumAssurd,
        string rc, string tc, string cvrTyp, string vhProvnz, string pendin, double ManRecAmt, string ManRecDt, string floodcvr, string contact_no) // The cool way
    {
        string ref_num = "";


        bool result = false;
        int i = 0;
        string sql = "SLIC_CNOTE.GENERATE_CN";

        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;

            #region


            cmd.Parameters.Add("UsrAttBR", OracleType.VarChar).Value = brno.ToString();

            cmd.Parameters.Add("branchNo", OracleType.VarChar).Value = brno.ToString();
            cmd.Parameters.Add("comDate", OracleType.VarChar).Value = comDate.Trim();
            cmd.Parameters.Add("duration", OracleType.Number).Value = duration;
            cmd.Parameters.Add("expDate", OracleType.VarChar).Value = expDate.Trim();
            cmd.Parameters.Add("insName1", OracleType.VarChar).Value = insName1.Trim();
            if (!String.IsNullOrEmpty(status))
            {
                cmd.Parameters.Add("status", OracleType.VarChar).Value = status.Trim();
            }
            else
            {
                cmd.Parameters.Add("status", OracleType.VarChar).Value = DBNull.Value;
            }
            //cmd.Parameters.Add("manualCN", OracleType.VarChar).Value = ManualCN;

            if (!String.IsNullOrEmpty(ManualCN))
            {
                cmd.Parameters.Add("manualCN", OracleType.VarChar).Value = ManualCN.Trim();
            }
            else
            {
                cmd.Parameters.Add("manualCN", OracleType.VarChar).Value = "0000";

            }

            if (!String.IsNullOrEmpty(manualRec))
            {
                cmd.Parameters.Add("manualRec", OracleType.VarChar).Value = manualRec.Trim();
            }
            else
            {
                cmd.Parameters.Add("manualRec", OracleType.VarChar).Value = DBNull.Value;

            }
            cmd.Parameters.Add("entryEPF", OracleType.VarChar).Value = entryEpf.Trim();
            cmd.Parameters.Add("entryIP", OracleType.VarChar).Value = entryIP.Trim();
            if (poCode != null)
            {
                cmd.Parameters.Add("poCode", OracleType.VarChar).Value = poCode.Trim();
            }
            else
            {
                cmd.Parameters.Add("poCode", OracleType.VarChar).Value = "0000";
            }
            cmd.Parameters.Add("prevRefNo", OracleType.VarChar).Value = preRefNo.Trim();
            cmd.Parameters.Add("deptCode", OracleType.VarChar).Value = dept.Trim();
            if (infUserType != null)
            {
                cmd.Parameters.Add("infUsrType", OracleType.VarChar).Value = infUserType.Trim();
            }
            else
            {
                cmd.Parameters.Add("infUsrType", OracleType.VarChar).Value = DBNull.Value;
            }
            cmd.Parameters.Add("infUsrCode", OracleType.VarChar).Value = infUserCode.Trim();
            if (BusAgtCode != null)
            {
                cmd.Parameters.Add("busyAgtCode", OracleType.Number).Value = BusAgtCode;
            }
            else
            {
                cmd.Parameters.Add("busyAgtCode", OracleType.Number).Value = 0;
            }

            cmd.Parameters.Add("assgn", OracleType.VarChar).Value = assignmnt.Trim();

            
            cmd.Parameters.Add("assgnName", OracleType.VarChar).Value = DBNull.Value;
            
            cmd.Parameters.Add("addr1", OracleType.VarChar).Value = addr1.Trim();
            cmd.Parameters.Add("addr2", OracleType.VarChar).Value = addr2.Trim();
            if (!String.IsNullOrEmpty(addr3))
            {
                cmd.Parameters.Add("addr3", OracleType.VarChar).Value = addr3.Trim();
            }
            else
            {
                cmd.Parameters.Add("addr3", OracleType.VarChar).Value = DBNull.Value;
            }
            if (!String.IsNullOrEmpty(addr4))
            {
                cmd.Parameters.Add("addr4", OracleType.VarChar).Value = addr4.Trim();
            }
            else
            {
                cmd.Parameters.Add("addr4", OracleType.VarChar).Value = DBNull.Value;
            }

            if (!String.IsNullOrEmpty(contact_no))
            {
                cmd.Parameters.Add("contact_no", OracleType.VarChar).Value = contact_no.Trim();
            }
            else
            {
                cmd.Parameters.Add("contact_no", OracleType.VarChar).Value = DBNull.Value;
            }

            cmd.Parameters.Add("buzType", OracleType.VarChar).Value = bustype.Trim();
            cmd.Parameters.Add("infUsrName", OracleType.VarChar).Value = infUserName.Trim();

            
            cmd.Parameters.Add("issuedPlz", OracleType.VarChar).Value = DBNull.Value;
            
            if (!String.IsNullOrEmpty(polNo))
            {
                cmd.Parameters.Add("polNo", OracleType.VarChar).Value = polNo.Trim();
            }
            else
            {
                cmd.Parameters.Add("polNo", OracleType.VarChar).Value = "0";
            }
            //cmd.Parameters["polNo"].Direction = ParameterDirection.Input;
            if (!String.IsNullOrEmpty(proNo))
            {
                cmd.Parameters.Add("propNo", OracleType.VarChar).Value = proNo.Trim();
            }
            else
            {
                cmd.Parameters.Add("propNo", OracleType.VarChar).Value = "0";
            }
            cmd.Parameters.Add("comTime", OracleType.VarChar).Value = comTym.Trim();
            if (!String.IsNullOrEmpty(vhNo))
            {
                cmd.Parameters.Add("vehNo", OracleType.VarChar).Value = vhNo.Trim();
            }
            else
            {
                cmd.Parameters.Add("vehNo", OracleType.VarChar).Value = "";
            }
            if (chNo != null)
            {
                cmd.Parameters.Add("chassNo", OracleType.VarChar).Value = chNo.Trim();
            }
            else
            {
                cmd.Parameters.Add("chassNo", OracleType.VarChar).Value = "";
            }
            if (cc != null)
            {
                cmd.Parameters.Add("cc", OracleType.Number).Value = cc;
            }
            else
            {
                cmd.Parameters.Add("cc", OracleType.Number).Value = 0;
            }
            cmd.Parameters.Add("vehType", OracleType.VarChar).Value = vType.Trim();
            cmd.Parameters.Add("polType", OracleType.VarChar).Value = pTyp.Trim();


            cmd.Parameters.Add("purpose", OracleType.VarChar).Value = purpos;
            if (mak != null)
            {
                cmd.Parameters.Add("make", OracleType.VarChar).Value = mak.Trim();
            }
            else
            {
                cmd.Parameters.Add("make", OracleType.VarChar).Value = "";
            }
            cmd.Parameters.Add("sumAssurd", OracleType.Number).Value = sumAssurd;
            cmd.Parameters.Add("rcc", OracleType.VarChar).Value = rc.Trim();
            cmd.Parameters.Add("tc", OracleType.VarChar).Value = tc.Trim();
            cmd.Parameters.Add("flood", OracleType.VarChar).Value = floodcvr.Trim();
            cmd.Parameters.Add("cvr_Type", OracleType.VarChar).Value = cvrTyp.Trim();
            if (vhProvnz != null)
            {
                cmd.Parameters.Add("veh_pro", OracleType.VarChar).Value = vhProvnz.Trim();
            }
            else
            {
                cmd.Parameters.Add("veh_pro", OracleType.VarChar).Value = "";
            }
            cmd.Parameters.Add("pendin_rsn", OracleType.VarChar).Value = pendin.Trim();
            cmd.Parameters.Add("manual_recAmnt", OracleType.Number).Value = ManRecAmt;

            if (ManRecDt != null)
            {
                cmd.Parameters.Add("manual_recDate", OracleType.VarChar).Value = ManRecDt;
            }
            else
            {
                cmd.Parameters.Add("manual_recDate", OracleType.VarChar).Value = DBNull.Value;
            }

            
            cmd.Parameters.Add("NIC_BregNo", OracleType.VarChar).Value = DBNull.Value;
            
            
            cmd.Parameters.Add("CvrIssuedPlz", OracleType.VarChar).Value = "999";
            
            cmd.Parameters.Add("comCode", OracleType.VarChar).Value = "C";


            cmd.Parameters.Add("locID", OracleType.VarChar).Value = "999";
            
            
            cmd.Parameters.Add("fueltyp", OracleType.VarChar).Value = DBNull.Value;
            

            cmd.Parameters.Add("refno", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            ref_num = cmd.Parameters["refno"].Value.ToString();

            
            #endregion

        }
        catch (Exception rr)
        {
            string d = rr.ToString();
            log lg = new log();
            lg.write_log(rr.ToString());

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }



        return ref_num;
    }

    private ArrayList get_pnd_rsns(string refno)
    {
        ArrayList result = new ArrayList();
        try
        {
            //conn.Open();
            string sql = "SELECT * FROM SLIC_CNOTE.PENDIN_RSN_RESULT WHERE REF_NO = :ref_no ";


            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = conn;

            OracleParameter cno_01 = new OracleParameter();
            cno_01.Value = refno;
            cno_01.ParameterName = "ref_no";

            cmd.Parameters.Add(cno_01);
            cmd.ExecuteReader();

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string g = reader["PENDN_RSN"].ToString();
                result.Add(g);

            }

        }
        catch (Exception d)
        {
            string dd = d.ToString();
        }
        finally
        {
            //conn.Close();
        }
        return result;
    }

    public string get_branchName(int brno)
    {
        string bname = "";

        try
        {
            conn.Open();
            string sql = "select brnam from GENPAY.BRANCH where brcod = :bno";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = conn;

            OracleParameter cno_01 = new OracleParameter();
            cno_01.DbType = DbType.Int32;
            cno_01.Value = brno;
            cno_01.ParameterName = "bno";

            cmd.Parameters.Add(cno_01);
            cmd.ExecuteReader();

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                bname = reader["brnam"].ToString();

            }
        }
        catch
        { }
        finally
        {
            conn.Close();

        }
        return bname;
    }

    public void covernote_print(string c_number, string epf, string ip, bool reprint)
    {
        Covernote cvr = new Covernote(c_number.Trim());

        string veh_no = "";
        string duration_t = "";
        string du_todate = "";
        string amount = "";
        bool tag = false;
        string value = "Rs. " + cvr.sumAssured + "/= ";
        string includ = "( Including ";
        ArrayList arr = new ArrayList();
        if (cvr.rc.Equals("Y"))
        {
            arr.Add("RCC");
        }
        if (cvr.tc.Equals("Y"))
        {
            arr.Add("TR");
        }
        if (cvr.fc.Equals("Y"))
        {
            arr.Add("Flood");
        }

        int y = 0;
        foreach (string sd in arr)
        {
            if (y == 0)
            {
                includ = includ + " " + sd;
            }
            else
            {
                includ = includ + " /" + sd;
            }
            y++;
        }
        includ = includ + " Cover. )";
        if (arr.Count > 0)
        {
            value = value + includ;
        }

        if (!String.IsNullOrEmpty(cvr.vehicleNO) && !String.IsNullOrEmpty(cvr.chassisNo))
        {
            veh_no = cvr.vehicleNO + " / " + cvr.chassisNo;
        }
        else if (String.IsNullOrEmpty(cvr.vehicleNO) && !String.IsNullOrEmpty(cvr.chassisNo))
        {
            veh_no = cvr.chassisNo;
        }
        else if (!String.IsNullOrEmpty(cvr.vehicleNO) && String.IsNullOrEmpty(cvr.chassisNo))
        {
            veh_no = cvr.vehicleNO;
        }

        if (Convert.ToInt32(cvr.duration) > 1)
        {
            duration_t = cvr.duration + " DAYS";
        }
        else
        {
            duration_t = cvr.duration + " DAY";
        }

        if (Convert.ToInt32(cvr.duration) % 10 == 1)
        {
            du_todate = cvr.duration + " ST DAY";
        }
        else if (Convert.ToInt32(cvr.duration) % 10 == 2)
        {
            if (Convert.ToInt32(cvr.duration) == 12)
            {
                du_todate = cvr.duration + " TH DAY";
            }
            else
            {
                du_todate = cvr.duration + " ND DAY";
            }
        }
        else if (Convert.ToInt32(cvr.duration) % 10 == 3)
        {
            if (Convert.ToInt32(cvr.duration) == 13)
            {
                du_todate = cvr.duration + " TH DAY";
            }
            else
            {
                du_todate = cvr.duration + " RD DAY";
            }
        }
        else
        {
            du_todate = cvr.duration + " TH DAY";
        }
        // ----------------------------------

        if (cvr.recAmount == null || cvr.recAmount == 0)
        {
            amount = "As agreed";
        }
        else
        {
            amount = cvr.recAmount.ToString("N2");
        }

        #region Get Emp Name
        string usrName = "";
        string informedBy = cvr.informedUserCode.ToString();
        //auth.as400_get_EmpName(informedBy, out usrName);

        //call the database to get the name relevant to the epf no
        #endregion

        //foreach (string i in cvr.pendin_list)
        //{
        //    if (i.Equals("Premium"))
        //    {
        //        tag = true;
        //    }
        //}
        //if (tag)
        //{
        //    amount = cvr.recAmount.ToString();
        //}
        //else
        //{
        //    amount = "As agreed";
        //}


        Document document = new Document(PageSize.A4, 50, 50, 25, 25);
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

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.ScalePercent(10f);
        //watermark.SetAbsolutePosition(76, 190);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);

        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        //document.Add(new Paragraph("\n"));
        //document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        Paragraph titleLine = new Paragraph("(Established by the Insurance Corporation Act No. 2 of 1961)", titleFont);
        Paragraph titleLine1 = new Paragraph("TEMPORARY COVER NOTE", titleFont1);
        titleLine.SetAlignment("Center");
        titleLine1.SetAlignment("Center");
        document.Add(titleLine);
        document.Add(titleLine1);

        document.Add(new Paragraph("\n"));
        //document.Add(new Paragraph("\n"));

        int[] clmwidths = { 25, 30, 10, 35 };

        PdfPTable tbl1 = new PdfPTable(4);

        tbl1.SetWidths(clmwidths);

        tbl1.WidthPercentage = 100;
        tbl1.HorizontalAlignment = 0;
        tbl1.SpacingBefore = 15;
        tbl1.SpacingAfter = 10;
        tbl1.DefaultCell.Border = 0;



        tbl1.AddCell(new Phrase("Motor Department ", boldTableFont));
        tbl1.AddCell(new Phrase("", bodyFont));
        tbl1.AddCell(new Phrase("Ref No", bodyFont));
        tbl1.AddCell(new Phrase(": " + c_number, bodyFont));


        tbl1.AddCell(new Phrase("", bodyFont));
        tbl1.AddCell(new Phrase("", bodyFont));
        tbl1.AddCell(new PdfPCell(new Phrase("Customer Portal", bodyFont)) { Colspan = 2, Border = 0 });
        //tbl1.AddCell(new Phrase("Office", bodyFont));
        //tbl1.AddCell(new Phrase(": " + cvr.get_branchName(cvr.branch), bodyFont));

        

        document.Add(tbl1);



        if (!String.IsNullOrEmpty(cvr.status))
        {
            Paragraph ins2 = new Paragraph(cvr.status + " " + cvr.insuredName1, subTitleFont);
            document.Add(ins2);
        }
        else
        {
            Paragraph ins1 = new Paragraph(cvr.insuredName1, subTitleFont);
            document.Add(ins1);
        }

        Paragraph p1 = new Paragraph(cvr.add1, subTitleFont);
        Paragraph p2 = new Paragraph(cvr.add2, subTitleFont);

        document.Add(p1);
        document.Add(p2);

        if (!String.IsNullOrEmpty(cvr.add3))
        {
            Paragraph p3 = new Paragraph(cvr.add3, subTitleFont);
            document.Add(p3);
        }
        if (!String.IsNullOrEmpty(cvr.add4))
        {
            Paragraph pd4 = new Paragraph(cvr.add4, subTitleFont);
            document.Add(pd4);
        }


        //Paragraph p3 = new Paragraph("having proposed for insurance in respect of the Motor Vehicle described in the Schedule below  and having paid  the sum of      Rs.   As agreed       the risk is hereby held covered in terms of the Company's usual form of Comprehensive Policy         applicable thereto for a period of     15   DAYS     this is to say from  10:25:01 AM         on the   2013/08/10     to the same time on the      15  TH DAY     thereafter unless the cover be terminated by the Company by notice in writing in which case insurance will thereupon cease and a  proportionate  part of the annual premium otherwise  payable for  such insurance will be  charged for the time  the company has been on risk.", bodyFont2);
        //document.Add(p3);

        //Chunk chunk = new Chunk(" the Motor Vehicle described in the Schedule below  and having paid  the sum of      Rs.   As agreed       the risk is hereby held covered in terms of the Company's usual form of Comprehensive Policy         applicable thereto for a period of     15   DAYS     this is to say from  10:25:01 AM         on the   2013/08/10     to the same time on the      15  TH DAY     thereafter unless the cover be terminated by the Company by notice in writing in which case insurance will thereupon cease and a  proportionate  part of the annual premium otherwise  payable for  such insurance will be  charged for the time  the company has been on risk.", bodyFont2);
        //document.Add(new Chunk("having proposed for insurance in respect of ", bodyFont2));
        //chunk.SetUnderline(0.5f, -1.5f);
        //document.Add(chunk);

        document.Add(new Chunk("having proposed for insurance in respect of the Motor Vehicle described in the Schedule below  and having paid  the sum of      Rs. ", bodyFont2));

        Chunk ch1 = new Chunk(" " + amount + " ", bodyFont2);
        ch1.SetUnderline(0.5f, -1.5f);
        document.Add(ch1);

        document.Add(new Chunk(" the risk is hereby held covered in terms of the Company's usual form of ", bodyFont2));

        Chunk ch2 = new Chunk(" " + cvr.policyType + " ", bodyFont2);
        ch2.SetUnderline(0.5f, -1.5f);
        document.Add(ch2);

        document.Add(new Chunk(" applicable thereto for a period of ", bodyFont2));

        Chunk ch3 = new Chunk(" " + duration_t + " ", bodyFont2);
        ch3.SetUnderline(0.5f, -1.5f);
        document.Add(ch3);

        document.Add(new Chunk(" this is to say from ", bodyFont2));

        Chunk ch4 = new Chunk(" " + cvr.com_time + " ", bodyFont2);
        ch4.SetUnderline(0.5f, -1.5f);
        document.Add(ch4);

        document.Add(new Chunk(" on the ", bodyFont2));

        Chunk ch5 = new Chunk(" " + cvr.com_date_only + " ", bodyFont2);
        ch5.SetUnderline(0.5f, -1.5f);
        document.Add(ch5);

        document.Add(new Chunk(" to the same time on the ", bodyFont2));

        Chunk ch6 = new Chunk(" " + du_todate + " ", bodyFont2);
        ch6.SetUnderline(0.5f, -1.5f);
        document.Add(ch6);

        document.Add(new Chunk(" thereafter unless the cover be terminated by the Company by notice in writing in which case insurance will thereupon cease and a proportionate  part of the annual premium otherwise  payable for  such insurance will be  charged for the time the company has been on risk. ", bodyFont2));




        document.Add(new Paragraph("\n"));
        Paragraph p4 = new Paragraph("This cover note is valid only upto " + cvr.exp_date_only, bodyFont2);
        document.Add(p4);
        document.Add(new Paragraph("\n"));
        int[] clmwidths2 = { 40, 60 };

        PdfPTable tbl2 = new PdfPTable(2);
        tbl2.SetWidths(clmwidths2);

        tbl2.WidthPercentage = 100;
        tbl2.HorizontalAlignment = 0;
        tbl2.SpacingBefore = 1;
        tbl2.SpacingAfter = 1;
        tbl2.DefaultCell.Border = 0;


        tbl2.AddCell(new Phrase("Pending Reasons ", bodyFont2_bold));
        tbl2.AddCell(new Phrase("", bodyFont2_bold));
        foreach (string i in cvr.pendin_list)
        {

            tbl2.AddCell(new Phrase(i, bodyFont2));
            tbl2.AddCell(new Phrase("", bodyFont2));

        }
        document.Add(tbl2);

        document.Add(new Paragraph("\n"));

        Paragraph s = new Paragraph("SCHEDULE", boldTableFont);
        s.SetAlignment("center");
        document.Add(s);

        int[] clmwidths3 = { 25, 25, 25, 25 };
        PdfPTable t = new PdfPTable(4);
        t.SetWidths(clmwidths3);
        t.AddCell(new PdfPCell(new Phrase("Policy Number", bodyFont2)) { Colspan = 1, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase(":  "+cvr.policyNo, bodyFont2_bold)) { Colspan = 2, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase("", bodyFont2)) { Colspan = 1, Border = 0 });

        t.AddCell(new PdfPCell(new Phrase("Value of the Vehicle", bodyFont2)) { Colspan = 1, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase(":  " + value, bodyFont2_bold)) { Colspan = 2, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase("", bodyFont2)) { Colspan = 1, Border = 0 });

        t.AddCell(new PdfPCell(new Phrase(" ", bodyFont2)) { Colspan = 1, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase(" ", bodyFont2_bold)) { Colspan = 3, Border = 0 });

        t.AddCell(new PdfPCell(new Phrase("Make", bodyFont2)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase("Horse Power or C.C.", bodyFont2)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase("Distinctive Number", bodyFont2)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase("", bodyFont2)) { Colspan = 1, Border = 0 });
        //t.AddCell(new PdfPCell(new Phrase("Used only for the following purposes", bodyFont2)) { Colspan = 1 });

        t.AddCell(new PdfPCell(new Phrase(cvr.make.ToUpper(), bodyFont2_bold)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase(cvr.cc, bodyFont2_bold)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase(veh_no, bodyFont2_bold)) { Colspan = 1 });
        //t.AddCell(new PdfPCell(new Phrase(cvr.purpose.ToUpper(), bodyFont2_bold)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase("", bodyFont2)) { Colspan = 1, Border = 0 });

        t.WidthPercentage = 100;
        t.HorizontalAlignment = 0;
        t.SpacingBefore = 10;
        t.SpacingAfter = 1;
        t.DefaultCell.Border = 1;
        
        document.Add(t);

        document.Add(new Paragraph("\nPlease note that if any claim is made prior to the renewal date of the policy, there will be a revision in the premium paid.\n\n", bodyFont2_bold));


        Paragraph s2 = new Paragraph("SRI LANKA", bodyFont);
        s2.SetAlignment("center");
        document.Add(s2);

        Paragraph s3 = new Paragraph("( The Motor Traffic Act. No. 14 of 1951)", bodyFont);
        s3.SetAlignment("center");
        document.Add(s3);

        Paragraph s4 = new Paragraph("CERTIFICATE OF INSURANCE", boldTableFont);
        s4.SetAlignment("center");
        document.Add(s4);

        document.Add(new Chunk("We hereby certify that the Covering Note is issued in accordance with the provisions of Part VI of the Motor Traffic Act No.  14 of 1951. ", bodyFont2));


        int[] clmwidths4 = { 12, 48, 40 };
        PdfPTable t2 = new PdfPTable(3);
        t2.SetWidths(clmwidths4);

        t2.WidthPercentage = 100;
        t2.HorizontalAlignment = 0;
        t2.SpacingBefore = 10;
        t2.SpacingAfter = 10;
        t2.DefaultCell.Border = 0;


        //********************* Commented this to include the cover note issued place to the Cover note print on 10th Jan 2014***********************//
        //t2.AddCell(new Phrase("Place  ", bodyFont2));
        //t2.AddCell(new Phrase(": " + cvr.get_branchName(Convert.ToInt32((string)Session["BrCode"])), boldTableFont));
        //t2.AddCell(new Phrase("SRI LANKA INSURANCE CORPORATION", bodyFont2));
        //******************************************************************************************************************************************//

        //t2.AddCell(new Phrase("Issued Place  ", bodyFont2));
        //t2.AddCell(new Phrase(": Online Customer Portal", boldTableFont));
        //t2.AddCell(new Phrase("SRI LANKA INSURANCE CORPORATION", bodyFont2));

        //t2.AddCell(new Phrase("Issued Date     ", bodyFont2));
        //t2.AddCell(new Phrase(": " + DateTime.Today.ToString("yyyy-MM-dd"), boldTableFont));
        //t2.AddCell(new Phrase(" ", bodyFont2));
 
        //t2.AddCell(new Phrase(" ", bodyFont2));
        //t2.AddCell(new Phrase(" ", boldTableFont));
        //t2.AddCell(new Phrase("                   (Authorised Insurer)", bodyFont2));

        //t2.AddCell(new Phrase(" ", bodyFont2));
        //t2.AddCell(new Phrase("", boldTableFont));
        //t2.AddCell(new Phrase(" ", bodyFont2));

        t2.AddCell(new Phrase("Issued Place  ", bodyFont2));
        t2.AddCell(new Phrase(": Online Customer Portal", boldTableFont));
        t2.AddCell(new Phrase("This is a System Generated Document,", bodyFont2));

        t2.AddCell(new Phrase("Issued Date     ", bodyFont2));
        t2.AddCell(new Phrase(": " + DateTime.Today.ToString("yyyy-MM-dd"), boldTableFont));
        t2.AddCell(new Phrase("Signatue is not required.", bodyFont2));

        //t2.AddCell(new Phrase(" ", bodyFont2));
        //t2.AddCell(new Phrase(" ", boldTableFont));
        //t2.AddCell(new Phrase("                   (Authorised Insurer)", bodyFont2));



        document.Add(t2);

        int[] clmwidths5 = { 8, 14, 12, 10, 12, 10 };
        PdfPTable t3 = new PdfPTable(6);
        t3.SetWidths(clmwidths5);

        t3.WidthPercentage = 100;
        t3.HorizontalAlignment = 0;
        t3.SpacingBefore = 10;
        t3.SpacingAfter = 0;
        t3.DefaultCell.Border = 0;


        t3.AddCell(new Phrase("Receipt No  ", bodyFont4));
        t3.AddCell(new Phrase(": " + cvr.recNo, bodyFont4));
        t3.AddCell(new Phrase("Receipt Amount", bodyFont4));
        t3.AddCell(new Phrase(": " + cvr.recAmount.ToString("N2"), bodyFont4));
        t3.AddCell(new Phrase("Receipt Date", bodyFont4));
        t3.AddCell(new Phrase(": " + cvr.manRctDate, bodyFont4));

        document.Add(t3);

        //Random rnd = new Random();
        document.Close();
        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Covernote-{0}.pdf", c_number.Trim()));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
    }

    public void cover_note_email(string c_number, string epf, string ip, bool reprint, string to_add)
    {
        Covernote cvr = new Covernote(c_number.Trim());
        CustProfile cp = new CustProfile(epf);

        string veh_no = "";
        string duration_t = "";
        string du_todate = "";
        string amount = "";
        bool tag = false;
        string value = "Rs. " + cvr.sumAssured + "/= ";
        string includ = "( Including ";
        ArrayList arr = new ArrayList();
        if (cvr.rc.Equals("Y"))
        {
            arr.Add("RCC");
        }
        if (cvr.tc.Equals("Y"))
        {
            arr.Add("TR");
        }
        if (cvr.fc.Equals("Y"))
        {
            arr.Add("Flood");
        }

        int y = 0;
        foreach (string sd in arr)
        {
            if (y == 0)
            {
                includ = includ + " " + sd;
            }
            else
            {
                includ = includ + " /" + sd;
            }
            y++;
        }
        includ = includ + " Cover. )";
        if (arr.Count > 0)
        {
            value = value + includ;
        }

        if (!String.IsNullOrEmpty(cvr.vehicleNO) && !String.IsNullOrEmpty(cvr.chassisNo))
        {
            veh_no = cvr.vehicleNO + " / " + cvr.chassisNo;
        }
        else if (String.IsNullOrEmpty(cvr.vehicleNO) && !String.IsNullOrEmpty(cvr.chassisNo))
        {
            veh_no = cvr.chassisNo;
        }
        else if (!String.IsNullOrEmpty(cvr.vehicleNO) && String.IsNullOrEmpty(cvr.chassisNo))
        {
            veh_no = cvr.vehicleNO;
        }

        if (Convert.ToInt32(cvr.duration) > 1)
        {
            duration_t = cvr.duration + " DAYS";
        }
        else
        {
            duration_t = cvr.duration + " DAY";
        }

        if (Convert.ToInt32(cvr.duration) % 10 == 1)
        {
            du_todate = cvr.duration + " ST DAY";
        }
        else if (Convert.ToInt32(cvr.duration) % 10 == 2)
        {
            if (Convert.ToInt32(cvr.duration) == 12)
            {
                du_todate = cvr.duration + " TH DAY";
            }
            else
            {
                du_todate = cvr.duration + " ND DAY";
            }
        }
        else if (Convert.ToInt32(cvr.duration) % 10 == 3)
        {
            if (Convert.ToInt32(cvr.duration) == 13)
            {
                du_todate = cvr.duration + " TH DAY";
            }
            else
            {
                du_todate = cvr.duration + " RD DAY";
            }
        }
        else
        {
            du_todate = cvr.duration + " TH DAY";
        }
        // ----------------------------------

        if (cvr.recAmount == null || cvr.recAmount == 0)
        {
            amount = "As agreed";
        }
        else
        {
            amount = cvr.recAmount.ToString("N2");
        }

        #region Get Emp Name
        string usrName = "";
        string informedBy = cvr.informedUserCode.ToString();
        //auth.as400_get_EmpName(informedBy, out usrName);

        //call the database to get the name relevant to the epf no
        #endregion

        


        Document document = new Document(PageSize.A4, 50, 50, 25, 25);
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

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
        //watermark.ScalePercent(10f);
        //watermark.SetAbsolutePosition(76, 190);
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        document.Add(watermark);

        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        //document.Add(new Paragraph("\n"));
        //document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        document.Add(new Paragraph("\n"));
        Paragraph titleLine = new Paragraph("(Established by the Insurance Corporation Act No. 2 of 1961)", titleFont);
        Paragraph titleLine1 = new Paragraph("TEMPORARY COVER NOTE", titleFont1);
        titleLine.SetAlignment("Center");
        titleLine1.SetAlignment("Center");
        document.Add(titleLine);
        document.Add(titleLine1);

        document.Add(new Paragraph("\n"));
        //document.Add(new Paragraph("\n"));

        int[] clmwidths = { 25, 30, 10, 35 };

        PdfPTable tbl1 = new PdfPTable(4);

        tbl1.SetWidths(clmwidths);

        tbl1.WidthPercentage = 100;
        tbl1.HorizontalAlignment = 0;
        tbl1.SpacingBefore = 15;
        tbl1.SpacingAfter = 10;
        tbl1.DefaultCell.Border = 0;



        tbl1.AddCell(new Phrase("Motor Department ", boldTableFont));
        tbl1.AddCell(new Phrase("", bodyFont));
        tbl1.AddCell(new Phrase("Ref No", bodyFont));
        tbl1.AddCell(new Phrase(": " + c_number, bodyFont));


        tbl1.AddCell(new Phrase("", bodyFont));
        tbl1.AddCell(new Phrase("", bodyFont));
        tbl1.AddCell(new PdfPCell(new Phrase("Customer Portal", bodyFont)) { Colspan = 2, Border = 0 });
        //tbl1.AddCell(new Phrase("Office", bodyFont));
        //tbl1.AddCell(new Phrase(": " + cvr.get_branchName(cvr.branch), bodyFont));



        document.Add(tbl1);



        if (!String.IsNullOrEmpty(cvr.status))
        {
            Paragraph ins2 = new Paragraph(cvr.status + " " + cvr.insuredName1, subTitleFont);
            document.Add(ins2);
        }
        else
        {
            Paragraph ins1 = new Paragraph(cvr.insuredName1, subTitleFont);
            document.Add(ins1);
        }

        Paragraph p1 = new Paragraph(cvr.add1, subTitleFont);
        Paragraph p2 = new Paragraph(cvr.add2, subTitleFont);

        document.Add(p1);
        document.Add(p2);

        if (!String.IsNullOrEmpty(cvr.add3))
        {
            Paragraph p3 = new Paragraph(cvr.add3, subTitleFont);
            document.Add(p3);
        }
        if (!String.IsNullOrEmpty(cvr.add4))
        {
            Paragraph pd4 = new Paragraph(cvr.add4, subTitleFont);
            document.Add(pd4);
        }


        //Paragraph p3 = new Paragraph("having proposed for insurance in respect of the Motor Vehicle described in the Schedule below  and having paid  the sum of      Rs.   As agreed       the risk is hereby held covered in terms of the Company's usual form of Comprehensive Policy         applicable thereto for a period of     15   DAYS     this is to say from  10:25:01 AM         on the   2013/08/10     to the same time on the      15  TH DAY     thereafter unless the cover be terminated by the Company by notice in writing in which case insurance will thereupon cease and a  proportionate  part of the annual premium otherwise  payable for  such insurance will be  charged for the time  the company has been on risk.", bodyFont2);
        //document.Add(p3);

        //Chunk chunk = new Chunk(" the Motor Vehicle described in the Schedule below  and having paid  the sum of      Rs.   As agreed       the risk is hereby held covered in terms of the Company's usual form of Comprehensive Policy         applicable thereto for a period of     15   DAYS     this is to say from  10:25:01 AM         on the   2013/08/10     to the same time on the      15  TH DAY     thereafter unless the cover be terminated by the Company by notice in writing in which case insurance will thereupon cease and a  proportionate  part of the annual premium otherwise  payable for  such insurance will be  charged for the time  the company has been on risk.", bodyFont2);
        //document.Add(new Chunk("having proposed for insurance in respect of ", bodyFont2));
        //chunk.SetUnderline(0.5f, -1.5f);
        //document.Add(chunk);

        document.Add(new Chunk("having proposed for insurance in respect of the Motor Vehicle described in the Schedule below  and having paid  the sum of      Rs. ", bodyFont2));

        Chunk ch1 = new Chunk(" " + amount + " ", bodyFont2);
        ch1.SetUnderline(0.5f, -1.5f);
        document.Add(ch1);

        document.Add(new Chunk(" the risk is hereby held covered in terms of the Company's usual form of ", bodyFont2));

        Chunk ch2 = new Chunk(" " + cvr.policyType + " ", bodyFont2);
        ch2.SetUnderline(0.5f, -1.5f);
        document.Add(ch2);

        document.Add(new Chunk(" applicable thereto for a period of ", bodyFont2));

        Chunk ch3 = new Chunk(" " + duration_t + " ", bodyFont2);
        ch3.SetUnderline(0.5f, -1.5f);
        document.Add(ch3);

        document.Add(new Chunk(" this is to say from ", bodyFont2));

        Chunk ch4 = new Chunk(" " + cvr.com_time + " ", bodyFont2);
        ch4.SetUnderline(0.5f, -1.5f);
        document.Add(ch4);

        document.Add(new Chunk(" on the ", bodyFont2));

        Chunk ch5 = new Chunk(" " + cvr.com_date_only + " ", bodyFont2);
        ch5.SetUnderline(0.5f, -1.5f);
        document.Add(ch5);

        document.Add(new Chunk(" to the same time on the ", bodyFont2));

        Chunk ch6 = new Chunk(" " + du_todate + " ", bodyFont2);
        ch6.SetUnderline(0.5f, -1.5f);
        document.Add(ch6);

        document.Add(new Chunk(" thereafter unless the cover be terminated by the Company by notice in writing in which case insurance will thereupon cease and a proportionate  part of the annual premium otherwise  payable for  such insurance will be  charged for the time the company has been on risk. ", bodyFont2));

        document.Add(new Paragraph("\n"));

        Paragraph p4 = new Paragraph("This cover note is valid only upto " + cvr.exp_date_only, bodyFont2);
        document.Add(p4);
        document.Add(new Paragraph("\n"));
        int[] clmwidths2 = { 40, 60 };

        PdfPTable tbl2 = new PdfPTable(2);
        tbl2.SetWidths(clmwidths2);

        tbl2.WidthPercentage = 100;
        tbl2.HorizontalAlignment = 0;
        tbl2.SpacingBefore = 1;
        tbl2.SpacingAfter = 1;
        tbl2.DefaultCell.Border = 0;


        tbl2.AddCell(new Phrase("Pending Reasons ", bodyFont2_bold));
        tbl2.AddCell(new Phrase("", bodyFont2_bold));
        foreach (string i in cvr.pendin_list)
        {

            tbl2.AddCell(new Phrase(i, bodyFont2));
            tbl2.AddCell(new Phrase("", bodyFont2));

        }
        document.Add(tbl2);

        //document.Add(new Paragraph("\n"));

        Paragraph s = new Paragraph("SCHEDULE", boldTableFont);
        s.SetAlignment("center");
        document.Add(s);

        int[] clmwidths3 = { 25, 25, 25, 25 };
        PdfPTable t = new PdfPTable(4);
        t.SetWidths(clmwidths3);
        t.AddCell(new PdfPCell(new Phrase("Policy Number", bodyFont2)) { Colspan = 1, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase(":  " + cvr.policyNo, bodyFont2_bold)) { Colspan = 2, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase("", bodyFont2)) { Colspan = 1, Border = 0 });

        t.AddCell(new PdfPCell(new Phrase("Value of the Vehicle", bodyFont2)) { Colspan = 1, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase(":  " + value, bodyFont2_bold)) { Colspan = 2, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase("", bodyFont2)) { Colspan = 1, Border = 0 });

        t.AddCell(new PdfPCell(new Phrase(" ", bodyFont2)) { Colspan = 1, Border = 0 });
        t.AddCell(new PdfPCell(new Phrase(" ", bodyFont2_bold)) { Colspan = 3, Border = 0 });

        t.AddCell(new PdfPCell(new Phrase("Make", bodyFont2)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase("Horse Power or C.C.", bodyFont2)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase("Distinctive Number", bodyFont2)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase("", bodyFont2)) { Colspan = 1, Border = 0 });
        //t.AddCell(new PdfPCell(new Phrase("Used only for the following purposes", bodyFont2)) { Colspan = 1 });

        t.AddCell(new PdfPCell(new Phrase(cvr.make.ToUpper(), bodyFont2_bold)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase(cvr.cc, bodyFont2_bold)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase(veh_no, bodyFont2_bold)) { Colspan = 1 });
        //t.AddCell(new PdfPCell(new Phrase(cvr.purpose.ToUpper(), bodyFont2_bold)) { Colspan = 1 });
        t.AddCell(new PdfPCell(new Phrase("", bodyFont2)) { Colspan = 1, Border = 0 });

        t.WidthPercentage = 100;
        t.HorizontalAlignment = 0;
        t.SpacingBefore = 10;
        t.SpacingAfter = 1;
        t.DefaultCell.Border = 1;


        document.Add(t);

        //document.Add(new Paragraph("\n"));

        document.Add(new Paragraph("\nPlease note that if any claim is made prior to the renewal date of the policy, there will be a revision in the premium paid.\n\n", bodyFont2_bold));

        Paragraph s2 = new Paragraph("SRI LANKA", bodyFont);
        s2.SetAlignment("center");
        document.Add(s2);

        Paragraph s3 = new Paragraph("( The Motor Traffic Act. No. 14 of 1951)", bodyFont);
        s3.SetAlignment("center");
        document.Add(s3);

        Paragraph s4 = new Paragraph("CERTIFICATE OF INSURANCE", boldTableFont);
        s4.SetAlignment("center");
        document.Add(s4);

        document.Add(new Chunk("We hereby certify that the Covering Note is issued in accordance with the provisions of Part VI of the Motor Traffic Act No.  14 of 1951. ", bodyFont2));


        int[] clmwidths4 = { 12, 48, 40 };
        PdfPTable t2 = new PdfPTable(3);
        t2.SetWidths(clmwidths4);

        t2.WidthPercentage = 100;
        t2.HorizontalAlignment = 0;
        t2.SpacingBefore = 10;
        t2.SpacingAfter = 10;
        t2.DefaultCell.Border = 0;


        //********************* Commented this to include the cover note issued place to the Cover note print on 10th Jan 2014***********************//
        //t2.AddCell(new Phrase("Place  ", bodyFont2));
        //t2.AddCell(new Phrase(": " + cvr.get_branchName(Convert.ToInt32((string)Session["BrCode"])), boldTableFont));
        //t2.AddCell(new Phrase("SRI LANKA INSURANCE CORPORATION", bodyFont2));
        //******************************************************************************************************************************************//

        t2.AddCell(new Phrase(" ", bodyFont2));
        t2.AddCell(new Phrase("", boldTableFont));
        t2.AddCell(new Phrase(" ", bodyFont2));

        t2.AddCell(new Phrase("Issued Place  ", bodyFont2));
        t2.AddCell(new Phrase(": Online Customer Portal", boldTableFont));
        t2.AddCell(new Phrase("This is a System Generated Document,", bodyFont2));

        t2.AddCell(new Phrase("Issued Date     ", bodyFont2));
        t2.AddCell(new Phrase(": " + DateTime.Today.ToString("yyyy-MM-dd"), boldTableFont));
        t2.AddCell(new Phrase("Signatue is not required.", bodyFont2));

         

        document.Add(t2);

        int[] clmwidths5 = { 8, 14, 12, 10, 12, 10 };
        PdfPTable t3 = new PdfPTable(6);
        t3.SetWidths(clmwidths5);

        t3.WidthPercentage = 100;
        t3.HorizontalAlignment = 0;
        t3.SpacingBefore = 20;
        t3.SpacingAfter = 0;
        t3.DefaultCell.Border = 0;


        t3.AddCell(new Phrase("Receipt No  ", bodyFont4));
        t3.AddCell(new Phrase(": " + cvr.recNo, bodyFont4));
        t3.AddCell(new Phrase("Receipt Amount", bodyFont4));
        t3.AddCell(new Phrase(": " + cvr.recAmount.ToString("N2"), bodyFont4));
        t3.AddCell(new Phrase("Receipt Date", bodyFont4));
        t3.AddCell(new Phrase(": " + cvr.manRctDate, bodyFont4));

        document.Add(t3);


        writer.CloseStream = false;

        document.Close();

        output.Position = 0;

        Db_Email email = new Db_Email();
        //email.send_app_email(cp.O_email, "SLIC Motor Covernote", "Dear Customer, \nYour covernote is attached to this email. It is valid for " + cvr.duration.ToString() + " days starting from " + cvr.com_time + " on " + cvr.com_date_only + " to the same time on the " + cvr.duration.ToString() + "th day (" + cvr.exp_date_only + "). Renewal receipts and original certificate of the insured will be posted in due course (luxury/semi luxury taxes not applicable). For any queries, please contact SLIC Motor - 01123557012.", "<p>Dear Customer,<br/><br/>Your covernote is attached to this email. <br/>It is valid for " + cvr.duration.ToString() + " days starting from " + cvr.com_time + " on " + cvr.com_date_only + " to the same time on the " + cvr.duration.ToString() + "<sup>th</sup> day (" + cvr.exp_date_only + ").<br/>Renewal receipts and original certificate of the insured, will be posted in due course (luxury/semi luxury taxes not applicable).<br/>For any queries, please contact SLIC Motor - 01123557012. ", output, "covernote-" + c_number + ".pdf", "ansumalak@srilankainsurance.com");
        email.send_app_email(cp.O_email, "SLIC Motor Covernote", "Dear Customer, \nYour covernote is attached to this email. It is valid for " + cvr.duration.ToString() + " days starting from " + cvr.com_time + " on " + cvr.com_date_only + " to the same time on the " + cvr.duration.ToString() + "th day (" + cvr.exp_date_only + "). Renewal receipts and original certificate of the insured will be posted in due course (luxury/semi luxury taxes not applicable). For any queries, please contact SLIC Motor - 01123557012.", "<p>Dear Customer,<br/><br/>Your covernote is attached to this email. <br/>It is valid for " + cvr.duration.ToString() + " days starting from " + cvr.com_time + " on " + cvr.com_date_only + " to the same time on the " + cvr.duration.ToString() + "<sup>th</sup> day (" + cvr.exp_date_only + ").<br/>Renewal receipts and original certificate of the insured, will be posted in due course (luxury/semi luxury taxes not applicable).<br/>For any queries, please contact SLIC Motor - 01123557012. ", output, "covernote-" + c_number + ".pdf", null);

        output.Close();


    }
}
