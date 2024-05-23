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
        public JsonResult insertActivity( ActivityViewModels activity)
        {


            TryValidateModel(activity);
            if (ModelState.IsValid)
            {
                //CREO UN OGGETTO UTENTE a cui assegno i valori del ViewModel
                Activity utenteObj = new Activity
                {
                    name = activity.name,
                    description = activity.description,
                    date = activity.date,
                    priority = activity.priority,
                    category = activity.category
                };

                try
                {
                    int ReturnCode = 0;
                    ActivityManager.insertActivity(utenteObj, out ReturnCode);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CustomError", "Errore durante la creazione dell'utente ");
                }
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