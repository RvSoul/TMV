using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Car;

namespace TMV.Application.Car
{
    internal class CarService : ICarService, IDynamicApiController, ITransient
    {
        public bool AddCar(CarModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeCar(int id)
        {
            throw new NotImplementedException();
        }

        public List<CarDTO> GetCarList(Request_Car dto, out int count)
        {
            throw new NotImplementedException();
        }

        public bool UpCar(CarModel model)
        {
            throw new NotImplementedException();
        }
    }
}
