using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Eagles.LMS.Data;
using Eagles.LMS.Models;

namespace Eagles.LMS.Helper
{
    public class SendEmail
    {
        EmcNewsContext db = new EmcNewsContext();


        public bool SendMail(EmailDTO obj)
        {
            try
            {

                var fromAddress = new MailAddress("web@empcnews.com", "EMPC");
                var toAddress = new MailAddress(obj.To, "Mr Test");
                const string fromPassword = "P@ssw0rd@2022";
                string subject = obj.Subject;
                string body = obj.Message;

                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    //Timeout = 20000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return false;
            }


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