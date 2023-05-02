using TMV.DTO.Car;
using TMV.DTO.Users;

namespace TMV.Web.Pages.Car.ViewModel
{
    public class CarPage: PageBase<CarDTO>
    {
        public string PlateNumber { get; set; }

        public string Type { get; set; }
        public string ExerciseCode { get; set; }
        public string DriverName { get; set; }

        public CarPage(List<CarDTO> datas)
        {
            Datas = new List<CarDTO>();
            Datas.AddRange(datas);
        }
        public CarPage()
        {
        }
    }
}
