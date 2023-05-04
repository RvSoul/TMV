using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.Tr
{
    public class Request_TransportationRecords : ModelDTO
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
        /// 矿号
        /// </summary> 
        public string MineCode { get; set; }

        /// <summary>
        /// 状态-1.正常，2.告警
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 衡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary> 
        public string STime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary> 
        public string ETime { get; set; }

    }
  
    public class TransportationRecordsModel
    {


    }

    public class TransportationRecordsDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 车辆Id
        /// </summary> 
        public Guid CarId { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary> 
        public string PlateNumber { get; set; }

        /// <summary>
        /// 运输计划Id
        /// </summary> 
        public Guid CollieryId { get; set; }
        /// <summary>
        /// 矿号
        /// </summary> 
        public string MineCode { get; set; }

        /// <summary>
        /// 毛量
        /// </summary> 
        public int RoughWeight { get; set; }

        /// <summary>
        /// 皮重
        /// </summary> 
        public int? TareWeight { get; set; }

        /// <summary>
        /// 净重
        /// </summary> 
        public int? NetWeight { get; set; }

        /// <summary>
        /// 进厂时间
        /// </summary> 
        public DateTime STime { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary> 
        public DateTime? ETime { get; set; }


        /// <summary>
        /// 状态-1.正常，2.告警
        /// </summary> 
        public int State { get; set; }


        /// <summary>
        /// 是否上传-1.未上传，2.已上传
        /// </summary> 
        public int IsUpload { get; set; }
        /// <summary>
        /// 称重记录
        /// </summary>
        public List<ScalageRecordsDTO> ScalageRecordsData { get; set; }
    }
}
