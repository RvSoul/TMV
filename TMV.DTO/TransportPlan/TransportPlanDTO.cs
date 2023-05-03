using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.TransportPlan
{

    public class Request_TransportPlan : ModelDTO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
    }

    public class TransportPlanModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 进煤吨数
        /// </summary>
        public int Tonnage { get; set; }
        /// <summary>
        /// 发货单位
        /// </summary>
        public string SendUnit { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        public string ReceiptUnit { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string CargoName { get; set; }
        /// <summary>
        /// 承运单位
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// 矿号
        /// </summary>
        public string MineCode { get; set; }
        /// <summary>
        /// 矿名
        /// </summary>
        public string MineName { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string ShipName { get; set; }
        /// <summary>
        /// 船运单位
        /// </summary>
        public string ShippingUnit { get; set; }
        /// <summary>
        /// 采样方式-1.人采，2.机采
        /// </summary>
        public int Sampling { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
    public class TransportPlanDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 进煤吨数
        /// </summary>
        public int Tonnage { get; set; }
        /// <summary>
        /// 发货单位
        /// </summary>
        public string SendUnit { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        public string ReceiptUnit { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string CargoName { get; set; }
        /// <summary>
        /// 承运单位
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// 矿号
        /// </summary>
        public string MineCode { get; set; }
        /// <summary>
        /// 矿名
        /// </summary>
        public string MineName { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string ShipName { get; set; }
        /// <summary>
        /// 船运单位
        /// </summary>
        public string ShippingUnit { get; set; }
        /// <summary>
        /// 采样方式-1.人采，2.机采
        /// </summary>
        public int Sampling { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
