using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
namespace Database3rdHandin.Account
{
    public partial class OnlyWhenLoggedIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["PatientName"] != null)
            {
                Master.ShowContent = true;
                Master.ShowLogout = true;
                UpdateGridview();
            } else if (Session["DentistName"] != null)
            {
                Master.ShowContent = true;
                Master.ShowLogout = true;

                if (!Page.IsPostBack)
                {
                    DropDownListPatients.AutoPostBack = true;
                    DropDownListDays.AutoPostBack = true;
                    UpdateGridviewDentist();
                }
            }
            else
            {
                Master.ShowContent = false;
                Master.ShowLogout = false;
            }

            
        }

        public void UpdateGridview()
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist");
            SqlCommand cmd = null;
            SqlDataReader rdr = null;


            try
            {
                conn.Open();

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllRes";

                SqlParameter in1 = cmd.Parameters.Add("@P_id", SqlDbType.Int);
                in1.Direction = ParameterDirection.Input;
                in1.Value = Convert.ToInt32(Session["PatientId"].ToString());

                rdr = cmd.ExecuteReader();

                GridViewRes.DataSource = rdr;
                GridViewRes.DataBind();

                DropDownListPatients.Visible = false;
                DropDownListDays.Visible = false;
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

        public void UpdateGridviewDentist()
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist");
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
           


            try
            {
                conn.Open();

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ShowDentistAll";

                rdr = cmd.ExecuteReader();

                GridViewRes.DataSource = rdr;
                GridViewRes.DataBind();

                


               
            }
            catch (Exception ex)
            {

                LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;

            string sqlsel = "select * from Patient";

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlsel, conn);

                ds = new DataSet();
                da.Fill(ds, "MyTreatments");

                dt = ds.Tables["MyTreatments"];
                

                DropDownListPatients.DataSource = dt;
                DropDownListPatients.DataTextField = "P_Id";
                DropDownListPatients.DataValueField = "P_Id";
                DropDownListPatients.DataBind();

                DropDownListPatients.Items.Insert(0, "Select a Patient");

            } catch(Exception ex) {
                LabelMessage.Text = ex.Message;
            } finally {
                conn.Close();
            }

           

            string sqlselday = "select * from Reservations";

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlselday, conn);

                ds = new DataSet();
                da.Fill(ds, "MyRes");

                dt = ds.Tables["MyRes"];

                DropDownListDays.DataSource = dt;
                DropDownListDays.DataTextField = "R_Date";
                DropDownListDays.DataValueField = "R_Date";
                DropDownListDays.DataBind();

                DropDownListDays.Items.Insert(0, "Select a Day");
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

        protected void GridViewRes_SelectedIndexChanged(object sender, EventArgs e)
        {

            LabelMessage.Text = GridViewRes.SelectedRow.Cells[1].Text;
            TextBoxDate.Text = GridViewRes.SelectedRow.Cells[2].Text;
            TextBoxP_id.Text = GridViewRes.SelectedRow.Cells[4].Text;
            TextBoxD_id.Text = GridViewRes.SelectedRow.Cells[5].Text;
            TextBoxT_id.Text = GridViewRes.SelectedRow.Cells[6].Text;
                ButtonUpdate.Enabled = true;

            if (!string.IsNullOrEmpty(TextBoxDate.Text))
            {

                string Value = TextBoxDate.Text;
                Value = Value.Replace('-', '/');
                TextBoxDate.Text = Value.ToString();
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");
                

            }





        }

        protected void GridViewPerson_SelectedIndexChanged(object sender, EventArgs e)
        {

            LabelMessage.Text = GridViewPerson.SelectedRow.Cells[1].Text;
            TextBoxDate.Text = GridViewPerson.SelectedRow.Cells[2].Text;
            TextBoxP_id.Text = GridViewPerson.SelectedRow.Cells[4].Text;
            TextBoxD_id.Text = GridViewPerson.SelectedRow.Cells[5].Text;
            TextBoxT_id.Text = GridViewPerson.SelectedRow.Cells[6].Text;
            ButtonUpdate.Enabled = true;

            if (!string.IsNullOrEmpty(TextBoxDate.Text))
            {

                string Value = TextBoxDate.Text;
                Value = Value.Replace('-', '/');
                TextBoxDate.Text = Value.ToString();
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");


            }





        }

        protected void GridViewDays_SelectedIndexChanged(object sender, EventArgs e)
        {

            LabelMessage.Text = GridViewDays.SelectedRow.Cells[1].Text;
            TextBoxDate.Text = GridViewDays.SelectedRow.Cells[2].Text;
            TextBoxP_id.Text = GridViewDays.SelectedRow.Cells[4].Text;
            TextBoxD_id.Text = GridViewDays.SelectedRow.Cells[5].Text;
            TextBoxT_id.Text = GridViewDays.SelectedRow.Cells[6].Text;
            ButtonUpdate.Enabled = true;
            ButtonDelete.Enabled = true;
            if (!string.IsNullOrEmpty(TextBoxDate.Text))
            {

                string Value = TextBoxDate.Text;
                Value = Value.Replace('-', '/');
                TextBoxDate.Text = Value.ToString();
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");


            }

        }
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        protected void DropDownListPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewPerson.Visible = true;


            if (Convert.ToInt32(DropDownListPatients.SelectedValue) != 0)
            {
                GridViewRes.Visible = false;
                GridViewDays.Visible = false;
                

                LabelMessage.Text = DropDownListPatients.SelectedValue.ToString();

                try
                {
                    conn.Open();

                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SelectAllRes";

                    SqlParameter in1 = cmd.Parameters.Add("@P_id", SqlDbType.Int);
                    in1.Direction = ParameterDirection.Input;
                    in1.Value = Convert.ToInt32(DropDownListPatients.SelectedValue.ToString());



                    rdr = cmd.ExecuteReader();

                    GridViewPerson.DataSource = rdr;
                    GridViewPerson.DataBind();


                }
                catch (Exception ex)
                {

                    LabelMessage.Text = ex.Message + ex.StackTrace;
                }
                finally
                {
                    conn.Close();
                }


            }
        }


        protected void DropDownListDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (Convert.ToInt32(DropDownListDays.SelectedIndex) != 0)
            {
                GridViewRes.Visible = false;
                GridViewPerson.Visible = false;
                GridViewDays.Visible = true;
                LabelMessage.Text = TextBoxP_id.Text;

                try
                {
                    conn.Open();

                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SelectResByDay";

                    SqlParameter in1 = cmd.Parameters.Add("@Date", SqlDbType.DateTime);
                    in1.Direction = ParameterDirection.Input;
                    in1.Value = Convert.ToDateTime(DropDownListDays.SelectedValue.ToString());



                    rdr = cmd.ExecuteReader();

                    GridViewDays.DataSource = rdr;
                    GridViewDays.DataBind();


                }
                catch (Exception ex)
                {

                    LabelMessage.Text = ex.Message + ex.StackTrace;
                }
                finally
                {
                    conn.Close();
                }


            }
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
                cmd.CommandText = "UpdateReservation";

               

                SqlParameter in1 = cmd.Parameters.Add("@Date", SqlDbType.DateTime);
                SqlParameter in2 = cmd.Parameters.Add("@P_Id", SqlDbType.Int);
                SqlParameter in3 = cmd.Parameters.Add("@D_Id", SqlDbType.Int);
                SqlParameter in4 = cmd.Parameters.Add("@T_Id", SqlDbType.Int);
                SqlParameter in5 = cmd.Parameters.Add("@R_Id", SqlDbType.Int);

                in1.Direction = ParameterDirection.Input;
                in1.Value = DateTime.Parse(TextBoxDate.Text);
                in2.Direction = ParameterDirection.Input;
                in2.Value = TextBoxP_id.Text;
                in3.Direction = ParameterDirection.Input;
                in3.Value = TextBoxD_id.Text;

                in4.Direction = ParameterDirection.Input;
                in4.Value = TextBoxT_id.Text;

                in5.Direction = ParameterDirection.Input;
                in5.Value = Convert.ToInt32(LabelMessage.Text);




                cmd.ExecuteNonQuery();

                LabelMessage.Text = "Reservation has been updated";
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message + ex.StackTrace;
            }
            finally
            {
                conn.Close();
            }

            if (Session["PatientName"] != null)
            {
                UpdateGridview();
            }
            else if (Session["DentistName"] != null)
            {
                UpdateGridviewDentist();
                
            }

            try
            {
                conn.Open();

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllRes";

                SqlParameter in1 = cmd.Parameters.Add("@P_id", SqlDbType.Int);
                in1.Direction = ParameterDirection.Input;
                in1.Value = Convert.ToInt32(TextBoxP_id.Text);



                rdr = cmd.ExecuteReader();

                GridViewPerson.DataSource = rdr;
                GridViewPerson.DataBind();


            }
            catch (Exception ex)
            {

                LabelMessage.Text = ex.Message + ex.StackTrace;
            }
            finally
            {
                conn.Close();
            }

            try
            {
                conn.Open();

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectResByDay";

                SqlParameter in1 = cmd.Parameters.Add("@Date", SqlDbType.DateTime);
                in1.Direction = ParameterDirection.Input;
                in1.Value = Convert.ToDateTime(TextBoxDate.Text);



                rdr = cmd.ExecuteReader();

                GridViewDays.DataSource = rdr;
                GridViewDays.DataBind();


            }
            catch (Exception ex)
            {

                LabelMessage.Text = ex.Message + ex.StackTrace;
            }
            finally
            {
                conn.Close();
            }
        }


        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist");
            SqlDataAdapter da = null;
            SqlCommandBuilder cb = null;
            DataSet ds = null;
            DataTable dt = null;
            string sqlsel = "select * from reservations";

            try
            {
                //conn.Open();
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlsel, conn);

                cb = new SqlCommandBuilder(da);

                ds = new DataSet();
                da.Fill(ds, "MyTreatments");

                dt = ds.Tables["MyTreatments"];

                foreach (DataRow row in dt.Select("R_Id = " + Convert.ToInt32(LabelMessage.Text)))
                {
                    row.Delete();
                }

                da.Update(ds, "MyTreatments");
                
                LabelMessage.Text = "Reservation has been deleted";
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            if (Session["PatientName"] != null)
            {
                UpdateGridview();
            }
            else if (Session["DentistName"] != null)
            {
                UpdateGridviewDentist();

            }

        }



    }
}