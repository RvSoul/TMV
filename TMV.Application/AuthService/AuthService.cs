using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMV.Core.CM;
using TMV.Core.Const;
using TMV.DTO.Users;
using Microsoft.AspNetCore.Identity;
using TMV.Application.Users;
using SqlSugar;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using TMV.DTO.Authorization;

namespace TMV.Application.AuthService
{
    [ApiDescriptionSettings("授权")]
    [LoggingMonitor]
    public class AuthService: IDynamicApiController,IAuthService, ITransient
    {
        ISqlSugarClient  _sqlSugarClient;
        public AuthService(ISqlSugarClient sqlSugarClient) 
        {
            _sqlSugarClient=sqlSugarClient;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInputDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [Description("登录")]
        public async Task<LoginOutDto> Login(LoginInputDTO loginInputDTO)
        {
            var data = _sqlSugarClient.Queryable<TMV_Users>().Where(w => w.Name == loginInputDTO.Account && w.Pwd == loginInputDTO.Password).First();
            if (data != null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimConst.UserId, data.Id.ToString()));
                identity.AddClaim(new Claim(ClaimConst.Account, data.Name));
                identity.AddClaim(new Claim(ClaimConst.IsSuperAdmin, data.Type.ToString()));
                identity.AddClaim(new Claim(ClaimConst.IsOpenApi, false.ToString()));

                // var config = sysBase.ConfigValue.ToInt(2880);
                var diffTime = DateTimeOffset.Now.AddMinutes(30);
                await App.HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = diffTime,
                });
                return new LoginOutDto() { UserId = data.Id.ToString(),Name= data.Name,Type= data.Type, Msg= "登录成功" };
            }
            else
            {
                return new LoginOutDto() {Msg = "登录失败，用户名或密码错误。" };
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public async Task LoginOut()
        {
              await App.HttpContext?.SignOutAsync("cookies");
              //App.HttpContext?.SignoutToSwagger();
              await Task.CompletedTask;
        }
    }
}
