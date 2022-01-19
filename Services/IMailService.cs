using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using NestLinkV2.Models;
using NestLinkV2.Library.Mail;

namespace NestLinkV2.Services
{
    public interface IMailService
    {
        void SendEmailNow(Mail mail);
        void SendEmailDelayMinutes(Mail mail, int delay);
        void SendEmailDelayHours(Mail mail, int delay);
        void SendEmailDelayDays(Mail mail, int delay);
        void SendEmailAtDateTime(Mail mail, DateTime dateTime);
    }
}

