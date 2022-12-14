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
    public partial class ReviewPoll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        private DataTable GetData()
        {

            PollBL obj = new PollBL();
            obj.PollStatus = "Published";

            return obj.ReadPolls();

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
                temp.Columns.Add("CreatedBy");

                DataRow tempRow = temp.NewRow();
                tempRow["PollId"] = "0";
                tempRow["PollStatus"] = "You currently have no polls to review.";
                tempRow["Category"] = "";
                tempRow["Question"] = "";
                tempRow["StartDate"] = "";
                tempRow["EndDate"] = "";
                tempRow["PollVisibility"] = "";
                tempRow["ResultsVisibility"] = "";
                tempRow["CreatedBy"] = "";

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

        protected void gvReviewInbox_Commands(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;

            LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button 
            GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row 
            GridView myGrid = (GridView)sender; // the gridview 
            string PollId = GridView1.DataKeys[myRow.RowIndex].Values[0].ToString();

            if (commandName == "View")
            {
                Session["PollIdReview"] = PollId;
                string strURL = "ReviewPollPage.aspx";
                if (HttpContext.Current != null)
                {
                    //EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
                    //string strURLWithData = strURL +
                    //  obj.Encrypt(string.Format("CaseLogId={0}&CaseId={1}&StatusId={2}", CaseLogId, CaseId, StatusId));

                    HttpContext.Current.Response.Redirect(strURL);
                }
            }
            else if (commandName == "Edit")
            {
                //string strURL = "CaptureConsentForm.aspx?";
                //if (HttpContext.Current != null)
                //{
                //    EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
                //    string strURLWithData = strURL +
                //      obj.Encrypt(string.Format("ApplicationId={0}", ApplicationId));

                //    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + strURLWithData + "');", true);
                //    HttpContext.Current.Response.Redirect(strURLWithData);
                //}
            }

        }


    }
}