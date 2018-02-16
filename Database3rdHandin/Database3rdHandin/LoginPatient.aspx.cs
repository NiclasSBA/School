using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace Database3rdHandin
{
    public partial class LoginPatient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["PatientName"] != null)
            {
                Master.ShowContent = true;
                Master.ShowLogout = true;
                
            }
            else
            {
                
                Master.ShowLogout = false;
            }

        }

        SqlConnection conn = new SqlConnection(@" data source= localhost\sqlexpress; integrated security= true; database=Dentist;");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        //string SqlString = "";



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
                cmd.CommandText = "CheckLoginPatient";

                SqlParameter in1 = cmd.Parameters.Add("@Email", SqlDbType.Text);
                in1.Direction = ParameterDirection.Input;
                in1.Value = TextEmail.Text;

                SqlParameter in2 = cmd.Parameters.Add("@Password", SqlDbType.Text);
                in2.Direction = ParameterDirection.Input;
                in2.Value = TextPass.Text;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    HttpContext.Current.Session["PatientName"] = in1.Value.ToString();
                    HttpContext.Current.Session["PatientId"] = rdr.GetValue(0).ToString();
                    string userlogged = HttpContext.Current.Session["PatientName"].ToString();



                    ErrorLabel.Text = "Login Sucess.." + Session["id"] + "...!!";
                    MessageLabel.Text = "Currently Logged User is" + (userlogged != null ? userlogged : "Unknown user");
                    Response.Redirect("~/Account/OnlyWhenLoggedIn.aspx");
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