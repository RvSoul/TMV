using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;
using TMV.DTO.TransportPlan;

namespace TMV.Application.TransportPlan.Services
{
    public interface ITransportPlanServiceDM
    {
        ResultPageEntity<TransportPlanDTO> GetTransportPlanList(Request_TransportPlan dto);

        /// <summary>
        /// 根基采样方式获取编号
        /// </summary>
        /// <param name="Sampling">1.人采，2.机采</param>
        /// <returns></returns>
        ResultEntity<string> GetCode(int Sampling);
        ResultEntity<bool> AddTransportPlan(TransportPlanModel model);

        ResultEntity<bool> UpTransportPlan(TransportPlanModel model);

        ResultEntity<bool> DeTransportPlan(Guid id);
    }
}
