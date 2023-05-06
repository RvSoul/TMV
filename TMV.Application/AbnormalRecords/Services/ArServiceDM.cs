using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.AbnormalRecords.Services;
using TMV.Core.CM;
using TMV.DTO.AbnormalRecords;
using TMV.DTO;
using TMV.DTO.ModelData;
using Furion.LinqBuilder;
using TMV.DTO.Tr;
using StackExchange.Profiling.Internal;

namespace TMV.Application.AbnormalRecords.Services
{
    public class ArServiceDM : IArServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        public ArServiceDM(ISqlSugarClient db)
        {
            c = db;
        }

        public ResultPageEntity<AbnormalRecordsDTO> GetAbnormalRecordsList(Request_AbnormalRecords dto)
        {
            int total = 0;

            var query = c.Queryable<TMV_AbnormalRecords, TMV_TransportationRecords, TMV_Users>((a, b, u) => a.TId == b.Id && a.UserId == u.Id)
                .WhereIF(!string.IsNullOrWhiteSpace(dto.Code), (a, b, u) => b.Code == dto.Code)
                .Select((a, b, u) => new AbnormalRecordsDTO()
                {
                    Id = a.Id,
                    AbnormalCause = a.AbnormalCause,
                    Code = b.Code,
                    Disposal = a.Disposal,
                    DisposalTime = a.DisposalTime,
                    TId = a.TId,
                    UserId = a.UserId,
                    UserName = u.Name
                })
                .ToPageList(dto.PageIndex, dto.PageSize, ref total);
            return new ResultPageEntity<AbnormalRecordsDTO>() { Data = query, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = total };
        }



        public ResultEntity<bool> DeAbnormalRecords(Guid id)
        {
            var result = c.Deleteable<TMV_AbnormalRecords>().In(id).ExecuteCommand();

            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Success(false);
            }
        }



        public ResultEntity<bool> UpAbnormalRecords(AbnormalRecordsModel model)
        {
            var data = c.Queryable<TMV_AbnormalRecords>().Where(w => w.Id == model.Id).First();

            data.UserId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            data.Disposal = model.Disposal;
            data.DisposalTime = DateTime.Now;

            var arli = c.Queryable<TMV_AbnormalRecords>().Where(w => w.TId == data.TId).ToList();
            TMV_TransportationRecords tr = c.Queryable<TMV_TransportationRecords>().Where(w => w.Id == data.TId).First();
            if (tr == null) return new ResultEntityUtil<bool>().Failure(false, "订单不存在");
            tr.State = 1;
            foreach (var item in arli)
            {
                if (item.Disposal.IsNullOrEmpty())
                {
                    tr.State = 2;
                    break;
                }
            }
            c.Updateable(tr).ExecuteCommand();

            var result = c.Updateable(data).ExecuteCommand();
            if (result > 0)
            {
                return new ResultEntityUtil<bool>().Success(true);
            }
            else
            {
                return new ResultEntityUtil<bool>().Failure(false);
            }
        }


    }
}
