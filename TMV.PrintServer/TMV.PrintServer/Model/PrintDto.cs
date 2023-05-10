using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.PrintServer.Model
{
    public class PrintDto
    {
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 称号
        /// </summary>
        public string scalenumber { get; set; }
        /// <summary>
        /// 运输单号
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// 发货单位
        /// </summary>
        public string shipper { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        public string consignee { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 承运单位
        /// </summary>
        public string carryunit { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string specification { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string splatenumber { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>
        public string roughweight { get; set; }
        /// <summary>
        /// 皮重
        /// </summary>
        public string tareweight { get; set; }
        /// <summary>
        /// 扣重
        /// </summary>
        public string buckleweight { get; set; }
        /// <summary>
        /// 净重
        /// </summary>
        public string netweight { get; set; }
        /// <summary>
        /// 船号
        /// </summary>
        public string shipnumber { get; set; }
        /// <summary>
        /// 重车司磅员
        /// </summary>
        public string truckscar { get; set; }
        /// <summary>
        /// 空车司磅员
        /// </summary>
        public string emptycar { get; set; }
        /// <summary>
        /// 过重车时间
        /// </summary>
        public string trucktime { get; set; }
        /// <summary>
        /// 过轻车时间
        /// </summary>
        public string lighttime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
