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
using System.Security.Claims;

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


        //quando faccio un controller per qualcosa e ho bisogno che prima l'utente venga utenticato
        //faccio partire una chiamata a questo, se mi ritorna ok allora procedo. Quindi prima di ogni cosa
        //biosgna passare da questo metodo, fornendogli il token
        //PER OGNI TIPO DI UTENTE REGISTRATO
        //mi torna il nome di chi al momento si è loggato
        [Authorize]
        [HttpGet]
        [Route("api/Account/authorizeUser")]
        public JsonResult authenticate() //mi serve per verificare se l'identità di un utente è ok
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Json(new { msg = "ok", nome = identity.Name });
        }


        //SOLO PER GLI AMMINISTRATORI
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/Account/authorizeAdmin")]
        public JsonResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity; //prende l'identità dell'admin

            //prende il ruolo dell'admin e verifica che sia presente - store procedure
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);

            return Json(new { nome = identity.Name, ruolo = string.Join(",", roles.ToList())});
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}