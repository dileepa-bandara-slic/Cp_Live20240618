using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for TRV_Client
/// </summary>
public class TRV_Client
{
    OracleCommand command = new OracleCommand();
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

    public string clientId { get; set; }
    public string passport_no { get; set; }
    public string nic_no { get; set; }
    public string dob { get; set; }
    public string status { get; set; }
    public string profession { get; set; } 
    public string full_name { get; set; }
    public string initials { get; set; }
    public string last_name { get; set; }
    public string mobileno { get; set; }
    public string home_add1 { get; set; }
    public string home_add2 { get; set; }
    public string home_add3 { get; set; }
    public string home_add4 { get; set; }
    public string UserID { get; set; }
    public string error { get; set; }
	public TRV_Client()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bool GenerateClient(TRV_Client Client)
    {
        bool sucess = false;

        try
        {
            #region get Next  client ID
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            /*
            command.CommandText = "CLIENTDB.CLIENT_MODULE.getClientId";
            command.Parameters.AddWithValue("pBranchCode", 999);
            command.Parameters.AddWithValue("pCustomerType", "P");
            OracleParameter param = new OracleParameter("pClientId", OracleType.Number);
            param.Direction = ParameterDirection.Output;
            command.Parameters.Add(param);
            command.ExecuteNonQuery();
            clientId = command.Parameters["pClientId"].Value.ToString();
            command.Parameters.Clear();
            */

            using (OracleCommand cmd = new OracleCommand("CLIENTDB.CLIENT_MODULE.getClientId", oconn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("pBranchCode",999);
                cmd.Parameters.AddWithValue("pCustomerType", "P");
                OracleParameter param = new OracleParameter("pClientId", OracleType.Number);
                param.Direction = ParameterDirection.Output;
                OracleDataReader dr = cmd.ExecuteReader();
                clientId = command.Parameters["pClientId"].Value.ToString();  
                dr.Close();
                command.Parameters.Clear();
            }
            #endregion

            #region Update Cleint Details
            /*
            command.CommandText = "CLIENTDB.CLIENT_MODULE.personalClientInsert";

            command.Parameters.AddWithValue("cliet_id", Convert.ToInt64(clientId));
            command.Parameters.AddWithValue("passport_no", Client.passport_no.Trim().ToUpper());
            command.Parameters.AddWithValue("nic_no", Client.nic_no.Trim().ToUpper());
            command.Parameters.AddWithValue("dob",  Client.dob.Trim());
            command.Parameters.AddWithValue("status", Client.status.Trim());
            command.Parameters.AddWithValue("profession", Client.profession); 
            command.Parameters.AddWithValue("full_name", Client.full_name.ToUpper());
            command.Parameters.AddWithValue("initials", Client.initials.ToUpper());
            command.Parameters.AddWithValue("last_name", Client.last_name.ToUpper());
            //command.Parameters.AddWithValue("callname", this.txtCallname.Text.Trim());
            command.Parameters.AddWithValue("mobile", Client.mobileno);
            command.Parameters.AddWithValue("home_add1", Client.home_add1.Replace("'", "`"));
            command.Parameters.AddWithValue("home_add2", Client.home_add2.Replace("'", "`"));
            command.Parameters.AddWithValue("home_add3", Client.home_add3.Trim().Replace("'", "`"));
            command.Parameters.AddWithValue("home_add4", Client.home_add4.Replace("'", "`"));
            command.Parameters.AddWithValue("user_id", Client.UserID.Trim());
            command.Parameters.AddWithValue("date_time", DateTime.Now.Date);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            */
            #endregion
            oconn.Close();

            error = "sure";

            sucess = true;
        }
        catch (Exception exc)
        {
            sucess = false;
            error = exc.Message;

        }
        return sucess;
    }
}