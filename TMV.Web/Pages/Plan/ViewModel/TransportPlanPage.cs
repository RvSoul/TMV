using TMV.DTO.Scale;
using TMV.DTO.TransportPlan;

namespace TMV.Web.Pages.Plan.ViewModel
{
    public class TransportPlanPage: PageBase<TransportPlanDTO>
    {
        public string Code { get; set; }
        public string CargoName { get; set; }
        public string Carrier { get; set; }
        public string Sampling { get; set; }

        public TransportPlanPage(List<TransportPlanDTO> datas)
        {
            Datas = new List<TransportPlanDTO>();
            Datas.AddRange(datas);
        }
        public TransportPlanPage()
        {
        }
    }
}
