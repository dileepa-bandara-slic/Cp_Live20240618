using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for GT_Quotation_mem
/// </summary>
public class TRV_Policy_mem
{
    public string POL_NO { get; private set; }
    public string member_id { get; private set; }
    public string member_type { get; private set; }
    public string memType_desc { get; private set; }
    public string gender { get; private set; }
    public string genderDesc { get; private set; }
    public string dob { get; private set; }
    public double age { get; private set; }    
    public string name { get; private set; }
    public string ppno { get; private set; }
    public string title { get; private set; }
    public double base_amount_usd { get; private set; }
    public string Enrty_Date { get; private set; }
    DataManager dm = new DataManager();

	public TRV_Policy_mem()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public TRV_Policy_mem(string mem_id,string polno)
    {
        string sql = "select POL_NO, MEM_ID, MEM_TYPE, GENDER, to_char(DOB, 'yyyy/mm/dd') AS DOB , AGE, NAME, PP_NO, TITLE," +
                     " BASE_AMOUNT_USD, ENTERED_DATE, decode(MEM_TYPE, 'M', 'Main Life', 'S', 'Spouse', 'C', 'Child', 'O', 'Other','N','Not Specified'), " +
                     " decode(GENDER, 'M', 'Male', 'F', 'Female') from SLIGEN.TRV_POL_MEM_DETAILS  WHERE MEM_ID = '" + mem_id.Trim() + "' AND POL_NO = '"+ polno + "' ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                POL_NO = row[0].ToString().Trim();
                member_id = mem_id.Trim();
                member_type = row[2].ToString().Trim();
                gender = row[3].ToString().Trim();
                dob = row[4].ToString().Trim();
                age = Convert.ToDouble(row[5].ToString().Trim());
                name = row[6].ToString().Trim();
                ppno = row[7].ToString().Trim();
                title = row[8].ToString().Trim();
                base_amount_usd = Convert.ToDouble(row[9].ToString().Trim());      
                Enrty_Date = row[10].ToString().Trim();
                memType_desc = row[11].ToString().Trim();
                genderDesc = row[12].ToString().Trim();
            }

        }
        dm.connclose();
    }
}