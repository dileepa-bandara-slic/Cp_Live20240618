using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_PolRef
/// </summary>
public class TRV_PolRef
{
    OracleConnection objOraCon = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();

    public string SUFX1 { get; set; }
    public int BRCD { get; set; }
    public string SUFX2 { get; set; }
    public int YEAR { get; set; }
    public int SEQ { get; set; }

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
    public TRV_PolRef()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public TRV_PolRef(int brCode, int year,string Poltype)
    {
        try
        {
            connectDB();

            string sql = "SELECT SUFX1,BRCD,SUFX2,YEAR,SEQ FROM SLIGEN.TRV_POLREFNO " +
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

    public bool UpdateMaxSeqNo(int brCode, int lastTwoDigit, int seqNo,string Poltype)
    {
        int RECORD_COUNT = 0;
        bool state = false;
        try
        {
            connectDB();
            string recCount = "SELECT count(*) AS RECORD_COUNT FROM SLIGEN.TRV_POLREFNO WHERE BRCD = :brCode   AND YEAR = :year  and SUFX2= :Poltype  ";
            using (OracleCommand cmd = new OracleCommand(recCount, objOraCon))
            {
                OracleParameter opbrcode = new OracleParameter();
                opbrcode.Value = brCode;
                opbrcode.ParameterName = "brCode";

                OracleParameter opYear = new OracleParameter();
                opYear.Value = lastTwoDigit;
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
                    RECORD_COUNT = int.Parse(reader[0].ToString().Trim()); 
                }
                reader.Close();

                if (RECORD_COUNT == 0)
                {
                    string inserYr = "INSERT INTO SLIGEN.TRV_POLREFNO (BRCD,YEAR,SUFX1,SUFX2,SEQ) VALUES ( :brCode, :year,'G', :Poltype, :seqNo)";

                    cmd.CommandText = inserYr;

                    OracleParameter oppbrcode = new OracleParameter();
                    oppbrcode.Value = brCode;
                    oppbrcode.ParameterName = "brCode";

                    OracleParameter oppYear = new OracleParameter();
                    oppYear.Value = lastTwoDigit;
                    oppYear.ParameterName = "year";

                    OracleParameter oppPolTYpe = new OracleParameter();
                    oppPolTYpe.DbType = DbType.String;
                    oppPolTYpe.Value = Poltype;
                    oppPolTYpe.ParameterName = "Poltype";

                    OracleParameter oseq = new OracleParameter();
                    oseq.DbType = DbType.Int32;
                    oseq.Value = seqNo;
                    oseq.ParameterName = "seqNo";                     

                    cmd.Parameters.Add(oppbrcode);
                    cmd.Parameters.Add(oppYear);
                    cmd.Parameters.Add(oppPolTYpe);
                    cmd.Parameters.Add(oseq); 
 

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    state = true;
                }
                else
                {
                    string updateYr = "UPDATE SLIGEN.TRV_POLREFNO SET SEQ = :seqNo  WHERE BRCD = :brCode  AND YEAR= :year and SUFX2= :Poltype ";

                    cmd.CommandText = updateYr;

                    OracleParameter oppbrcode = new OracleParameter();
                    oppbrcode.Value = brCode;
                    oppbrcode.ParameterName = "brCode";

                    OracleParameter oppYear = new OracleParameter();
                    oppYear.Value = lastTwoDigit;
                    oppYear.ParameterName = "year";

                    OracleParameter oppPolTYpe = new OracleParameter();
                    oppPolTYpe.DbType = DbType.String;
                    oppPolTYpe.Value = Poltype;
                    oppPolTYpe.ParameterName = "Poltype";

                    OracleParameter oseq = new OracleParameter();
                    oseq.DbType = DbType.Int32;
                    oseq.Value = seqNo;
                    oseq.ParameterName = "seqNo";

                    cmd.Parameters.Add(oppbrcode);
                    cmd.Parameters.Add(oppYear);
                    cmd.Parameters.Add(oppPolTYpe);
                    cmd.Parameters.Add(oseq);


                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    state = true;
                }

            }
        }
        catch { state = false; }

        disconnectDB();
        return state;
    }

    public int GetMaxSeqNo(int brCode, int lastTwoDigit,string Poltype)
    {
        int seqNo = 0;
        this.connectDB();
        string maxSeq = "SELECT NVL(MAX(SEQ),0) AS SEQ FROM SLIGEN.TRV_POLREFNO WHERE BRCD = " + brCode + " AND YEAR = " + lastTwoDigit + " and SUFX2='"+Poltype+"' ";
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