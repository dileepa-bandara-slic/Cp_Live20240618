using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ReadAmount
/// </summary>
public class ReadAmount
{
	public ReadAmount()
	{

	}


    //**************** readToArray ****************

    public int[] readToArray(string amount)
    {
        int iTemp;
        int iNumLength = amount.Length;
        int[] arrSingleNum = new int[iNumLength];
        iTemp = amount.Length - 1; //- 1;
        for (int i = 0; i < iNumLength; i++)
        {
            arrSingleNum[iTemp] = int.Parse((amount.ToString()).Substring(i, 1));
            iTemp--;
        }

        return arrSingleNum;
    }

    //************ readThreeDigitAmount *****************************

    public string readThreeDigitAmount(string amount, string[] name, string[] name1, string[] name3, int iStart, string str)
    {
        String strAmount = "";
        int iNumLength = amount.Length;
        int[] arrSingleNum = new int[iNumLength];
        arrSingleNum = readToArray(amount);


        for (int j = iStart; j < iNumLength; j++)
        {
            if (j == iStart)
            {

                int n = arrSingleNum[j];
                strAmount = name[n];

            }

            if (j == (iStart + 1))
            {

                int n2 = arrSingleNum[j];
                if ((arrSingleNum[j] == 1) && (arrSingleNum[j - 1] == 0))
                {
                    strAmount = "Ten";
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

                if ((double.Parse(amount) > 100) && ((double.Parse(amount) % 100) != 0))
                {
                    strAmount = str + strAmount;
                }

            }
            if (j == (iStart + 2))
            {
                int n3 = arrSingleNum[j];
                if (arrSingleNum[j] != 0)
                {
                    strAmount = name[n3] + " Hundred  " + strAmount;
                }
            }

        }
        return strAmount;

    }

    //******************** readAmount ****************************

    public string readAmount(string amount, string curType, string cts)
    {

        string strAmount = "";
        string[] name ={ "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        string[] name1 ={ "", "Ten ", "Twenty ", "Thirty ", "Fourty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
        string[] name3 ={ "", "Eleven ", "Twelve", " Thirteen", "Fourteen", "Fifteen", "Sixteen", " Seventeen", "Eighteen", "Nineteen" };

        int iRsLength = amount.Length - 3;
        int iCtsLength = amount.Length;

        string Rs = amount.Substring(0, iRsLength);
        string Cts = amount.Substring((iCtsLength - 2), 2);

        if (Cts.Equals("00"))
            strAmount = readThreeDigitAmount(Rs, name, name1, name3, 0, " and ") + " Only.";
        else
            strAmount = readThreeDigitAmount(Rs, name, name1, name3, 0, "") + " and " + cts + readThreeDigitAmount(Cts, name, name1, name3, 0, " ") + " Only.";

        //-------------------------------

        if (iRsLength > 3)
        {
            double temp = double.Parse((Rs.ToString().Substring((Rs.ToString().Length - 4), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 3), 1)));

            if (temp != 0)
            {
                strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") + " Thousand " + strAmount;
            }
            else if ((temp == 0) && (iRsLength == 5))
                strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") + " Thousand " + strAmount;
            else
                strAmount = readThreeDigitAmount(Rs, name, name1, name3, 3, " ") + strAmount;

        }
        if (iRsLength > 6)
        {
            double temp1 = double.Parse((Rs.ToString().Substring((Rs.ToString().Length - 7), 1)) + (Rs.ToString().Substring((Rs.ToString().Length - 6), 1)));
            if (temp1 != 0)
            {
                strAmount = readThreeDigitAmount(Rs, name, name1, name3, 6, " ") + " Million " + strAmount;
            }
            else if ((temp1 == 0) && (iRsLength == 8))
                strAmount = readThreeDigitAmount(Rs, name, name1, name3, 6, " ") + " Million " + strAmount;
            else
                strAmount = readThreeDigitAmount(Rs, name, name1, name3, 6, " ") + strAmount;

        }

        if (iRsLength > 9)
        {
            //var temp2=parseInt(Rs.charAt(Rs.length-11)+Rs.charAt(Rs.length-10)+Rs.charAt(Rs.length-9))
            //if(temp1 != 0)
            //{
            strAmount = readThreeDigitAmount(Rs, name, name1, name3, 9, " ") + " Billion " + strAmount;
            //}
            //else
            //	strAmount=readThreeDigitAmount(Rs,name,name1,name3,9," ") +" BILLION " +strAmount ;
        }

        return curType + " " + strAmount;
    }





}
