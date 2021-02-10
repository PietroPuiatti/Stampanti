using Stampanti.Data;
using Stampanti.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Stampanti.Models.Validators;

namespace Stampanti.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(IStampantiRepository repository)
        {
            _stampantiRepository = repository;
            

        }

        private IStampantiRepository _stampantiRepository;
        
        public ActionResult Index()
        {
            
            return View(_stampantiRepository.GetStampanti());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]   
        public ActionResult Create(Stampante model)
        {

            var validator = new StampantiValidator();

            ValidationResult result = validator.Validate(model);

            if (result.IsValid)
            {
                _stampantiRepository.AddStampante(model);

                string message = "Stampante aggiunta con successo";
                ViewBag.Message = message;
                return RedirectToAction("Index");
            }
            else
            {
                foreach (ValidationFailure failer in result.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
            }
            return View(model);


           
        }

        public ActionResult Update(int id)
        {
            var printer = _stampantiRepository.GetStampanteById(id);
            return View(printer);
        }

        [HttpPost]
        public ActionResult Update(Stampante printer)
        {
            var validator = new StampantiValidator();

            ValidationResult result = validator.Validate(printer);

            if (result.IsValid)
            {
                _stampantiRepository.UpdateStampante(printer);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (ValidationFailure failer in result.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
            }
            return View(printer);
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
       
    }
}