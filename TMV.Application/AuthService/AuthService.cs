using Microsoft.AspNetCore.Authentication;
using TMV.Core.CM;
using TMV.DTO.Users;
using System.ComponentModel;
using TMV.DTO.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using TMV.Core.Const;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TMV.Application.AuthService
{
    [ApiDescriptionSettings("授权")]
    [LoggingMonitor]
    public class AuthService: IDynamicApiController,IAuthService, ITransient
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IMemoryCache _memoryCache;
        ISqlSugarClient _sqlSugarClient;
        public AuthService(ISqlSugarClient sqlSugarClient, ILogger<AuthService> logger, IMemoryCache memoryCache) 
        {
            _sqlSugarClient=sqlSugarClient;
            _logger = logger;
            _memoryCache=memoryCache;
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
            loginInputDTO.Password = MD5Encryption.Encrypt(loginInputDTO.Password);
            var data = _sqlSugarClient.Queryable<TMV_Users>().Where(w => w.Name == loginInputDTO.Account && w.Pwd == loginInputDTO.Password).First();
            if (data != null)
            {
                //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                //identity.AddClaim(new Claim(ClaimConst.UserId, data.Id.ToString()));
                //identity.AddClaim(new Claim(ClaimConst.Account, data.Name));
                //identity.AddClaim(new Claim(ClaimConst.IsSuperAdmin, data.Type.ToString()));
                //identity.AddClaim(new Claim(ClaimConst.IsOpenApi, false.ToString()));

                //// var config = sysBase.ConfigValue.ToInt(2880);
                //var diffTime = DateTimeOffset.Now.AddMinutes(30);
                //await App.HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties()
                //{
                //    IsPersistent = true,
                //    ExpiresUtc = diffTime,
                //});
               
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

        /// <summary>
        /// 衡传送数据接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SetDataInfo(string data)
        {
            _logger.LogInformation(data);
           await Task.CompletedTask;
        }
    }
}
