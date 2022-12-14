using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VotingSystem.BusinessLayer;
using CommonLibrary;
using System.Configuration;
using System.Data;

namespace VotingSystem
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReset_ServerClick(object sender, EventArgs e)
        {
            UserBL obj = new UserBL();

            obj.Email = txtEmail.Value.Trim();

            DataTable table = obj.ReadUserExist();

            if (table.Rows.Count > 0)
            {
                string UserId = table.Rows[0]["UserId"].ToString();

                //Send email with activation link

                string email = obj.Email;

                //Build Email Message and Send

                string subject = "BU Voting System - Reset Password";
                string emailMsg = "<b>BU Voting System</b>";
                emailMsg += "<br><br>";
                emailMsg += "Please click ";

                //
                //Create link
                //Changed to use app settings
                string ApplicationServer = ConfigurationManager.AppSettings["ApplicationServer"].ToString();
                string strURL = "http://" + ApplicationServer + "/ResetPassword.aspx?";
                string strURLWithData = strURL +
                EncryptQueryString(string.Format("UserId={0}",
                UserId));
                //
                emailMsg += "<a href='" + strURLWithData + "'>here</a>";
                emailMsg += " to reset your password.<br><br>";


                //Send Email

                MailHelper.SendMailMessage("no-reply@coralite.co.za", email, "", "", subject, emailMsg);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check your email to reset your password.')", true);

            }
            else
            {
                //Message
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account does not exist!')", true);
            }
        }

        public string EncryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Encrypt(strQueryString);
        }
    }
}