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
    /// 称重记录
    /// </summary>
    [SugarTable("ScalageRecords")]
    public class ScalageRecords
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 衡Id
        /// </summary>
        [DefaultValue("衡Id")]
        public Guid ScaleId { get; set; }

        /// <summary>
        /// 运输Id
        /// </summary>
        [DefaultValue("运输Id")]
        public Guid TId { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [DefaultValue("重量")]
        public int Weigh { get; set; }

        /// <summary>
        /// 称重时间
        /// </summary>
        [DefaultValue("称重时间")]
        public DateTime AddTime { get; set; }
    }
}
