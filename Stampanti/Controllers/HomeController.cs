using Stampanti.Data;
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
            _stampantiRepository = new StampantiRepository();
            _stampantiRepository.AddStampante(new Stampante { Nome = "Nome", IP = "2.2.2.2", Port = 9100 });
            _stampantiRepository.AddStampante(new Stampante { Nome = "Nome2", IP = "2.4.3.1", Port = 9100 });
            _stampantiRepository.AddStampante(new Stampante { Nome = "Nome3", IP = "5.4.3.1", Port = 9100 });

        }

        private StampantiRepository _stampantiRepository;
        
        public ActionResult Index()
        {
            
            return View(_stampantiRepository.GetStampanti());
        }

        public ActionResult AggiungiStampante()
        {
 
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Aggiungi il nome del reparto: ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Seleziona la riga da cambiare ";
            _stampantiRepository.GetStampante("");
            return View(_stampantiRepository.GetStampanti());
        }

        
    }
}