using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using CookieAuthenticationOptions = Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions;
using CorsOptions = Microsoft.Owin.Cors.CorsOptions;
using CorsPolicy = System.Web.Cors.CorsPolicy;

[assembly: OwinStartupAttribute(typeof(ToDoList.Startup))]
namespace ToDoList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
            var _context = new OwinContext(app.Properties);

            var policy = new CorsPolicy()
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = false,
                SupportsCredentials = false
            };
            policy.ExposedHeaders.Add("content-disposition");
            app.UseCors(new CorsOptions()
            {
                PolicyProvider = new CorsPolicyProvider()
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });

        }

        public void ConfigureAuth(IAppBuilder app)
        {

            var secret = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JwtTokenSecret"]);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = GetAudience(),
                    ValidIssuer = GetIssuer(),
                    IssuerSigningKey = GetSymmetricSecurityKey(),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true

                }
            });

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
                Provider = new CookieAuthenticationProvider()
                {
                    OnApplyRedirect = ctx =>
                    {
                        if (!IsApiRequest(ctx.Request) && !IsAjaxRequest(ctx.Request))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                }

            });

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //// Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(10));

            //// Enables the application to remember the second login verification factor such as phone or email.
            //// Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            //// This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }

        public static string GetAudience()
        {
            return "MyAudience";
        }

        public static string GetIssuer()
        {
            return "MyIssuer";
        }

        public static string GetSecurityKey()
        {
            return "My Super Security Key That Is At Least 32 change";
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var issuerSigningKey = GetSecurityKey();
            byte[] data = Encoding.UTF8.GetBytes(issuerSigningKey);
            var result = new SymmetricSecurityKey(data);
            return result;
        }

        private static bool IsAjaxRequest(IOwinRequest request)
        {
            IReadableStringCollection query = request.Query;
            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            IHeaderDictionary headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        private static bool IsApiRequest(IOwinRequest request)
        {
            string apiPath = VirtualPathUtility.ToAbsolute("~/api/");
            return request.Uri.LocalPath.StartsWith(apiPath);
        }

    }
}
