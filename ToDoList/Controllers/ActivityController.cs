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

        [HttpGet]
        public async Task<JsonResult> ExpiredActivities(DateTime today) //arriva il parametro da frontend
        {
            
            var retval = ActivityManager.findExpiredActivities(today);

            
            return Json(new
            {
                retval
            }, JsonRequestBehavior.AllowGet);
        }


          [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult>  EditActivity(int Id, string Description, DateTime data, int priority, int category)
        {

           
            try
            {
                .UpdateLocations(Id, Description, data,priority,category, out ReturnCode);
            }
            catch (Exception ex)
            {
               
            }
            return Json(new { success="modificato"}, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> RemoveActivity(int id)
        {
         
           
            try
            {
                int ReturnCode = 0;
                .DeleteActivity(id); 
            }
            catch (Exception ex)
            {
               
            }
            return await Task.FromResult(Json(response));
        }




    }
}