using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace iTrip.Codes
{
    public class PublicMethod
    {
        static public bool Send(MailAddress MessageForm, string MessageTo, string MessageSubject, string MessageBody)
        {
            MailMessage message = new MailMessage();
            message.To.Add(MessageTo);
            message.From = MessageForm;
            message.Subject = MessageSubject;
            message.Body = MessageBody;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            SmtpClient sc = new SmtpClient();
            sc.Host = "smtp.qq.com";
            sc.Port = 25;
            sc.Credentials = new System.Net.NetworkCredential("958077497@qq.com", "&a2198685z.");
            try
            {
                sc.Send(message); 
            }
            catch
            {
                return false;
            }
            return true;
        }
        
    }
}