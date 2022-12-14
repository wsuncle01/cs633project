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

namespace VotingSystem
{
    public partial class SearchPoll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private DataTable GetData()
        {

            PollBL obj = new PollBL();
            obj.UserId = int.Parse(Session["UserId"].ToString());

            string interest = ddlCategory.Value == "Select Category:" ? "0" : ddlCategory.Value;
            string pollStatus = ddlPollStatus.Value == "Poll Status:" ? "%" : ddlPollStatus.Value;
            string resultsVisibility = ddlResultsVisibility.Value == "Results Visibility:" ? "%" : ddlResultsVisibility.Value;

            obj.InterestId = int.Parse(interest);
            obj.PollStatus = pollStatus;
            obj.ResultsVisibility = resultsVisibility;

            DataTable table = obj.ReadPollSearch();


            //1   Pollster
            //2   Students Part-Time
            //3   Students Full-Time
            //4   Faculty

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    if (table.Rows[i]["PollStatus"].ToString() == "Completed" && table.Rows[i]["ResultsVisibility"].ToString() == "Private")
                    {
                        table.Rows.RemoveAt(i);
                        continue;
                    }

                    string PollVisibility = table.Rows[i]["PollVisibility"].ToString();
                    if ((PollVisibility == "Students" || PollVisibility == "Students Part-Time" || PollVisibility == "Students Full-Time") && Session["RoleId"].ToString() == "4")
                    {
                        table.Rows.RemoveAt(i);
                    }
                    else if (PollVisibility == "Faculty" && (Session["RoleId"].ToString() == "2" || Session["RoleId"].ToString() == "3"))
                    {
                        table.Rows.RemoveAt(i);
                    }
                    else if (PollVisibility == "Students Part-Time" && Session["RoleId"].ToString() == "3")
                    {
                        table.Rows.RemoveAt(i);
                    }
                    else if (PollVisibility == "Students Full-Time" && Session["RoleId"].ToString() == "2")
                    {
                        table.Rows.RemoveAt(i);
                    }
                }
            }

            return table;

        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            GridView1.DataSource = GetData();  //GetData is your method that you will use to obtain the data you're populating the GridView with
            GridView1.DataBind();

            if (GridView1.Rows.Count == 0)
            {
                //Create DataTable
                DataTable temp = new DataTable();
                temp.Columns.Add("PollId");
                temp.Columns.Add("PollStatus");
                temp.Columns.Add("Category");
                temp.Columns.Add("Question");
                temp.Columns.Add("StartDate");
                temp.Columns.Add("EndDate");
                temp.Columns.Add("PollVisibility");
                temp.Columns.Add("ResultsVisibility");
                temp.Columns.Add("Voted");

                DataRow tempRow = temp.NewRow();
                tempRow["PollId"] = "0";
                tempRow["PollStatus"] = "No results match your search criteria.";
                tempRow["Category"] = "";
                tempRow["Question"] = "";
                tempRow["StartDate"] = "";
                tempRow["EndDate"] = "";
                tempRow["PollVisibility"] = "";
                tempRow["ResultsVisibility"] = "";
                tempRow["Voted"] = "";

                temp.Rows.Add(tempRow);


                GridView1.DataSource = temp;
                GridView1.DataBind();

                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = false;

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

        protected void gvSearch_Commands(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;

            LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button 
            GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row 
            GridView myGrid = (GridView)sender; // the gridview 
            string PollId = GridView1.DataKeys[myRow.RowIndex].Values[0].ToString();

            if (commandName == "Vote")
            {
                Session["PollId"] = PollId;
                string strURL = "VotePoll.aspx";
                if (HttpContext.Current != null)
                {
                    //EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
                    //string strURLWithData = strURL +
                    //  obj.Encrypt(string.Format("CaseLogId={0}&CaseId={1}&StatusId={2}", CaseLogId, CaseId, StatusId));

                    HttpContext.Current.Response.Redirect(strURL);
                }
            }
            else if (commandName == "ViewResults")
            {
                //
                string strURL = "PollResults.aspx";
                if (HttpContext.Current != null)
                {
                    Session["PollId"] = PollId;
                    //EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
                    //string strURLWithData = strURL +
                    //  obj.Encrypt(string.Format("CaseLogId={0}&CaseId={1}&StatusId={2}", CaseLogId, CaseId, StatusId));

                    HttpContext.Current.Response.Redirect(strURL);
                }
            }

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell statusCell = e.Row.Cells[9];
                if (statusCell.Text == "1")
                {
                    statusCell.Text = "Yes";

                    statusCell = e.Row.Cells[0];
                    statusCell.Text = "";
                }
                else if (statusCell.Text == "0")
                {
                    statusCell.Text = "No";
                }

                statusCell = e.Row.Cells[2];
                if (statusCell.Text == "Live")
                {
                    statusCell = e.Row.Cells[1];
                    statusCell.Text = "";
                }

                statusCell = e.Row.Cells[2];
                if (statusCell.Text == "Completed")
                {
                    statusCell = e.Row.Cells[0];
                    statusCell.Text = "";
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            ddlCategory.SelectedIndex = 0;
            ddlPollStatus.SelectedIndex = 0;
            ddlResultsVisibility.SelectedIndex = 0;
        }
    }
}