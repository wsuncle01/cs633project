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
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTotals();
            }
        }

        private void LoadTotals()
        {
            PollBL poll = new PollBL();
            poll.PollStatus = "Live";
            DataTable table = poll.ReadPolls();

            int runningTotal = table.Rows.Count;

            LivePolls.InnerText = table.Rows.Count.ToString();

            //Completed

            poll.PollStatus = "Completed";
            table = poll.ReadPolls();

            runningTotal += table.Rows.Count;

            CompletedPolls.InnerText = table.Rows.Count.ToString();

            //Total Polls

            TotalPolls.InnerText = runningTotal.ToString();

            //Total Votes

            VoteBL vote = new VoteBL();
            TotalVotes.InnerText = vote.ReadVotes().Rows.Count.ToString();

        }

        private DataTable GetData()
        {

            PollBL obj = new PollBL();
            obj.UserId = int.Parse(Session["UserId"].ToString());

            DataTable table = obj.ReadPollDashboard();


            //1   Pollster
            //2   Students Part-Time
            //3   Students Full-Time
            //4   Faculty

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
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
                temp.Columns.Add("Category");
                temp.Columns.Add("Question");
                temp.Columns.Add("StartDate");
                temp.Columns.Add("EndDate");
                temp.Columns.Add("PollVisibility");
                temp.Columns.Add("ResultsVisibility");
                temp.Columns.Add("Voted");

                DataRow tempRow = temp.NewRow();
                tempRow["PollId"] = "0";
                tempRow["Category"] = "There are currently no live polls for you.";
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

        protected void gvDashboardInbox_Commands(object sender, GridViewCommandEventArgs e)
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

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell statusCell = e.Row.Cells[7];
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
            }
        }

    }
}