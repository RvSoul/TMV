using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;

namespace TMV.Web.Core.ApiAttribute
{
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            ResultEntity<bool> result = new();
             
            result.Msg = context.Exception.Message;

            context.Result = new OkObjectResult(result);

            //设置异常已经处理,否则会被其他异常过滤器覆盖
            context.ExceptionHandled = true;
        }
    }
}
