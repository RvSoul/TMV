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

namespace TMV.Application.Car.Services
{
    public class CarServiceDM : ICarServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        public CarServiceDM(ISqlSugarClient db)
        {
            c = db;
        }

        public bool AddCar(CarModel model)
        {
            TMV_Car pt = GetMapperDTO.SetModel<TMV_Car, CarModel>(model);
            pt.AddTime = DateTime.Now;
            var result = c.Insertable(pt).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeCar(int id)
        {
            var result = c.Deleteable<TMV_Car>().In(id).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<CarDTO> GetCarList(Request_Car dto, out int count)
        {
            int total = 0;
            Expression<Func<TMV_Car, bool>> expr = AutoAssemble.Splice<TMV_Car, Request_Car>(dto);

            var li = c.Queryable<TMV_Car>().Where(expr).ToPageList(dto.PageIndex, dto.PageSize, ref total);
            count = total;

            return GetMapperDTO.GetDTOList<TMV_Car, CarDTO>(li);
        }

        public bool UpCar(CarModel model)
        {
            var data = c.Queryable<TMV_Car>().InSingle(model.Id);

            var result = c.Updateable(data).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
