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
    public partial class UpdateProfile : System.Web.UI.Page
    {
        const string passphrase = "password";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            UserBL obj = new UserBL();
            obj.UserId = int.Parse(Session["UserId"].ToString());
            DataTable table = obj.ReadUserById();

            if (table.Rows.Count > 0)
            {
                txtFirstName.Value = table.Rows[0]["FirstName"].ToString();
                txtLastName.Value = table.Rows[0]["LastName"].ToString();
                txtEmail.Value = table.Rows[0]["Email"].ToString();
            }

            UserInterestBL userInt = new UserInterestBL();
            userInt.UserId = int.Parse(Session["UserId"].ToString());
            DataTable interest = userInt.ReadUserInterest();

            if (interest.Rows.Count > 0)
            {
                for (int i = 0; i < interest.Rows.Count; i++)
                {
                    //chkInterest.SelectedValue = interest.Rows[i]["InterestId"].ToString();
                    chkInterest.Items[int.Parse(interest.Rows[i]["InterestId"].ToString())-1].Selected = true;
                }
            }

        }

        protected void btnUpdateProfile_ServerClick(object sender, EventArgs e)
        {
            UserBL obj = new UserBL();

            //if (txtEmail.Value.Trim() == string.Empty || txtPassword.Value.Trim() == string.Empty)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter an email and password!')", true);
            //}
            if (txtPassword.Value.Trim() != txtRepeatPassword.Value.Trim())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password do not match!')", true);
            }
            else
            {

                int UserId = int.Parse(Session["UserId"].ToString());

                obj.FirstName = txtFirstName.Value.Trim();
                obj.LastName = txtLastName.Value.Trim();
                obj.Email = txtEmail.Value.Trim();
                obj.Password = EncryptString(txtPassword.Value.Trim());

                //Update User
                obj.UpdateUser();

                if (obj.Password != string.Empty)
                    obj.UpdateUserAndPassword();

                //Add Interest

                UserInterestBL ui = new UserInterestBL();
                ui.UserId = UserId;

                //Remove current interest before adding
                ui.DeleteUserInterest();

                for (int i = 0; i < chkInterest.Items.Count; i++)
                {
                    if (chkInterest.Items[i].Selected)
                    {
                        int interestId = int.Parse(chkInterest.Items[i].Value.ToString());
                        ui.InterestId = interestId;
                        ui.CreateUserInterest();
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Profile updated successfully.')", true);

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