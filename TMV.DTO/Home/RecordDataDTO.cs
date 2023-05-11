using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO.Home
{
	public class RecordDataDTO
	{
		/// <summary>
		/// 物流数据Id
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// 衡器入口光幕-0:入口光幕正常;1:阻挡
		/// </summary>
		public int inX { get; set; }
		/// <summary>
		/// 衡器出口光幕-0:出口光幕正常;1:阻挡;
		/// </summary>
		public int outX { get; set; }
		/// <summary>
		/// 单号
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 车号
		/// </summary>
		public string PlateNumber { get; set; }
		/// <summary>
		/// 驾驶员
		/// </summary>
		public string DriverName { get; set; }
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
		/// 规格型号--
		/// </summary>
		public string Ggxh { get; set; }
		/// <summary>
		/// 承运单位
		/// </summary>
		public string Carrier { get; set; }
		/// <summary>
		/// 货单号--
		/// </summary>
		public string Hdh { get; set; }
		/// <summary>
		/// 船名
		/// </summary>
		public string ShipName { get; set; }
		/// <summary>
		/// 卸煤点--
		/// </summary>
		public string Xmd { get; set; }

		public List<ScalageRecordsItem> scli { get; set; }
	}

	public class ScalageRecordsItem 
	{
		/// <summary>
		/// 称重衡Id
		/// </summary>
		public Guid ScaleId { get; set; }
		/// <summary>
		/// 衡名称
		/// </summary> 
		public string ScaleName { get; set; }


		/// <summary>
		/// 衡类型
		/// </summary> 
		public string ScaleType { get; set; }
		 
		/// <summary>
		/// 重量
		/// </summary> 
		public int Weigh { get; set; }

		/// <summary>
		/// 称重时间
		/// </summary> 
		public DateTime AddTime { get; set; }
	}
}
