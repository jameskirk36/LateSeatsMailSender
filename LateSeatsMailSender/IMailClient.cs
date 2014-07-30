using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LateSeatsMailSender
{
    public interface IMailClient
    {
        void SendMail(string mailTo, string mailFrom, string mailSubject, string body);
    }
}
