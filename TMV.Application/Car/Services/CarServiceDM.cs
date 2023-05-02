using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Car;
using TMV.DTO.ModelData;
using TMV.Core.CM;
using TMV.DTO.Users;
using TMV.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TMV.Application.Car.Services
{
    public class CarServiceDM : ICarServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        public CarServiceDM(ISqlSugarClient db)
        {
            c = db;
        }

        public ResultEntity<bool> AddCar(CarModel model)
        {
            TMV_Car data = new TMV_Car();
            data.Id = Guid.NewGuid();
            data.PlateNumber = model.PlateNumber;
            data.Type = model.Type;
            data.SizeC = model.SizeC;
            data.SizeK = model.SizeK;
            data.SizeG = model.SizeG;
            data.Surveyor = model.Surveyor;
            data.TieBar1 = model.TieBar1;
            data.TieBar2 = model.TieBar2;
            data.TieBar3 = model.TieBar3;
            data.RatedWeight = model.RatedWeight;
            data.ExerciseCode = model.ExerciseCode;
            data.TAgCode = model.TAgCode;
            data.EmptyWeight = model.EmptyWeight;
            data.FullWeight = model.FullWeight;
            data.DriverName = model.DriverName;
            data.Sex = model.Sex;
            data.Age = model.Age;
            data.NativePlace = model.NativePlace;
            data.DrivingCode = model.DrivingCode;
            data.AddName = model.AddName;
            data.AddTime = DateTime.Now;

            var result = c.Insertable(data).ExecuteCommand();
            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false);
            }
        }

        public ResultEntity<bool> DeCar(Guid id)
        {
            var result = c.Deleteable<TMV_Car>().In(id).ExecuteCommand();

            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false);
            }
        }

        public ResultPageEntity<CarDTO> GetCarList(Request_Car dto)
        {
            int total = 0;
            Expression<Func<TMV_Car, bool>> expr = AutoAssemble.Splice<TMV_Car, Request_Car>(dto);
            var query = c.Queryable<TMV_Car>().Where(expr).WhereIF(!string.IsNullOrWhiteSpace(dto.Type),x=>x.Type==Convert.ToInt32(dto.Type)).ToPageList(dto.PageIndex, dto.PageSize, ref total);
            var list = query.Adapt<List<CarDTO>>();
            return new ResultPageEntity<CarDTO>() { Data = list, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = total };
        }

        public ResultEntity<bool> UpCar(CarModel model)
        {
            var data = c.Queryable<TMV_Car>().InSingle(model.Id);
            data.PlateNumber = model.PlateNumber;
            data.Type = model.Type;
            data.SizeC = model.SizeC;
            data.SizeK = model.SizeK;
            data.SizeG = model.SizeG;
            data.Surveyor = model.Surveyor;
            data.TieBar1 = model.TieBar1;
            data.TieBar2 = model.TieBar2;
            data.TieBar3 = model.TieBar3;
            data.RatedWeight = model.RatedWeight;
            data.ExerciseCode = model.ExerciseCode;
            data.TAgCode = model.TAgCode;
            data.EmptyWeight = model.EmptyWeight;
            data.FullWeight = model.FullWeight;
            data.DriverName = model.DriverName;
            data.Sex = model.Sex;
            data.Age = model.Age;
            data.NativePlace = model.NativePlace;
            data.DrivingCode = model.DrivingCode;
            data.AddName = model.AddName;

            var result = c.Updateable(data).ExecuteCommand();
            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false);
            }
        }
    }
}
