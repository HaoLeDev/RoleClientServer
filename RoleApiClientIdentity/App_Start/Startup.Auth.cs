using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RoleApiClientIdentity.EF;

[assembly: OwinStartup(typeof(TimeAttendency.WebApp.App_Start.Startup))]

namespace TimeAttendency.WebApp.App_Start
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(RoleUserDbContext.Create);

            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<UserManager<APPLICATION_USER>>(CreateManager);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/oauth/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                 context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                //var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                //if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                UserManager<APPLICATION_USER> userManager = context.OwinContext.GetUserManager<UserManager<APPLICATION_USER>>();
                APPLICATION_USER user;
                try
                {
                    user = await userManager.FindAsync(context.UserName, context.Password);
                }
                catch
                {
                    // Could not retrieve the user due to error.
                    context.SetError("server_error");
                    context.Rejected();
                    return;
                }
                if (user != null)
                {
                    ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                                   user,
                                   DefaultAuthenticationTypes.ExternalBearer);
                    context.Validated(identity);

                    //var applicationGroupService = ServiceFactory.Get<IApplicationGroupService>();
                    //var listGroup = applicationGroupService.GetListGroupByUserId(user.Id);
                    //if (listGroup.Any(x => x.Name == CommonConstants.Administrator))
                    //{
                    //    ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                    //                   user,
                    //                   DefaultAuthenticationTypes.ExternalBearer);
                    //    context.Validated(identity);
                    //}
                    //else
                    //{
                    //    context.Rejected();
                    //    context.SetError("invalid_group", "Bạn không phải là admin");
                    //}
                }
                else
                {
                    context.Rejected();
                    context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.");
                }
            }
        }

        private static UserManager<APPLICATION_USER> CreateManager(IdentityFactoryOptions<UserManager<APPLICATION_USER>> options, IOwinContext context)
        {
            var userStore = new UserStore<APPLICATION_USER>(context.Get<RoleUserDbContext>());
            var owinManager = new UserManager<APPLICATION_USER>(userStore);
            return owinManager;
        }
    }
}
