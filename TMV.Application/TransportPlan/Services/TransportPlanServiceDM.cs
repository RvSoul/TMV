using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.TransportPlan.Services;
using TMV.Application.Users;
using TMV.Core.CM;
using TMV.DTO.TransportPlan;
using TMV.DTO.ModelData;
using TMV.DTO.TransportPlan;
using TMV.DTO;
using TMV.DTO.Scale;

namespace TMV.Application.TransportPlan.Services
{
    
    public class TransportPlanServiceDM : ITransportPlanServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        public TransportPlanServiceDM(ISqlSugarClient db)
        {
            c = db;
        }

        public ResultEntity<bool> AddTransportPlan(TransportPlanModel model)
        {
            TMV_TransportPlan pt = GetMapperDTO.SetModel<TMV_TransportPlan, TransportPlanModel>(model); 

            var result = c.Insertable(pt).ExecuteCommand();
            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false);
            }
        }

        public ResultEntity<bool> DeTransportPlan(Guid id)
        {
            var result = c.Deleteable<TMV_TransportPlan>().In(id).ExecuteCommand();
            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true); 
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false);
            }
        }

        public ResultPageEntity<TransportPlanDTO> GetTransportPlanList(Request_TransportPlan dto, out int count)
        {
            int total = 0;
            Expression<Func<TMV_TransportPlan, bool>> expr = AutoAssemble.Splice<TMV_TransportPlan, Request_TransportPlan>(dto);

            var li = c.Queryable<TMV_TransportPlan>().Where(expr).ToPageList(dto.PageIndex, dto.PageSize, ref total);
            count = total;

            var list = li.Adapt<List<TransportPlanDTO>>();
            return new ResultPageEntity<TransportPlanDTO>() { Data = list, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = count };
             
             
        }

        public ResultEntity<bool> UpTransportPlan(TransportPlanModel model)
        {
            var data = c.Queryable<TMV_TransportPlan>().InSingle(model.Id);

            var result = c.Updateable(data).ExecuteCommand();
            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false); 
            }
        }
    }
}
