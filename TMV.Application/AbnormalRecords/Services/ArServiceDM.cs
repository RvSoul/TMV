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
            #region Eendregion
            Expression<Func<TMV_AbnormalRecords, bool>> expr = n => true;
            //if (!dto.Name.IsNullOrEmpty())
            //{
            //    TMV_Scale scale = c.Queryable<TMV_Scale>().Where(w => w.Name == dto.Name.ToString()).First();
            //    if (scale == null)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }

            //    List<TMV_ScalageRecords> srli = c.Queryable<TMV_ScalageRecords>().Where(w => w.ScaleId == scale.Id).ToList();
            //    if (srli.Count() == 0)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }
            //    List<Guid> li = srli.Select(s => s.TId).ToList();
            //    List<TMV_TransportationRecords> trli = c.Queryable<TMV_TransportationRecords>().Where(w => li.Contains(w.Id)).ToList();
            //    if (trli.Count() == 0)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }
            //    List<Guid> tridli = srli.Select(s => s.Id).ToList();


            //    expr = expr.And2(n => tridli.Contains(n.TId));
            //}
            //if (!dto.PlateNumber.IsNullOrEmpty())
            //{
            //    TMV_Car car = c.Queryable<TMV_Car>().Where(w => w.PlateNumber == dto.PlateNumber).First();
            //    if (car == null)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }

            //    List<TMV_TransportationRecords> trli = c.Queryable<TMV_TransportationRecords>().Where(w => w.CarId == car.Id).ToList();
            //    if (trli.Count() == 0)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }
            //    List<Guid> tridli = trli.Select(s => s.Id).ToList();


            //    expr = expr.And2(n => tridli.Contains(n.TId));
            //}
            //if (!dto.MineCode.IsNullOrEmpty())
            //{
            //    TMV_TransportPlan tp = c.Queryable<TMV_TransportPlan>().Where(w => w.MineCode == dto.MineCode && w.AddTime.Date == DateTime.Now.Date).First();
            //    if (tp == null)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }

            //    List<TMV_TransportationRecords> trli = c.Queryable<TMV_TransportationRecords>().Where(w => w.CollieryId == tp.Id).ToList();
            //    if (trli.Count() == 0)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }
            //    List<Guid> tridli = trli.Select(s => s.Id).ToList();


            //    expr = expr.And2(n => tridli.Contains(n.TId));
            //}

            //if (!dto.State.IsNullOrEmpty())
            //{
            //    List<TMV_TransportationRecords> trli = c.Queryable<TMV_TransportationRecords>().Where(w => w.State == Convert.ToInt32(dto.State)).ToList();
            //    if (trli.Count() == 0)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }
            //    List<Guid> tridli = trli.Select(s => s.Id).ToList();

            //    expr = expr.And2(n => tridli.Contains(n.TId));
            //}
            //if (!dto.UserName.IsNullOrEmpty())
            //{
            //    TMV_Users user = c.Queryable<TMV_Users>().Where(w => w.Name == dto.UserName).First();
            //    if (user != null)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }

            //    expr = expr.And2(w => w.UserId == user.Id);
            //}
            //if (!dto.TId.IsNullOrEmpty())
            //{

            //    expr = expr.And2(w => w.TId == Guid.Parse(dto.TId));
            //}
            //if (!dto.UserId.IsNullOrEmpty())
            //{

            //    expr = expr.And2(w => w.UserId == Guid.Parse(dto.UserId));
            //}

            //if (!dto.STime.IsNullOrEmpty())
            //{
            //    List<TMV_TransportationRecords> trli = c.Queryable<TMV_TransportationRecords>().Where(w => w.STime.Date >= Convert.ToDateTime(dto.STime).Date).ToList();
            //    if (trli.Count() == 0)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }
            //    List<Guid> tridli = trli.Select(s => s.Id).ToList();

            //    expr = expr.And2(n => tridli.Contains(n.TId));
            //}
            //if (!dto.ETime.IsNullOrEmpty())
            //{
            //    List<TMV_TransportationRecords> trli = c.Queryable<TMV_TransportationRecords>().Where(w => w.STime.Date <= Convert.ToDateTime(dto.ETime).Date).ToList();
            //    if (trli.Count() == 0)
            //    {
            //        return new ResultPageEntityUtil<AbnormalRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
            //    }
            //    List<Guid> tridli = trli.Select(s => s.Id).ToList();

            //    expr = expr.And2(n => tridli.Contains(n.TId));
            //}

            #endregion
            int total = 0;

            var query = c.Queryable<TMV_AbnormalRecords, TMV_TransportationRecords, TMV_Users>((a, b, u) => a.TId == b.Id && a.UserId == u.Id).WhereIF(!string.IsNullOrWhiteSpace(dto.Code),(a, b, u)=>b.Code== dto.Code)
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
