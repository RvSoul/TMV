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
        /// 车牌号
        /// </summary>
        [SelectField("and", "like", "string")]
        [DefaultValue("车牌号")]
        public string PlateNumber { get; set; }

    }
    public class CarModel
    {
        public Guid Id { get; set; }

    }
    public class CarDTO
    {

    }
}
