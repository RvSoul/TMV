using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.TransportPlan;

namespace TMV.Application.TransportPlan.Services
{
    public interface ITransportPlanServiceDM
    {
        List<TransportPlanDTO> GetTransportPlanList(Request_TransportPlan dto, out int count);
        bool AddTransportPlan(TransportPlanModel model);

        bool UpTransportPlan(TransportPlanModel model);

        bool DeTransportPlan(int id);
    }
}
