using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace Database3rdHandin
{
    public partial class Login : System.Web.UI.Page
    {

        DataSet ds;
        DataTable dt;
        SqlConnection conn = new SqlConnection(@" data source= localhost\sqlexpress; integrated security= true; database=Dentist;");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        //string SqlString = "";

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["DentistName"] != null)
            {
                Master.ShowContent = true;
                Master.ShowLogout = true;
                
            }
            else
            {
                
                Master.ShowLogout = false;
            }


                UpdatePage();

            
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));
                dt = ds.Tables["Sponsor"];

            
        }



        public void UpdatePage()
        {
           
            try
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/XMLFile/XMLSponsors.xml"));
                dt = ds.Tables["Sponsor"];
           
            }
            catch
            {
                // The XML file does not excist or is empty; dt made only for display of repeater heading
                MakeNewDataSetAndDataTable();
            }
            finally
            {

                RepeaterSponsorsFooter.DataSource = dt;
                RepeaterSponsorsFooter.DataBind();
            }

            

            //DropDownList.Items.Insert(0, "You can choose");

            /*
            TextBoxID.Text = "";
            TextBoxName.Text = "";
            TextBoxBreed.Text = "";
            TextBoxPicture.Text = "";
            ButtonCreate.Enabled = true;
            ButtonUpdate.Enabled = false;
            ButtonDelete.Enabled = false;*/
        }

        public void MakeNewDataSetAndDataTable()
        {
            ds = new DataSet("Sponsors");
            dt = ds.Tables.Add("Sponsor");
            dt.Columns.Add("SponsorID", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Website", typeof(string));
            dt.Columns.Add("Picture", typeof(string));
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            /*
            string Uname = TextName.Text;
            string Pass = TextPass.Text;
            SqlString = "select * from Patient where P_FirstName='" + Uname + "' and P_Password='" + Pass + "'";
            cmd = new SqlCommand(SqlString, conn);
            rdr = cmd.ExecuteReader();
            */
            try
            {
                //open connection. this establishes a session with the database
               
                ErrorLabel.Text = "No error message";
                

                conn.Open();

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CheckLogin";

                SqlParameter in1 = cmd.Parameters.Add("@Email", SqlDbType.Text);
                in1.Direction = ParameterDirection.Input;
                in1.Value = TextEmail.Text;

                SqlParameter in2 = cmd.Parameters.Add("@Password", SqlDbType.Text);
                in2.Direction = ParameterDirection.Input;
                in2.Value = TextPass.Text;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    Session["DentistName"] = in1.Value.ToString();
                    Session["id"] = rdr.GetValue(0).ToString();
                    string userlogged = HttpContext.Current.Session["DentistName"].ToString();
                    
                   
                   
                    ErrorLabel.Text = "Login Sucess.." + Session["id"] + "...!!";
                    MessageLabel.Text = "Currently Logged User is" + (userlogged!=null?  userlogged: "Unknown user");
                    Response.Redirect("Treatments.aspx");
                }
                else
                {
                   ErrorLabel.Text = "UserId & Password Is not correct Try again..!!";

                }


            }
            catch (Exception ex)
            {
                //Display error
                ErrorLabel.Text = "Error: " + ex.Message + ex.StackTrace;

            }
            finally
            {
                conn.Close();

            }
        }
    }
}