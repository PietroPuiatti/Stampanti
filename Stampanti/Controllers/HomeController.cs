using Stampanti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stampanti.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            ElencoStampanti = new List<Stampante>();
            ElencoStampanti.Add(new Stampante() { Nome = "Boh", IP = "1.1.1.1", Port = 9100 });
        }
        public List<Stampante> ElencoStampanti { get; set; }

        public ActionResult Index()
        {
            ViewBag.Stampanti = ElencoStampanti;
            
            return View();
        }

        public ActionResult AggiungiStampante()
        {
            
            ElencoStampanti.Add(new Stampante() { Nome = "Boh", IP= "1.1.1.1", Port=9100});
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Aggiungi il nome del reparto: ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Aggiungi stampante e port: ";

            return View();
        }
    }
}