using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.Core.CM
{
    [SugarTable("SocketConfig")]
    public class TMV_SocketConfig
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Remark { get; set; }
    }
}
