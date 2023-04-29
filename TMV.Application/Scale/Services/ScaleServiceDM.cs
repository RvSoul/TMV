using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Scale;
using TMV.Core.CM;
using TMV.DTO.Scale;
using TMV.DTO.ModelData;
using TMV.DTO.Scale;

namespace TMV.Application.Scale.Services
{
    public class ScaleServiceDM : IScaleServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        public ScaleServiceDM(ISqlSugarClient db)
        {
            c = db;
        }

        public bool AddScale(ScaleModel model)
        {
            TMV_Scale pt = GetMapperDTO.SetModel<TMV_Scale, ScaleModel>(model);

            var result = c.Insertable(pt).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool QtScale(Guid id)
        {
            var result = c.Deleteable<TMV_Scale>().In(id).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ScaleDTO> GetScaleList(Request_Scale dto, out int count)
        {
            int total = 0;
            Expression<Func<TMV_Scale, bool>> expr = AutoAssemble.Splice<TMV_Scale, Request_Scale>(dto);

            var li = c.Queryable<TMV_Scale>().Where(expr).ToPageList(dto.PageIndex, dto.PageSize, ref total);
            count = total;

            return GetMapperDTO.GetDTOList<TMV_Scale, ScaleDTO>(li);
        }

        public bool UpScale(ScaleModel model)
        {
            var data = c.Queryable<TMV_Scale>().InSingle(model.Id);

            var result = c.Updateable(data).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
