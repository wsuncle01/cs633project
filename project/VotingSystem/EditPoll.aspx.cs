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
    public partial class EditPoll : System.Web.UI.Page
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
            obj.PollId = int.Parse(Session["PollId"].ToString());

            DataTable table = obj.ReadPoll();

            if (table.Rows.Count > 0)
            {
                ddlCategory.Value = table.Rows[0]["InterestId"].ToString();
                txtQuestion.Value = table.Rows[0]["Question"].ToString();
                ddlPollStatus.Value = table.Rows[0]["PollStatus"].ToString();
                txtDescription.Value = table.Rows[0]["Description"].ToString();
                txtAnswerOption1.Value = table.Rows[0]["AnswerOption1"].ToString();
                txtAnswerOption2.Value = table.Rows[0]["AnswerOption2"].ToString();
                txtAnswerOption3.Value = table.Rows[0]["AnswerOption3"].ToString();
                txtAnswerOption4.Value = table.Rows[0]["AnswerOption4"].ToString();
                txtAnswerOption5.Value = table.Rows[0]["AnswerOption5"].ToString();

                txtStartDate.Value = DateTime.Parse(table.Rows[0]["StartDate"].ToString()).ToString("HH:mm MM/dd/yyyy");
                txtEndDate.Value = DateTime.Parse(table.Rows[0]["EndDate"].ToString()).ToString("HH:mm MM/dd/yyyy");

                ddlPollVisibility.Value = table.Rows[0]["PollVisibility"].ToString();
                ddlResultsVisibility.Value = table.Rows[0]["ResultsVisibility"].ToString();
            }

        }

        private DataTable GetData()
        {

            PollReviewCommentBL obj = new PollReviewCommentBL();
            obj.PollId = int.Parse(Session["PollId"].ToString());

            return obj.ReadPollReviewComments();

        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            GridView1.DataSource = GetData();  //GetData is your method that you will use to obtain the data you're populating the GridView with
            GridView1.DataBind();

            if (GridView1.Rows.Count == 0)
            {
                //Create DataTable
                DataTable temp = new DataTable();
                temp.Columns.Add("PollReviewCommentId");
                temp.Columns.Add("Comment");

                DataRow tempRow = temp.NewRow();
                tempRow["PollReviewCommentId"] = "0";
                tempRow["Comment"] = "You currently have no comments.";

                temp.Rows.Add(tempRow);


                GridView1.DataSource = temp;
                GridView1.DataBind();

            }

            if (GridView1.Rows.Count > 0)
            {
                //Replace the <td> with <th> and adds the scope attribute
                GridView1.UseAccessibleHeader = true;

                //Adds the <thead> and <tbody> elements required for DataTables to work
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                //Adds the <tfoot> element required for DataTables to work
                GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
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

                obj.PollId = int.Parse(Session["PollId"].ToString());
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
            }

            if (ddlPollStatus.Value == "Draft")
            {
                obj.UpdatePoll(); //Update Poll

                //Msg with redirect
                string scriptText = "alert('Poll updated successfully.'); window.location='" + Request.ApplicationPath + "ViewPoll.aspx'";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);

            }
            else if (ddlPollStatus.Value == "Published")
            {
                obj.UpdatePoll(); //Update Poll

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
                //ddlCategory.SelectedIndex = 0;
                //txtQuestion.Value = string.Empty;
                //ddlPollStatus.SelectedIndex = 0;
                //txtDescription.Value = string.Empty;
                //txtAnswerOption1.Value = string.Empty;
                //txtAnswerOption2.Value = string.Empty;
                //txtAnswerOption3.Value = string.Empty;
                //txtAnswerOption4.Value = string.Empty;
                //txtAnswerOption5.Value = string.Empty;
                //txtStartDate.Value = string.Empty;
                //txtEndDate.Value = string.Empty;
                //ddlPollVisibility.SelectedIndex = 0;
                //ddlResultsVisibility.SelectedIndex = 0;

                //Msg with redirect
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Poll created successfully. Your poll is pending review and you will receive an email once it has been reviewed.')", true);
                string scriptText = "alert('Poll updated successfully. Your poll is pending review and you will receive an email once it has been reviewed.'); window.location='" + Request.ApplicationPath + "ViewPoll.aspx'";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);

            }
        }
    }
}