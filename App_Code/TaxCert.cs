using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for TaxCert
/// </summary>
public class TaxCert
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);


	public TaxCert()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void enter_tax_certicates(int year, string epf, string ip, int policyNo, out bool result, out string refno)
    {
        result = false;
        refno = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string sql2 = "INSERT INTO SLIC_NET_LIFE.TAX_CERTIFICATES (REF_NO, TAX_YEAR, ENTRY_USR, ENTRY_DATE, ENTRY_IP, POLICYNO) " +
                " VALUES (:ref_no , :yr, :usr01, sysdate, :ipp, :polNo)";

            using (OracleCommand cmd = new OracleCommand(sql2, oconn))
            {
                refno = this.generate_renwReceiptNo(year, cmd);
                cmd.CommandText = sql2;
                //cmd.Parameters.Clear();

                OracleParameter oprefno = new OracleParameter();
                oprefno.Value = refno;
                oprefno.ParameterName = "ref_no";

                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "yr";

                OracleParameter opusr = new OracleParameter();
                opusr.Value = epf;
                opusr.ParameterName = "usr01";

                OracleParameter opip = new OracleParameter();
                opip.Value = ip;
                opip.ParameterName = "ipp";

                OracleParameter opolNo = new OracleParameter();
                opolNo.DbType = DbType.Int32;
                opolNo.Value = policyNo;
                opolNo.ParameterName = "polNo";

                cmd.Parameters.Add(oprefno);
                cmd.Parameters.Add(oYear);
                cmd.Parameters.Add(opusr);
                cmd.Parameters.Add(opip);
                cmd.Parameters.Add(opolNo);



                int i = 0;
                i = cmd.ExecuteNonQuery();

                if (i > 0)
                    result = true;

            }

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log(refno);
            logger.write_log("Failed at insert_newSeq " + e.ToString());
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        //return result;
    }

    private string generate_renwReceiptNo(int year, OracleCommand pconn)
    {
        pconn.Parameters.Clear();
        string result = "";
        string id = "";
        //string n = "";
        try
        {
            //oconn.Open();
            string sql = "SELECT * FROM SLIC_NET_LIFE.TAX_REFNO WHERE YEAR = :yr ";
            int rows = 0;
            //using (OracleCommand cmd = new OracleCommand(sql, oconn))
            //{
                pconn.CommandText = sql;
                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "yr";


                pconn.Parameters.Add(oYear);

                OracleDataReader refNoReader = pconn.ExecuteReader();

                while (refNoReader.Read())
                {
                    rows++;
                }
                refNoReader.Close();
                pconn.Parameters.Clear();
            //}

            if (rows > 0)
            {
                
                result = update_renwSeq(sql, year, pconn).ToString();
            }
            else
            {
                result = insert_newSeq(year, pconn).ToString();
            }

            string seq = result.ToString().PadLeft(7, '0');

            id = year.ToString() + "/" + seq;

            result = id;
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at generate_renwReceiptNo " + e.ToString());
        }
        finally
        {
            //if (oconn.State == ConnectionState.Open)
            //{
            //    oconn.Close();
            //}
        }
        return result;
    }

    private int update_renwSeq(string sql, int year, OracleCommand ppconn)
    {
        int seqNo = 0;
        try
        {
            seqNo = get_max_refNo(sql, year, ppconn);
            string sql2 = "UPDATE SLIC_NET_LIFE.TAX_REFNO SET REFNO = :seqNo WHERE YEAR = :year";
            
            //oconn.Open();

            //using (OracleCommand cmd = new OracleCommand(sql2, oconn))
            //{

            ppconn.CommandText = sql2;

                OracleParameter oSeqNo = new OracleParameter();
                oSeqNo.DbType = DbType.Int32;
                oSeqNo.Value = seqNo;
                oSeqNo.ParameterName = "seqNo";

                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "year";



                ppconn.Parameters.Add(oSeqNo);
                ppconn.Parameters.Add(oYear);

                ppconn.ExecuteNonQuery();
                ppconn.Parameters.Clear();

           // }
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at update_renwSeq " + e.ToString());
        }
        finally
        {
            //if (oconn.State == ConnectionState.Open)
            //{
            //    oconn.Close();
            //}
        }
        return seqNo;
    }

    private int get_max_refNo(string sql, int year, OracleCommand pppcom)
    {
        int result = 0;
        try
        {
            //if (oconn.State != ConnectionState.Open)
            //{
            //    oconn.Open();
            //}

            //using (OracleCommand cmd = new OracleCommand(sql, oconn))
            //{
            pppcom.CommandText = sql;

                OracleParameter oYear = new OracleParameter();
                oYear.DbType = DbType.Int32;
                oYear.Value = year;
                oYear.ParameterName = "yr";


                pppcom.Parameters.Add(oYear);

                OracleDataReader reader = pppcom.ExecuteReader();
                while (reader.Read())
                {
                    result = Convert.ToInt32(reader["REFNO"]);
                    break;
                }
                pppcom.Parameters.Clear();
            //}
            result++;
        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at get_max_propNo " + e.ToString());
        }
        finally
        {
            //if (oconn.State == ConnectionState.Open)
            //{
            //    oconn.Close();
            //}
        }
        return result;
    }

    private int insert_newSeq(int year, OracleCommand ppppcom)
    {
        try
        {
            //if (oconn.State != ConnectionState.Open)
            //{
            //    oconn.Open();
            //}
            string sql2 = "INSERT INTO SLIC_NET_LIFE.TAX_REFNO VALUES (:year ," + 1 + ")";

            //using (OracleCommand cmd = new OracleCommand(sql2, oconn))
            //{          
            ppppcom.CommandText = sql2;

                OracleParameter oYear = new OracleParameter();
                oYear.Value = year;
                oYear.ParameterName = "year";


                ppppcom.Parameters.Add(oYear);

                ppppcom.ExecuteNonQuery();
                ppppcom.Parameters.Clear();

            //}

        }
        catch (Exception e)
        {
            log logger = new log();
            logger.write_log("Failed at insert_newSeq " + e.ToString());
        }
        finally
        {
            //if (oconn.State == ConnectionState.Open)
            //{
            //    oconn.Close();
            //}
        }
        return 1;
    }

}