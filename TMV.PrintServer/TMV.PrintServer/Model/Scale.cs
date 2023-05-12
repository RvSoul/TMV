using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.PrintServer.Model
{
    /// <summary>
    /// 衡管理
    /// </summary>
    [SugarTable("Scale")]
    public class Scale
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 衡名称
        /// </summary>
        [DefaultValue("衡名称")]
        public string Name { get; set; }

        /// <summary>
        /// 衡类型-1.重，2.混合,3.轻
        /// </summary>
        [DefaultValue("衡类型")]
        public int Type { get; set; }

        /// <summary>
        /// 衡状态-1.启用，2.停用
        /// </summary>
        [DefaultValue("衡状态")]
        public int State { get; set; }

        /// <summary>
        /// 启停用时间
        /// </summary>
        [DefaultValue("启停用时间")]
        public DateTime UpTime { get; set; }
    }
}
