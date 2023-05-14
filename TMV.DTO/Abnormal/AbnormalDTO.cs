using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.Abnormal
{
    public class Request_Abnormal : ModelDTO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
    }
    public class AbnormalModel
    {
        public Guid Id { get; set; }

		/// <summary>
		/// 异常编号
		/// </summary> 
		public int Code { get; set; }

		/// <summary>
		/// 异常原因
		/// </summary> 
		public string AbnormalCause { get; set; }

		/// <summary>
		/// 异常处理方式
		/// </summary> 
		public string Disposal { get; set; }
    }
    public class AbnormalDTO
    {
        public Guid Id { get; set; }

		/// <summary>
		/// 异常编号
		/// </summary> 
		[Required(AllowEmptyStrings =false,ErrorMessage="编号不能为空")]
		public int Code { get; set; }

		/// <summary>
		/// 异常原因
		/// </summary> 
		[Required(AllowEmptyStrings = false, ErrorMessage = "异常原因不能为空")]
		public string AbnormalCause { get; set; }

		/// <summary>
		/// 异常处理方式
		/// </summary> 
		[Required(AllowEmptyStrings = false, ErrorMessage = "处理方式不能为空")]
		public string Disposal { get; set; }
    }
}
