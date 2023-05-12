using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.Tr
{
    public class Request_ScalageRecordsDTO : ModelDTO
    {
        /// <summary>
        /// 衡Id
        /// </summary> 
        public string ScaleId { get; set; }

        /// <summary>
        /// 运输Id
        /// </summary> 
        public string TId { get; set; }
    }
    public class ScalageRecordsDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 衡Id
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
        /// 运输Id
        /// </summary> 
        public Guid TId { get; set; }
        /// <summary>
        /// 重量
        /// </summary> 
        public int Weigh { get; set; }

        /// <summary>
        /// 称重时间
        /// </summary> 
        public DateTime AddTime { get; set; }
    }


    public class Request_ScalageRecordsDTO2 : ModelDTO
    {
        /// <summary>
        /// 衡Id
        /// </summary> 
        public Guid Id { get; set; }
    }
    public class ScalageRecordsDTO2
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }
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
        /// 毛量
        /// </summary> 
        public int? RoughWeight { get; set; }

        /// <summary>
        /// 皮重
        /// </summary> 
        public int? TareWeight { get; set; }

        /// <summary>
        /// 净重
        /// </summary> 
        public int? NetWeight { get; set; }

        /// <summary>
        /// 称重时间
        /// </summary> 
        public DateTime AddTime { get; set; }
    }
}
