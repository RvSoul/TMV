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
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [DefaultValue("编号")]
        public string Code { get; set; }

        /// <summary>
        /// 进煤吨数
        /// </summary>
        [DefaultValue("进煤吨数")]
        public int Tonnage { get; set; }
        /// <summary>
        /// 发货单位
        /// </summary>
        [DefaultValue("发货单位")]
        public string SendUnit { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        [DefaultValue("收货单位")]
        public string ReceiptUnit { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        [DefaultValue("货物名称")]
        public string CargoName { get; set; }
        /// <summary>
        /// 承运单位
        /// </summary>
        [DefaultValue("承运单位")]
        public string Carrier { get; set; }
        /// <summary>
        /// 到货日期
        /// </summary>
        [DefaultValue("到货日期")]
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// 矿号
        /// </summary>
        [DefaultValue("矿号")]
        public string MineCode { get; set; }
        /// <summary>
        /// 矿名
        /// </summary>
        [DefaultValue("矿名")]
        public string MineName { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        [DefaultValue("船名")]
        public string ShipName { get; set; }
        /// <summary>
        /// 船运单位
        /// </summary>
        [DefaultValue("船运单位")]
        public string ShippingUnit { get; set; }
        /// <summary>
        /// 采样方式-1.人采，2.机采
        /// </summary>
        [DefaultValue("采样方式")]
        public int Sampling { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DefaultValue("创建时间")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 煤矿单位
        /// </summary>
        public string MEIKDW { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GONGYS { get; set; }
        /// <summary>
        /// 船号
        /// </summary>
        public string ShipCode { get; set; }
    }
}
