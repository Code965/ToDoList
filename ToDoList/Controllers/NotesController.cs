using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Manager;
using System.Threading.Tasks;

namespace ToDoList.Controllers
{
    public class NotesController : Controller
    {
        // GET: Notes
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult newNotes(string title, string DescriptionNotes, DateTime DateNotes, int CategoryNotes)
        {
            //CREO UN OGGETTO UTENTE a cui assegno i valori del ViewModel
            Notes activityObj = new Notes
            {
                title = title,
                DescriptionNotes = DescriptionNotes,
                DateNotes = DateNotes,
                CategoryNotes = CategoryNotes
            };

            try
            {
                int ReturnCode = 0;
                NotesManager.insertNotes(activityObj, out ReturnCode);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", "Errore durante la creazione dell'attività ");
            }

            return Json(new { ok = "inserito" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> NotesList() //arriva il parametro da frontend
        {
            var retval = NotesManager.findNotes();

            return Json(new
            {
                retval
            }, JsonRequestBehavior.AllowGet);
        }

    }
}