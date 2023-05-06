using Furion;
using Furion.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using TMV.Web.Core.Const;
using TMV.Application.Users.Services;
using Furion.DataEncryption;

namespace TMV.Web.Core.Handle
{
    public class BlazorAuthorizeHandler : AppAuthorizeHandler
    {
        IUsersServiceDM _usersService;
        public BlazorAuthorizeHandler(IUsersServiceDM usersService)
        {
            _usersService = usersService;
        }
        public override async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                await AuthorizeHandleAsync(context);
                // 自动刷新 token
                //if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext()))
                //{
                //    await AuthorizeHandleAsync(context);
                //}
                //else context.Fail();    // 授权失败
            }
            else
            {
                Fail(context);
                await App.HttpContext?.SignOutAsync();
            }

            static void Fail(AuthorizationHandlerContext context)
            {
                context.Fail(); // 授权失败
                DefaultHttpContext currentHttpContext = context.GetCurrentHttpContext();
                if (currentHttpContext == null)
                    return;
                currentHttpContext.SignoutToSwagger();
            }
        }

        /// <summary>
        /// 授权判断逻辑，授权通过返回 true，否则返回 false
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            //这里鉴别密码是否改变
            var userId = context.User.Claims.FirstOrDefault(it => it.Type == ClaimConst.UserId).Value;
            var user = _usersService.GetUset(Guid.Parse(userId) );
            if (user == null) { return false; }
            return true;
        }
    }
}
