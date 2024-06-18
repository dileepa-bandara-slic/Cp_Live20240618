using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HPL_SQL
/// </summary>
public class HPL_SQL
{
    public string GetPolicyInformation(string ref_no)
    {
        string executor = string.Empty;
        executor += "SELECT TITLE||FULL_NAME AS CUSNAME, TO_CHAR(COM_DATE,'dd/MM/yyyy') AS COM_DATE, NVL(POLICY_NUMBER, '0') AS POLICY_NUMBER, SUM_ASSURD, NVL(ANU_PREMIUM,'0') AS ANU_PREMIUM FROM SLIC_NET.PROPOSAL_DETAILS WHERE REF_NO = '" + ref_no + "'";
        return executor;
    }

    public string GetPaymentConfirmationInfo(string ref_no)
    {
        string executor = string.Empty;
        //executor += "SELECT HPLPAY.POLICYNO, HPLPAY.REFNO,  (HPLPAY.TITLE||HPLPAY.FULLNAME) AS CNAME, HPLPAY.SUM_ASSURD, HPLPAY.ANU_PREMIUM, ('From : '||TO_CHAR(HPLPAY.POL_START_DATE, 'DD/MM/YYYY')||' To : '|| TO_CHAR(HPLPAY.POL_END_DATE, 'DD/MM/YYYY')) AS COV_PERIOD, "; 
        //executor += "TO_CHAR(HPLPAY.ADDED_DATE, 'DD/MM/YYYY') AS PAID_DATE, HPLPAY.EMAIL, HPLPAY.MOBILE_NUMBER, HPLPAY.ADDRESS1, HPLPAY.ADDRESS2, HPLPAY.ADDRESS3, HPLPAY.ADDRESS4, HPLPAY.RISK_ADRS1, HPLPAY.RISK_ADRS2, HPLPAY.RISK_ADRS3, HPLPAY.RISK_ADRS4, ";
        //executor += "HPLPAY.PLAN_TYPE, HPL_PI.LL_BPFF, HPL_PI.LL_CEPE, HPL_PI.LL_SRD, HPL_PI.LL_SPRB, HPL_PI.LL_SB, HPL_PI.LLED, HPL_PI.TOT_SUM_INS  FROM  SLIGEN.HPL_PAYMENTS HPLPAY ";
        //executor += "INNER JOIN  SLIC_NET.HPL_PKG_INFO HPL_PI ";
        //executor += "ON HPL_PI.PLAN = HPLPAY.PLAN_TYPE WHERE HPLPAY.REFNO = '" + ref_no + "'";

        executor += "SELECT HPLPAY.POLICYNO, HPLPAY.REFNO,  (HPLPAY.TITLE||HPLPAY.FULLNAME) AS CNAME, HPLPAY.SUM_ASSURD, HPLPAY.ANU_PREMIUM, ('From : '||TO_CHAR(HPLPAY.POL_START_DATE, 'DD/MM/YYYY')||' To : '|| TO_CHAR(HPLPAY.POL_END_DATE, 'DD/MM/YYYY')) AS COV_PERIOD, ";
        executor += "TO_CHAR(HPLPAY.ADDED_DATE, 'DD/MM/YYYY') AS PAID_DATE, HPLPAY.EMAIL, HPLPAY.MOBILE_NUMBER, HPLPAY.ADDRESS1, HPLPAY.ADDRESS2, HPLPAY.ADDRESS3, HPLPAY.ADDRESS4, HPLPAY.RISK_ADRS1, HPLPAY.RISK_ADRS2, HPLPAY.RISK_ADRS3, HPLPAY.RISK_ADRS4, ";
        executor += "HPLPAY.PLAN_TYPE, HPL_PI.LL_BPFF, HPL_PI.LL_CEPE, HPL_PI.LL_SRD, HPL_PI.LL_SPRB, HPL_PI.LL_SB, HPL_PI.LLED, HPL_PI.TOT_SUM_INS, HPLPD.AGTCODE  FROM  SLIGEN.HPL_PAYMENTS HPLPAY ";
        executor += "INNER JOIN  SLIC_NET.HPL_PKG_INFO HPL_PI ";
        executor += "ON HPL_PI.PLAN = HPLPAY.PLAN_TYPE ";
        executor += "INNER JOIN  SLIC_NET.PROPOSAL_DETAILS HPLPD ";
        executor += "ON HPLPAY.REFNO = HPLPD.REF_NO ";
        executor += "WHERE HPLPAY.REFNO = '" + ref_no + "'";
        return executor;
    }

    public string GetAgencyConfirmation(int agency)
    {
        string executor = string.Empty;
        executor += "SELECT NVL(COUNT(*),0) FROM AGENT.AGENT WHERE STCD='0' AND AGENCY =" + agency;
        return executor;
    }
}