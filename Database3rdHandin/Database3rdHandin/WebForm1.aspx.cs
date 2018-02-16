using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Database3rdHandin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Timer timer = new Timer();
        //private double servicePollInterval;
        private int id;
        private int status;
        private string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            // SendEMail("Butthole", "This works");
           // "~/App_Data/dentistslistdata.ser")
            FileStream fs = new FileStream(Server.MapPath("/XMLFile/ServiceStatus.txt"), FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);

            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine("Verifying for any request to process..." + DateTime.Now.ToLongTimeString());

            DataTable dt = GetMessages();
            foreach (DataRow dr in dt.Rows)
            {
                id = (int)dr[0];
                UpdateMessageStatus(id, 1);
               
                status = (int)dr[6];
                message = dr[1].ToString();
                m_streamWriter.WriteLine("Sending Email/SMS for the request id = {0} and the message is: {1}", id, message);
                SendEMail(String.Format("Re:Request#{0}", id), message);
                UpdateMessageStatus(id, 2);
            }

            m_streamWriter.Flush();
            m_streamWriter.Close();
        }

        void timer_Elapsed(object sender, EventArgs e)
        {
            
        }

        public DataTable GetMessages()
        {
            DataTable dt = new DataTable();

            // Open the connection  
            using (SqlConnection cnn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist;"))
            {
                cnn.Open();

                // Define the command  
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MessageCheckStatus";


                   
                    // Define the data adapter and fill the dataset  
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                cnn.Close();
            }
            return dt;
        }

        public DataTable UpdateMessageStatus(int id, int status)
        {
            DataTable dt = new DataTable();

            // Open the connection  
            using (SqlConnection cnn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = Dentist;"))
            {
                cnn.Open();

                // Define the command  
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UpdateMessageStatusTo1";

                    SqlParameter in1 = new SqlParameter("@id", id);
                    cmd.Parameters.Add(in1);

                    SqlParameter in2 = new SqlParameter("@status", status);
                    cmd.Parameters.Add(in2);

                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            return dt;
        }

        public void SendEMail(string subject, string body)
        {
            
            const string accountSid = "ACc4e09d6167239c6a3c71f7bba392d993";
            const string authToken = "bbb38eea2d0e8d5ca48009a33f149a78";

            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("+4527591034");
            var message = MessageResource.Create(to, from: new PhoneNumber("+16507694298"), body: "You have an appointment at Dr Dentist at" + body + " We look forward to see you!");
            
            /*
            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential("niclas_andersen@hotmail.com", "Fister221");
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.live.com";
                SmtpServer.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("niclas_andersen@hotmail.com");
                Byte i;
                mail.To.Add("niclas_andersen@hotmail.com");
                mail.Subject = subject;
                mail.Body = "You have an appointment at Dr Dentist at" + body + " We look forward to seeng you!";

                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                SmtpServer.Send(mail);
                UpdateMessageStatus(id, 2);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "EMail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }*/
        }
    

}
}