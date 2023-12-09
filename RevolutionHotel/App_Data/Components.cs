using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;

namespace RevolutionHotel
{
    public class Components
    {
        public static SqlConnection connection;
        public static SqlConnection GetConnectionToBD()
        {
            try
            {
                string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                connection = new SqlConnection(str);
                connection.Open();
                //if (connection != null || connection.State == ConnectionState.Closed)
                //{
                //    connection = new SqlConnection(@"Data Source = JOEL\JOEL; Initial Catalog = revolution; User ID = portals; Password = portals; MultipleActiveResultSets=true;");
                //    connection.Open();
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return connection;
        }

        public static void SendEmailAlerts(string recipient, string subject, string body)
        {
            string senderEmail = "wanjala.n.joel@gmail.com";
            string senderPassword = "sjxo tphf xoxg zsgx";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderEmail);
            message.Subject = subject;
            message.To.Add(new MailAddress(recipient));
            message.Body = $"<html><body>{body}</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 25;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }

        public static bool ValidPassword(string password)
        {
            bool b = false;
            try
            {
                string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$";
                if (Regex.IsMatch(password, pattern))
                {
                    b = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return b;
        }

        public static string GenerateRandomId()
        {
            string id = string.Empty;
            try
            {
                string num = "1234567890abcdefghijklmnopqrstuvwxyz";
                int len = num.Length;
                int otpdigit = 15;
                string finaldigit;

                int getindex;

                for (int i = 0; i <= otpdigit; i++)
                {
                    do
                    {
                        getindex = new Random().Next(0, len);
                        finaldigit = num.ToCharArray()[getindex].ToString();
                    }
                    while (id.IndexOf(finaldigit) != -1);
                    id += finaldigit;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return id.ToUpper();
        }
    }
}