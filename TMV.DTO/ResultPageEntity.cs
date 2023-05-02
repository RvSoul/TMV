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
        public  List<T> Data { get; set; }
        /// <summary>
        ///  返回的结果总数
        /// </summary>
        public int Count { get { return count; } set { count = value; } }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
