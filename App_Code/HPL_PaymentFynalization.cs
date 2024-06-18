using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HPL_PaymentFynalization
/// </summary>
public class HPL_PaymentFynalization
{
    public string HPLPolicy { get; set; }
    public string HPLRefNo { get; set; }
    public string HPLCus_Name { get; set; }
    public string HPLSumAssured { get; set; }
    public string HPLPremium{ get; set; }
    public string HPLCoverPeriod { get; set; }
    public string HPLDateOfPayment { get; set; }
    public string HPLEmail { get; set; }
    public string HPLContactNo { get; set; }


    /// <summary>
    /// Receipt Module
    /// </summary>
    public string HPL_ADDRESS1 { get; set; }
    public string HPL_ADDRESS2 { get; set; }
    public string HPL_ADDRESS3 { get; set; }
    public string HPL_ADDRESS4 { get; set; }

    public string HPL_RL_ADDRESS1 { get; set; }
    public string HPL_RL_ADDRESS2 { get; set; }
    public string HPL_RL_ADDRESS3 { get; set; }
    public string HPL_RL_ADDRESS4 { get; set; }

    public string HPL_PLAN { get; set; }
    
    public string HPL_LLBPFF { get; set; }
    public string HPL_LLCEPE { get; set; }
    public string HPL_LLSRD { get; set; }
    public string HPL_LLPRB { get; set; }
    public string HPL_LLSB { get; set; }
    public string HPL_LLSD { get; set; }
    public string HPL_TOT_SUM_INS { get; set; }

    public int HPL_AGTCODE { get; set; }
}