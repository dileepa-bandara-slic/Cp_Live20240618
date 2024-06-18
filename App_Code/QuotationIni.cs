using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;

/// <summary>
/// Summary description for QuotationIni
/// </summary>
public class QuotationIni
{
    public OracleCommand oraComm = new OracleCommand();
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);
	public QuotationIni()
	{
		//
		// TODO: Add constructor logic here
		//
	}    

    public string generate_proposalID(string dpt,int year, string type, int branch)
    {
        string result = "";
        string id = "";
        string n = "";
       
        string sql = "SELECT * FROM SLIGEN.GEN_QUOT_REF WHERE DEPARTMENT = '" + dpt.Trim() + "' AND YEAR = " + year.ToString().Trim() + " AND TYP = '" + type.Trim() + "' and BRANCH = '"+branch.ToString()+"'";        
        
        conn.Open();
        using (OracleCommand cmd = new OracleCommand(sql, conn))
        {
            OracleDataReader sqlReader = cmd.ExecuteReader();

            if (sqlReader.HasRows)
            {
                result = update_propSeq(sql, dpt, year, type, branch).ToString();
            }
            else
            {
                result = insert_newSeq(dpt, year, type, branch).ToString();
            }
        }

        string seq = result.ToString();

        string brnch = branch.ToString();

        for (int j = brnch.Length; j < 3; j++)
        {
            brnch = "0" + brnch;
        }

        //for (int k = seq.Length; k < 7; k++)
        //{
        //    seq = "0" + seq;
        //}
        id = "G/" + brnch.Trim() + "/" + type + "/" + year.ToString() + "/" + seq;

        result = id;

        if (conn != null) conn.Close();

        return result;
    }

    private int update_propSeq(string sql, string dpt, int year, string type, int branch)
    {
        int seqNo = 0;
        seqNo = get_max_propNo(sql);
        string sql2 = "UPDATE SLIGEN.GEN_QUOT_REF SET REFNO = " + seqNo + " WHERE DEPARTMENT = '" + dpt.Trim() + "' AND YEAR = " + year.ToString().Trim() + " AND TYP = '" + type.Trim() + "' and BRANCH = '" + branch.ToString() + "'";
        oraComm.Connection = conn;
        oraComm.CommandText = sql2;
        oraComm.ExecuteNonQuery();
        return seqNo;
    }

    private int get_max_propNo(string sql)
    {
        int result = 0;
        try
        {

            oraComm.Connection = conn;
            oraComm.CommandText = sql;
            OracleDataReader reader = oraComm.ExecuteReader();
            while (reader.Read())
            {
                result = Convert.ToInt32(reader["REFNO"]);
                break;
            }
            result++;
        }
        catch
        { }
        finally
        {

        }
        return result;
    }

    private int insert_newSeq(string dpt, int year, string type, int branch)
    {
        string sql2 = "INSERT INTO SLIGEN.GEN_QUOT_REF VALUES ('" + dpt + "', " + year.ToString().Trim() + " , '"+type+"'," + 1 + ", "+branch+")";
        oraComm.Connection = conn;
        oraComm.CommandText = sql2;
        oraComm.ExecuteNonQuery();
        return 1;
    }
}