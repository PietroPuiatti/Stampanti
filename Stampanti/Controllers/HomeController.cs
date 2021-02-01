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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]   
        public ActionResult Create(Stampante model)
        {
            _stampantiRepository.AddStampante(model); 

            string message = "Stampante aggiunta con successo";
            ViewBag.Message = message;
            return RedirectToAction("Index"); 
        }

        public ActionResult Update(int id)
        {
            var printer = _stampantiRepository.GetStampanteById(id);
            return View(printer);
        }

       
        [HttpPost]
        public ActionResult Update(int id, Stampante model)
        {
            var printer = _stampantiRepository.GetStampanteById(id);

            if (printer != null)
            {
                printer.Nome = model.Nome;
                printer.IP = model.IP;
                printer.Port = model.Port;

                _stampantiRepository.Save();

                return RedirectToAction("Index");
            }else
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Delete(int id)
        {
     
              _stampantiRepository.DeleteStampante(id);
              return RedirectToAction("Index");
    
        }

        public ActionResult Contact(Stampante sp)
        {
            ViewBag.Nome = sp.Nome;
            ViewBag.IP = sp.IP;
            ViewBag.Port = sp.Port;
           
            return View();
        }

        
    }
}