using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;

//namespace secureacceptance
//{
    //public static class Security
    public static class Security
    {
    //Change when go live
    //private const String SECRET_GEN_KEY = "9408e9e1067048c1b99593d7a6b459831d7c55ef5bd54006a59fc6ac4db00c49e8e214dcae4944bcb07432db808f09a49990658b80794115802a9ad8c72954f653821f852e2c4948984cdd682f09c7052ae0aeccd8ed408ab5878c942b7f5d0cba261ca6ebca4b46922c137f707fac2d7b364c55797c4fe58112f4755909e676";
    //private const String SECRET_LIFE_KEY = "b29fb65eb0464dabab635504b9e0bed64818f7927f6c4a6da475c2abc29f524f418e0d7cf1264df4884f6eaa76b8a2107d0c661b4bf14604817babbecf4743240d283897bb92470db37e5e485e87bf0f8b31a811442040b78da3293e267c5c7384ea1d5444c246ddacb59fddff8509065fdb6606bff14e20b463e02471cb8cc7";

        private const String SECRET_GEN_KEY = "3895a7d29d5a45ada901905a74fb8ccf99a9231f6c3a4c5d8905f4e26e6093a0d7df4da6968641c39789f838b8b7762bc74768a1aa564ee08ab84499a4d63fa334d4df5bc2fd40608c08721cf710c04b2d0942409c194738a9e696484928e36e504da29e96ee41618350f27b0c49bca3402ee9d5f96442d08d6d477d1aac6cd3";
										//"3d57edf7be08496a9c98d0fbc3c4ac9c54e6552b737e4ee7a0ee54517ce58c17a54ead5af32a4b1eabdccfc2aa66c98eadc3d82aa31940febb18fe3e73ddb4fb19177a373dc9402883eec75d49c987fc5ff75f6e7aa944678daec1253e70faf4e9d18548ddf9434abf8d452efc100740d8510e0cc9a84765883e9a5a5acff5ea";(2023)
										//"9882a7a6132843eba9de83591cc71e17891aa92ba23747e28f722f8bebe47e07649731d3451449ba8d2dfb794a1e3a720da78beb201c43a9a95b7a443ff8a39792d6bed0d6d6418696351ebb61e20d76cf82315feb4e4d25be42931f216864af09e4f36ad528425b9c47c570a1c9610adc623d62af304b1692a45e4d09e95b3a";
    
	private const String SECRET_LIFE_KEY = "eb78e5dda47b487cb8e24798cb919ef9eae4343cf3da424694ec5e22c050815b26becc44fcc24c8eb4ea19cadc6c9f3d5b811d98e3704d3eb94d62564519905bfcbd8d974a4d4b018d07d20f916f006ec5de93bc7ece495b81fe2a4bcbbe1f4d1d425d530fd84abfb3c2d9f590f86158129944705aec410c9a38bfbcae7d9b32";
    //"4c00de612e2e4772ad54aab3ca660a843bc071b2fce84635b0f1793790bab720ee843ffd15a2451bb7376999abda4eea296724c878704f3e9a60b40189a28d3a02f536ad69a04e25a331333aed47c231af4b32cd16c64c4ca231f7b182f19c31a7284304af7a4e85a033ed56392da7970d934d85178b4d33af9b98d2b900dcc1";
	//"d3759b1fc9cc4257a9d2a5625fe7ad062788bd676fc743d4b40e202e902befbddaa246fad5f44df4a504fb97fe407412bce48a09d1c54a92a5a53d0cc2f413951239696b1b764c45a92c832bb47f38fb037233cab9514ff28bb27b77187b323a533d7a6d8acc40969e68ee5fed3d440cb70d24f98cb24d46b237835215f6c0a4";

    public static String sign(IDictionary<string, string> paramsArray, string keyType)  
        {

            string SECRET_KEY = "";
            if (keyType.Equals("G"))
            {
                SECRET_KEY = SECRET_GEN_KEY;
            }
            if (keyType.Equals("L"))
            {
                SECRET_KEY = SECRET_LIFE_KEY;
            }

            return sign(buildDataToSign(paramsArray), SECRET_KEY);
        }

        private static String sign(String data, String secretKey) {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static String buildDataToSign(IDictionary<string,string> paramsArray) {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

	        foreach (String signedFieldName in signedFieldNames)
	        {
	             dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
	        }

            return commaSeparate(dataToSign);
        }

        private static String commaSeparate(IList<string> dataToSign) {
            return String.Join(",", dataToSign);                         
        }
    }
//}
