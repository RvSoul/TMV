using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.Core.CM
{

    /// <summary>
    /// 运输计划
    /// </summary>
    [SugarTable("TransportPlan")]
    public partial class TransportPlan
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 矿号
        /// </summary>
        [DefaultValue("矿号")]
        public string MineCode { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [DefaultValue("编号")]
        public int Code { get; set; }

        /// <summary>
        /// 船运单位
        /// </summary>
        [DefaultValue("船运单位")]
        public int ShippingUnit { get; set; }

        /// <summary>
        /// 承运单位
        /// </summary>
        [DefaultValue("承运单位")]
        public string Carrier { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DefaultValue("创建时间")]
        public DateTime AddTime { get; set; }

    }
}
