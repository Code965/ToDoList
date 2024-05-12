using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Manager;
using System.Text;
using System.Configuration;

namespace ToDoList.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var utente = UsersManager.findUser(email, password);

            if (utente != null)
            {

                if (email == utente.email && password == utente.password)
                {
                    var key = Startup.GetSymmetricSecurityKey();
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new List<System.Security.Claims.Claim>();

                    //valori dell'utente che fa il login
                    claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Sid, value: "8675309"));
                    claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Name, value: "Domenico"));
                    claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Surname, value: "Giannone"));
                    claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Role, value: "Admin"));


                    //pacchetto del token con tutti i valori necessari
                    var jwtToken = new JwtSecurityToken(
                        issuer: Startup.GetIssuer(),
                        audience: Startup.GetAudience(),
                        expires: DateTime.Now.AddMinutes(60),
                        claims: claims,
                        signingCredentials: credentials);

                    string token = new JwtSecurityTokenHandler().WriteToken(jwtToken); //creazione del token vera e propria
                    return RedirectToAction("Index");
                    //return Json(new { token = token, email = utente.email, id = utente.id_user });

                }
                else
                {
                    return Json(new { error = "errore utente non trovato" });
                }
            }
            else
            {
                return Json(new { error = "utente non esistente" });
            }

        }


        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult Login(string email, string password)
        //{
        //    var key = Startup.GetSymmetricSecurityKey();
        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var claims = new List<System.Security.Claims.Claim>();

        //    //valori dell'utente che fa il login
        //    claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Sid, value: "8675309"));
        //    claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Name, value: "Domenico"));
        //    claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Surname, value: "Giannone"));
        //    claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Role, value: "Admin"));

        //    //pacchetto del token con tutti i valori necessari
        //    var jwtToken = new JwtSecurityToken(
        //        issuer: Startup.GetIssuer(),
        //        audience: Startup.GetAudience(),
        //        expires: DateTime.Now.AddMinutes(60),
        //        claims: claims,
        //        signingCredentials: credentials);

        //    string token = new JwtSecurityTokenHandler().WriteToken(jwtToken); //creazione del token vera e propria
        //    return Json(new { token = token});
        //}

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetSomeData()
        {

            var result = new Users()
            {
               
                name = User.Identity.Name,
                id_user = Convert.ToInt32(System.Security.Claims.ClaimsPrincipal.Current.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}