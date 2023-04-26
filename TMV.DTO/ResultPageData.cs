using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO
{
    public class ResultPageData
    {
        public string Msg { get; set; }
        public bool IsSuccess { get; set; }
        public int Total { get; set; }
        public object Data { get; set; }
    }
}
