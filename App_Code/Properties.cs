using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

// This class holds the basic properties of the payment gateway connection
public class Properties
{
    public string Version { get; private set; }
    public string MerchantID { get; private set; }
    public string AcquiredId { get; private set; }
    public string MerRespUrl { get; private set; }
    public string PurchaseCurrency { get; private set; }
    public string PurchaseCurrencyExponent { get; private set; }
    public string SignatureMethod { get; private set; }
    public string CaptureFlag { get; private set; }
    public string Password { get; private set; }
    public string HostUrl { get; private set; }

    //Cybersource Migration
    public string AccessKey { get; private set; }
    public string ProfileId { get; private set; }
    public string Locale { get; private set; }
    public string TransactionType { get; private set; }
    public string Currency { get; private set; }
    public string BillToAddressCountry { get; private set; }
    public string SecretKey { get; private set; }

    //Amex IPG
    public string IPGKeyPath_1 { get; private set; }

    public string IPGClientService_IP { get; private set; }

    public string IPGClientService_Port { get; private set; }
    public Properties()
    {

        Version = "1.0.0";
        MerchantID = "03123901";
        AcquiredId = "415738";
        //MerRespUrl = "https://www.srilankainsurance.net/General/Authorized/Products/PayReceipt.aspx";
        PurchaseCurrency = "144";
        PurchaseCurrencyExponent = "2";
        SignatureMethod = "SHA1";
        CaptureFlag = "A";
        Password = "jJ36tG1O";
        //HostUrl = "https://www.hnbpg.hnb.lk/SENTRY/PaymentGateway/Application/ReDirectLink.aspx"; //Their Url

        HostUrl = "https://secureacceptance.cybersource.com/pay"; //Their Url
        MerRespUrl = "https://www.srilankainsurance.net/General/Authorized/Products/PayReceipt.aspx";
        AccessKey = "3449f310a2b43196849e004051855be3";
			      //"cbe91c59aca5341d9ed95d6dc7b53fa5";(2023)		
			      //"8db48ffd7647324cb99538336dfa8dc5";
        ProfileId = "B6581B8A-E9B0-4420-8D72-F44DE30BBDF8";
        Locale = "en";
        //TransactionType = "authorization";
        TransactionType = "sale";
        Currency = "LKR";
        BillToAddressCountry = "LK";
        SecretKey ="3895a7d29d5a45ada901905a74fb8ccf99a9231f6c3a4c5d8905f4e26e6093a0d7df4da6968641c39789f838b8b7762bc74768a1aa564ee08ab84499a4d63fa334d4df5bc2fd40608c08721cf710c04b2d0942409c194738a9e696484928e36e504da29e96ee41618350f27b0c49bca3402ee9d5f96442d08d6d477d1aac6cd3";
    		     //"3d57edf7be08496a9c98d0fbc3c4ac9c54e6552b737e4ee7a0ee54517ce58c17a54ead5af32a4b1eabdccfc2aa66c98eadc3d82aa31940febb18fe3e73ddb4fb19177a373dc9402883eec75d49c987fc5ff75f6e7aa944678daec1253e70faf4e9d18548ddf9434abf8d452efc100740d8510e0cc9a84765883e9a5a5acff5ea";(2023)
                 //"9882a7a6132843eba9de83591cc71e17891aa92ba23747e28f722f8bebe47e07649731d3451449ba8d2dfb794a1e3a720da78beb201c43a9a95b7a443ff8a39792d6bed0d6d6418696351ebb61e20d76cf82315feb4e4d25be42931f216864af09e4f36ad528425b9c47c570a1c9610adc623d62af304b1692a45e4d09e95b3a";

    }

    public Properties(string life)
    {

        Version = "1.0.0";
        MerchantID = "03119902";
        AcquiredId = "415738";
        //MerRespUrl = "https://www.srilankainsurance.net/Life/Authorized/Products/PayReceipt.aspx";
        PurchaseCurrency = "144";
        PurchaseCurrencyExponent = "2";
        SignatureMethod = "SHA1";
        CaptureFlag = "A";
        Password = "L6bH4n6R";
        //HostUrl = "https://www.hnbpg.hnb.lk/SENTRY/PaymentGateway/Application/ReDirectLink.aspx"; //Their Url.

        HostUrl = "https://secureacceptance.cybersource.com/pay"; //Their Url
        MerRespUrl = "https://www.srilankainsurance.net/Life/Authorized/Products/PayReceipt.aspx";
	
		AccessKey = "94181d7eed62338fab1b9f5c4a75130a"; //"31a66862bbb6379bb98be0c792add645"; "be25631b706e3df1ab2f0947b1b32f01";
        
		ProfileId = "A5FACDC6-DC3F-4F94-95B7-06172E21DE04";
        Locale = "en";
        //TransactionType = "authorization";
        TransactionType = "sale";
        Currency = "LKR";
        BillToAddressCountry = "LK";

		SecretKey = "eb78e5dda47b487cb8e24798cb919ef9eae4343cf3da424694ec5e22c050815b26becc44fcc24c8eb4ea19cadc6c9f3d5b811d98e3704d3eb94d62564519905bfcbd8d974a4d4b018d07d20f916f006ec5de93bc7ece495b81fe2a4bcbbe1f4d1d425d530fd84abfb3c2d9f590f86158129944705aec410c9a38bfbcae7d9b32";
        //"4c00de612e2e4772ad54aab3ca660a843bc071b2fce84635b0f1793790bab720ee843ffd15a2451bb7376999abda4eea296724c878704f3e9a60b40189a28d3a02f536ad69a04e25a331333aed47c231af4b32cd16c64c4ca231f7b182f19c31a7284304af7a4e85a033ed56392da7970d934d85178b4d33af9b98d2b900dcc1";
        //"d3759b1fc9cc4257a9d2a5625fe7ad062788bd676fc743d4b40e202e902befbddaa246fad5f44df4a504fb97fe407412bce48a09d1c54a92a5a53d0cc2f413951239696b1b764c45a92c832bb47f38fb037233cab9514ff28bb27b77187b323a533d7a6d8acc40969e68ee5fed3d440cb70d24f98cb24d46b237835215f6c0a4";

    }

    public Properties(string ins_type, string card_Type)
    {
        if (ins_type.ToUpper().Equals("GENERAL") && card_Type.ToUpper().Equals("AMEX"))
        {
            Version = "1.0.0";
            //MerchantID = "03123901";
            AcquiredId = "415738";
            //MerRespUrl = "https://www.srilankainsurance.net/General/Authorized/Products/PayReceipt.aspx";
            PurchaseCurrency = "144";
            PurchaseCurrencyExponent = "2";
            SignatureMethod = "SHA1";
            CaptureFlag = "A";
            Password = "jJ36tG1O";
           
            //Change when go live 
            MerchantID = "SLICIB";
            HostUrl = "https://www.ipayamex.lk/ipg/servlet_pay"; //Their Url

            MerRespUrl = "https://www.srilankainsurance.net/General/Authorized/Products/PayReceipt_Amex.aspx";
            
            Locale = "eng";
            TransactionType = "SaleTxn";
            Currency = "LKR";
            BillToAddressCountry = "LK";
            IPGKeyPath_1 = "C:\\Merchant\\iPay_Client_Service\\keys\\";
            IPGClientService_IP = "127.0.0.1";
            IPGClientService_Port = "55555";
        }

        if (ins_type.ToUpper().Equals("LIFE") && card_Type.ToUpper().Equals("AMEX"))
        {
            Version = "1.0.0";
            //MerchantID = "03123901";
            AcquiredId = "415738";
            //MerRespUrl = "https://www.srilankainsurance.net/General/Authorized/Products/PayReceipt.aspx";
            PurchaseCurrency = "144";
            PurchaseCurrencyExponent = "2";
            SignatureMethod = "SHA1";
            CaptureFlag = "A";
            Password = "jJ36tG1O";
            //HostUrl = "https://www.hnbpg.hnb.lk/SENTRY/PaymentGateway/Application/ReDirectLink.aspx"; //Their Url

            //Change when go live 
            MerchantID = "SLICLIFEIB";
            HostUrl = "https://www.ipayamex.lk/ipg/servlet_pay"; //Their Url

            MerRespUrl = "https://www.srilankainsurance.net/Life/Authorized/Products/PayReceipt_Amex.aspx";
           
            Locale = "eng";
            TransactionType = "SaleTxn";
            Currency = "LKR";
            BillToAddressCountry = "LK";
            IPGKeyPath_1 = "C:\\Merchant\\iPay_Client_Service\\keys\\";
            IPGClientService_IP = "127.0.0.1";
            IPGClientService_Port = "55555";
        }
    }


    public bool isValidSignature(string signature, string refNo)
    {
        bool retVal = false;
        if (signature != "" && refNo != "")
        {
            string test = Password + MerchantID + AcquiredId + refNo;


            string hashSig = Hash.Hash.GetHash(Password + MerchantID + AcquiredId + refNo, Hash.Hash.HashType.SHA1);
            if (signature == Hash.Hash.GetHash(Password + MerchantID + AcquiredId + refNo, Hash.Hash.HashType.SHA1))
            {
                retVal = true;
            }

            /*
                string hashSig = Hash.Hash.Get_SHA1(Password + MerchantID + AcquiredId + refNo);
                        if (signature == Hash.Hash.Get_SHA1(Password + MerchantID + AcquiredId + refNo))
                        {
                            retVal = true;
                        }*/
        }

        return retVal;
    }

    public string getSignature(string refNo, string amount, string currency)
    {
        log logger = new log();
        string valuetobehashed = Password + MerchantID + AcquiredId + refNo + amount + currency;
        logger.write_log("Comment - " + valuetobehashed);
        string hash = Hash.Hash.GetHash(valuetobehashed, Hash.Hash.HashType.SHA1);

        //string hash =  Hash.Hash.Get_SHA1(valuetobehashed);


        logger.write_log("And the hash - " + hash);

        return hash;

    }
    public byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

}

