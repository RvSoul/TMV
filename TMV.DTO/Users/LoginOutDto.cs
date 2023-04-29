using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO.Users
{
    public class LoginOutDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Msg { get; set; }
    }
}
