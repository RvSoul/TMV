using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Users;
using TMV.DTO.TransportPlan;

namespace TMV.Application.TransportPlan.Services
{
    public class TransportPlanServiceDM : ITransportPlanServiceDM, IDynamicApiController, ITransient
    {
        public bool AddTransportPlan(TransportPlanModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeTransportPlan(int id)
        {
            throw new NotImplementedException();
        }

        public List<TransportPlanDTO> GetTransportPlanList(Request_TransportPlan dto, out int count)
        {
            throw new NotImplementedException();
        }

        public bool UpTransportPlan(TransportPlanModel model)
        {
            throw new NotImplementedException();
        }
    }
}
