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
    public partial class VotePoll : System.Web.UI.Page
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
            obj.PollId = Session["PollId"] == null ? 0 : int.Parse(Session["PollId"].ToString());

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
                TimeSpan difference = DateTime.Parse(table.Rows[0]["EndDate"].ToString()) - DateTime.Now;
                txtTimeRemaining.InnerText = "Time Remaining: " + difference.Days + " days, " + difference.Hours + " hours, " + difference.Minutes + " minutes";

                //Question and Answers
                txtQuestion.InnerText = table.Rows[0]["Question"].ToString();
                //txtAnswerOption1.InnerText = "Answer Option 1: " + table.Rows[0]["AnswerOption1"].ToString();
                //txtAnswerOption2.InnerText = "Answer Option 2: " + table.Rows[0]["AnswerOption2"].ToString();
                //txtAnswerOption3.InnerText = table.Rows[0]["AnswerOption3"].ToString() == string.Empty ? "" : "Answer Option 3: " + table.Rows[0]["AnswerOption3"].ToString();
                //txtAnswerOption4.InnerText = table.Rows[0]["AnswerOption4"].ToString() == string.Empty ? "" : "Answer Option 4: " + table.Rows[0]["AnswerOption4"].ToString();
                //txtAnswerOption5.InnerText = table.Rows[0]["AnswerOption5"].ToString() == string.Empty ? "" : "Answer Option 5: " + table.Rows[0]["AnswerOption5"].ToString();

                ListItem item = new ListItem();
                item.Text = table.Rows[0]["AnswerOption1"].ToString();
                item.Value = "1";
                rblAnswerOptions.Items.Add(item);

                item = new ListItem();
                item.Text = table.Rows[0]["AnswerOption2"].ToString();
                item.Value = "2";
                rblAnswerOptions.Items.Add(item);

                if (table.Rows[0]["AnswerOption3"].ToString() != string.Empty)
                {
                    item = new ListItem();
                    item.Text = table.Rows[0]["AnswerOption3"].ToString();
                    item.Value = "3";
                    rblAnswerOptions.Items.Add(item);
                }

                if (table.Rows[0]["AnswerOption4"].ToString() != string.Empty)
                {
                    item = new ListItem();
                    item.Text = table.Rows[0]["AnswerOption4"].ToString();
                    item.Value = "4";
                    rblAnswerOptions.Items.Add(item);
                }

                if (table.Rows[0]["AnswerOption5"].ToString() != string.Empty)
                {
                    item = new ListItem();
                    item.Text = table.Rows[0]["AnswerOption5"].ToString();
                    item.Value = "5";
                    rblAnswerOptions.Items.Add(item);
                }

                rblAnswerOptions.DataBind();

            }
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {

            VoteBL obj = new VoteBL();
            obj.PollId = int.Parse(Session["PollId"].ToString());
            if (rblAnswerOptions.SelectedValue.ToString() == "1")
                obj.AnswerOption1 = true;
            else if (rblAnswerOptions.SelectedValue.ToString() == "2")
                obj.AnswerOption2 = true;
            else if (rblAnswerOptions.SelectedValue.ToString() == "3")
                obj.AnswerOption3 = true;
            else if (rblAnswerOptions.SelectedValue.ToString() == "4")
                obj.AnswerOption4 = true;
            else if (rblAnswerOptions.SelectedValue.ToString() == "5")
                obj.AnswerOption5 = true;

            obj.UserId = int.Parse(Session["UserId"].ToString());

            obj.CreateVote();

            string scriptText = "alert('Thank you for voting.'); window.location='" + Request.ApplicationPath + "Dashboard.aspx'";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
        }
    }
}