using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO.Home
{
    public class HomeCountDTO
    {
        /// <summary>
        /// 净重总量
        /// </summary>
        public int? Jzzl { get; set; }

        /// <summary>
        /// 今日车次
        /// </summary>
        public int? Jrcc { get; set; }

        /// <summary>
        /// 告警次数
        /// </summary>
        public int? Gjcs { get; set; }

        /// <summary>
        /// 今日矿数
        /// </summary>
        public int? Jrks { get; set; }
    }
}
