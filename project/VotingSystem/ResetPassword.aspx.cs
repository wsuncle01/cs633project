using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VotingSystem.BusinessLayer;

namespace VotingSystem
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        const string passphrase = "password";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strReq = "";
                strReq = Request.RawUrl;
                strReq = strReq.Substring(strReq.IndexOf('?') + 1);

                if (!strReq.Equals(""))
                {
                    strReq = DecryptQueryString(strReq);

                    if (strReq != "The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or a non-white space character among the padding characters. ")
                    {
                        if (strReq != "Invalid length for a Base-64 char array or string.")
                        {

                            if (strReq.Length > 0)
                            {
                                //Parse the value...
                                string[] arrMsgs = strReq.Split('&');
                                string[] arrIndMsg;
                                string UserId = "";
                                arrIndMsg = arrMsgs[0].Split('='); //Get the Deadline OfficialID
                                UserId = arrIndMsg[1].ToString().Trim();

                                ViewState["UserId"] = UserId;

                            }
                        }
                    }
                }
            }
        }

        protected void btnReset_ServerClick(object sender, EventArgs e)
        {
            string UserId = ViewState["UserId"].ToString();

            UserBL obj = new UserBL();

            obj.UserId = int.Parse(UserId);
            obj.Password = EncryptString(txtPassword.Value.Trim());

            obj.UpdateUserPassword();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password reset successfully!')", true);

        }

        private string DecryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Decrypt(strQueryString);
        }

        public static string EncryptString(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

    }

}