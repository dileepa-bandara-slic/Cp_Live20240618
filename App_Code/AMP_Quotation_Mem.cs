using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Quotation_Mem
/// </summary>
public class AMP_Quotation_Mem
{
    public string quotaion_id { get; private set; }
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


    public string Name { get; private set; }
    public string NIC { get; private set; }

    DataManager dm = new DataManager();

	public AMP_Quotation_Mem()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public AMP_Quotation_Mem(string mem_id)
    {
        string sql = "select QT_ID, MEM_ID, MEM_TYPE, GENDER, to_char(DOB, 'yyyy/mm/dd') AS DOB , AGE, BASE_AMOUNT, MATERN_SLIC, FL_DISCONTD, FINAL_PRM, ENTERED_EPF, ENTERED_DATE, NAME, NIC  from SLIGEN.AMP_QT_MEM_DETAILS  WHERE MEM_ID = '" + mem_id.Trim() + "' ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                quotaion_id = row[0].ToString().Trim();
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

                if (row[12] == null)
                    Name = "";
                else
                    Name = row[12].ToString().Trim();

                if (row[13] == null)
                    NIC = "";
                else
                    NIC = row[13].ToString().Trim();

            }

        }
        dm.connclose();
    }


    //public bool insert_member(string Qid, string Mid, string MType, string gender, string dob, double age, double baseAmnt, double materSlic, double flDscnt, double finalPerm, string epf)
    //{
    //    bool result = false;
 
    //}
}