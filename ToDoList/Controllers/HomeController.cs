﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {

        //Fa il login
        public ActionResult Index()
        {
            return View();
        }
    }
}