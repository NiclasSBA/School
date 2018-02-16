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
    public partial class UserSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist");
            SqlCommand cmd = null;

            string sqlins = "INSERT INTO Patient values(@P_FirstName, @P_LastName, @P_Age, @P_CPR, @P_Email, @P_Password, @P_Gender)";


            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlins, conn);
                cmd.Parameters.Add("@P_FirstName", SqlDbType.Text);
                cmd.Parameters.Add("@P_LastName", SqlDbType.Text);
                cmd.Parameters.Add("@P_Age", SqlDbType.Int);
                cmd.Parameters.Add("@P_CPR", SqlDbType.Text);
                cmd.Parameters.Add("@P_Email", SqlDbType.Text);
                cmd.Parameters.Add("@P_Password", SqlDbType.Text);
                cmd.Parameters.Add("@P_Gender", SqlDbType.Text);


                cmd.Parameters["@P_FirstName"].Value = TextBoxFirstName.Text;
                cmd.Parameters["@P_LastName"].Value = TextBoxLastName.Text;
                cmd.Parameters["@P_Age"].Value = Int32.Parse(TextBoxAge.Text);
                cmd.Parameters["@P_CPR"].Value = TextBoxCPR.Text;
                cmd.Parameters["@P_Email"].Value = TextBoxEmail.Text;
                cmd.Parameters["@P_Password"].Value = TextBoxPassword.Text;
                cmd.Parameters["@P_Gender"].Value = TextBoxGender.Text;

                cmd.ExecuteNonQuery();

                LabelMessage.Text = "Added New user bro";
            }


            catch (Exception ex)
            {

                LabelMessage.Text = ex.Message;
            }

            finally
            {

                conn.Close();
            }



        }
    }
}