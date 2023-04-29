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
    /// 称重记录
    /// </summary>
    [SugarTable("ScalageRecords")]
    public partial class TMV_ScalageRecords
    {
        [Key]
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
