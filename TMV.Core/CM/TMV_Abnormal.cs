using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.Core.CM
{
    /// <summary>
    /// 异常处理记录
    /// </summary>
    [SugarTable("Abnormal")]
    public class TMV_Abnormal
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 异常编号
        /// </summary>
        [DefaultValue("运输Id")]
        public int Code { get; set; }
          
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
    }
}
