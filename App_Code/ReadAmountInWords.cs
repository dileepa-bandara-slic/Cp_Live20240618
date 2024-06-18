using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for ReadAmountInWords
/// </summary>
public class ReadAmountInWords
{
    public ReadAmountInWords()
    {
    }

    public int[] readToArray(string amount)
    {
        int iTemp;
        int iNumLength = amount.Length;
        int[] arrSingleNum = new int[iNumLength];
        iTemp = amount.Length - 1; //- 1;
        for (int i = 0; i < iNumLength; i++) 
        {
            arrSingleNum[iTemp] = int.Parse((amount.ToString()).Substring(i,1));
            iTemp--;
        }

        return arrSingleNum;
    }

    //************ readThreeDigitAmount *****************************

    public string readThreeDigitAmount(string amount, string [] name, string [] name1, string [] name3, int iStart, string str) 
    {
        String strAmount = "";
        int iNumLength = amount.Length;
        int[] arrSingleNum = new int[iNumLength];
        arrSingleNum = readToArray(amount);


        for (int j = iStart; j < iNumLength; j++)
        { 
            if (j==iStart){

                int n = arrSingleNum[j];
                strAmount =name[n];
                
            }

            if (j == (iStart + 1)) {

                int n2 = arrSingleNum[j];
                if ((arrSingleNum[j] == 1) && (arrSingleNum[j - 1] == 0))
                {
                    strAmount = "TEN";
                }
                else if (arrSingleNum[j] == 1)
                {
                    int nn = arrSingleNum[j - 1];
                    strAmount = name3[nn];
                }
                else
                {
                    strAmount = name1[n2] + strAmount;
                }
                
                if ((double.Parse(amount) > 100) && ((double.Parse(amount)% 100) != 0))
                {
                    strAmount = str + strAmount;
                }
         			
            }
            if (j == (iStart + 2))
            {
                int n3 = arrSingleNum[j];
                if (arrSingleNum[j] != 0)               
                {
                    strAmount = name[n3] + " HUNDRED  " + strAmount;
                }
            }
                        
        }
        return strAmount;
       
    }

    //******************** readAmount ****************************

    public string readAmount(string amount, string curType, string cts)
    {

        string strAmount = "";
        string[] name ={ "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
        string[] name1 ={ "", "TEN ", "TWENTY ", "THIRTY ", "FORTY ", "FIFTY ", "SIXTY ", "SEVENTY ", "EIGHTY ", "NINETY " };
        string[] name3 ={ "", "ELEVEN ", "TWELVE", " THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", " SEVENTEEN", "EIGHTEEN", "NINETEEN" };

        int iRsLength = amount.Length - 3;
        int iCtsLength = amount.Length;

        string Rs = amount.Substring(0, iRsLength);
        string Cts = amount.Substring((iCtsLength - 2), 2);

        if (Cts.Equals("00"))
        { strAmount = readThreeDigitAmount(Rs, name, name1, name3, 0, " AND ") + " ONLY."; }
        else
        { strAmount = readThreeDigitAmount(Rs, name, name1, name3, 0, "") + " AND " + cts + readThreeDigitAmount(Cts, name, name1, name3, 0, " ") + " ONLY."; }
        
        //-------------------------------
        double temp1 = 0;
        double temp2 = 0;
        if ((iRsLength > 6) && ((iRsLength == 7)))
        {
            temp1 = double.Parse(Rs.ToString().Substring((Rs.ToString().Length - 7), 1));
        }
        else if ((iRsLength > 6) && ((iRsLength == 8)))
        {
            temp1 = double.Parse((Rs.ToString().Substring((Rs.ToString().Length - 8), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 7), 1)));
        }
        else if ((iRsLength > 6) && ((iRsLength == 9)))
        {
            temp1 = double.Parse((Rs.ToString().Substring((Rs.ToString().Length - 9), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 8), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 7), 1)));
        }
        else if ((iRsLength > 9) && (iRsLength == 10))
        {
            temp2 = double.Parse(Rs.ToString().Substring((Rs.ToString().Length - 10), 1));
        }
        else if ((iRsLength > 9) && (iRsLength == 11))
        {
            temp2 = double.Parse((Rs.ToString().Substring((Rs.ToString().Length - 11), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 10), 1)));
        }
        else if ((iRsLength > 9) && (iRsLength == 12))
        {
            temp2 = double.Parse((Rs.ToString().Substring((Rs.ToString().Length - 12), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 11), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 10), 1)));
        }

        #region //------------- old code --------------------
        if (iRsLength > 3)
        {
            double temp = 0;
            if (iRsLength == 4)
            {
                temp = double.Parse(Rs.ToString().Substring((Rs.ToString().Length - 4), 1));
            }
            else if (iRsLength == 5)
            {
                temp = double.Parse((Rs.ToString().Substring((Rs.ToString().Length - 5), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 4), 1)));
            }
            else if (iRsLength == 6)
            {
                temp = double.Parse((Rs.ToString().Substring((Rs.ToString().Length - 6), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 5), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 4), 1)));
            }

            if (temp != 0)
            {
                if (temp1 != 0)
                {
                    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") + strAmount;
                }
                else
                {
                    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") + " THOUSAND " + strAmount;
                }
            }
            else
            {
                if (iRsLength <= 6)
                {
                    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") + " THOUSAND " + strAmount;
                }
                else
                {
                    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") + " THOUSAND " + strAmount;
                    //if (readThreeDigitAmount(Rs, name, name1, name3, 3, " ").Equals(""))
                    //{
                    //    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") +  strAmount;
                    //}
                    //else
                    //{
                    //    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") + " THOUSANDE " + strAmount;
                    //}
                }

            }
        }
        if (iRsLength > 6)
        {
            if (temp1 != 0)
            {
                if (temp2 != 0)
                {
                    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 6, " ") + strAmount;
                }
                else
                {
                    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 6, " ") + " MILLION " + strAmount;                    
                }
            }
            else
            {
                if (iRsLength <= 9)
                {
                    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 6, " ") + " MILLION " + strAmount;                    
                }
                else
                {
                    strAmount = readThreeDigitAmount(Rs, name, name1, name3, 6, " ") + " MILLION " + strAmount;                   
                }
            }
        }

        if (iRsLength > 9)
        {
            //var temp2=parseInt(Rs.charAt(Rs.length-11)+Rs.charAt(Rs.length-10)+Rs.charAt(Rs.length-9))
            //if(temp1 != 0)
            //{
            strAmount = readThreeDigitAmount(Rs, name, name1, name3, 9, " ") + " BILLION " + strAmount;
            //}
            //else
            //	strAmount=readThreeDigitAmount(Rs,name,name1,name3,9," ") +" BILLION " +strAmount ;
        }
        #endregion


        return curType + " " + strAmount;
    }  
    
}

	

