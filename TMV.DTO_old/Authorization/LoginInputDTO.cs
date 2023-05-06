using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO.Authorization
{
    public class LoginInputDTO
    {
        // <summary>
        /// 账号
        ///</summary>
        [Required(ErrorMessage = "账号不能为空"), MinLength(3, ErrorMessage = "账号不能少于4个字符")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        ///</summary>
        [Required(ErrorMessage = "密码不能为空"), MinLength(3, ErrorMessage = "密码不能少于3个字符")]
        public string Password { get; set; }
    }
}
