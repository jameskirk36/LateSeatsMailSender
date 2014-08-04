using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LateSeatsMailSender;
using LateSeatsMailSender.Mail;
using Newtonsoft.Json;

namespace LSF.Mailer.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ContentResult Index(string json)
        {
            var realMailSender = new SmtpClientWrapper("smtp.laterooms.com", 25);
            LateSeatMailAlerter.SendMailWithAttachment(json, realMailSender);

            return Content("done", "text/string");
        }

    }
}
