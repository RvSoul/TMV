using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;
using TMV.DTO.Car;

namespace TMV.Application.Car.Services
{
    public interface ICarServiceDM
    {
        ResultEntity<bool> AddCar(CarModel model);

        ResultPageEntity<CarDTO> GetCarList(Request_Car dto);

        ResultEntity<bool> UpCar(CarModel model);

        ResultEntity<bool> DeCar(Guid id);
    }
}
