using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_Receipt
/// </summary>
public class TRV_Receipt
{
    OracleConnection objOraCon = new OracleConnection();
    OracleCommand objOraCom = new OracleCommand();

    public int PMBRN { get; set; }
    public int PMSEQ { get; set; }
    public string PMTYPSES { get; set; }

    public string PMTYP { get; set; }
    public string PMPDT { get; set; }
    public DataTable dtRecData ;
    public string ip { get; set; }
    public string user { get; set; }
    public int printtag { get; set; }
    public string polno { get; set; }

    public int schPrinted { get; set; }
    public double finalPrmRs { get; set; }

    public int SCH_BRANCH { get; set; }
    public TRV_Receipt()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public TRV_Receipt(string Polno)
    {
        dtRecData = new DataTable();
        try
        {
            this.connectDB();
            string recSql = "select pmbrn , pmseq, to_char(pmpdt,'yyyy/mm/dd') as pmpdt," +
                        " decode(pmtyp, '111', 'Receipt Deposit Online'," +
                        "               '112', 'Receipt Deposit Manual'," +
                        "               '211', 'Debit Deposit'," +
                        "               '311', 'Endorsement Deposit Payment'," +
                        "               '321', 'Endorsement Debit Payment') as pmtyp,pmtyp as pmtypval,pmtot" +
                        " from GENPAY.PAYFLE where pmpol = '" + Polno + "' and pmdep = 'G' and PMDEL = 0";

            OracleDataAdapter daOra = new OracleDataAdapter(recSql, objOraCon);
            daOra.Fill(dtRecData);
        }

        catch (Exception exc)
        {
            this.disconnectDB();
        }
        finally
        {
            this.disconnectDB();
        }
    }

    public TRV_Receipt(string Polno,string pmdst,string pmdex)
    {
        dtRecData = new DataTable();
        try
        {
            this.connectDB();
            string recSql = "select pmbrn , pmseq, to_char(pmpdt,'yyyy/mm/dd') as pmpdt," +
                        " decode(pmtyp, '111', 'Receipt Deposit Online'," +
                        "               '112', 'Receipt Deposit Manual'," +
                        "               '211', 'Debit Deposit'," +
                        "               '311', 'Endorsement Deposit Payment'," +
                        "               '321', 'Endorsement Debit Payment') as pmtyp,pmtyp as pmtypval,pmtot" +
                        " from GENPAY.PAYFLE where pmpol = '" + Polno + "' and pmdep = 'G' and PMDEL = 0 and to_char(PMDST,'yyyy/MM/dd')='"+ pmdst + "' AND to_char(PMDEX,'yyyy/MM/dd')='"+ pmdex + "'";

            OracleDataAdapter daOra = new OracleDataAdapter(recSql, objOraCon);
            daOra.Fill(dtRecData);
        }

        catch (Exception exc)
        {
            this.disconnectDB();
        }
        finally
        {
            this.disconnectDB();
        }
    }


    public TRV_Receipt(string Polno, string pmdst, string pmdex, double pmTot)
    {
        double lowVal = pmTot - 2;
        double MaxVal = pmTot + 2;

        dtRecData = new DataTable();
        try
        {
            this.connectDB();
            string recSql = "select pmbrn , pmseq, to_char(pmpdt,'yyyy/mm/dd') as pmpdt," +
                        " decode(pmtyp, '111', 'Receipt Deposit Online'," +
                        "               '112', 'Receipt Deposit Manual'," +
                        "               '211', 'Debit Deposit'," +
                        "               '311', 'Endorsement Deposit Payment'," +
                        "               '321', 'Endorsement Debit Payment') as pmtyp,pmtyp as pmtypval,pmtot" +
                        " from GENPAY.PAYFLE where pmpol = '" + Polno + "' and pmdep = 'G' and PMDEL = 0 and " +
                        "to_char(PMDST,'yyyy/MM/dd')='" + pmdst + "' AND to_char(PMDEX,'yyyy/MM/dd')='" + pmdex + "' AND pmtot BETWEEN " + lowVal + " AND "+ MaxVal + "";

            OracleDataAdapter daOra = new OracleDataAdapter(recSql, objOraCon);
            daOra.Fill(dtRecData);
        }

        catch (Exception exc)
        {
            this.disconnectDB();
        }
        finally
        {
            this.disconnectDB();
        }
    }

    public void checkEndoPrmAmount(string polNo, string nEW_DEP, string nEW_ARR, string type)
    {
        try
        {
            connectDB();

            string sql = "select FINAL_PREMIUM_RS from sligen.TRV_POL_PRM_ENDO where polno='" + polno + "' AND to_char(NEW_DEP,'yyyy/MM/dd')='"+ nEW_DEP + "' AND to_char(NEW_ARR,'yyyy/MM/dd')='"+ nEW_ARR + "' AND UPD_TYPE='"+type+ "' AND seq_no =(select max(seq_no) as seq_no from sligen.TRV_POL_PRM_ENDO where polno='" + polno + "' AND UPD_TYPE='" + type + "')";//and sch_print=0

            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    finalPrmRs = double.Parse(reader[0].ToString().Trim());

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

    public void connectDB()
    {
        try
        {
            objOraCon.ConnectionString = ConfigurationManager.AppSettings.Get("OracleDB").ToString();
            if (objOraCon.State != ConnectionState.Open)
                objOraCon.Open();
        }
        catch (Exception e)
        {
            string t = e.ToString();
        }
    }
    public void disconnectDB()
    {
        if (objOraCon.State != ConnectionState.Closed)
        {
            objOraCon.Close();
        }
    }

    public bool Insert_DataSet(TRV_Receipt receipt)
    {
        bool result = false;

        try
        {
            //Proposal prop = new Proposal();
            //refID = prop.generate_proposalID("G", Convert.ToInt32(DateTime.Today.ToString("yyyy")), "GTI");

            //if (!String.IsNullOrEmpty(refID))
            //{
            using (OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]))
            {
                connection.Open();

                OracleCommand command = connection.CreateCommand();
                OracleTransaction transaction;

                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = transaction;
                try
                {
                    string insSql = "UPDATE SLIGEN.TRV_POL_MAST SET SCH_PRINT="+receipt.printtag+" , SCH_PRINTBY='"+receipt.user+"'" +
                                    ",SCH_PDATE=sysdate,SCH_PIP='" + receipt.ip + "', SCH_BRANCH=" + receipt.SCH_BRANCH + ", SER_BRCD=402 where POLNO='" + receipt.polno + "' and  SCH_PRINT=0 ";

                    command.CommandText = insSql;

                    command.ExecuteNonQuery();
                    /*
                    string insPolDetweb = "Insert into SLIC_NET.POL_DET_FOR_WEB(USERNAME, POLICY_NUMBER, POLICY_TYPE, CREATED_DATE) "+
                                          "Values(:usernm, :polNo, :polType, sysdate) ";

                    command.CommandText = insPolDetweb;
                     

                    OracleParameter oUsernm = new OracleParameter();
                    oUsernm.DbType = DbType.String;
                    oUsernm.Value = receipt.user;
                    oUsernm.ParameterName = "usernm";

                    OracleParameter oPolno = new OracleParameter();
                    oPolno.DbType = DbType.String;
                    oPolno.Value = receipt.polno;
                    oPolno.ParameterName = "polNo";

                    OracleParameter oPolTyp = new OracleParameter();
                    oPolTyp.DbType = DbType.String;
                    oPolTyp.Value = "G";
                    oPolTyp.ParameterName = "polType";                    

                    command.Parameters.Add(oUsernm);
                    command.Parameters.Add(oPolno);
                    command.Parameters.Add(oPolTyp);
                   

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    string UpdatePropDet = "Update SLIC_NET.PROPOSAL_DETAILS set  POL_ISSUED = 'Y'  where USERNAME = :usernm2 and POLICY_NUMBER = :polNo2 " +
                                            " and POL_TYPE = 'G'  ";

                    command.CommandText = UpdatePropDet;


                    OracleParameter oUsernm2 = new OracleParameter();
                    oUsernm2.DbType = DbType.String;
                    oUsernm2.Value = receipt.user;
                    oUsernm2.ParameterName = "usernm2";

                    OracleParameter oPolno2 = new OracleParameter();
                    oPolno2.DbType = DbType.String;
                    oPolno2.Value = receipt.polno;
                    oPolno2.ParameterName = "polNo2";                     

                    command.Parameters.Add(oUsernm2);
                    command.Parameters.Add(oPolno2);                    

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    */

                    transaction.Commit();
                    result = true;
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                }
            }
        }
        catch (Exception exc)
        {
            
        }
        return result;
    }

    public void checkSchPrint(string polno)
    {
        try
        {
            connectDB();

            string sql = "select sch_print from sligen.trv_pol_mast where polno='" + polno+"' and sch_print=1";

            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    schPrinted = int.Parse(reader[0].ToString().Trim());
                    
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

    public void checkPrmAmount(string polno)
    {
        try
        {
            connectDB();

            string sql = "select FINAL_PREMIUM_RS from sligen.trv_pol_mast where polno='" + polno + "' ";//and sch_print=0

            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    finalPrmRs = double.Parse(reader[0].ToString().Trim());

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

    public void checkEndoPrmSchmChange(string polno, string pLAN, string nEW_PLAN, string edit_Type)
    {
        try
        {
            connectDB();

            string sql = "select FINAL_PREMIUM_RS from sligen.TRV_POL_PRM_ENDO where polno='" + polno + "' AND PLAN='" + pLAN + "' AND NEW_PLAN ='" + nEW_PLAN + "' AND UPD_TYPE='" + edit_Type + "' AND seq_no =(select max(seq_no) as seq_no from sligen.TRV_POL_PRM_ENDO where polno='" + polno + "' AND UPD_TYPE='" + edit_Type + "')";//and sch_print=0

            using (OracleCommand cmd = new OracleCommand(sql, objOraCon))
            {
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    finalPrmRs = double.Parse(reader[0].ToString().Trim());

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
}