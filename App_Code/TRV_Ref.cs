using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for TRV_Ref
/// </summary>
public class TRV_Ref
{
    OracleConnection objOraCon = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();

    public string SUFX1 { get; set; }
    public int BRCD { get; set; }
    public string SUFX2 { get; set; }
    public int YEAR { get; set; }
    public int SEQ { get; set; }
    public TRV_Ref()
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

    public TRV_Ref(int brCode, int year,string Poltype)
    {
        try
        {
            connectDB();

            string sql = "SELECT SUFX1,BRCD,SUFX2,YEAR,SEQ FROM SLIGEN.TRV_REFNO " +
                         " WHERE BRCD = :brCode AND YEAR = :year and SUFX2= :Poltype";

            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleParameter opbrcode = new OracleParameter();
                opbrcode.Value = brCode;
                opbrcode.ParameterName = "brCode";

                OracleParameter opYear = new OracleParameter();
                opYear.Value = year;
                opYear.ParameterName = "year";

                OracleParameter opPolTYpe = new OracleParameter();
                opPolTYpe.DbType = DbType.String;
                opPolTYpe.Value = Poltype;
                opPolTYpe.ParameterName = "Poltype";

                cmd.Parameters.Add(opbrcode);
                cmd.Parameters.Add(opYear);
                cmd.Parameters.Add(opPolTYpe);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SUFX1 = reader[0].ToString().Trim();
                    BRCD = Convert.ToInt32(reader[1].ToString());
                    SUFX2 = reader[2].ToString().Trim();
                    YEAR = Convert.ToInt32(reader[3].ToString());
                    SEQ = Convert.ToInt32(reader[4].ToString());

                }
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            disconnectDB();
        }

    }

    public bool UpdateMaxSeqNo(int brCode, int lastTwoDigit,int seqNo,string PolType)
    {
        int RECORD_COUNT = 0;
        int val1 = 0;
        bool state = false;
        this.connectDB();
        string reCount = "SELECT count(*) AS RECORD_COUNT FROM SLIGEN.TRV_REFNO WHERE BRCD = " + brCode + " AND YEAR = "+ lastTwoDigit + " and SUFX2='"+PolType+"'";
        objOraCom.CommandType = CommandType.Text;
        objOraCom.CommandText = reCount;
        objOraCom.Connection = objOraCon;
        OracleDataReader objOraRdr = objOraCom.ExecuteReader();

        while (objOraRdr.Read())
        {
            if (!objOraRdr.IsDBNull(0))
            {
                RECORD_COUNT = objOraRdr.GetInt32(0);
            }

            if (RECORD_COUNT == 0)
            {
                string inserYr = "";
                 
                    inserYr = "INSERT INTO SLIGEN.TRV_REFNO (BRCD,YEAR,SUFX1,SUFX2,SEQ) VALUES (" + brCode + "," + lastTwoDigit + ",'G','"+PolType+"'," + seqNo + ")";
                 
                objOraCom.CommandText = inserYr;
                val1 = objOraCom.ExecuteNonQuery();
                
            }
            else
            {
                string updateYr = "UPDATE SLIGEN.TRV_REFNO SET SEQ = " + seqNo + " WHERE BRCD = " + brCode + " AND YEAR= "+ lastTwoDigit + " and SUFX2='"+PolType+"'";

                objOraCom.CommandText = updateYr;
                val1 = objOraCom.ExecuteNonQuery();
            }
        }
        objOraRdr.Close();

        if (val1 >= 1)
        {
            state = true;
        }
        else
        {
            state = false;
        }
        this.disconnectDB();
        return state;
    }

    public int GetMaxSeqNo(int brCode, int lastTwoDigit,string Poltype)
    {
        int seqNo = 0;
        this.connectDB();
        string maxSeq = "SELECT NVL(MAX(SEQ),0) AS SEQ FROM SLIGEN.TRV_REFNO WHERE BRCD = " + brCode + " AND YEAR = " + lastTwoDigit + " and SUFX2='"+Poltype+"'";
        objOraCom.CommandType = CommandType.Text;
        objOraCom.CommandText = maxSeq;
        objOraCom.Connection = objOraCon;
        OracleDataReader objOraRdr = objOraCom.ExecuteReader();
        while (objOraRdr.Read())
        {
            if (!objOraRdr.IsDBNull(0))
            {
                seqNo = Convert.ToInt32(objOraRdr[0].ToString());
            }
            
        }
        objOraRdr.Close();
        this.disconnectDB();
        return seqNo;
    }
    
}