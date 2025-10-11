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
        //inserisce una nuova attività
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
        //trova tutte le attività con la data corrente
        public async Task<JsonResult> ActivityList(DateTime today) //arriva il parametro da frontend
        {
            var retval = ActivityManager.findActivity(today);

            return Json(new
            {
               retval
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //Trova tutte le attività scadute
        public async Task<JsonResult> ExpiredActivities(DateTime today) //arriva il parametro da frontend
        {
            
            var retval = ActivityManager.findExpiredActivities(today);

            
            return Json(new
            {
                retval
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> EditActivity(int activity_id, string name, string description, DateTime dateActivity, int priority, int category)
        {
            bool retval = false;
            try
            {
                int ReturnCode = 0;
               retval = ActivityManager.UpdateActivity(activity_id,name,description,dateActivity,priority,category,out ReturnCode);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", "Errore durante la modifica dell'attività ");
            }

            return Json(new { success = "modificato" , boleano = retval}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveActivity(int id)
        {
            try
            {
                int ReturnCode = 0;
                ActivityManager.DeleteActivity(id, out ReturnCode); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", "Errore durante l'eliminazione dell'attività ");
            }
            return await Task.FromResult(Json(new { success = "eliminato"}));
        }



        //[HttpPost]
        //public JsonResult filterActivity(string filter)
        //{

        //    try
        //    {
        //        int ReturnCode = 0;
        //        var retval = ActivityManager.searchActivity(filter);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("CustomError", "Errore durante l'eliminazione dell'attività ");
        //    }


        //    return Json(retval, 
        //        JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public async Task<JsonResult> filterActivity(string filter) //arriva il parametro da frontend
        {

            int ReturnCode = 0;
            var retval = ActivityManager.searchActivity(filter);


            return Json(new
            {
                retval
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> filterActivity_ExpiredActivities(string filter) //arriva il parametro da frontend
        {

            int ReturnCode = 0;
            var retval = ActivityManager.searchActivity_ExpiredActivities(filter);


            return Json(new
            {
                retval
            }, JsonRequestBehavior.AllowGet);
        }


    }
}