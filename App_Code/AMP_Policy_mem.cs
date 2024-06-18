using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Policy_mem
/// </summary>
public class AMP_Policy_mem
{
    public string PolicyNo { get; private set; }
    public string member_id { get; private set; }
    public string member_type { get; private set; }
    public string gender { get; private set; }
    public string dob { get; private set; }
    public double age { get; private set; }
    public double base_amount { get; private set; }
    public double matern_slic { get; private set; }
    public double fl_discount { get; private set; }
    public double final_prm { get; private set; }
    public string Entry_EPF { get; private set; }
    public string Enrty_Date { get; private set; }
    public string Start_Date { get; private set; }
    public string name { get; private set; }
    DataManager dm = new DataManager();
	public AMP_Policy_mem()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public AMP_Policy_mem(string mem_id)
    {
        string sql = "select POLICY_NUMBER, MEM_ID, MEM_TYPE, GENDER, to_char(DOB, 'yyyy/mm/dd') AS DOB , AGE, BASE_AMOUNT, MATERN_SLIC, FL_DISCONTD, FINAL_PRM, ENTERED_EPF, ENTERED_DATE, START_DATE, NAME  from SLIGEN.AMP_QT_MEM_DETAILS  WHERE MEM_ID = '" + mem_id.Trim() + "' ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                PolicyNo = row[0].ToString().Trim();
                member_id = mem_id.Trim();
                member_type = row[2].ToString().Trim();
                gender = row[3].ToString().Trim();
                dob = row[4].ToString().Trim();
                age = Convert.ToDouble(row[5].ToString().Trim());
                base_amount = Convert.ToDouble(row[6].ToString().Trim());
                matern_slic = Convert.ToDouble(row[7].ToString().Trim());
                fl_discount = Convert.ToDouble(row[8].ToString().Trim());
                final_prm = Convert.ToDouble(row[9].ToString().Trim());


                Entry_EPF = row[10].ToString().Trim();
                Enrty_Date = row[11].ToString().Trim();
                Start_Date = row[12].ToString().Trim();
                name = row[13].ToString().Trim();

            }

        }
    }
}