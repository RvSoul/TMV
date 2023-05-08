using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.AbnormalRecords.Services;
using TMV.Core.CM;
using TMV.DTO.Abnormal;
using TMV.DTO;
using Furion.LinqBuilder;

namespace TMV.Application.Abnormal.Servives
{

    public class AbnormalServiceDM : IAbnormalServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        public AbnormalServiceDM(ISqlSugarClient db)
        {
            c = db;
        }

        public ResultEntity<bool> AddAbnormal(AbnormalModel model)
        {

            TMV_Abnormal pt = model.Adapt<TMV_Abnormal>();
            pt.Id = Guid.NewGuid();

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

        public ResultEntity<bool> DeAbnormal(Guid id)
        {
            var result = c.Deleteable<TMV_Abnormal>().In(id).ExecuteCommand();
            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false);
            }
        }

        public ResultPageEntity<AbnormalDTO> GetAbnormalList(Request_Abnormal dto)
        {
            int total = 0;
            var expr = Expressionable.Create<TMV_Abnormal>();
            if (!dto.Code.IsNullOrEmpty())
            {
                expr = expr.And(n => n.Code == Convert.ToInt32(dto.Code));
            }

            var li = c.Queryable<TMV_Abnormal>().Where(expr.ToExpression()).ToPageList(dto.PageIndex, dto.PageSize, ref total);

            var list = li.Adapt<List<AbnormalDTO>>();
            return new ResultPageEntity<AbnormalDTO>() { Data = list, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = total };
        }

        public ResultEntity<bool> UpAbnormal(AbnormalModel model)
        {


            var data = model.Adapt<TMV_Abnormal>();

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