using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

[assembly: OwinStartup(typeof(Cuentas.Ar.Startup))]
namespace Cuentas.Ar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
        //public void Configuration(IAppBuilder app)
        //{
        //    app.UseCookieAuthentication(new CookieAuthenticationOptions
        //    {
        //        AuthenticationType = "Cookie",
        //        LoginPath = new PathString("/Account/Login")
        //    });
        //    app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        //    app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
        //        {
        //            ClientId = "",
        //            ClientSecret = "",
        //            CallbackPath = new PathString("/en/Account/ExternalLoginCallback")
        //        }
        //    );
        //}
    }
}