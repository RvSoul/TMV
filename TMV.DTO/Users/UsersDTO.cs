﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.Users
{
    public class UsersView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string PassWord { get; set; }
        public int Type { get; set; }
        public DateTime CreateTime { get; set; }

    }

    public class UsersDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [DefaultValue("用户名称")]
        [SelectField("and", "like", "string")]
        public string? Name { get; set; }
    }
}
