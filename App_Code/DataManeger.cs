using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Runtime.Serialization;
using System.Data.SqlClient;

/// <summary>
/// Summary description for OraManager
/// </summary>

[Serializable()]
public class DataManager
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OraLifeDB"]);

    [NonSerialized()]
    public OracleConnection oraConn = new OracleConnection();
    [NonSerialized()]
    public OracleCommand oraComm = new OracleCommand();
    [NonSerialized()]
    public OracleTransaction oraTrans;

    public DataManager()
    {
        try
        {
            oraConn.ConnectionString = ConfigurationManager.AppSettings["OracleDB"];
            if (oraConn.State != ConnectionState.Open)
                oraConn.Open();
        }
        catch (Exception ee)
        {
            throw ee;
        }
    }

    public void readSql(string sql)
    {
        if (oraConn.State != ConnectionState.Open)
        { oraConn.Open(); }
        try
        {
            oraComm.CommandText = sql;
            oraComm.Connection = oraConn;
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }

    public void connClose()
    {
        oraConn.Close();
        oraConn.Dispose();
    }
    
    public DataSet getrow(string sql)
    {
        
        try
        {
            oraComm.Connection = oraConn;
            //oraConn.ConnectionString = ConfigurationManager.AppSettings["DBConString"];
            OracleDataAdapter dataAdd = new OracleDataAdapter(sql, oraConn);
            DataSet ds = new DataSet();
            ds.Clear();
            dataAdd.Fill(ds);
           

            if (oraConn.State == ConnectionState.Open)
            {
                oraConn.Close();
            }
            return ds;
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }

    public void begintransaction()
    {

        oraTrans = oraConn.BeginTransaction();
        oraComm.Transaction = oraTrans;
    }
    public void rollback()
    {
        oraTrans.Rollback();
    }
    public void connclose()
    {
        if (oraConn.State != ConnectionState.Closed)
        {
            oraConn.Close();
            //oraConn.Dispose();
        }
    }
    public void commit()
    {
        oraTrans.Commit();
    }


    public int InsertRecords(string sSql)
    {
        if (oraConn.State != ConnectionState.Open)
        { oraConn.Open(); }
        try
        {
            int numRows = 0;
            oraComm.Connection = oraConn;
            oraComm.CommandText = sSql;

            numRows = oraComm.ExecuteNonQuery();

            return numRows;
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }

    public int existRecored(string sSql)
    {
        if (oraConn.State != ConnectionState.Open)
        { oraConn.Open(); }
        try
        {
            int mobile = 0;
            oraComm.Connection = oraConn;
            oraComm.CommandText = sSql;

            mobile = oraComm.ExecuteNonQuery();

            return mobile;
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }

    public string getMobileNo(string username)
    {
        string retrievedMobileNo = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkMobileMatch = "select mobile_number from ulweb.webusers where username =:username";

            using (OracleCommand cmd = new OracleCommand(checkMobileMatch, oconn))
            {
                cmd.Parameters.AddWithValue("username", username);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            retrievedMobileNo = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            username = "Error";
            log logger = new log();
            //logger.write_log("mobile no does not exist" + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        log logger2 = new log();
        logger2.write_log("mobileNo " + retrievedMobileNo);
        return retrievedMobileNo;
    }

    public string getEmailMobile(string email)
    {
        string retrievedMobileNo = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkMobileMatch = "select mobile_number from ulweb.webusers where email =:email";

            using (OracleCommand cmd = new OracleCommand(checkMobileMatch, oconn))
            {
                cmd.Parameters.AddWithValue("email", email);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            retrievedMobileNo = reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            email = "Error";
            log logger = new log();
            //logger.write_log("mobile no does not exist" + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        log logger2 = new log();
        logger2.write_log("mobileNo " + retrievedMobileNo);
        return retrievedMobileNo;
    }

    public bool checkUsernameExist(string username)
    {
        bool usernameExist = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkUserNameMatch = "select username from ulweb.webusers where username =:username";

            using (OracleCommand cmd = new OracleCommand(checkUserNameMatch, oconn))
            {
                cmd.Parameters.AddWithValue("username", username);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    usernameExist = true;
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            usernameExist = false;
            log logger = new log();
            //logger.write_log("Username does not exist: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return usernameExist;
    }

    public bool checkEmailExist(string email)
    {
        bool emailExist = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string checkEmailMatch = "select email from ulweb.webusers where email = :email";

            using (OracleCommand cmd = new OracleCommand(checkEmailMatch, oconn))
            {
                cmd.Parameters.AddWithValue("email", email);

                OracleDataReader reader = (OracleDataReader)cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    emailExist = true;
                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            emailExist = false;
            log logger = new log();
            //logger.write_log("Email does not exist: " + e.ToString());

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return emailExist;
    }
}

