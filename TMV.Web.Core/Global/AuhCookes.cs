using Furion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMV.Web.Core.Const;
using TMV.DTO.Users;

namespace TMV.Web.Core.Global
{
    public class AuhCookes
    {
        public async Task SetCookes(LoginOutDto data)
        {
            try
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimConst.UserId, data.UserId.ToString()));
                identity.AddClaim(new Claim(ClaimConst.Account, data.Name));
                identity.AddClaim(new Claim(ClaimConst.IsSuperAdmin, data.Type.ToString()));
                identity.AddClaim(new Claim(ClaimConst.IsOpenApi, false.ToString()));

                // var config = sysBase.ConfigValue.ToInt(2880);
                var diffTime = DateTimeOffset.Now.AddMinutes(30);
                await App.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = diffTime,
                });
            }
            catch (Exception ex)
            {

                throw;
            }
           
            //return Task.CompletedTask;
        }
    }
}
