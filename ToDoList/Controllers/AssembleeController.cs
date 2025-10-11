using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class AssembleeController : Controller
    {
        // GET: Assemblee
        public ActionResult Index()
        {
            return View();
        }
    }
}