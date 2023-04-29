using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Car;

namespace TMV.Application.Car.Services
{
    public interface ICarServiceDM
    {
        bool AddCar(CarModel model);

        List<CarDTO> GetCarList(Request_Car dto, out int count);

        bool UpCar(CarModel model);

        bool DeCar(Guid id);
    }
}
