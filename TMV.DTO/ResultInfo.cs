﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO
{

    public class ResultInfo
    {
        /// <summary>
        /// 下位机设备ID
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 应答
        /// </summary>
        public string Ack { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        ///  返回消息
        /// </summary>
        public string Message { get; set; }
    }

    public class ResultInfoUtil
    {

        public ResultInfo Success(string sn, string ack, string message = "操作成功")
        {
            return new ResultInfo
            {
                Sn = sn,
                Ack = ack,
                Error = "0",
                Message = message
            };
        }
        public ResultInfo Failure(string sn, string ack, string error, string message = "操作成功")
        {
            return new ResultInfo
            {
                Sn = sn,
                Ack = ack,
                Error = error,
                Message = message
            };
        }
    }
}
