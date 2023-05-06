using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO
{
    public class ResultPageEntity<T>
    {
        private bool issucess = false;
        private int count = 0;
        /// <summary>
        /// 是否成功--默认为false
        /// </summary>
        public bool IsSuccess { get { return issucess; } set { issucess = value; } }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public List<T> Data { get; set; }
        /// <summary>
        ///  返回的结果总数
        /// </summary>
        public int Count { get { return count; } set { count = value; } }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ResultPageEntityUtil<T>
    {
        /// <summary>
        /// 返回成功的响应实体
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ResultPageEntity<T> Success(List<T> data, int count, string message = "操作成功")
        {
            return new ResultPageEntity<T>
            {
                Data = data,
                IsSuccess = true,
                Count = count,
                Msg = message
            };
        }

        public ResultPageEntity<T> Success(List<T> data, int count, int PageIndex, int PageSize, string message = "操作成功")
        {
            return new ResultPageEntity<T>
            {
                Data = data,
                IsSuccess = true,
                Count = count,
                Msg = message,
                PageIndex = PageIndex,
                PageSize = PageSize
            };
        }

        /// <summary>
        /// 返回成功的响应实体
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ResultPageEntity<T> Success(List<T> data, string message = "操作成功")
        {
            return Success(data, 1, message);
        }

        /// <summary>
        /// 返回失败的响应实体
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ResultPageEntity<T> Failure(List<T> data, string message = "操作失败")
        {
            return new ResultPageEntity<T>
            {
                Data = data,
                IsSuccess = false,
                Msg = message
            };
        }


        /// <summary>
        /// 返回失败的响应实体
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ResultPageEntity<bool> Failure(string message = "操作失败")
        {
            return new ResultPageEntity<bool>
            {
                IsSuccess = false,
                Msg = message
            };
        }
    }
}
