/**********************************************************
 * Company  : Interblocks Ltd
 * Created  : 2011.6.10
 **********************************************************/ 


using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Sockets;
using System.Text;
using System.Xml;

using System.Web.Configuration;

public class IShroff
{
    private int m_iBufferSize;
    private int m_iReadTimeout;
    private Socket m_oSocket;

    private string strHostIP = "127.0.0.1";
    private string strHostPort = "55555"; 

    private string strErrorCode = "";
    private string strErrorMessage = "";
    private string strEncryptedInvoice = "";
    private string strPTReceipt = "";
    private string strKeyPath = "";

    public string iPayClient_SUCCESS = "00";

    public IShroff()
    {
        this.strErrorCode = iPayClient_SUCCESS;

        this.m_iReadTimeout = 5000;
        this.m_iBufferSize = 4096;   
        this.m_oSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        Properties propAmex = new Properties("General", "Amex");
        this.strHostIP = propAmex.IPGClientService_IP;
        this.strHostPort = propAmex.IPGClientService_Port;
        
    }

    private bool connect(string strIP, int iPort)
    {
        bool bReturn = true;
        try
        {
            this.m_oSocket.ReceiveTimeout = m_iReadTimeout; 
            this.m_oSocket.Connect(strIP, iPort);
            bReturn = true;
        }
        catch (Exception oEx)
        {
            Console.WriteLine("Exception 1 :" + oEx.Message);
            bReturn = false;
        }
        return bReturn; 
    }

    private bool send(string strMessage,  ref string strReceived)
    {
        byte[] abBuffer = ASCIIEncoding.ASCII.GetBytes(strMessage);
        bool bReturn = true;
        try
        {
            if (send(abBuffer))
            {
                if (readForXml(ref strReceived))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception oEx)
        {
            Console.WriteLine("Exception 2 :" + oEx.Message);
            bReturn = false;
        }

        return bReturn;
    }

    private bool send(byte[] abBuffer)
    {
        bool bReturn = true;
        int iSentTotal = 0;
        int iSent = 0;
        try
        {
            if (this.m_oSocket.Connected)
            {
                while (iSentTotal < abBuffer.Length)
                {
                    iSent = 0;
                    iSent = this.m_oSocket.Send(abBuffer, iSentTotal, (abBuffer.Length - iSentTotal),SocketFlags.None);
                    iSentTotal += iSent;
                }
            }
            else
            {
                bReturn = false;
            }
        }
        catch (Exception oEx)
        {
            Console.WriteLine("Exception 3 :" + oEx.Message);
            bReturn = false;
        }

        return bReturn; 
    }

    private bool readForXml(ref string strReceived)
    {
        int iRead = 0;
        string strXmlRcv = "";
        bool bReturn = false;
        byte[] abBuffer = new byte[m_iBufferSize];    
        try
        {
            while(true)
            {
                if ((iRead = this.m_oSocket.Receive(abBuffer)) > 0)
                {
                    strXmlRcv += ASCIIEncoding.ASCII.GetString(abBuffer,0,iRead);
                    try
                    {
                        XmlDocument oXml = new XmlDocument();
                        oXml.LoadXml(strXmlRcv);
                        strReceived = strXmlRcv;
                        bReturn = true;
                        break;
                    }
                    catch (Exception oEx)
                    {
                        Console.WriteLine("Exception 4 :" + oEx.Message);
                    }
                }
                else
                {
                    bReturn = false;
                    break;
                }
            }
        }
        catch (Exception oEx)
        {
            Console.WriteLine("Exception 5 :" + oEx.Message);
            bReturn = false;
        }

        return bReturn;
    }

    private void close()
    {
        try
        {
            if (this.m_oSocket.Connected)
            {
                this.m_oSocket.Shutdown(SocketShutdown.Both);   
                this.m_oSocket.Close();
            }
        }
        catch (Exception oEx)
        {
            Console.WriteLine("Exception 6 :" + oEx.Message);
        }
    }

    public string getErrorCode()
    {
        return this.strErrorCode;
    }

    public string getErrorMessage()
    {
        return this.strErrorMessage;
    }

    public string getEncryptedInvoice()
    {
        this.strErrorCode = iPayClient_SUCCESS;
        return this.strEncryptedInvoice;
    }
    
    public string getPlainTextReceipt()
    {
        this.strErrorCode = iPayClient_SUCCESS;
        return this.strPTReceipt;
    }

    public void setSecurityKeysPath(string sKeyPath)
    {
        this.strErrorCode = iPayClient_SUCCESS;
        if (sKeyPath.Length > 0) this.strKeyPath = sKeyPath;
    }

    public bool setPlainTextInvoice(string PTInvoice)
    {
        this.strErrorCode = iPayClient_SUCCESS;

        connect(this.strHostIP, Convert.ToInt32(this.strHostPort));

        //Wrap Plain text invoice to send to iPay Client Service
        string strWrappedPTInvoice = "<PTInvoice>" + PTInvoice + "</PTInvoice>";
        string strWrappedEncryptedInvoice = "";

        string strWrappedKeyPath = "";
        if (this.strKeyPath.Length > 0) strWrappedKeyPath = "<Key_Path>" + this.strKeyPath + "</Key_Path>";

        bool bResult = send(strWrappedPTInvoice + strWrappedKeyPath, ref strWrappedEncryptedInvoice);
        close();

        if(bResult)
        {
            //Use XML parser to unwrap EncryptedInvoice
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(strWrappedEncryptedInvoice);

            if(IsNodeExist(oXml,"Error"))
            {
                bResult = false;
                this.strErrorCode = GetNodeValue(oXml,"error_code");
                this.strErrorMessage = GetNodeValue(oXml,"error_msg");
            }
            else
            {
                bResult = true;
                this.strEncryptedInvoice = GetNodeValue(oXml,"EncryptedInvoice");
                this.strErrorCode = iPayClient_SUCCESS;
            }
        }
        else
        {
            this.strErrorCode = "-100";
            this.strErrorMessage = "Failed communication with iPayClientService";            
        }

        return bResult;
    }

    public bool setEncryptedReceipt(string strEncryptedReceipt)
    {
        this.strErrorCode = iPayClient_SUCCESS;

        connect(this.strHostIP, Convert.ToInt32(this.strHostPort));

        //Wrap Encrypted Receipt to send to iPay Client Service
        string strWrappedEncryptedReceipt = "<EncryptedReceipt>" + strEncryptedReceipt + "</EncryptedReceipt>";
        string strWrappedPTReceipt = "";

        string strWrappedKeyPath = "";
        if (this.strKeyPath.Length > 0) strWrappedKeyPath = "<Key_Path>" + this.strKeyPath + "</Key_Path>";

        bool bResult = send(strWrappedEncryptedReceipt + strWrappedKeyPath, ref strWrappedPTReceipt);
        close();

        if(bResult)
        {
            //Use XML parser to unwrap plain text receipt
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(strWrappedPTReceipt);

            if (IsNodeExist(oXml, "Error"))
            {
                bResult = false;
                this.strErrorCode = GetNodeValue(oXml, "error_code");
                this.strErrorMessage = GetNodeValue(oXml, "error_msg");
            }
            else
            {
                bResult = true;
                this.strPTReceipt = GetNodeXML(oXml, "PTReceipt");
                this.strErrorCode = iPayClient_SUCCESS;
            }
        }
        else
        {
            this.strErrorCode = "-100";
            this.strErrorMessage = "Failed communication with iPayClientService";            
        }

        return bResult;
    }

    private string GetNodeValue(XmlDocument oXml, string strNode)
    {
        try
        {
            return oXml.GetElementsByTagName(strNode)[0].InnerText;
        }
        catch
        {
            return "";
        }
    }

    private string GetNodeXML(XmlDocument oXml, string strNode)
    {
        try
        {
            return oXml.GetElementsByTagName(strNode)[0].InnerXml;
        }
        catch
        {
            return "";
        }
    }

    private bool IsNodeExist(XmlDocument oXml, string strNode)
    {
        try
        {
            if (oXml.GetElementsByTagName(strNode).Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
}
