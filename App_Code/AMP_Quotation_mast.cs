using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Globalization;

/// <summary>
/// Summary description for Quotation_mast
/// </summary>
public class AMP_Quotation_mast
{

    public string quotationID { get; private set; }
    public string dept { get; private set; }
    public string type { get; private set; }
    public string status { get; private set; }
    public string name_1 { get; private set; }
    public string name_2 { get; private set; }
    public string add_1 { get; private set; }
    public string add_2 { get; private set; }
    public string add_3 { get; private set; }
    public string add_4 { get; private set; }
    public string home_tp { get; private set; }
    public string mobile_tp { get; private set; }
    public int no_persons { get; private set; }
    public double tot_individual { get; private set; }
    public double discount_rate_family { get; private set; }
    public double discount_amnt_family { get; private set; }
    public double net_premium { get; private set; }
    public double admin_fee { get; private set; }
    public double policy_fee { get; private set; }
    public double nbt { get; private set; }
    public double vat { get; private set; }
    public double final_premium { get; private set; }
    public double re_creed_amnt { get; private set; }
    public double slic_retention { get; private set; }
    public double taxes_Exp { get; private set; }
    public string Entry_EPF { get; private set; }
    public string Enrty_Date { get; private set; }
    public string Nic { get; private set; }
    public List<AMP_Quotation_Mem> members = new List<AMP_Quotation_Mem>();// {get; private set;}
    public string plan { get; private set; }
    public int branch { get; private set; }
    public double plan_limit { get; private set; }
    public double ncb { get; private set; }
    public double accumulative { get; private set; }

    DataManager dm = new DataManager();

	public AMP_Quotation_mast()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public AMP_Quotation_mast(string qid)
    {
        string sql = "select QUOT_NUMBER, DEPT, TYPE, STATUS, NAME_1, NAME_2, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, HOME_PHONE_NO, MOBILE_PHONE_NO, NUM_OF_PERSONS, DISCOUNT_RATE_FAMILY, "
            + " NET_PREMIUM, ADMIN_FEE, POLICY_FEE, NBT, VAT, FINAL_PREMIUM, RE_CEEDED_AMOUNT, SLIC_RETENTION, TAXES_EXPENSES, ENTERED_EPF, to_char(ENTERED_DATE , 'yyyy/mm/dd') AS ENTERED_DATE , NIC_NUMBER, PLAN, BRANCH_CODE, PLAN_LIMIT, NCB, ACCUMULATIVE"
        +" from SLIGEN.QUOT_MAST  WHERE QUOT_NUMBER = '" + qid.Trim() + "' ";
        if (dm.existRecored(sql) > 0)
        {
            DataSet ds = new DataSet();
            ds = dm.getrow(sql);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                quotationID = qid.Trim();
                dept = row[1].ToString().Trim();
                type = row[2].ToString().Trim();
                status = row[3].ToString().Trim();
                name_1 = row[4].ToString().Trim();
                name_2 = row[5].ToString().Trim();
                add_1 = row[6].ToString().Trim();
                add_2 = row[7].ToString().Trim();
                add_3 = row[8].ToString().Trim();
                add_4 = row[9].ToString().Trim();
                home_tp = row[10].ToString().Trim();
                mobile_tp = row[11].ToString().Trim();
                no_persons = Convert.ToInt32(row[12].ToString().Trim());
                //tot_individual = Convert.ToDouble(row[13].ToString().Trim());
                discount_rate_family = Convert.ToDouble(row[13].ToString().Trim());
                //discount_amnt_family = Convert.ToDouble(row[15].ToString().Trim());
                net_premium = Convert.ToDouble(row[14].ToString().Trim());
                admin_fee = Convert.ToDouble(row[15].ToString().Trim());
                policy_fee = Convert.ToDouble(row[16].ToString().Trim());
                nbt = Convert.ToDouble(row[17].ToString().Trim());
                vat = Convert.ToDouble(row[18].ToString().Trim());
                final_premium = Convert.ToDouble(row[19].ToString().Trim());
                re_creed_amnt = Convert.ToDouble(row[20].ToString().Trim());
                slic_retention = Convert.ToDouble(row[21].ToString().Trim());
                taxes_Exp = Convert.ToDouble(row[22].ToString().Trim());
                Entry_EPF = row[23].ToString().Trim();
                Enrty_Date = row[24].ToString().Trim();
                Nic = row[25].ToString().Trim();
                plan = row[26].ToString().Trim();
                branch = Convert.ToInt32(row[27].ToString().Trim());
                plan_limit= Convert.ToDouble(row[28].ToString().Trim());
                ncb = Convert.ToDouble(row[29].ToString().Trim());
                accumulative = Convert.ToDouble(row[30].ToString().Trim());
            }
            this.get_members(quotationID);
        }

        dm.connclose();
    }

    public DataTable get_members_lst(string quotation_id)
    {
        string sql = "select mem_type, gender, count(*) " +
" from sligen.amp_qt_mem_details " +
" where qt_id = '" + quotation_id + "' and mem_type <> 'M' " +
" group by mem_type, gender " +
" order by mem_type desc, gender desc";
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

    public DataTable get_All_members_lst(string quotation_id)
    {
        string sql = "select mem_type, gender, count(*) " +
" from sligen.amp_qt_mem_details " +
" where qt_id = '" + quotation_id + "' " +
" group by mem_type, gender " +
" order by mem_type desc, gender desc";
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


    public DataTable get_All_members_lst_policy(string quotation_id)
    {
        string sql = "select nic, name, to_char(dob,'yyyy/mm/dd')  as dob, mem_type, gender, PP_NO" +
" from sligen.amp_qt_mem_details " +
" where qt_id = '" + quotation_id + "'" +
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

    public bool females_exists(string quotation_id)
    {
        string sql = "select *  from sligen.amp_qt_mem_details  where qt_id = '" + quotation_id.Trim() + "' and gender = 'F'";
        bool result = false;

        if (dm.existRecored(sql) > 0)
        {
            result = true;
        }
        dm.oraConn.Close();

        return result;

    }

    private void get_members(string quotation_id)
    {
        AMP_Quotation_Mem mem;
        string sql = "select MEM_ID from SLIGEN.AMP_QT_MEM_DETAILS  WHERE QT_ID = '" + quotation_id.Trim() + "'  ";
         if (dm.existRecored(sql) > 0)
         {
             DataSet ds = new DataSet();
             ds = dm.getrow(sql);
             DataTable dt = ds.Tables[0];

             foreach (DataRow row in dt.Rows)
             {
                 string member_id = row[0].ToString().Trim();
                 mem = new AMP_Quotation_Mem(member_id);
                 members.Add(mem);
             }
         }
         dm.connclose();
    }

    public bool Insert_rec( string dept, string type, string status, string name1, string name2, string add1, string add2, string add3, string add4, string home_tp, string mobile_tp, int numPersons,  double dicsRate,  double netPremm, 
                            double admnFee, double polFee, double nbt, double vat, double finalPrem, double re_creedAmnt, double slic_ret, double taxs, string epf ,string nic, int branch,double planLimt, double ncb_amnt,
                            double accumulative_amnt, string plan, DataTable dtMemTable, string ip, double slicDiscount, double slicRetenForRe, double stdLoading, out string quotNum)
    {
        bool result = false;
        quotNum = "";

        try
        {
            QuotationIni qin = new QuotationIni();
            string qid = qin.generate_proposalID(dept, Convert.ToInt32(DateTime.Today.ToString("yy")), "MP", branch); // product change from AMP

            if (!String.IsNullOrEmpty(qid))
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]))
                {
                    connection.Open();

                    OracleCommand command = connection.CreateCommand();
                    OracleTransaction transaction;

                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.Transaction = transaction;
                    try
                    {

                        string sql = "INSERT INTO SLIGEN.QUOT_MAST (QUOT_NUMBER, DEPT, TYPE, STATUS, NAME_1, NAME_2, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, HOME_PHONE_NO, MOBILE_PHONE_NO, NUM_OF_PERSONS,  DISCOUNT_RATE_FAMILY,  NET_PREMIUM, ADMIN_FEE, POLICY_FEE, NBT, VAT,FINAL_PREMIUM, RE_CEEDED_AMOUNT, SLIC_RETENTION, TAXES_EXPENSES, ENTERED_EPF, ENTERED_DATE, NIC_NUMBER, PLAN, BRANCH_CODE, PLAN_LIMIT, NCB,ACCUMULATIVE, SLIC_DISCOUNT, SLIC_RETEN_FOR_RE, STANDRD_LOADING) " +
                                      "Values (:qno, :dpt, :type, :stat, :name1, :name2, :add1, :add2, :add3, :add4, :hmno, :mobno, :noOfP,  :dscRate,  :ntPrem, :admnFee, :polFee, :nbt, :vat, :fnlPrem, :recdr, :slicRet, :tax, :epf, sysdate , :nic, :pln, :brnch, :plnl, :ncb, :accm, :sldisc, :slretre, :stdLding)";

                        command.CommandText = sql;

                        OracleParameter oqno = new OracleParameter();
                        oqno.Value = qid;
                        oqno.ParameterName = "qno";

                        OracleParameter odpt = new OracleParameter();
                        odpt.Value = dept;
                        odpt.ParameterName = "dpt";

                        OracleParameter otype = new OracleParameter();
                        otype.Value = type;
                        otype.ParameterName = "type";

                        OracleParameter ostat = new OracleParameter();
                        ostat.Value = status;
                        ostat.ParameterName = "stat";

                        OracleParameter oname1 = new OracleParameter();
                        oname1.Value = name1;
                        oname1.ParameterName = "name1";

                        OracleParameter oname2 = new OracleParameter();
                        oname2.DbType = DbType.String;
                        if (String.IsNullOrEmpty(name2))
                        {
                            oname2.Value = DBNull.Value;
                        }
                        else
                        {
                            oname2.Value = name2;
                        }
                        oname2.ParameterName = "name2";

                        OracleParameter oadd1 = new OracleParameter();
                        oadd1.DbType = DbType.String;
                        if (String.IsNullOrEmpty(add1))
                        {
                            oadd1.Value = DBNull.Value;
                        }
                        else
                        {
                            oadd1.Value = add1;
                        }
                        oadd1.ParameterName = "add1";

                        OracleParameter oadd2 = new OracleParameter();
                        oadd2.DbType = DbType.String;
                        if (String.IsNullOrEmpty(add2))
                        {
                            oadd2.Value = DBNull.Value;
                        }
                        else
                        {
                            oadd2.Value = add2;
                        }
                        oadd2.ParameterName = "add2";

                        OracleParameter oadd3 = new OracleParameter();
                        oadd3.DbType = DbType.String;
                        if (String.IsNullOrEmpty(add3))
                        {
                            oadd3.Value = DBNull.Value;
                        }
                        else
                        {
                            oadd3.Value = add3;
                        }
                        oadd3.ParameterName = "add3";

                        OracleParameter oadd4 = new OracleParameter();
                        oadd4.DbType = DbType.String;
                        if (String.IsNullOrEmpty(add4))
                        {
                            oadd4.Value = DBNull.Value;
                        }
                        else
                        {
                            oadd4.Value = add4;
                        }
                        oadd4.ParameterName = "add4";

                        OracleParameter ohmno = new OracleParameter();
                        ohmno.DbType = DbType.String;
                        if (String.IsNullOrEmpty(home_tp))
                        {
                            ohmno.Value = DBNull.Value;
                        }
                        else
                        {
                            ohmno.Value = home_tp;
                        }
                        ohmno.ParameterName = "hmno";

                        OracleParameter omobno = new OracleParameter();
                        omobno.DbType = DbType.String;
                        if (String.IsNullOrEmpty(mobile_tp))
                        {
                            omobno.Value = DBNull.Value;
                        }
                        else
                        {
                            omobno.Value = mobile_tp;
                        }
                        omobno.ParameterName = "mobno";

                        OracleParameter onoOfP = new OracleParameter();
                        onoOfP.DbType = DbType.Int32;
                        onoOfP.Value = numPersons;
                        onoOfP.ParameterName = "noOfP";

                        //OracleParameter ototInd = new OracleParameter();
                        //ototInd.DbType = DbType.Double;
                        //ototInd.Value = tot_individual;
                        //ototInd.ParameterName = "totInd";

                        OracleParameter odscRate = new OracleParameter();
                        odscRate.DbType = DbType.Double;
                        odscRate.Value = dicsRate;
                        odscRate.ParameterName = "dscRate";

                        //OracleParameter odscAmnt = new OracleParameter();
                        //odscAmnt.DbType = DbType.Double;
                        //odscAmnt.Value = dicsAmnt;
                        //odscAmnt.ParameterName = "dscAmnt";

                        OracleParameter ontPrem = new OracleParameter();
                        ontPrem.DbType = DbType.Double;
                        ontPrem.Value = netPremm;
                        ontPrem.ParameterName = "ntPrem";

                        OracleParameter oadmnFee = new OracleParameter();
                        oadmnFee.DbType = DbType.Double;
                        oadmnFee.Value = admnFee;
                        oadmnFee.ParameterName = "admnFee";

                        OracleParameter opolFee = new OracleParameter();
                        opolFee.DbType = DbType.Double;
                        opolFee.Value = polFee;
                        opolFee.ParameterName = "polFee";

                        OracleParameter onbt = new OracleParameter();
                        onbt.DbType = DbType.Double;
                        onbt.Value = nbt;
                        onbt.ParameterName = "nbt";

                        OracleParameter ovat = new OracleParameter();
                        ovat.DbType = DbType.Double;
                        ovat.Value = vat;
                        ovat.ParameterName = "vat";

                        OracleParameter ofnlPrem = new OracleParameter();
                        ofnlPrem.DbType = DbType.Double;
                        ofnlPrem.Value = finalPrem;
                        ofnlPrem.ParameterName = "fnlPrem";

                        OracleParameter orecdr = new OracleParameter();
                        orecdr.DbType = DbType.Double;
                        orecdr.Value = re_creedAmnt;
                        orecdr.ParameterName = "recdr";

                        OracleParameter oslicRet = new OracleParameter();
                        oslicRet.DbType = DbType.Double;
                        oslicRet.Value = slic_ret;
                        oslicRet.ParameterName = "slicRet";

                        OracleParameter otax = new OracleParameter();
                        otax.DbType = DbType.Double;
                        otax.Value = taxs;
                        otax.ParameterName = "tax";

                        OracleParameter oepf = new OracleParameter();
                        oepf.Value = epf;
                        oepf.ParameterName = "epf";

                        OracleParameter onic = new OracleParameter();
                        onic.DbType = DbType.String;
                        if (String.IsNullOrEmpty(nic))
                        {
                            onic.Value = DBNull.Value;
                        }
                        else
                        {
                            onic.Value = nic;
                        }
                        onic.ParameterName = "nic";

                        OracleParameter opln = new OracleParameter();
                        opln.Value = plan;
                        opln.ParameterName = "pln";

                        OracleParameter osbrnch = new OracleParameter();
                        osbrnch.DbType = DbType.Int32;
                        osbrnch.Value = branch;
                        osbrnch.ParameterName = "brnch";

                        OracleParameter oplnl = new OracleParameter();
                        oplnl.DbType = DbType.Double;
                        oplnl.Value = planLimt;
                        oplnl.ParameterName = "plnl";

                        OracleParameter oncb = new OracleParameter();
                        oncb.DbType = DbType.Double;
                        oncb.Value = ncb_amnt;
                        oncb.ParameterName = "ncb";

                        OracleParameter oaccm = new OracleParameter();
                        oaccm.DbType = DbType.Double;
                        oaccm.Value = accumulative_amnt;
                        oaccm.ParameterName = "accm";

                        OracleParameter osldisc = new OracleParameter();
                        osldisc.DbType = DbType.Double;
                        osldisc.Value = slicDiscount;
                        osldisc.ParameterName = "sldisc";

                        OracleParameter oslretre = new OracleParameter();
                        oslretre.DbType = DbType.Double;
                        oslretre.Value = slicRetenForRe;
                        oslretre.ParameterName = "slretre";

                        OracleParameter ostdLding = new OracleParameter();
                        ostdLding.DbType = DbType.Double;
                        ostdLding.Value = stdLoading;
                        ostdLding.ParameterName = "stdLding";

                        command.Parameters.Add(oqno);
                        command.Parameters.Add(odpt);
                        command.Parameters.Add(otype);
                        command.Parameters.Add(ostat);
                        command.Parameters.Add(oname1);
                        command.Parameters.Add(oname2);
                        command.Parameters.Add(oadd1);
                        command.Parameters.Add(oadd2);
                        command.Parameters.Add(oadd3);
                        command.Parameters.Add(oadd4);
                        command.Parameters.Add(ohmno);
                        command.Parameters.Add(omobno);
                        command.Parameters.Add(onoOfP);
                        //command.Parameters.Add(ototInd);
                        command.Parameters.Add(odscRate);
                        // command.Parameters.Add(odscAmnt);
                        command.Parameters.Add(ontPrem);
                        command.Parameters.Add(oadmnFee);
                        command.Parameters.Add(opolFee);
                        command.Parameters.Add(onbt);
                        command.Parameters.Add(ovat);
                        command.Parameters.Add(ofnlPrem);
                        command.Parameters.Add(orecdr);
                        command.Parameters.Add(oslicRet);
                        command.Parameters.Add(otax);
                        command.Parameters.Add(oepf);
                        command.Parameters.Add(onic);
                        command.Parameters.Add(opln);
                        command.Parameters.Add(osbrnch);

                        command.Parameters.Add(oplnl);
                        command.Parameters.Add(oncb);
                        command.Parameters.Add(oaccm);
                        command.Parameters.Add(osldisc);
                        command.Parameters.Add(oslretre);
                        command.Parameters.Add(ostdLding);

                        command.ExecuteNonQuery();

                        command.Parameters.Clear();

                        int i = 0;
                        foreach (DataRow row in dtMemTable.Rows)
                        {
                            i++;
                            if (i <= numPersons)
                            {
                                string category = row["Category"].ToString().Substring(0,1);
                                string gender = row["Gender"].ToString().Substring(0, 1);
                                string birthDate = row["Dob"].ToString();
                                double age = double.Parse(row["Age"].ToString());
                                double baseRate = 0; // double.Parse(row["BaseRate"].ToString());
                                double maternSlic = 0; // double.Parse(row["MaternSlic"].ToString());
                                double flDiscountedAmt = 0; // double.Parse(row["FlDiscountedAmt"].ToString());
                                double finalPremium = 0;// double.Parse(row["FinalPremium"].ToString());
                                double bmiRate = 0; // double.Parse(row["BmiRate"].ToString());
                                double height = 0; // double.Parse(row["Height"].ToString());
                                double weight = 0; // double.Parse(row["Weight"].ToString());
                                double bmiLoading = 0; // double.Parse(row["BmiLoading"].ToString());
                                double bmiValue = 0; //double.Parse(row["BmiVal"].ToString());
                                double mobRate = 0; // double.Parse(row["MobRate"].ToString());
                                double mobLoading = 0; // double.Parse(row["MobLoading"].ToString());

                                sql = "INSERT INTO SLIGEN.AMP_QT_MEM_DETAILS (QT_ID, MEM_ID, MEM_TYPE, GENDER, DOB, AGE, BASE_AMOUNT, MATERN_SLIC, FL_DISCONTD, FINAL_PRM, ENTERED_EPF, ENTERED_DATE, WEIGHT_KG, HEIGHT_CM, BMI_RATE, BMI_LOADING, MOBDT_RATE, MOBDT_LOADING, BMI_VALUE) " +
                                  "Values (:qid, :memid, :mtype, :gen, :dob, :age, :base, :mat, :dsc, :fnlprm, :epf, sysdate, :weight, :height, :bmirate, :bmild, :mobrate, :mobval, :bmival)";

                                command.CommandText = sql;

                                OracleParameter oqid = new OracleParameter();
                                oqid.Value = qid;
                                oqid.ParameterName = "qid";

                                OracleParameter omemid = new OracleParameter();
                                omemid.Value = qid + "_" + i.ToString();
                                omemid.ParameterName = "memid";

                                OracleParameter omtype = new OracleParameter();
                                omtype.Value = category;
                                omtype.ParameterName = "mtype";

                                OracleParameter ogen = new OracleParameter();
                                ogen.Value = gender;
                                ogen.ParameterName = "gen";

                                OracleParameter odob = new OracleParameter();
                                odob.DbType = DbType.Date;
                                odob.Value = birthDate;
                                odob.ParameterName = "dob";

                                OracleParameter oage = new OracleParameter();
                                oage.DbType = DbType.Double;
                                oage.Value = age;
                                oage.ParameterName = "age";

                                OracleParameter obase = new OracleParameter();
                                obase.DbType = DbType.Double;
                                obase.Value = baseRate;
                                obase.ParameterName = "base";

                                OracleParameter omat = new OracleParameter();
                                omat.DbType = DbType.Double;
                                omat.Value = maternSlic;
                                omat.ParameterName = "mat";

                                OracleParameter odsc = new OracleParameter();
                                odsc.DbType = DbType.Double;
                                odsc.Value = flDiscountedAmt;
                                odsc.ParameterName = "dsc";

                                OracleParameter ofnlprm = new OracleParameter();
                                ofnlprm.DbType = DbType.Double;
                                ofnlprm.Value = finalPremium;
                                ofnlprm.ParameterName = "fnlprm";

                                OracleParameter o1epf = new OracleParameter();
                                o1epf.Value = epf;
                                o1epf.ParameterName = "epf";

                                OracleParameter oweight = new OracleParameter();
                                oweight.DbType = DbType.Double;
                                oweight.Value = weight;
                                oweight.ParameterName = "weight";

                                OracleParameter oheight = new OracleParameter();
                                oheight.DbType = DbType.Double;
                                oheight.Value = height;
                                oheight.ParameterName = "height";

                                OracleParameter obmirate = new OracleParameter();
                                obmirate.DbType = DbType.Double;
                                obmirate.Value = bmiRate;
                                obmirate.ParameterName = "bmirate";

                                OracleParameter obmild = new OracleParameter();
                                obmild.DbType = DbType.Double;
                                obmild.Value = bmiLoading;
                                obmild.ParameterName = "bmild";

                                OracleParameter omobrate = new OracleParameter();
                                omobrate.DbType = DbType.Double;
                                omobrate.Value = mobRate;
                                omobrate.ParameterName = "mobrate";

                                OracleParameter omobval = new OracleParameter();
                                omobval.DbType = DbType.Double;
                                omobval.Value = mobLoading;
                                omobval.ParameterName = "mobval";

                                OracleParameter obmival = new OracleParameter();
                                obmival.DbType = DbType.Double;
                                obmival.Value = bmiValue;
                                obmival.ParameterName = "bmival";

                                command.Parameters.Add(oqid);
                                command.Parameters.Add(omemid);
                                command.Parameters.Add(omtype);
                                command.Parameters.Add(ogen);
                                command.Parameters.Add(odob);
                                command.Parameters.Add(oage);
                                command.Parameters.Add(obase);
                                command.Parameters.Add(omat);
                                command.Parameters.Add(odsc);
                                command.Parameters.Add(ofnlprm);
                                command.Parameters.Add(o1epf);
                                command.Parameters.Add(oweight);
                                command.Parameters.Add(oheight);
                                command.Parameters.Add(obmirate);
                                command.Parameters.Add(obmild);
                                command.Parameters.Add(omobrate);
                                command.Parameters.Add(omobval);
                                command.Parameters.Add(obmival);

                                command.ExecuteNonQuery();

                                command.Parameters.Clear();
                            }
                        }

                        transaction.Commit();
                        result = true;
                        quotNum = qid;

                        //AMP_Quotation_print pdfPrint = new AMP_Quotation_print();
                        //pdfPrint.print_quotation(qid, epf, ip, false);
                    }
                    catch (Exception u2)
                    {
                        transaction.Rollback();
                        log lg = new log();
                        lg.write_log("Failed at AMP_Quotation_mast - Insert_rec : " + u2.ToString());
                    }
                }
            }
        }
        catch (Exception u)
        {
            string g = u.ToString();
            log lg = new log();
            lg.write_log("Failed at AMP_Quotation_mast - Insert_rec : " + u.ToString());
        }
        finally
        {

        }
        
        return result;

    }

    public bool Update_rec( string quotNo, double dicsRate, double netPremm, double admnFee, double polFee, double nbt, double vat, double finalPrem, double re_creedAmnt, double slic_ret, double taxs, double planLimt, double ncb_amnt,
                            double accumulative_amnt, string plan, DataTable dtMemTable, double slicDiscount, double slicRetenForRe, double stdLoading)
    {
        bool result = false;
        
        try
        {
           
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]))
                {
                    connection.Open();

                    OracleCommand command = connection.CreateCommand();
                    OracleTransaction transaction;

                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.Transaction = transaction;
                    try
                    {

                        string sql = "UPDATE SLIGEN.QUOT_MAST" +
                                     " SET DISCOUNT_RATE_FAMILY = :dscRate," +
                                     " NET_PREMIUM = :ntPrem, " +
                                     " ADMIN_FEE = :admnFee," +
                                     " POLICY_FEE = :polFee," +
                                     " NBT = :nbt," +
                                     " VAT = :vat," +
                                     " FINAL_PREMIUM = :fnlPrem," +
                                     " RE_CEEDED_AMOUNT = :recdr," +
                                     " SLIC_RETENTION = :slicRet," +
                                     " TAXES_EXPENSES = :tax," +
                                     " ENTERED_DATE = sysdate," +
                                     " PLAN = :pln," +
                                     " PLAN_LIMIT = :plnl," +
                                     " NCB = :ncb," +
                                     " ACCUMULATIVE = :accm," +
                                     " SLIC_DISCOUNT = :sldisc," +
                                     " SLIC_RETEN_FOR_RE = :slretre," +
                                     " STANDRD_LOADING = :stdLding " +
                                     " where QUOT_NUMBER = :quotNo";

                        command.CommandText = sql;

                        OracleParameter oqno = new OracleParameter();
                        oqno.Value = quotNo;
                        oqno.ParameterName = "quotNo";     
                                 
                        OracleParameter odscRate = new OracleParameter();
                        odscRate.DbType = DbType.Double;
                        odscRate.Value = dicsRate;
                        odscRate.ParameterName = "dscRate";                        

                        OracleParameter ontPrem = new OracleParameter();
                        ontPrem.DbType = DbType.Double;
                        ontPrem.Value = netPremm;
                        ontPrem.ParameterName = "ntPrem";

                        OracleParameter oadmnFee = new OracleParameter();
                        oadmnFee.DbType = DbType.Double;
                        oadmnFee.Value = admnFee;
                        oadmnFee.ParameterName = "admnFee";

                        OracleParameter opolFee = new OracleParameter();
                        opolFee.DbType = DbType.Double;
                        opolFee.Value = polFee;
                        opolFee.ParameterName = "polFee";

                        OracleParameter onbt = new OracleParameter();
                        onbt.DbType = DbType.Double;
                        onbt.Value = nbt;
                        onbt.ParameterName = "nbt";

                        OracleParameter ovat = new OracleParameter();
                        ovat.DbType = DbType.Double;
                        ovat.Value = vat;
                        ovat.ParameterName = "vat";

                        OracleParameter ofnlPrem = new OracleParameter();
                        ofnlPrem.DbType = DbType.Double;
                        ofnlPrem.Value = finalPrem;
                        ofnlPrem.ParameterName = "fnlPrem";

                        OracleParameter orecdr = new OracleParameter();
                        orecdr.DbType = DbType.Double;
                        orecdr.Value = re_creedAmnt;
                        orecdr.ParameterName = "recdr";

                        OracleParameter oslicRet = new OracleParameter();
                        oslicRet.DbType = DbType.Double;
                        oslicRet.Value = slic_ret;
                        oslicRet.ParameterName = "slicRet";

                        OracleParameter otax = new OracleParameter();
                        otax.DbType = DbType.Double;
                        otax.Value = taxs;
                        otax.ParameterName = "tax";                        

                        OracleParameter opln = new OracleParameter();
                        opln.Value = plan;
                        opln.ParameterName = "pln";                        

                        OracleParameter oplnl = new OracleParameter();
                        oplnl.DbType = DbType.Double;
                        oplnl.Value = planLimt;
                        oplnl.ParameterName = "plnl";

                        OracleParameter oncb = new OracleParameter();
                        oncb.DbType = DbType.Double;
                        oncb.Value = ncb_amnt;
                        oncb.ParameterName = "ncb";

                        OracleParameter oaccm = new OracleParameter();
                        oaccm.DbType = DbType.Double;
                        oaccm.Value = accumulative_amnt;
                        oaccm.ParameterName = "accm";

                        OracleParameter osldisc = new OracleParameter();
                        osldisc.DbType = DbType.Double;
                        osldisc.Value = slicDiscount;
                        osldisc.ParameterName = "sldisc";

                        OracleParameter oslretre = new OracleParameter();
                        oslretre.DbType = DbType.Double;
                        oslretre.Value = slicRetenForRe;
                        oslretre.ParameterName = "slretre";

                        OracleParameter ostdLding = new OracleParameter();
                        ostdLding.DbType = DbType.Double;
                        ostdLding.Value = stdLoading;
                        ostdLding.ParameterName = "stdLding";

                        command.Parameters.Add(oqno);                        
                        command.Parameters.Add(odscRate);                        
                        command.Parameters.Add(ontPrem);
                        command.Parameters.Add(oadmnFee);
                        command.Parameters.Add(opolFee);
                        command.Parameters.Add(onbt);
                        command.Parameters.Add(ovat);
                        command.Parameters.Add(ofnlPrem);
                        command.Parameters.Add(orecdr);
                        command.Parameters.Add(oslicRet);
                        command.Parameters.Add(otax);                        
                        command.Parameters.Add(opln);                        
                        command.Parameters.Add(oplnl);
                        command.Parameters.Add(oncb);
                        command.Parameters.Add(oaccm);
                        command.Parameters.Add(osldisc);
                        command.Parameters.Add(oslretre);
                        command.Parameters.Add(ostdLding);

                        command.ExecuteNonQuery();

                        command.Parameters.Clear();

                        int i = 0;
                        foreach (DataRow row in dtMemTable.Rows)
                        {
                            i++;
                            string memId = row["MemId"].ToString();
                            string category = row["Category"].ToString().Substring(0,1);
                            string gender = row["Gender"].ToString().Substring(0, 1);
                            string birthDate = row["Dob"].ToString();
                            double age = double.Parse(row["Age"].ToString());
                            double baseRate = 0; // double.Parse(row["BaseRate"].ToString());
                            double maternSlic = 0; // double.Parse(row["MaternSlic"].ToString());
                            double flDiscountedAmt = 0; // double.Parse(row["FlDiscountedAmt"].ToString());
                            double finalPremium = 0; // double.Parse(row["FinalPremium"].ToString());
                            double bmiRate = 0; // double.Parse(row["BmiRate"].ToString());
                            double height = 0; // double.Parse(row["Height"].ToString());
                            double weight = 0; // double.Parse(row["Weight"].ToString());
                            double bmiLoading = 0; // double.Parse(row["BmiLoading"].ToString());
                            double bmiValue = 0; // double.Parse(row["BmiVal"].ToString());
                            double mobRate = 0; // double.Parse(row["MobRate"].ToString());
                            double mobLoading = 0; // double.Parse(row["MobLoading"].ToString());

                            sql = "UPDATE SLIGEN.AMP_QT_MEM_DETAILS" +
                                  " SET MEM_TYPE = :mtype," +
                                  " GENDER = :gen," +
                                  " DOB = :dob," +
                                  " AGE = :age," +
                                  " BASE_AMOUNT = :base," +
                                  " MATERN_SLIC = :mat," +
                                  " FL_DISCONTD = :dsc," +
                                  " FINAL_PRM = :fnlprm," +
                                  " ENTERED_DATE = sysdate," +
                                  " WEIGHT_KG = :weight," +
                                  " HEIGHT_CM = :height," +
                                  " BMI_RATE = :bmirate," +
                                  " BMI_LOADING = :bmild," +
                                  " MOBDT_RATE = :mobrate," +
                                  " MOBDT_LOADING = :mobval," +
                                  " BMI_VALUE = :bmival" +
                                  " WHERE QT_ID = :qid" +
                                  " AND MEM_ID = :memid";

                            command.CommandText = sql;

                            OracleParameter oqid = new OracleParameter();
                            oqid.Value = quotNo;
                            oqid.ParameterName = "qid";

                            OracleParameter omemid = new OracleParameter();
                            omemid.Value = memId;
                            omemid.ParameterName = "memid";

                            OracleParameter omtype = new OracleParameter();
                            omtype.Value = category;
                            omtype.ParameterName = "mtype";

                            OracleParameter ogen = new OracleParameter();
                            ogen.Value = gender;
                            ogen.ParameterName = "gen";

                            OracleParameter odob = new OracleParameter();
                            odob.DbType = DbType.Date;
                            odob.Value = birthDate;
                            odob.ParameterName = "dob";

                            OracleParameter oage = new OracleParameter();
                            oage.DbType = DbType.Double;
                            oage.Value = age;
                            oage.ParameterName = "age";

                            OracleParameter obase = new OracleParameter();
                            obase.DbType = DbType.Double;
                            obase.Value = baseRate;
                            obase.ParameterName = "base";

                            OracleParameter omat = new OracleParameter();
                            omat.DbType = DbType.Double;
                            omat.Value = maternSlic;
                            omat.ParameterName = "mat";

                            OracleParameter odsc = new OracleParameter();
                            odsc.DbType = DbType.Double;
                            odsc.Value = flDiscountedAmt;
                            odsc.ParameterName = "dsc";

                            OracleParameter ofnlprm = new OracleParameter();
                            ofnlprm.DbType = DbType.Double;
                            ofnlprm.Value = finalPremium;
                            ofnlprm.ParameterName = "fnlprm";                            

                            OracleParameter oweight = new OracleParameter();
                            oweight.DbType = DbType.Double;
                            oweight.Value = weight;
                            oweight.ParameterName = "weight";

                            OracleParameter oheight = new OracleParameter();
                            oheight.DbType = DbType.Double;
                            oheight.Value = height;
                            oheight.ParameterName = "height";

                            OracleParameter obmirate = new OracleParameter();
                            obmirate.DbType = DbType.Double;
                            obmirate.Value = bmiRate;
                            obmirate.ParameterName = "bmirate";

                            OracleParameter obmild = new OracleParameter();
                            obmild.DbType = DbType.Double;
                            obmild.Value = bmiLoading;
                            obmild.ParameterName = "bmild";

                            OracleParameter omobrate = new OracleParameter();
                            omobrate.DbType = DbType.Double;
                            omobrate.Value = mobRate;
                            omobrate.ParameterName = "mobrate";

                            OracleParameter omobval = new OracleParameter();
                            omobval.DbType = DbType.Double;
                            omobval.Value = mobLoading;
                            omobval.ParameterName = "mobval";

                            OracleParameter obmival = new OracleParameter();
                            obmival.DbType = DbType.Double;
                            obmival.Value = bmiValue;
                            obmival.ParameterName = "bmival";

                            command.Parameters.Add(oqid);
                            command.Parameters.Add(omemid);
                            command.Parameters.Add(omtype);
                            command.Parameters.Add(ogen);
                            command.Parameters.Add(odob);
                            command.Parameters.Add(oage);
                            command.Parameters.Add(obase);
                            command.Parameters.Add(omat);
                            command.Parameters.Add(odsc);
                            command.Parameters.Add(ofnlprm);
                            command.Parameters.Add(oweight);
                            command.Parameters.Add(oheight);
                            command.Parameters.Add(obmirate);
                            command.Parameters.Add(obmild);
                            command.Parameters.Add(omobrate);
                            command.Parameters.Add(omobval);
                            command.Parameters.Add(obmival);

                            command.ExecuteNonQuery();

                            command.Parameters.Clear();

                        }

                        if (i > 0)
                        {
                            transaction.Commit();
                            result = true;
                        }
                        
                    }
                    catch (Exception u)
                    {
                        transaction.Rollback();
                        log lg = new log();
                        lg.write_log("Failed at AMP_Quotation_mast - Update_rec : " + u.ToString());
                    }
                }
            
        }
        catch (Exception u)
        {
            string g = u.ToString();
            log lg = new log();
            lg.write_log("Failed at AMP update : " + u.ToString());
        }
        finally
        {

        }

        return result;

    }
}