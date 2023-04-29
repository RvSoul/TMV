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
    [SugarTable("Users")]
    public class TMV_Users
    {
        [SugarColumn(IsPrimaryKey = true)]
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

        /// <summary>
        /// 注册时间
        /// </summary>
        [DefaultValue("注册时间")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 用户类型-1.管理，2.值班
        /// </summary>
        [DefaultValue("用户类型")]
        public int Type { get; set; }
    }
}
