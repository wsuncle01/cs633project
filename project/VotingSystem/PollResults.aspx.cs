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
using DotNet.Highcharts;
using DotNet.Highcharts.Attributes;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace VotingSystem
{
    public partial class PollResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTotals();
            }
        }

        private void LoadPieChart(int AnswerOption1, int AnswerOption2, int AnswerOption3, int AnswerOption4, int AnswerOption5)
        {

            DataTable pollData = GetData();

            string Question = "";
            string AnswerOptionStr1 = "";
            string AnswerOptionStr2 = "";
            string AnswerOptionStr3 = "";
            string AnswerOptionStr4 = "";
            string AnswerOptionStr5 = "";

            if (pollData.Rows.Count > 0)
            {
                Question = pollData.Rows[0]["Question"].ToString();
                AnswerOptionStr1 = pollData.Rows[0]["AnswerOption1"].ToString();
                AnswerOptionStr2 = pollData.Rows[0]["AnswerOption2"].ToString();
                AnswerOptionStr3 = pollData.Rows[0]["AnswerOption3"].ToString();
                AnswerOptionStr4 = pollData.Rows[0]["AnswerOption4"].ToString();
                AnswerOptionStr5 = pollData.Rows[0]["AnswerOption5"].ToString();
            }

            int totalAnswers = 2;

            if (AnswerOptionStr3 != string.Empty)
                totalAnswers++;

            if (AnswerOptionStr4 != string.Empty)
                totalAnswers++;

            if (AnswerOptionStr5 != string.Empty)
                totalAnswers++;


            object[] points = new object[totalAnswers];

            Point answer1 = new Point();
            answer1.Name = AnswerOptionStr1;
            answer1.Y = AnswerOption1;
            answer1.Color = System.Drawing.Color.Green;
            answer1.Sliced = true;
            answer1.Selected = true;

            points[0] = answer1;

            Point answer2 = new Point();
            answer2.Name = AnswerOptionStr2;
            answer2.Y = AnswerOption2;
            answer2.Color = System.Drawing.Color.Gold;

            points[1] = answer2;

            if (AnswerOptionStr3 != string.Empty)
            {
                Point answer3 = new Point();
                answer3.Name = AnswerOptionStr3;
                answer3.Y = AnswerOption3;
                answer3.Color = System.Drawing.Color.Red;
                points[2] = answer3;
            }

            if (AnswerOptionStr4 != string.Empty)
            {
                Point answer4 = new Point();
                answer4.Name = AnswerOptionStr4;
                answer4.Y = AnswerOption4;
                answer4.Color = System.Drawing.Color.Blue;
                points[3] = answer4;
            }

            if (AnswerOptionStr5 != string.Empty)
            {
                Point answer5 = new Point();
                answer5.Name = AnswerOptionStr5;
                answer5.Y = AnswerOption5;
                answer5.Color = System.Drawing.Color.Purple;
                points[4] = answer5;
            }





            double total = 0;

            Credits cred = new Credits();
            cred.Enabled = false;

            Highcharts chartCases = new Highcharts("chartCases")
            .SetCredits(cred)
            .InitChart(new Chart { PlotShadow = false })
                //.SetTitle(new Title { Text = "Cases for " + DateTime.Now.Year })
                .SetTitle(new Title { Text = Question })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ Math.round(this.percentage) +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            //Color = ColorTranslator.FromHtml("#000000"),
                            //ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ Math.round(this.percentage) +' %'; }"
                        },
                        ShowInLegend = true
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = "Vote",
                    Data = new Data(points)
                    //Data = new Data(new object[]
                    //{
                    //    new Point
                    //    {
                    //        Name = "Cases Closed",
                    //        Y = 50,
                    //        Color = System.Drawing.Color.Green
                    //    },
                    //    new Point
                    //    {
                    //        Name = "Cases In Progress",
                    //        Y = 50,
                    //        Color = System.Drawing.Color.Gold
                    //    },
                    //    new Point
                    //    {
                    //        Name = "Department Response Required",
                    //        Y = 100,
                    //        Sliced = true,
                    //        Selected = true,
                    //        Color = System.Drawing.Color.Red
                    //    },
                    //    new Point
                    //    {
                    //        Name = "Customer Response Required",
                    //        Y = 100,
                    //        Color = System.Drawing.Color.Blue
                    //    }
                    //})
                });

            ltrChartCases.Text = chartCases.ToHtmlString();
        }

        private void LoadTotals()
        {
            VoteBL obj = new VoteBL();
            obj.PollId = int.Parse(Session["PollId"].ToString());
            DataTable votes = obj.ReadVotes();

            int AnswerOption1 = 0;
            int AnswerOption2 = 0;
            int AnswerOption3 = 0;
            int AnswerOption4 = 0;
            int AnswerOption5 = 0;

            if (votes.Rows.Count > 0)
            {
                for (int i = 0; i < votes.Rows.Count; i ++)
                {
                    if (votes.Rows[i]["AnswerOption1"].ToString() == "True")
                        AnswerOption1++;

                    if (votes.Rows[i]["AnswerOption2"].ToString() == "True")
                        AnswerOption2++;

                    if (votes.Rows[i]["AnswerOption3"].ToString() == "True")
                        AnswerOption3++;

                    if (votes.Rows[i]["AnswerOption4"].ToString() == "True")
                        AnswerOption4++;

                    if (votes.Rows[i]["AnswerOption5"].ToString() == "True")
                        AnswerOption5++;

                }
            }

            DataTable pollData = GetData();

            if (pollData.Rows.Count > 0)
            {
                txtAnswerOption1.InnerText = pollData.Rows[0]["AnswerOption1"].ToString() + " - Total Votes: " + AnswerOption1;
                txtAnswerOption2.InnerText = pollData.Rows[0]["AnswerOption2"].ToString() + " - Total Votes: " + AnswerOption2;

                if (pollData.Rows[0]["AnswerOption3"].ToString() != string.Empty)
                {
                    txtAnswerOption3.InnerText = pollData.Rows[0]["AnswerOption3"].ToString() + " - Total Votes: " + AnswerOption3;
                }

                if (pollData.Rows[0]["AnswerOption4"].ToString() != string.Empty)
                {
                    txtAnswerOption4.InnerText = pollData.Rows[0]["AnswerOption4"].ToString() + " - Total Votes: " + AnswerOption4;
                }

                if (pollData.Rows[0]["AnswerOption5"].ToString() != string.Empty)
                {
                    txtAnswerOption5.InnerText = pollData.Rows[0]["AnswerOption5"].ToString() + " - Total Votes: " + AnswerOption5;
                }
            }

            LoadPieChart(AnswerOption1, AnswerOption2, AnswerOption3, AnswerOption4, AnswerOption5);

        }

        private DataTable GetData()
        {

            PollBL obj = new PollBL();
            obj.PollId = int.Parse(Session["PollId"].ToString());

            return obj.ReadPoll();

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
                //temp.Columns.Add("CreatedBy");

                DataRow tempRow = temp.NewRow();
                tempRow["PollId"] = "0";
                tempRow["PollStatus"] = "You currently have no polls.";
                tempRow["Category"] = "";
                tempRow["Question"] = "";
                tempRow["StartDate"] = "";
                tempRow["EndDate"] = "";
                tempRow["PollVisibility"] = "";
                tempRow["ResultsVisibility"] = "";
                //tempRow["CreatedBy"] = "";

                temp.Rows.Add(tempRow);


                GridView1.DataSource = temp;
                GridView1.DataBind();

                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = false;
                GridView1.Columns[2].Visible = false;

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

    }
}