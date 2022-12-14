using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VotingSystem.BusinessLayer;
using CommonLibrary;
using System.Globalization;
using System.Configuration;

namespace VotingSystem
{
    public partial class ReviewPollPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            PollBL obj = new PollBL();
            obj.PollId = Session["PollIdReview"] == null ? 0 : int.Parse(Session["PollIdReview"].ToString());

            DataTable table = obj.ReadPoll();

            if (table.Rows.Count > 0)
            {
                //Poll Details
                txtCategory.InnerText = "Category: " + table.Rows[0]["Category"].ToString();
                txtDescription.InnerText = table.Rows[0]["Description"].ToString() == string.Empty ? "" : "Description: " + table.Rows[0]["Description"].ToString();
                txtStartDate.InnerText = "Start Date: " + DateTime.Parse(table.Rows[0]["StartDate"].ToString());
                txtEndDate.InnerText = "End Date: " + DateTime.Parse(table.Rows[0]["EndDate"].ToString());
                txtPollVisibility.InnerText = "Poll Visibility: " + table.Rows[0]["PollVisibility"].ToString();
                txtResultsVisibility.InnerText = "Results Visibility: " + table.Rows[0]["ResultsVisibility"].ToString();
                txtCreatedBy.InnerText = "Created By: " + table.Rows[0]["CreatedBy"].ToString();

                ViewState["UserEmail"] = table.Rows[0]["Email"].ToString();

                //Question and Answers
                txtQuestion.InnerText = table.Rows[0]["Question"].ToString();
                txtAnswerOption1.InnerText = "Answer Option 1: " + table.Rows[0]["AnswerOption1"].ToString();
                txtAnswerOption2.InnerText = "Answer Option 2: " + table.Rows[0]["AnswerOption2"].ToString();
                txtAnswerOption3.InnerText = table.Rows[0]["AnswerOption3"].ToString() == string.Empty ? "" : "Answer Option 3: " + table.Rows[0]["AnswerOption3"].ToString();
                txtAnswerOption4.InnerText = table.Rows[0]["AnswerOption4"].ToString() == string.Empty ? "" : "Answer Option 4: " + table.Rows[0]["AnswerOption4"].ToString();
                txtAnswerOption5.InnerText = table.Rows[0]["AnswerOption5"].ToString() == string.Empty ? "" : "Answer Option 5: " + table.Rows[0]["AnswerOption5"].ToString();
            }
        }

        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {

            PollBL obj = new PollBL();
            obj.PollId = int.Parse(Session["PollIdReview"].ToString());
            obj.PollStatus = "Live";
            obj.UpdatePollStatus();

            //Add Comment

            PollReviewCommentBL comment = new PollReviewCommentBL();
            comment.PollId = int.Parse(Session["PollIdReview"].ToString());
            comment.Comment = txtComments.Value.Trim();
            comment.UserId = int.Parse(Session["UserId"].ToString());
            comment.CreatePollReviewComment();

            //Send Email

            sendEmail("Approved");

            string scriptText = "alert('Poll approved.'); window.location='" + Request.ApplicationPath + "ReviewPoll.aspx'";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
        }

        protected void btnReject_ServerClick(object sender, EventArgs e)
        {

            if (txtComments.Value.Trim() != string.Empty)
            {

                PollBL obj = new PollBL();

                obj.PollId = int.Parse(Session["PollIdReview"].ToString());

                obj.PollStatus = "Draft";

                obj.UpdatePollStatus();

                //Add Comment

                PollReviewCommentBL comment = new PollReviewCommentBL();
                comment.PollId = int.Parse(Session["PollIdReview"].ToString());
                comment.Comment = txtComments.Value.Trim();
                comment.UserId = int.Parse(Session["UserId"].ToString());
                comment.CreatePollReviewComment();

                //Send Email

                sendEmail("Rejected");


                string scriptText = "alert('Poll rejected.'); window.location='" + Request.ApplicationPath + "ReviewPoll.aspx'";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter comments for rejecting this poll!')", true);
            }
        }

        private void sendEmail(string decision)
        {
            string subject = "BU Voting System - Poll Review Outcome";

            //Build msg
            string emailMsg = "<b>BU Voting System</b>";
            emailMsg += "<br><br>";
            emailMsg += "Your poll has been reviewed.";
            emailMsg += "<br><br>";
            emailMsg += "Poll Question: " + txtQuestion.InnerText;
            emailMsg += "<br><br>";
            emailMsg += "Decision: " + decision;
            emailMsg += "<br><br>";
            emailMsg += "Comments: " + txtComments.Value;
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

            MailHelper.SendMailMessage("no-reply@coralite.co.za", ViewState["UserEmail"].ToString(), "", "", subject, emailMsg);

        }
    }
}