using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
namespace Database3rdHandin
{
    public partial class BookReservation : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(@" data source= localhost\sqlexpress; integrated security= true; database=Dentist;");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string SqlString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarExtender1.SelectedDate = DateTime.Today;
            }
            if (Session["PatientName"] != null)
            {
                Master.ShowContent = true;
                Master.ShowLogout = true;

                if (!Page.IsPostBack)
                {

                    UpdateList();
                    UpdateGridview();
                }
            }
            else
            {
                Master.ShowContent = false;
                Master.ShowLogout = false;
                
            }

            
        }


        public void UpdateList()
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist");
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

                dt = ds.Tables["MyTreatments"];

               

                DropDownListTreatments.DataSource = dt;
                DropDownListTreatments.DataTextField = "T_Name";
                DropDownListTreatments.DataValueField = "T_Id";
                DropDownListTreatments.DataBind();
                DropDownListTreatments.Items.Insert(0, "Select a Treatment");
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message + ex.StackTrace;
            }
            finally
            {
                conn.Close();   // SqlDataAdapter closes connextion by itself; but can fail in case of errors
            }
        }

        public void UpdateGridview()
        {
            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;

            string sqlsel = "select * from dentists";

            try
            {
                //conn.Open();   SqlDataAdapter opens the connextion itself

                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlsel, conn);

                ds = new DataSet();

                da.Fill(ds, "Mydentists");
                /*
                dt.Columns.Add("SponsorID", typeof(Int32));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Website", typeof(string));
                dt.Columns.Add("Picture", typeof(string));*/
                dt = ds.Tables["Mydentists"];



                /*
                ds = new DataSet("Sponsors");
            dt = ds.Tables.Add("Sponsor");

                dt.Columns.Add("SponsorID", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Website", typeof(string));
            dt.Columns.Add("Picture", typeof(string));
        }
                 */
                

                DropDownListDentist.DataSource = dt;
                DropDownListDentist.DataTextField = "D_DentistName";
                DropDownListDentist.DataValueField = "D_DentistId";
                DropDownListDentist.DataBind();
                DropDownListDentist.Items.Insert(0, "Select a Dentist");
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            

            try
            {
                if (!string.IsNullOrEmpty(TextBoxCalendar.Text))
                {

                    string Value = TextBoxCalendar.Text.ToString();
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");
                    DateTime date = DateTime.Parse(Value);
                    string dateToday = " " + date.ToString("d");
                    DayOfWeek day = date.DayOfWeek;
                    string dayToday = " " + day.ToString();

                    if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
                    {
                        LabelMessage.Text = "Cant book in weekend or holidays";
                        return;
                    }
                    else
                    {
                        LabelMessage.Text = day.ToString();


                    }
                }
                else
                {
                    LabelMessage.Text = "Something is no right";
                }

            }
            catch (Exception r)
            {

                Label1.Text = r.Message + " " + r.StackTrace;
                string Value = TextBoxCalendar.Text.ToString();
                LabelDay.Text = TextBoxCalendar.Text.ToString();

            }
           
                SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist;");
                SqlCommand cmd = null;
            


                try
                {
                    conn.Open();


                        cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CreateReservation";
                
                // SqlParameter in1 = cmd.Parameters.Add("@Id", SqlDbType.Int);
                       SqlParameter in1 = cmd.Parameters.Add("@Date", SqlDbType.DateTime);
                       SqlParameter in2 = cmd.Parameters.Add("@P_Id", SqlDbType.Int);
                       SqlParameter in3 = cmd.Parameters.Add("@D_Id", SqlDbType.Int);
                       SqlParameter in4 = cmd.Parameters.Add("@T_Id", SqlDbType.Int);
                SqlParameter in5 = cmd.Parameters.Add("@R_MessageStatus", SqlDbType.Int);

                in1.Direction = ParameterDirection.Input;
                        in1.Value = Convert.ToDateTime(TextBoxCalendar.Text);

                        in2.Direction = ParameterDirection.Input;
                        in2.Value = Convert.ToInt32(Session["PatientId"].ToString());

                        in3.Direction = ParameterDirection.Input;
                        in3.Value = Convert.ToInt32(DropDownListDentist.SelectedValue);

                in4.Direction = ParameterDirection.Input;
                        in4.Value = Convert.ToInt32(DropDownListTreatments.SelectedValue);

                in5.Direction = ParameterDirection.Input;
                in5.Value = 0;
                cmd.ExecuteNonQuery();

                LabelMessage.Text = "Reservation Booked";
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
               
                conn.Close();
            }

            UpdateList();

        }

        protected void DropDownListDentist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DropDownListDentist.SelectedValue) != 0)
            {
                LabelMessage.Text = "You chose DentistID " + DropDownListDentist.SelectedValue;
                try
                {
                    conn.Open();

                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MySelectDentist";

                    SqlParameter in1 = cmd.Parameters.Add("@id", SqlDbType.Int);
                    in1.Direction = ParameterDirection.Input;
                    in1.Value = Convert.ToInt32(DropDownListDentist.SelectedValue.ToString());


                    rdr = cmd.ExecuteReader();
                    rdr.Read();

                    rdr.Close(); 

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
            else
            {
            }



        }



    }
}

    
