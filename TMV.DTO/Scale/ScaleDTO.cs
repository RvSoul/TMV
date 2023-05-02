using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.Scale
{
    public class Request_Scale : ModelDTO
    {
        public string Name { get; set; }
    }

    public class ScaleModel
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// 衡名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 衡类型-1.重，2.轻,3.混合
        /// </summary> 
        public int Type { get; set; }
         
    }
    public class ScaleDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 衡名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 衡类型-1.重，2.轻,3.混合
        /// </summary> 
        public int Type { get; set; }

        /// <summary>
        /// 衡状态-1.启用，2.停用
        /// </summary> 
        public int State { get; set; }

        /// <summary>
        /// 启停用时间
        /// </summary> 
        public DateTime UpTime { get; set; }
    }
}
