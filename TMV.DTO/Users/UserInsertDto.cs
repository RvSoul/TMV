using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO.Users
{
    public class UserInsertDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string PassWord { get; set; }
        public int Type { get; set; }
    }
}
