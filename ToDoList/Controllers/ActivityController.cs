using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToDoList.ViewModels;
using ToDoList.Models;
using ToDoList.Manager;

namespace ToDoList.Controllers
{
    public class ActivityController : Controller
    {
        // GET: Activity
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult newActivity( string name, string description, DateTime date, int priority, int category)
        {
                //CREO UN OGGETTO UTENTE a cui assegno i valori del ViewModel
                Activity activityObj = new Activity
                {
                    name = name,
                    description = description,
                    dateActivity = date,
                    priority = priority,
                    category = category
                };

                try
                {
                    int ReturnCode = 0;
                    ActivityManager.insertActivity(activityObj, out ReturnCode);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CustomError", "Errore durante la creazione dell'attività ");
                }
            
            return Json(new { ok = "inserito" }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public async Task<JsonResult> ActivityList(DateTime today) //arriva il parametro da frontend
        {
            var retval = ActivityManager.findActivity(today);

            return Json(new
            {
               retval
            }, JsonRequestBehavior.AllowGet);
        }






    }
}