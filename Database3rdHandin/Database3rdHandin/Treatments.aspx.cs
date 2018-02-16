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
    
    public partial class Treatments : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@" data source= localhost\sqlexpress; integrated security= true; database=Dentist;");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string SqlString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ButtonDelete.Enabled = false;
                ButtonUpdate.Enabled = false;
                UpdateGridview();


               
            }
            if (IsPostBack)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MOVEHERE';", true);
            }

            DropDownListTreatments.AutoPostBack = true;
            TreatmentRepeater.DataBind();
            ShowData();

            if (Session["DentistName"] != null)
            {

                LabelUser.Text = Session["DentistName"].ToString();
                Master.ShowContent = true;
                Master.ShowLogout = true;

            }
            else
            {
                Master.ShowContent = false;
                Master.ShowLogout = false;
            }
        }





        public void ShowData()
        {

            try
            {
                //open connection. this establishes a session with the database
                conn.Open();
                ErrorLabel.Text = "";
                SqlString = "Select  * from Treatments";
                cmd = new SqlCommand(SqlString, conn);
                rdr = cmd.ExecuteReader();


                TreatmentRepeater.DataSource = rdr;
                TreatmentRepeater.DataBind();
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

      
        public void UpdateGridview()
        {
            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;

            string sqlsel = "select * from treatments";

            try
            {
                //conn.Open();   SqlDataAdapter opens the connextion itself

                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlsel, conn);

                ds = new DataSet();

                da.Fill(ds, "MyTreatments");
                /*
                dt.Columns.Add("SponsorID", typeof(Int32));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Website", typeof(string));
                dt.Columns.Add("Picture", typeof(string));*/
                dt = ds.Tables["MyTreatments"];
                
                

                /*
                ds = new DataSet("Sponsors");
            dt = ds.Tables.Add("Sponsor");

                dt.Columns.Add("SponsorID", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Website", typeof(string));
            dt.Columns.Add("Picture", typeof(string));
        }
                 */
                TreatmentRepeater.DataSource = dt;

              
                TreatmentRepeater.DataBind();
                
                DropDownListTreatments.DataSource = dt;
                DropDownListTreatments.DataTextField = "T_Name";
                DropDownListTreatments.DataValueField = "T_Id";
                DropDownListTreatments.DataBind();
                DropDownListTreatments.Items.Insert(0, "Select a Treatment");
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();   // SqlDataAdapter closes connextion by itself; but can fail in case of errors
            }
        }


        protected void DropDownListTreatments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DropDownListTreatments.SelectedValue) != 0)
            {
                LabelMessage.Text = "You chose TreatmentID " + DropDownListTreatments.SelectedValue;
                  try
                {
                    conn.Open();

                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MySelectTreatment";

                    SqlParameter in1 = cmd.Parameters.Add("@Id", SqlDbType.Int);
                    in1.Direction = ParameterDirection.Input;
                    in1.Value = Convert.ToInt32(DropDownListTreatments.SelectedValue.ToString());

                    
                    rdr = cmd.ExecuteReader();
                    rdr.Read();
                    TextBoxName.Text = rdr.GetString(1);
                    TextBoxPrice.Text = rdr.GetValue(2).ToString();
                    TextBoxDesc.Text = rdr.GetString(3);
                    TextBoxImage.Text = rdr.GetString(4);
                    
                    rdr.Close(); //Close before getting output parameter
                    LabelMessage.Text = "You have chosen Treatment ID: " + DropDownListTreatments.SelectedValue.ToString();
                    ButtonUpdate.Enabled = true;

                }
                catch (Exception ex)
                {
                    LabelMessage.Text = ex.Message + ex.StackTrace;
                }
                finally{
                    conn.Close();
                   
                }
                ButtonDelete.Enabled = true;
            }
            else
            {
                LabelMessage.Text = "You chose none";
                ButtonDelete.Enabled = false;
            }



        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist");
            SqlDataAdapter da = null;
            SqlCommandBuilder cb = null;
            DataSet ds = null;
            DataTable dt = null;
            string sqlsel = "select * from Treatments";

            try
            {
                //conn.Open();
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlsel, conn);

                cb = new SqlCommandBuilder(da);

                ds = new DataSet();
                da.Fill(ds, "MyTreatments");

                dt = ds.Tables["MyTreatments"];

                foreach (DataRow row in dt.Select("T_Id = " + Convert.ToInt32(DropDownListTreatments.SelectedValue)))
                {
                    row.Delete();
                }

                da.Update(ds, "MyTreatments");

                ButtonDelete.Enabled = false;
                LabelMessage.Text = "Treatment " + DropDownListTreatments.SelectedValue + " has been deleted";
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            UpdateGridview();

        }


        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist");
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateTreatments";
                
                SqlParameter in1 = cmd.Parameters.Add("@Id", SqlDbType.Int);
                SqlParameter in2 = cmd.Parameters.Add("@Name", SqlDbType.Text);
                SqlParameter in3 = cmd.Parameters.Add("@Price", SqlDbType.Int);
                SqlParameter in4 = cmd.Parameters.Add("@Desc", SqlDbType.Text);
                SqlParameter in5 = cmd.Parameters.Add("@Image", SqlDbType.Text);

                in1.Direction = ParameterDirection.Input;
                in1.Value = Convert.ToInt32(DropDownListTreatments.SelectedValue.ToString()); 

                in2.Direction = ParameterDirection.Input;
                in2.Value = TextBoxName.Text;
                in3.Direction = ParameterDirection.Input;
                in3.Value = TextBoxPrice.Text;

                in4.Direction = ParameterDirection.Input;
                in4.Value = TextBoxDesc.Text;

                in4.Direction = ParameterDirection.Input;
                in4.Value = TextBoxDesc.Text;

                in5.Direction = ParameterDirection.Input;
                in5.Value = TextBoxImage.Text;
                cmd.ExecuteNonQuery();

                LabelMessage.Text = "Treatment has been updated";
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message + ex.StackTrace;
            }
            finally
            {
                conn.Close();
            }

            UpdateGridview();
        }
    }
}