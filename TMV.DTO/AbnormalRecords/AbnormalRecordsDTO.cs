using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.AbnormalRecords
{
    public class Request_AbnormalRecords : ModelDTO
    {
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
        //public string Name { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 运输Id
        /// </summary>
        public string TId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary> 
        public string STime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary> 
        public string ETime { get; set; }

    }
    public class AbnormalRecordsModel
    {
        public Guid Id { get; set; }



        /// <summary>
        /// 异常处理方式
        /// </summary>
        public string Disposal { get; set; }

    }
    public class AbnormalRecordsDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 运输Id
        /// </summary>
        public Guid TId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 异常原因
        /// </summary>
        public string AbnormalCause { get; set; }

        /// <summary>
        /// 运输编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 异常处理方式
        /// </summary>
        public string Disposal { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? DisposalTime { get; set; }
    }
}
