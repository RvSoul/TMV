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
    public partial class TMV_TransportPlan
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [DefaultValue("编号")]
        public int Code { get; set; }

        /// <summary>
        /// 进煤吨数
        /// </summary>
        [DefaultValue("进煤吨数")]
        public int Tonnage { get; set; }
        /// <summary>
        /// 发货单位
        /// </summary>
        [DefaultValue("发货单位")]
        public int SendUnit { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        [DefaultValue("收货单位")]
        public int ReceiptUnit { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        [DefaultValue("货物名称")]
        public int CargoName { get; set; }
        /// <summary>
        /// 承运单位
        /// </summary>
        [DefaultValue("承运单位")]
        public int Carrier { get; set; }
        /// <summary>
        /// 到货日期
        /// </summary>
        [DefaultValue("到货日期")]
        public int ArrivalTime { get; set; }
        /// <summary>
        /// 矿号
        /// </summary>
        [DefaultValue("矿号")]
        public int MineCode { get; set; }
        /// <summary>
        /// 矿名
        /// </summary>
        [DefaultValue("矿名")]
        public int MineName { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        [DefaultValue("船名")]
        public int ShipName { get; set; }
        /// <summary>
        /// 船运单位
        /// </summary>
        [DefaultValue("船运单位")]
        public int ShippingUnit { get; set; }
        /// <summary>
        /// 采样方式-1.人采，2.机采
        /// </summary>
        [DefaultValue("采样方式")]
        public int Sampling { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DefaultValue("创建时间")]
        public int AddTime { get; set; }
        
    }
}
