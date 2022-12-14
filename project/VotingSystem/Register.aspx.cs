using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VotingSystem.BusinessLayer;
using CommonLibrary;
using System.Configuration;

namespace VotingSystem
{
    public partial class Register : System.Web.UI.Page
    {
        const string passphrase = "password";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_ServerClick(object sender, EventArgs e)
        {
            UserBL obj = new UserBL();
            if (ddlUserType.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a user type!')", true);
            }
            else if (txtEmail.Value.Trim() == string.Empty || txtPassword.Value.Trim() == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter an email and password!')", true);
            }
            else if (txtPassword.Value.Trim() != txtRepeatPassword.Value.Trim())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password do not match!')", true);
            }
            else
            {
                obj.RoleId = int.Parse(ddlUserType.Value.Trim());
                obj.FirstName = txtFirstName.Value.Trim();
                obj.LastName = txtLastName.Value.Trim();
                obj.Email = txtEmail.Value.Trim();
                obj.Password = EncryptString(txtPassword.Value.Trim());
                obj.UserStatusId = 1;

                if (obj.ReadUserExist().Rows.Count > 0)
                {
                    //Message
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email address already exists!')", true);
                }
                else
                {

                    int UserId = obj.CreateUser();

                    //Add Interest

                    UserInterestBL ui = new UserInterestBL();
                    ui.UserId = UserId;

                    for (int i = 0; i < chkInterest.Items.Count; i++)
                    {
                        if (chkInterest.Items[i].Selected)
                        {
                            int interestId = int.Parse(chkInterest.Items[i].Value.ToString());
                            ui.InterestId = interestId;
                            ui.CreateUserInterest();
                        }
                    }

                    //Send email with activation link

                    string email = obj.Email;

                    //Build Email Message and Send

                    string subject = "BU Voting System - Account Activation";
                    string emailMsg = "<b>Welcome to the BU Voting System</b>";
                    emailMsg += "<br><br>";
                    emailMsg += "Please click ";
                  
                    //
                    //Create link
                    //Changed to use app settings
                    string ApplicationServer = ConfigurationManager.AppSettings["ApplicationServer"].ToString();
                    string strURL = "http://" + ApplicationServer + "/AccountActivation.aspx?";
                    string strURLWithData = strURL +
                    EncryptQueryString(string.Format("UserId={0}",
                    UserId));
                    //
                    emailMsg += "<a href='" + strURLWithData + "'>here</a>";
                    emailMsg += " to activate your account.<br><br>";


                    //Send Email

                    //MailHelper.SendMailMessage("no-reply@coralite.co.za", email, "", "", subject, emailMsg);


                    //End send email

                    //Send Email to pollster

                    MailHelper.SendMailMessage("no-reply@coralite.co.za", "tyron@bu.edu", "", "", subject, "A new user has registered on the system. Please login to activate the account.");

                    //Message
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account registered successfully. Please check your email to activate your account.')", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account registered successfully. Please wait for the administrator to verify your account. You will receieve an email once your account is activated.')", true);
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

        public string EncryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Encrypt(strQueryString);
        }

    }
}