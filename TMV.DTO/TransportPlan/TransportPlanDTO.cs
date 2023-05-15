
using System.ComponentModel.DataAnnotations;
using TMV.DTO.ModelData;

namespace TMV.DTO.TransportPlan
{

    public class Request_TransportPlan : ModelDTO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string CargoName { get; set; }
        /// <summary>
        /// 承运单位
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// 采样方式
        /// </summary>
        public string Sampling { get; set; }
        /// <summary>
        /// 创建时间-开始
        /// </summary>
        public string StartAddTime { get; set; }
        /// <summary>
        /// 创建时间-结束
        /// </summary>
        public string EndAddTime { get; set; }
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
        [Required]
        public decimal Tonnage { get; set; }
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
        /// 发货日期
        /// </summary> 
        public DateTime SendTime { get; set; }
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
        public string AddTime { get; }
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
    public class TransportPlanDTO
    {
        public Guid Id { get; set; }

		/// <summary>
		/// 编号
		/// </summary>
		[Required(ErrorMessage = "编号不能为空")]
		public string Code { get; set; }

		/// <summary>
		/// 进煤吨数
		/// </summary>
		[Required(ErrorMessage = "进煤吨数不能为空")]
		public decimal Tonnage { get; set; }
		/// <summary>
		/// 发货单位
		/// </summary>
		[Required(ErrorMessage = "发货单位不能为空")]
		public string SendUnit { get; set; }
		/// <summary>
		/// 收货单位
		/// </summary>
		[Required(ErrorMessage = "收货单位不能为空")]
		public string ReceiptUnit { get; set; }
		/// <summary>
		/// 货物名称
		/// </summary>
		[Required(ErrorMessage = "货物名称不能为空")]
		public string CargoName { get; set; }
		/// <summary>
		/// 承运单位
		/// </summary>
		[Required(ErrorMessage = "承运单位不能为空")]
		public string Carrier { get; set; }
         /// <summary>
        /// 发货日期
        /// </summary> 
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime ArrivalTime { get; set; }
		/// <summary>
		/// 矿号
		/// </summary>
		[Required(ErrorMessage = "矿号不能为空")]
		public string MineCode { get; set; }
		/// <summary>
		/// 矿名
		/// </summary>
		[Required(ErrorMessage = "矿名不能为空")]
		public string MineName { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string ShipName { get; set; }
		/// <summary>
		/// 船运单位
		/// </summary>
		[Required(ErrorMessage = "船运单位不能为空")]
		public string ShippingUnit { get; set; }
		/// <summary>
		/// 采样方式-1.人采，2.机采
		/// </summary>
		[Required(ErrorMessage = "采样方式不能为空")]
		public int Sampling { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
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
