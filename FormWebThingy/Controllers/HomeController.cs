using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LateSeatsMailSender;
using Newtonsoft.Json;

namespace FormWebThingy.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ContentResult Index(string json)
        {
            var watchlist = JsonConvert.DeserializeObject<Watchlist>(json);
            FormGenerator.GenerateForm(watchlist);

            var mailSender = new MailSender();
            var realMailSender = new SmtpClientWrapper("smtp.laterooms.com", 25);

            mailSender.SendMail(json, realMailSender);


            return Content("done", "text/string");
        }

    }
}
