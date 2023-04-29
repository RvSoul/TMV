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
        /// <summary>
        /// 用户名称
        /// </summary>
        [DefaultValue("用户名称")]
        [SelectField("and", "like", "string")]
        public string? Name { get; set; }
    }
    public class UsersModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [DefaultValue("用户名称")]
        public string Name { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [DefaultValue("用户密码")]
        public string Pwd { get; set; }

        public int Type { get; set; }
    }

    public class UsersDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public int Type { get; set; }
        public DateTime AddTime { get; set; }

    }
}
