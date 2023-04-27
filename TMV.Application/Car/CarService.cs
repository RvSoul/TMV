using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Car.Services;
using TMV.DTO.Car;
using TMV.DTO;
using SqlSugar;

namespace TMV.Application.Car
{
    [ApiDescriptionSettings("车辆", Tag = "车辆管理")]
    public class CarService : IDynamicApiController, ITransient
    {
        public ICarServiceDM dm;
        public CarService(ICarServiceDM CarServices)
        {
            dm = CarServices;
        }
        #region 车辆
        /// <summary>
        /// 获取车辆列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCarList")]
        public ResultEntity<List<CarDTO>> GetCarList([FromQuery] Request_Car dto)
        {
            return new ResultEntityUtil<List<CarDTO>>().Success(dm.GetCarList(dto, out int count), count);
        }

        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddCar")]
        public ResultEntity<bool> AddCar([FromQuery] CarModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.AddCar(model));
        }
        /// <summary>
        /// 修改车辆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpCar")]
        public ResultEntity<bool> UpCar([FromQuery] CarModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.UpCar(model));
        }


        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeCar")]
        public ResultEntity<bool> DeCar(int Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.DeCar(Id));
        }
        #endregion
    }
}
