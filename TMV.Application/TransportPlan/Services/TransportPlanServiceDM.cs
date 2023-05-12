using TMV.Core.CM;
using TMV.DTO.TransportPlan;
using TMV.DTO;
using Furion.LinqBuilder;

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
            TMV_TransportPlan tp = c.Queryable<TMV_TransportPlan>().Where(w => w.MineCode == model.MineCode && w.AddTime.Date == DateTime.Now.Date).First();
            if (tp != null)
            {
                return new ResultEntityUtil<bool>().Failure("矿号已经存在");
            }
             
            TMV_TransportPlan pt = model.Adapt<TMV_TransportPlan>();
            pt.AddTime=DateTime.Now;

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

        public ResultPageEntity<TransportPlanDTO> GetTransportPlanList(Request_TransportPlan dto)
        {
            int total = 0;
            var expr = Expressionable.Create<TMV_TransportPlan>(); 
            if (!dto.Code.IsNullOrEmpty())
            {
                expr = expr.And(n => n.Code == dto.Code);
            }
            if (!dto.CargoName.IsNullOrEmpty())
            {
                expr = expr.And(n => n.CargoName == dto.CargoName);
            }

            if (!dto.Carrier.IsNullOrEmpty())
            {
                expr = expr.And(n => n.Carrier == dto.Carrier);
            }

            if (!dto.Sampling.IsNullOrEmpty())
            {
                expr = expr.And(n => n.Sampling == Convert.ToInt32(dto.Sampling));
            }

            if (!dto.StartAddTime.IsNullOrEmpty())
            {
                expr = expr.And(n => n.AddTime.Date >= Convert.ToDateTime(dto.StartAddTime).Date);
            }
            if (!dto.EndAddTime.IsNullOrEmpty())
            {
                expr = expr.And(n => n.AddTime.Date <= Convert.ToDateTime(dto.EndAddTime).Date);
            }

            var li = c.Queryable<TMV_TransportPlan>().Where(expr.ToExpression()).OrderByDescending(px => px.AddTime).ToPageList(dto.PageIndex, dto.PageSize, ref total);

            var list = li.Adapt<List<TransportPlanDTO>>().OrderByDescending(px => px.AddTime).ToList();
            return new ResultPageEntity<TransportPlanDTO>() { Data = list, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = total };
        }

        public ResultEntity<bool> UpTransportPlan(TransportPlanModel model)
        {
            TMV_TransportPlan tp = c.Queryable<TMV_TransportPlan>().Where(w => w.MineCode == model.MineCode && w.AddTime.Date == DateTime.Now.Date && w.Id != model.Id).First();
            if (tp != null)
            {
                return new ResultEntityUtil<bool>().Failure("矿号已经存在");
            }

            var data = model.Adapt<TMV_TransportPlan>();

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
