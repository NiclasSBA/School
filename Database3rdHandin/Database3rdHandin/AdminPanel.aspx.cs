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
    public partial class AdminPanel : System.Web.UI.Page
    {

        DataSet ds;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["DentistName"] != null)
            {
                Master.ShowContent = true;
                Master.ShowLogout = true;
               
            }
            else
            {
                Master.ShowContent = false;
                Master.ShowLogout = false;
            }

            if (!Page.IsPostBack)
            {
                UpdatePage();
            }
            else
            {
                if (DropDownListSponsor.SelectedIndex != 0)
                {
                    ds = new DataSet();
                    //ReadXML : Reads XML schema and data into the DataSet using the specified System.IO.Stream, in this case, server.mappath. 
                    ds.ReadXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));
                    dt = ds.Tables["Sponsor"];

                    foreach (DataRow r in dt.Select("SponsorID = " + Convert.ToInt32(DropDownListSponsor.SelectedValue)))
                    {
                        TextBoxID.Text = r["SponsorID"].ToString();
                        TextBoxName.Text = r["Name"].ToString();
                        TextBoxWebsite.Text = r["Website"].ToString();
                        TextBoxPicture.Text = r["Picture"].ToString();
                    }

                    LabelMessage.Text = TextBoxName.Text + " has been selected";

                    // Selected index must be 0 at next page load to avoid old data to be read into the TextBox's
                    DropDownListSponsor.SelectedIndex = 0;

                    ButtonCreateSponsor.Enabled = false;
                    ButtonUpdateSponsor.Enabled = true;
                    ButtonDeleteSponsor.Enabled = true;
                }
            }
            }

        public void UpdatePage()
        {
            DropDownListSponsor.AutoPostBack = true;
            DropDownListSponsor.Items.Clear();
            try
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));
                dt = ds.Tables["Sponsor"];

                DropDownListSponsor.DataSource = dt;
                DropDownListSponsor.DataTextField = dt.Columns[1].ToString();
                DropDownListSponsor.DataValueField = dt.Columns[0].ToString();
                DropDownListSponsor.DataBind();
            }
            catch
            {
                // The XML file does not excist or is empty; dt made only for display of repeater heading
                MakeNewDataSetAndDataTable();
            }
            finally
            {
                RepeaterSponsor.DataSource = dt;
                RepeaterSponsor.DataBind();

                DropDownListSponsor.Items.Insert(0, "You can choose a Sponsor");
            }

            TextBoxID.Text = "";
            TextBoxName.Text = "";
            TextBoxWebsite.Text = "";
            TextBoxPicture.Text = "";
            ButtonCreateSponsor.Enabled = true;
            ButtonUpdateSponsor.Enabled = false;
            ButtonDeleteSponsor.Enabled = false;
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

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist;");
            SqlCommand cmd = null;

            string sqlins = "INSERT INTO Treatments values(@T_Name, @T_Price, @T_Desc, @T_Image)";


            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlins, conn);
                cmd.Parameters.Add("@T_Name", SqlDbType.Text);
                cmd.Parameters.Add("@T_Price", SqlDbType.Int);
                cmd.Parameters.Add("@T_Desc", SqlDbType.Text);
                cmd.Parameters.Add("@T_Image", SqlDbType.Text);

                cmd.Parameters["@T_Name"].Value = TextName.Text;
                cmd.Parameters["@T_Price"].Value = Int32.Parse(TextPrice.Text);
                cmd.Parameters["@T_Desc"].Value = TextDesc.Text;

                cmd.Parameters["@T_Image"].Value = TextImage.Text;


                cmd.ExecuteNonQuery();

                LabelMessage.Text = "Added New treatment bro";
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

        protected void ButtonCreateSponsor_Click(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));
                dt = ds.Tables["Sponsor"];
            }
            catch
            {
                // The xml file does not excist
                MakeNewDataSetAndDataTable();
            }

            int maxSponsorID = 0;
            if (dt == null)
            {
                // The XML file excists, but is empty
                MakeNewDataSetAndDataTable();
            }
            else
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (Convert.ToInt32(r["SponsorID"].ToString()) > maxSponsorID) maxSponsorID = Convert.ToInt32(r["SponsorID"].ToString());
                }
            }

            DataRow newRow = dt.NewRow();
            newRow["SponsorID"] = maxSponsorID + 1;
            newRow["Name"] = TextBoxName.Text;
            newRow["Website"] = TextBoxWebsite.Text;
            newRow["Picture"] = TextBoxPicture.Text;
            dt.Rows.Add(newRow);

            ds.WriteXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));

            LabelMessage.Text = TextBoxName.Text + " has been created with SponsorID " + (maxSponsorID + 1);
            UpdatePage();
        }

        protected void ButtonUpdateSponsor_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));
            dt = ds.Tables["Sponsor"];

            foreach (DataRow r in dt.Select("SponsorID = " + TextBoxID.Text))
            {
                r["SponsorID"] = Convert.ToInt32(TextBoxID.Text);
                r["Name"] = TextBoxName.Text;
                r["Website"] = TextBoxWebsite.Text;
                r["Picture"] = TextBoxPicture.Text;
            }

            ds.WriteXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));
            LabelMessage.Text = TextBoxName.Text + " has been updated";
            UpdatePage();
        }

        protected void ButtonDeleteSponsor_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));
            dt = ds.Tables["Sponsor"];

            foreach (DataRow r in dt.Select("SponsorID = " + TextBoxID.Text))
            {
                r.Delete();
            }

            ds.WriteXml(Server.MapPath("~/XMLFile/XMLsponsors.xml"));
            LabelMessage.Text = TextBoxName.Text + " has been deleted";
            UpdatePage();
        }

        protected void ButtonCancelSponsor_Click(object sender, EventArgs e)
        {
            LabelMessage.Text = "Input fields have been cleared";
            UpdatePage();
        }
    }
}