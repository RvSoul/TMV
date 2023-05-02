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
using TMV.DTO;
using TMV.DTO.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TMV.Application.Scale.Services
{
    public class ScaleServiceDM : IScaleServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        public ScaleServiceDM(ISqlSugarClient db)
        {
            c = db;
        }

        public ResultEntity<bool> AddScale(ScaleModel model)
        {
            TMV_Scale data = new TMV_Scale();
            data.Id = Guid.NewGuid();
            data.Name = model.Name;
            data.Type = model.Type;
            data.State = 1;
            data.UpTime = DateTime.Now;


            var result = c.Insertable(data).ExecuteCommand();
            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false);
            }
        }

        public ResultEntity<bool> QtScale(Guid id)
        {

            var data = c.Queryable<TMV_Scale>().InSingle(id);
            //c.Queryable<TMV_Scale>().InSingle(id);
            if (data.State == 1)
            {
                data.State = 2;
            }
            else
            {
                data.State = 1;
            }
            data.UpTime = DateTime.Now;

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

        public ResultPageEntity<ScaleDTO> GetScaleList(Request_Scale dto)
        {
            int total = 0;
            Expression<Func<TMV_Scale, bool>> expr = AutoAssemble.Splice<TMV_Scale, Request_Scale>(dto);

            var li = c.Queryable<TMV_Scale>().Where(expr).ToPageList(dto.PageIndex, dto.PageSize, ref total);
            var list = li.Adapt<List<ScaleDTO>>();
            return new ResultPageEntity<ScaleDTO>() { Data = list, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = count }; 

        }

        public ResultEntity<bool> UpScale(ScaleModel model)
        {
            var data = c.Queryable<TMV_Scale>().InSingle(model.Id);
            data.Name = model.Name;
            data.Type = model.Type;

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
