using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VotingSystem.BusinessLayer;

namespace VotingSystem
{
    public partial class AccountActivation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strReq = "";
                strReq = Request.RawUrl;
                strReq = strReq.Substring(strReq.IndexOf('?') + 1);

                if (!strReq.Equals(""))
                {
                    strReq = DecryptQueryString(strReq);

                    if (strReq != "The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or a non-white space character among the padding characters. ")
                    {

                        if (strReq.Length > 0)
                        {
                            //Parse the value...
                            string[] arrMsgs = strReq.Split('&');
                            string[] arrIndMsg;
                            string UserId = "";
                            arrIndMsg = arrMsgs[0].Split('='); //Get the Deadline OfficialID
                            UserId = arrIndMsg[1].ToString().Trim();

                            UserBL obj = new UserBL();
                            obj.UserId = int.Parse(UserId);
                            obj.UserStatusId = 2;
                            obj.ActivateUser();

                        }
                    }
                }
            }
        }

        private string DecryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Decrypt(strQueryString);
        }
    }
}