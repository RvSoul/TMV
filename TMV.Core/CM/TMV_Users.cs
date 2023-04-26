using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace TMV.Core.CM
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [SugarTable("tmv_user")]
    public class TMV_Users
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DefaultValue("用户名称")]
        public string Name { get; set; }
        public string LoginName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [DefaultValue("用户密码")]
        public string PassWord { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [DefaultValue("注册时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户类型-1.管理，2.值班
        /// </summary>
        [DefaultValue("用户类型")]
        public int Type { get; set; }
    }
}
