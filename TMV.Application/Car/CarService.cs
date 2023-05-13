using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Car.Services;
using TMV.DTO.Car;
using TMV.DTO;
using SqlSugar;
using Npoi.Mapper;
using Microsoft.AspNetCore.Mvc.Formatters;

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
        public ResultPageEntity<CarDTO> GetCarList([FromQuery] Request_Car dto)
        {
            return dm.GetCarList(dto);
        }

        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddCar")]
        public ResultEntity<bool> AddCar([FromQuery] CarModel model)
        {
            return dm.AddCar(model);
        }
        /// <summary>
        /// 修改车辆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpCar")]
        public ResultEntity<bool> UpCar([FromQuery] CarModel model)
        {
            return dm.UpCar(model);
        }


        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeCar")]
        public ResultEntity<bool> DeCar(Guid Id)
        {
            return dm.DeCar(Id);
        }

        [HttpPost, NonUnify]
        public async Task<ResultEntity<bool>> UploadFileAsync(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            if (file is null || file.Length == 0L)
            {
                throw new FileNotFoundException($"抱歉，未找到您要上传的文件信息。");
            }

            using (var ms = file.OpenReadStream())
            {
                var mapper = new Mapper(ms) { UseDefaultValueAttribute = false };
                mapper
                    .Map<CarModel>("车牌号", t => t.PlateNumber)
                    .Map<CarModel>("车型", t => t.Type)
                    .Map<CarModel>("车厢长", t => t.SizeC)
                    .Map<CarModel>("车厢宽", t => t.SizeK)
                    .Map<CarModel>("车厢高", t => t.SizeG)
                    .Map<CarModel>("测量人员", t => t.Surveyor)
                    .Map<CarModel>("拉筋一", t => t.TieBar1)
                    .Map<CarModel>("拉筋二", t => t.TieBar2)
                    .Map<CarModel>("拉筋三", t => t.TieBar3)
                    .Map<CarModel>("水箱油箱为空时的标准皮重", t => t.EmptyWeight)
                    .Map<CarModel>("水箱油箱加满时的标准皮重", t => t.FullWeight)
                    .Map<CarModel>("额定载总重量", t => t.RatedWeight)
                    .Map<CarModel>("行驶证号", t => t.ExerciseCode)
                    .Map<CarModel>("电子标签编号", t => t.TAgCode)
                    .Map<CarModel>("驾驶员姓名", t => t.DriverName)
                    .Map<CarModel>("性别", t => t.Sex)
                    .Map<CarModel>("年龄", t => t.Age)
                    .Map<CarModel>("籍贯", t => t.NativePlace)
                    .Map<CarModel>("驾驶证号", t => t.DrivingCode)
                    .Map<CarModel>("建档人", t => t.AddName)
                    .Map<CarModel>("建档时间", t => t.AddTime);
                var objs1 = mapper.Take<CarModel>(0,999999); 

                if (objs1.Any()) return await dm.ImportCar(objs1.Select(t => t.Value).ToList());
                else throw new ArgumentException(nameof(objs1));
             

            }
        }

        #endregion
    }
}
