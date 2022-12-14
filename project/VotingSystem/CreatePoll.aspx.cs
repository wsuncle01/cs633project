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
    public partial class CreatePoll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["RoleId"] != null && (Session["RoleId"].ToString().Equals("2") || Session["RoleId"].ToString().Equals("3")))
                {
                    //ddlPollVisibility.Items.RemoveAt(1);
                    //ddlPollVisibility.Items.RemoveAt(2);

                    ddlPollVisibility.Items.Remove(ddlPollVisibility.Items.FindByValue("Everyone"));
                    ddlPollVisibility.Items.Remove(ddlPollVisibility.Items.FindByValue("Faculty"));
                }
            }
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {

            PollBL obj = new PollBL();

            bool valid = false;

            try
            {
                string startDateStr = txtStartDate.Value;
                string endDateStr = txtEndDate.Value;

                DateTime startDate = DateTime.ParseExact(startDateStr, "HH:mm MM/dd/yyyy", null);
                DateTime endDate = DateTime.ParseExact(endDateStr, "HH:mm MM/dd/yyyy", null);

                valid = true;
            }
            catch (Exception ms)
            {

            }

            if (ddlCategory.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a category!')", true);
            }
            else if (valid == true && DateTime.ParseExact(txtStartDate.Value, "HH:mm MM/dd/yyyy", null) > DateTime.ParseExact(txtEndDate.Value, "HH:mm MM/dd/yyyy", null))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Start date can not be greater than end date!')", true);
            }
            else if (ddlPollStatus.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a poll status!')", true);
            }
            else if (txtQuestion.Value.Trim() == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a question!')", true);
            }
            else if (txtAnswerOption1.Value.Trim() == string.Empty || txtAnswerOption2.Value.Trim() == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter at least 2 answer options!')", true);
            }
            else if (txtStartDate.Value.Trim() == string.Empty || txtEndDate.Value.Trim() == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a start date and end date!')", true);
            }
            else if (valid == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a valid start date and end date!')", true);
            }
            else if (ddlPollVisibility.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a poll visibility!')", true);
            }
            else if (ddlResultsVisibility.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a results visibility!')", true);
            }
            else
            {


                obj.InterestId = int.Parse(ddlCategory.Value.ToString());
                obj.Question = txtQuestion.Value.Trim();
                obj.PollStatus = ddlPollStatus.Value.Trim();
                obj.Description = txtDescription.Value.Trim();
                obj.AnswerOption1 = txtAnswerOption1.Value.Trim();
                obj.AnswerOption2 = txtAnswerOption2.Value.Trim();
                obj.AnswerOption3 = txtAnswerOption3.Value.Trim();
                obj.AnswerOption4 = txtAnswerOption4.Value.Trim();
                obj.AnswerOption5 = txtAnswerOption5.Value.Trim();

                string startDateStr = txtStartDate.Value;
                string endDateStr = txtEndDate.Value;

                DateTime startDate = DateTime.ParseExact(startDateStr, "HH:mm MM/dd/yyyy", null); 
                DateTime endDate = DateTime.ParseExact(endDateStr, "HH:mm MM/dd/yyyy", null);

                obj.StartDate = startDate;
                obj.EndDate = endDate;

                obj.PollVisibility = ddlPollVisibility.Value;
                obj.ResultsVisibility = ddlResultsVisibility.Value;

                obj.UserId = int.Parse(Session["UserId"].ToString());

                obj.CreatePoll();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Poll created successfully. Your poll is pending review and you will receive an email once it has been reviewed.')", true);

                //Send Email to pollster

                string subject = "BU Voting System - Poll Review Request";

                //Build msg
                string emailMsg = "<b>BU Voting System</b>";
                emailMsg += "<br><br>";
                emailMsg += "A new poll has been created on the system. Please login to review the poll.";
                emailMsg += "<br><br>";
                emailMsg += "Poll Question: " + obj.Question;
                emailMsg += "<br><br>";
                emailMsg += "Please click ";

                //
                //Create link
                //Changed to use app settings
                string ApplicationServer = ConfigurationManager.AppSettings["ApplicationServer"].ToString();
                string strURL = "http://" + ApplicationServer + "/Default.aspx";
                //
                emailMsg += "<a href='" + strURL + "'>here</a>";
                emailMsg += " to login.<br><br>";
                //End

                MailHelper.SendMailMessage("no-reply@coralite.co.za", "tyron@bu.edu", "", "", subject, emailMsg);

                //Clear fields
                ddlCategory.SelectedIndex = 0;
                txtQuestion.Value = string.Empty;
                ddlPollStatus.SelectedIndex = 0;
                txtDescription.Value = string.Empty;
                txtAnswerOption1.Value = string.Empty;
                txtAnswerOption2.Value = string.Empty;
                txtAnswerOption3.Value = string.Empty;
                txtAnswerOption4.Value = string.Empty;
                txtAnswerOption5.Value = string.Empty;
                txtStartDate.Value = string.Empty;
                txtEndDate.Value = string.Empty;
                ddlPollVisibility.SelectedIndex = 0;
                ddlResultsVisibility.SelectedIndex = 0;

            }


            //UserBL obj = new UserBL();
            //if (ddlUserType.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a user type!')", true);
            //}
            //else if (txtEmail.Value.Trim() == string.Empty || txtPassword.Value.Trim() == string.Empty)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select an email and password!')", true);
            //}
            //else if (txtPassword.Value.Trim() != txtRepeatPassword.Value.Trim())
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password do not match!')", true);
            //}
            //else
            //{
            //    obj.RoleId = int.Parse(ddlUserType.Value.Trim());
            //    obj.FirstName = txtFirstName.Value.Trim();
            //    obj.LastName = txtLastName.Value.Trim();
            //    obj.Email = txtEmail.Value.Trim();
            //    obj.Password = EncryptString(txtPassword.Value.Trim());
            //    obj.UserStatusId = 1;

            //    if (obj.ReadUserExist().Rows.Count > 0)
            //    {
            //        //Message
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email address already exists!')", true);
            //    }
            //    else
            //    {

            //        int UserId = obj.CreateUser();

            //        //Add Interest

            //        UserInterestBL ui = new UserInterestBL();
            //        ui.UserId = UserId;

            //        for (int i = 0; i < chkInterest.Items.Count; i++)
            //        {
            //            if (chkInterest.Items[i].Selected)
            //            {
            //                int interestId = int.Parse(chkInterest.Items[i].Value.ToString());
            //                ui.InterestId = interestId;
            //                ui.CreateUserInterest();
            //            }
            //        }

            //        //Send email with activation link

            //        string email = obj.Email;

            //        //Build Email Message and Send

            //        string subject = "BU Voting System - Account Activation";
            //        string emailMsg = "<b>Welcome to the BU Voting System</b>";
            //        emailMsg += "<br><br>";
            //        emailMsg += "Please click ";

            //        //
            //        //Create link
            //        //Changed to use app settings
            //        string ApplicationServer = ConfigurationManager.AppSettings["ApplicationServer"].ToString();
            //        string strURL = "http://" + ApplicationServer + "/AccountActivation.aspx?";
            //        string strURLWithData = strURL +
            //        EncryptQueryString(string.Format("UserId={0}",
            //        UserId));
            //        //
            //        emailMsg += "<a href='" + strURLWithData + "'>here</a>";
            //        emailMsg += " to activate your account.<br><br>";


            //        //Send Email

            //        //MailHelper.SendMailMessage("no-reply@coralite.co.za", email, "", "", subject, emailMsg);


            //        //End send email

            //        //Send Email to pollster

            //        MailHelper.SendMailMessage("no-reply@coralite.co.za", "tyron@bu.edu", "", "", subject, "A new user has registered on the system. Please login to activate the account.");

            //        //Message
            //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account registered successfully. Please check your email to activate your account.')", true);
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account registered successfully. Please wait for the administrator to verify your account. You will receieve an email once your account is activated.')", true);
            //    }
            //}
        }

    }
}