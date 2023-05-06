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
        public int ScaleType { get; set; }

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
}
