﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Hash
{
    public class Hash
    {
        public Hash() { }

        public enum HashType : int
        {
            MD5,
            SHA1,
            SHA256,
            SHA512
        }

        public static string GetHash(string text, HashType hashType)
        {
            string hashString;
            switch (hashType)
            {
                case HashType.MD5:
                    hashString = GetMD5(text);
                    break;
                case HashType.SHA1:
                    hashString = GetSHA1(text);
                    break;
                case HashType.SHA256:
                    hashString = GetSHA256(text);
                    break;
                case HashType.SHA512:
                    hashString = GetSHA512(text);
                    break;
                default:
                    hashString = "Invalid Hash Type";
                    break;
            }
            return hashString;
        }

        public static bool CheckHash(string original, string hashString, HashType hashType)
        {
            string originalHash = GetHash(original, hashType);
            return (originalHash == hashString);
        }

        private static string GetMD5(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            MD5 hashString = new MD5CryptoServiceProvider();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        private static string GetSHA1(string text)
        {
            //UnicodeEncoding UE = new UnicodeEncoding();
            //byte[] hashValue;
            //byte[] message = UE.GetBytes(text);

            //SHA1Managed hashString = new SHA1Managed();
            //string hex = "";

            //hashValue = hashString.ComputeHash(message);
            //foreach (byte x in hashValue)
            //{
            //    hex += String.Format("{0:x2}", x);
            //}
            //return hex;

            string hashValue = "";
            SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
            objSHA1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text.ToCharArray()));
            byte[] buffer = objSHA1.Hash;
            foreach (byte x in buffer)
            {
                hashValue += String.Format("{0:x2}", x);
            }

            //log logger = new log();
            //logger.write_log("Hash value: " + hashValue);
            hashValue = System.Convert.ToBase64String(buffer);


            return hashValue;
            
        }

        private static string GetSHA256(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        private static string GetSHA512(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            SHA512Managed hashString = new SHA512Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }


        public static string Get_SHA512(string input)
        {
            System.Security.Cryptography.SHA512 enc = SHA512Managed.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = enc.ComputeHash(bytes);
            return Get_String(hash).ToLower();
        }

        public static string Get_SHA1(string input)
        {
            System.Security.Cryptography.SHA1 enc = SHA1Managed.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = enc.ComputeHash(bytes);
            return Get_String(hash).ToLower();
        }

        private static string Get_String(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

    
}