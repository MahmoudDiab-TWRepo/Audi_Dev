using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;

namespace Eagles.LMS.Helper
{
    public class SendEmail
    {
        EmcNewsContext db = new EmcNewsContext();

        public bool SendMail(EmailDTO obj, string Key, string UserMail)
        {
            string Emails = "";
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            switch (Key)
            {
                case "Contact":
                    Emails = System.Configuration.ConfigurationManager.AppSettings["TOEMail"];
                    break;
                case "booking":
                    Emails = System.Configuration.ConfigurationManager.AppSettings["TOEMailbooking"];
                    break;
                case "Citizen":
                    Emails = System.Configuration.ConfigurationManager.AppSettings["TOEMailCitizen"];
                    break;

                    //default:
            }

            string Adimn_Subject = System.Configuration.ConfigurationManager.AppSettings["Admin_New_Booking"];
            string User_Subject = System.Configuration.ConfigurationManager.AppSettings["User_New_Booking"];
            string Bodymessage = System.Configuration.ConfigurationManager.AppSettings["UserBody"];

            string Reciever = Emails;
            using (MailMessage mm = new MailMessage(smtpSection.From, Reciever))
            {
                mm.Subject = User_Subject;
                mm.Body = Bodymessage + "</b>" + obj.Message;
                mm.IsBodyHtml = true;
                mm.CC.Add(UserMail);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);
            }

            return true;
        }


    }
    public class EmailDTO
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}