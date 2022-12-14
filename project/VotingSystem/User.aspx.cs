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
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private DataTable GetData()
        {

            UserBL obj = new UserBL();

            return obj.ReadUsers();

        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            GridView1.DataSource = GetData();  //GetData is your method that you will use to obtain the data you're populating the GridView with
            GridView1.DataBind();

            if (GridView1.Rows.Count == 0)
            {
                //Create DataTable
                DataTable temp = new DataTable();
                temp.Columns.Add("UserId");
                temp.Columns.Add("FirstName");
                temp.Columns.Add("LastName");
                temp.Columns.Add("Email");
                temp.Columns.Add("RoleName");
                temp.Columns.Add("UserStatus");

                DataRow tempRow = temp.NewRow();
                tempRow["UserId"] = "0";
                tempRow["FirstName"] = "There are currently no users.";
                tempRow["LastName"] = "";
                tempRow["Email"] = "";
                tempRow["RoleName"] = "";
                tempRow["UserStatus"] = "";

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

        protected void gvUser_Commands(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;

            LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button 
            GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row 
            GridView myGrid = (GridView)sender; // the gridview 
            string UserId = GridView1.DataKeys[myRow.RowIndex].Values[0].ToString();
            string Email = GridView1.DataKeys[myRow.RowIndex].Values[1].ToString();

            if (commandName == "Activate")
            {

                UserBL obj = new UserBL();
                obj.UserId = int.Parse(UserId);
                obj.UserStatusId = 2;
                obj.ActivateUser();

                string subject = "BU Voting System - Account Activation";

                string emailMsg = "<b>Welcome to the BU Voting System</b>";
                emailMsg += "<br><br>";
                emailMsg += "Your account has been activated. Please login to create your first poll.";
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

                //Send Email to user

                MailHelper.SendMailMessage("no-reply@coralite.co.za", Email, "", "", subject, emailMsg);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The user has been activated.')", true);

            }
            else if (commandName == "DeleteRow")
            {
                //PollBL obj = new PollBL();
                //obj.IsDeleted = true;
                //obj.PollId = int.Parse(PollId);
                //obj.DeletePoll();
                UserBL obj = new UserBL();
                obj.UserId = int.Parse(UserId);
                obj.IsDeleted = true;
                obj.DeleteUser();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The user has been deleted.')", true);

            }

        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text.Equals("Active"))
                {
                    e.Row.Cells[0].Text = "";
                }

                string UserId = GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();

                if (UserId == Session["UserId"].ToString())
                {
                    e.Row.Cells[1].Text = "";
                }
                
            }
        }

    }
}