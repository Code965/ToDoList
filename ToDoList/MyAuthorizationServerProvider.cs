using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ToDoList.Manager;


namespace ToDoList
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //var user = UsersManager.findUser(context.UserName, context.Password);
            //prendo username e password dal db

            var user = UsersManager.findUser(context.UserName, context.Password);

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == "admin" && context.Password == "admin") //lo inserisco qua
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Domenico Giannone"));
                context.Validated(identity);
            }
            else if (context.UserName == user.name && context.Password == user.password) 
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("username", user.name));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.name));
                context.Validated(identity);
            } 
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            } //in questo caso finisce e poi va a creare il token
        }
    }
}