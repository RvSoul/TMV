using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.Users
{
    public class Request_Users : ModelDTO
    {
        //[Required(ErrorMessage = "BiaoduanId字段必填！")]
        //[SelectField("and", "=", "string")]
        //public string? BiaoduanId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DefaultValue("用户名称")]
        [SelectField("and", "like", "string")]
        public string? Name { get; set; }

    }
    public class UsersModel
    { 
    }

    public class UsersDTO
    { 
    }
}
