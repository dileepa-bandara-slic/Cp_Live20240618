using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Policy_mast
/// </summary>
public class AMP_Policy_mast
{

    public string policyNo { get; private set; }
    public string comencementDate { get; private set; }
    public string startDate { get; private set; }
    public string endDate { get; private set; }
    public string status { get; private set; }
    public string name_1 { get; private set; }
    public string name_2 { get; private set; }
    public string add_1 { get; private set; }
    public string add_2 { get; private set; }
    public string add_3 { get; private set; }
    public string add_4 { get; private set; }
    public string homePhoneNo { get; private set; }
    public string mobilePhoneNo { get; private set; }
    public int no_persons { get; private set; }
    public double discount_rate_family { get; private set; }
    public double net_premium { get; private set; }
    public double admin_fee { get; private set; }
    public double policy_fee { get; private set; }
    public double nbt { get; private set; }
    public double vat { get; private set; }
    public double final_premium { get; private set; }
    public double re_creed_amnt { get; private set; }
    public double slic_retention { get; private set; }
    public double taxes_Exp { get; private set; }
    public string Nic { get; private set; }
    public string plan { get; private set; }
    public int branch { get; private set; }
    public string QuotationNO { get; private set; }
    public string receiptNo { get; private set; }
    public string Entry_EPF { get; private set; }
    public string Enrty_Date { get; private set; }

    public double plan_limit { get; private set; }

    public List<AMP_Policy_mem> members = new List<AMP_Policy_mem>();
    DataManager dm = new DataManager();

    public AMP_Policy_mast()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public AMP_Policy_mast(string policy_number)
    {
        string sql = "select POLICY_NUMBER,  to_char(COM_DATE, 'yyyy/mm/dd') as COM_DATE, to_char(START_DATE, 'yyyy/mm/dd') AS START_DATE, to_char(END_DATE, 'yyyy/mm/dd') AS END_DATE, STATUS, NAME_1, NAME_2, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, HOME_PHONE_NO, MOBILE_PHONE_NO, NUM_OF_PERSONS, DISCOUNT_RATE_FAMILY, "
            + " NET_PREMIUM, ADMIN_FEE, POLICY_FEE, NBT, VAT, FINAL_PREMIUM, RE_CEEDED_AMOUNT, SLIC_RETENTION, TAXES_EXPENSES, NIC_NUMBER, PLAN, BRANCH_CODE,QUOT_NUMBER,RECEIPT_NO, ENTERED_EPF, to_char(ENTERED_DATE , 'yyyy/mm/dd') AS ENTERED_DATE , plan_limit"
        + " from SLIGEN.AMP_POLICIES  WHERE POLICY_NUMBER = '" + policy_number.Trim() + "' ";
       if (dm.existRecored(sql) > 0)
       {
           DataSet ds = new DataSet();
           ds = dm.getrow(sql);
           DataTable dt = ds.Tables[0];

           foreach (DataRow row in dt.Rows)
           {
                policyNo = policy_number.Trim();
                comencementDate = row[1].ToString().Trim();
                startDate = row[2].ToString().Trim();
                endDate = row[3].ToString().Trim();
                status = row[4].ToString().Trim();
                name_1 = row[5].ToString().Trim();
                name_2 = row[6].ToString().Trim();
                add_1 = row[7].ToString().Trim();
                add_2 = row[8].ToString().Trim();
                add_3 = row[9].ToString().Trim();
                add_4 = row[10].ToString().Trim();
                homePhoneNo = row[11].ToString().Trim();
                mobilePhoneNo = row[12].ToString().Trim();
                no_persons = Convert.ToInt32(row[13].ToString().Trim());
                discount_rate_family = Convert.ToDouble(row[14].ToString().Trim());
                net_premium = Convert.ToDouble(row[15].ToString().Trim());
                admin_fee = Convert.ToDouble(row[16].ToString().Trim());
                policy_fee = Convert.ToDouble(row[17].ToString().Trim());
                nbt = Convert.ToDouble(row[18].ToString().Trim());
                vat = Convert.ToDouble(row[19].ToString().Trim());
                final_premium = Convert.ToDouble(row[20].ToString().Trim());
                re_creed_amnt = Convert.ToDouble(row[21].ToString().Trim());
                slic_retention = Convert.ToDouble(row[22].ToString().Trim());
                taxes_Exp = Convert.ToDouble(row[23].ToString().Trim());
                Nic = row[24].ToString().Trim();
                plan = row[25].ToString().Trim();
                branch = Convert.ToInt32(row[26].ToString().Trim());
                QuotationNO = row[27].ToString().Trim();
                receiptNo = row[28].ToString().Trim();
                Entry_EPF = row[29].ToString().Trim();
                Enrty_Date = row[30].ToString().Trim();

                if (row[31] != null)
                {
                    plan_limit = Convert.ToDouble(row[31].ToString().Trim());
                }
                else
                {
                    plan_limit = 0;
                }

           }
           this.get_members(policy_number);
       }

    }

    private void get_members(string policy_number)
    {
        AMP_Policy_mem  mem;
        string sql = "select MEM_ID from SLIGEN.AMP_POL_MEM_DETAILS  WHERE POLICY_NUMBER = '" + policy_number.Trim() + "'  ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                string member_id = row[0].ToString().Trim();
                mem = new AMP_Policy_mem(member_id);
                members.Add(mem);
            }
        }
    }

    public bool females_exists(string policy_number)
    {
        string sql = "select * from SLIGEN.AMP_POL_MEM_DETAILS  WHERE POLICY_NUMBER = '" + policy_number.Trim() + "' and gender = 'F'";
        bool result = false;

        if (dm.existRecored(sql) > 0)
        {
            result = true;
        }
        dm.oraConn.Close();

        return result;

    }

    public DataTable get_All_members_lst_policy(string policy_number)
    {
        string sql = "select MEM_ID,MEM_NAME, to_char(DOB,'yyyy/mm/dd')  as dob, MEM_TYPE" +
" from SLIGEN.AMP_POL_MEM_DETAILS " +
" where POLICY_NUMBER = '" + policy_number + "'" +
" order by mem_id ";
        DataTable dt = null;

        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            dt = ds.Tables[0];
        }
        dm.connclose();

        return dt;

    }

    
}