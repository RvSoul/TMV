using Furion;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.AuthService;
using TMV.DTO.Authorization;
using TMV.DTO.Users;
using TMV.Web.Core.Const;

namespace TMV.Web.Core.Handle
{
    public class WebsiteAuthenticator : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly IAuthService _dataProviderService;

        public WebsiteAuthenticator(ProtectedLocalStorage protectedLocalStorage, IAuthService dataProviderService)
        {
            _protectedLocalStorage = protectedLocalStorage;
           _dataProviderService = dataProviderService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();

            try
            {
               var storedPrincipal = await _protectedLocalStorage.GetAsync<string>("identity");
               // var userId = App.HttpContext.User.Claims.FirstOrDefault(it => it.Type == ClaimConst.UserId).Value;
                if (storedPrincipal.Success)
                {
                    var user = JsonConvert.DeserializeObject<LoginInputDTO>(storedPrincipal.Value);
                    var (us, isLookUpSuccess) =await LookUpUser(user.Account, user.Password);

                    if (isLookUpSuccess)
                    {
                        var identity = CreateIdentityFromUser(us);
                        principal = new(identity);
                    }
               }
            }
            catch
            {

            }

            return new AuthenticationState(principal);
        }

        public async Task<bool> LoginAsync(LoginInputDTO loginFormModel)
        {
            var (userInDatabase, isSuccess) =await LookUpUser(loginFormModel.Account, loginFormModel.Password);
            var principal = new ClaimsPrincipal();

            if (isSuccess)
            {
                var identity = CreateIdentityFromUser(userInDatabase);
                principal = new ClaimsPrincipal(identity);
                await _protectedLocalStorage.SetAsync("identity", JsonConvert.SerializeObject(loginFormModel));
                var diffTime = DateTimeOffset.Now.AddMinutes(30);
                //await App.HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties()
                //{
                //    IsPersistent = true,
                //    ExpiresUtc = diffTime,
                //});
                var sd = new AuthenticationState(principal);
                NotifyAuthenticationStateChanged(Task.FromResult(sd));
				return true;
			}
            return false;
        }

        public async Task LogoutAsync()
        {
            await _protectedLocalStorage.DeleteAsync("identity");
            var principal = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        private static  ClaimsIdentity CreateIdentityFromUser(LoginOutDto user)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimConst.UserId, user.UserId.ToString()));
            identity.AddClaim(new Claim(ClaimConst.Account, user.Name));
			identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
			identity.AddClaim(new Claim(ClaimConst.IsSuperAdmin, user.Type.ToString()));
            identity.AddClaim(new Claim(ClaimConst.IsOpenApi, false.ToString()));
            return identity;
        }

        private async Task<(LoginOutDto, bool) >LookUpUser(string username, string password)
        {
            var result =await _dataProviderService.Login(new LoginInputDTO() { Account= username ,Password= password });
			if(string.IsNullOrWhiteSpace(result.UserId))
				return (result, false);
			return (result, result is not null);
        }
    }
}
