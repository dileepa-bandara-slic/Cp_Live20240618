using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TRV_Proposal_mem
/// </summary>
public class TRV_Proposal_mem
{
    public string ref_id { get; set; }
    public string member_id { get; set; }
    public string member_type { get; set; }
    public string memType_desc { get; set; }
    public string gender { get; set; }
    public string genderDesc { get; set; }
    public string dob { get; set; }
    public double age { get; set; }
    public string name { get; set; }
    public string ppno { get; set; }
    public string title { get; set; }
    public double base_amount_usd { get; set; }
    public string Enrty_Date { get; set; }

    OracleConnection objOraCon = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();
    public TRV_Proposal_mem()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void connectDB()
    {
        objOraCon.ConnectionString = ConfigurationManager.AppSettings.Get("OracleDB").ToString();
        if (objOraCon.State != ConnectionState.Open)
            objOraCon.Open();
    }
    public void disconnectDB()
    {
        if (objOraCon.State != ConnectionState.Closed)
        {
            objOraCon.Close();
        }
    }
    public TRV_Proposal_mem(string mem_id)
    {

        this.connectDB();
        string sql = "select REF_NO, MEM_ID, MEM_TYPE, GENDER, to_char(DOB, 'yyyy/mm/dd') AS DOB , AGE, NAME, PP_NO, TITLE," +
                     " BASE_AMOUNT_USD, ENTERED_DATE, MEM_TYPE, " +
                     " decode(GENDER, 'M', 'Male', 'F', 'Female') from SLIGEN.TRV_QUOT_MEM_DETAILS  WHERE MEM_ID = '" + mem_id.Trim() + "' ";
        objOraCom.CommandType = CommandType.Text;
        objOraCom.CommandText = sql;
        objOraCom.Connection = objOraCon;
        OracleDataReader objOraRdr = objOraCom.ExecuteReader();
        while (objOraRdr.Read())
        {

            ref_id = objOraRdr[0].ToString().Trim();
            member_id = mem_id.Trim();
            member_type = objOraRdr[2].ToString().Trim();
            gender = objOraRdr[3].ToString().Trim();
            dob = objOraRdr[4].ToString().Trim();
            age = Convert.ToDouble(objOraRdr[5].ToString().Trim());
            name = objOraRdr[6].ToString().Trim();
            ppno = objOraRdr[7].ToString().Trim();
            title = objOraRdr[8].ToString().Trim();
            base_amount_usd = Convert.ToDouble(objOraRdr[9].ToString().Trim());
            Enrty_Date = objOraRdr[10].ToString().Trim();
            memType_desc = objOraRdr[11].ToString().Trim();
            genderDesc = objOraRdr[12].ToString().Trim();
        }

        objOraRdr.Close();
        this.disconnectDB();
        
    }
    public TRV_Proposal_mem(string MemID, DataTable dtMem)
    {
        this.connectDB();
        string sql = "select REF_NO as ref_id, MEM_ID as member_id, MEM_TYPE as member_type, GENDER as gender, to_char(DOB, 'yyyy/mm/dd') AS dob , AGE, NAME, PP_NO as ppno, TITLE," +
                     " BASE_AMOUNT_USD, ENTERED_DATE as Enrty_Date, MEM_TYPE as memType_desc, " +
                     " decode(GENDER, 'M', 'Male', 'F', 'Female') as genderDesc from SLIGEN.TRV_QUOT_MEM_DETAILS  WHERE REF_NO = '" + MemID.Trim() + "' ";
        using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
        {
            OracleDataAdapter dao = new OracleDataAdapter(sql, objOraCon);
            dao.Fill(dtMem);
        }
    }
}