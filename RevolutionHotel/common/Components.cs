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
using Microsoft.SqlServer.Server;

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
            //smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }

        public static string HashPasswords(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
            return passwordHash;
        }

        public static bool EnhanceHashedPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
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

        public static string SplitString(string input, int sliceLength)
        {
            string result = "";
            if(input.Length > sliceLength)
            {
                int startIndex = 0;
                string sliceString = input.Substring(startIndex, sliceLength);
                result = $"{sliceString}...";
            }
            else
            {
                result = input;
            }
            return result;
        }

        public static string GenerateRandomTransactionId(string username)
        {
            string id = string.Empty;
            try
            {
                string pattern = "67890QWERTYUIOPASDFGHJKLZXCVBNM12345";
                int len = pattern.Length;
                int otpdigit = 20;
                string finalstring;

                int getindex;

                for(int i = 0;i <= otpdigit; i++)
                {
                    do
                    {
                        getindex= new Random().Next(0, len);
                        finalstring = pattern.ToCharArray()[getindex].ToString();
                    }
                    while(id.IndexOf(finalstring) != -1);
                    id += finalstring;
                }
                
            }
            catch(Exception ex)
            {
                ex.Data.Clear();
            }
            return $"{GetFirstThreeLetterOfUsername(username)}{id}";
        }

        public static string GenerateOTP()
        {
            string id = string.Empty;
            try
            {
                string pattern = "0123456789abcdefghijklmnopqrstuvwxyz";
                int len = pattern.Length;
                int otpdigit = 6;
                string finalstring;

                int getindex;

                for (int i = 0; i <= otpdigit; i++)
                {
                    do
                    {
                        getindex = new Random().Next(0, len);
                        finalstring = pattern.ToCharArray()[getindex].ToString();
                    }
                    while (id.IndexOf(finalstring) != -1);
                    id += finalstring;
                }

            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return id.ToUpper();
        }

        public static string GetFirstThreeLetterOfUsername(string username)
        {
            string result = string.Empty;
            try
            {
                if (username.Length > 2)
                {
                    string splitString = username.Substring(0, 3);
                    result = splitString.Trim();
                }
                else
                {
                    result = $"{username}";
                }
            }
            catch(Exception ex)
            {
                ex.Data.Clear();
            }
            return result.ToUpper();
        }
        
    }
}