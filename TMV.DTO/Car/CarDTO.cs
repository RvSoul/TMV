using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.Car
{
    public class Request_Car : ModelDTO
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [SelectField("and", "like", "string")]
        [DefaultValue("用户名称")]
        public string? Name { get; set; }

    }
    public class CarModel
    {
        public int Id { get; set; }

    }
    public class CarDTO
    {

    }
}
