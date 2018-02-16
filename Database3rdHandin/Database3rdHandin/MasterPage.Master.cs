using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data;

namespace Database3rdHandin
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {


        private bool showContent = true;
        private bool showLogout = true;
        public bool ShowContent { get { return showContent; } set { showContent = value; } }
        public bool ShowLogout { get { return showLogout; }set { showLogout = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            MainPageContent.Visible = showContent;
            ButtonLogout.Visible = showLogout;

            
            

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Treatments.aspx");
        }

    }
}