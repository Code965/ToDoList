using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {

        //Fa il login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Authenticate( string email, string password)
        {
            //In questo caso crea il token
            var key = Startup.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: Startup.GetIssuer(),
                audience: Startup.GetAudience(),
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var claims = new List<System.Security.Claims.Claim>();

            claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Sid, value:"8675309"));
            claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Name, value:"Domenico"));
            claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Surname, value:"Giannone"));
            claims.Add(new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Role, value:"Admin"));


            return Json(new { token = jwtToken }); //mi ritorna il token
        

        [HttpGet]

        public ActionResult GetSomeData()
        {
            var utente = new Users() {

                id_user = System.Security.Claims.ClaimsPrincipal.Current.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Sid).Value,
                name = User.Identity.Name

                
            };
            

            return Json(utente, JsonRequestBehavior.AllowGet);
        }

    }
}