using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace TMV.Core.CM
{
    /// <summary>
    /// 物流订单
    /// </summary>
    [SugarTable("TransportationRecords")]
    public partial class TMV_TransportationRecords
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 车辆Id
        /// </summary>
        [DefaultValue("车辆Id")]
        public Guid CarId { get; set; }

        /// <summary>
        /// 运输计划Id
        /// </summary>
        [DefaultValue("运输计划Id")]
        public Guid CollieryId { get; set; }

        /// <summary>
        /// 毛量
        /// </summary>
        [DefaultValue("毛量")]
        public decimal? RoughWeight { get; set; }

        /// <summary>
        /// 皮重
        /// </summary>
        [DefaultValue("皮重")]
        public decimal? TareWeight { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
        [DefaultValue("净重")]
        public decimal? NetWeight { get; set; }
        /// <summary>
        /// 扣重
        /// </summary>
        [DefaultValue("扣重")]
        public decimal KouWeight { get; set; }

        /// <summary>
        /// 进厂时间
        /// </summary>
        [DefaultValue("进厂时间")]
        public DateTime STime { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary>
        [DefaultValue("出厂时间")]
        public DateTime? ETime { get; set; }


        /// <summary>
        /// 状态-  1.正常，2.告警
        /// </summary>
        [DefaultValue("状态")]
        public int State { get; set; }

        /// <summary>
        /// 是否上传-1.未上传，2.已上传
        /// </summary>
        [DefaultValue("是否上传")]
        public int IsUpload { get; set; }

        /// <summary>
        /// 重衡称名称
        /// </summary>
        public string ZScaleName { get; set; }

        /// <summary>
        /// 轻衡称名称
        /// </summary>
        public string QScaleName { get; set; }
    }
}
