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
        public string Name { get; set; }

        /// <summary>
        /// 用户类型-1.管理，2.值班
        /// </summary> 
        public string Type { get; set; }
    }
    public class UsersModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary> 
        public string Pwd { get; set; }
         

        /// <summary>
        /// 用户类型-1.管理，2.值班
        /// </summary> 
        public int Type { get; set; }
    }

    public class UsersDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary> 
        public string Pwd { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary> 
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 用户类型-1.管理，2.值班
        /// </summary> 
        public int Type { get; set; }

    }
}
