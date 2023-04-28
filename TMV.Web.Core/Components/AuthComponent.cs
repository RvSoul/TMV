using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using TMV.Web.Core.Handle;

namespace TMV.Web.Core.Components
{
    public sealed class AuthComponent : IServiceComponent
    {
        public void Load(IServiceCollection services, ComponentContext componentContext)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddJwt<BlazorAuthorizeHandler>(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;    // 更改默认验证为 Cookies
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;   // 更改默认验证为 Cookies
            });
        }
    }
}
