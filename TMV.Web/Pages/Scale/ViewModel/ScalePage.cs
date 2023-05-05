using TMV.DTO.Scale;
using TMV.DTO.Users;

namespace TMV.Web.Pages.Scale.ViewModel
{
    public class ScalePage: PageBase<ScaleDTO>
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public ScalePage(List<ScaleDTO> datas)
        {
            Datas = new List<ScaleDTO>();
            Datas.AddRange(datas);
        }
        public ScalePage()
        {
        }
    }
}
