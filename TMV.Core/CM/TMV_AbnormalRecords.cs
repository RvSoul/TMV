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
    /// 异常处理记录
    /// </summary>
    [SugarTable("AbnormalRecords")]
    public partial class TMV_AbnormalRecords
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 运输Id
        /// </summary>
        [DefaultValue("运输Id")]
        public Guid TId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [DefaultValue("用户id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 异常原因
        /// </summary>
        [DefaultValue("异常原因")]
        public string AbnormalCause { get; set; }

        /// <summary>
        /// 异常处理方式
        /// </summary>
        [DefaultValue("异常处理方式")]
        public string Disposal { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [DefaultValue("处理时间")]
        public DateTime DisposalTime { get; set; }
    }
}
