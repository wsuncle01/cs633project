using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VotingSystem.BusinessLayer;

namespace VotingSystem
{
    public partial class _Default : Page
    {

        const string passphrase = "password";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Update polls to completed if end date is past
                PollBL obj = new PollBL();
                obj.UpdatePollComplete();
            }
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            UserBL obj = new UserBL();
            if (txtUsername.Value.Trim() == string.Empty || txtPassword.Value.Trim() == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter an email address and password!')", true);
            }
            else
            {
                obj.Email = txtUsername.Value.Trim();
                obj.Password = EncryptString(txtPassword.Value.Trim());
                DataTable table = obj.ReadUser();

                if (table.Rows.Count > 0)
                {

                    if (table.Rows[0]["UserStatusId"].ToString() == "1")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check your email to active your account!')", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your account has not been activated yet. Please wait for the administrator to activate your account.')", true);
                    }
                    else
                    {
                        Session["UserId"] = table.Rows[0]["UserId"].ToString();
                        Session["FirstName"] = table.Rows[0]["FirstName"].ToString();
                        Session["RoleId"] = table.Rows[0]["RoleId"].ToString();

                        Response.Redirect("Dashboard.aspx");

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Incorrect email address or password!')", true);
                }
            }
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