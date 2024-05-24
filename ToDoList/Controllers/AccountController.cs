using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using ToDoList.Models;
using ToDoList.ViewModels;
using ToDoList.Manager;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }


        public ActionResult Registrati()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrati(UsersViewModel utente)
        {
            TryValidateModel(utente);
            if (ModelState.IsValid)
            {
                //CREO UN OGGETTO UTENTE a cui assegno i valori del ViewModel
                Users utenteObj = new Users
                {
                    name = utente.name,
                    surname = utente.surname,
                    email = utente.email,
                    password = utente.password,
                    dateBirth = utente.dateBirth
                };

                try
                {
                    int ReturnCode = 0;
                    UsersManager.insertUser(utenteObj, out ReturnCode);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CustomError", "Errore durante la creazione dell'utente ");
                }
            }

            return Json(new { ok = true, val = utente }, JsonRequestBehavior.AllowGet); //torna il model
        }

        
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}