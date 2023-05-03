using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMV.Core.CM;
using TMV.DTO;
using TMV.DTO.Authorization;
using TMV.DTO.Tr;
using TMV.DTO.ModelData;
using Furion.LinqBuilder;
using TMV.DTO.Users;
using AutoMapper.Internal;

namespace TMV.Application.Tr.Services
{
    public class TrServiceDM : ITrServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        public TrServiceDM(ISqlSugarClient db)
        {
            c = db;
        }

        public ResultEntity<bool> AddTransportationRecords(TransportationRecordsModel model)
        {
            throw new NotImplementedException();
        }

        public ResultEntity<bool> DeTransportationRecords(Guid id)
        {
            throw new NotImplementedException();
        }

        public ResultPageEntity<TransportationRecordsDTO> GetTransportationRecordsList(Request_TransportationRecords dto)
        {

            Expression<Func<TMV_TransportationRecords, bool>> expr = n => true;
            if (!dto.Name.IsNullOrEmpty())
            {
                TMV_Scale scale = c.Queryable<TMV_Scale>().Where(w => w.Name == dto.Name.ToString()).First();
                if (scale == null)
                {
                    return new ResultPageEntityUtil<TransportationRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
                }

                List<TMV_ScalageRecords> srli = c.Queryable<TMV_ScalageRecords>().Where(w => w.ScaleId == scale.Id).ToList();
                if (srli.Count() == 0)
                {
                    return new ResultPageEntityUtil<TransportationRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
                }
                List<Guid> li = srli.Select(s => s.TId).ToList();
                expr = expr.And2(n => li.Contains(n.Id));
            }
            if (!dto.PlateNumber.IsNullOrEmpty())
            {
                TMV_Car car = c.Queryable<TMV_Car>().Where(w => w.PlateNumber == dto.PlateNumber).First();
                if (car == null)
                {
                    return new ResultPageEntityUtil<TransportationRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
                }

                expr = expr.And2(n => n.CarId == car.Id);
            }
            if (!dto.MineCode.IsNullOrEmpty())
            {
                TMV_TransportPlan tp = c.Queryable<TMV_TransportPlan>().Where(w => w.MineCode == dto.MineCode && w.AddTime.Date == DateTime.Now.Date).First();
                if (tp == null)
                {
                    return new ResultPageEntityUtil<TransportationRecordsDTO>().Success(null, dto.PageIndex, dto.PageSize, 0);
                }

                expr = expr.And2(n => n.CollieryId == tp.Id);
            }
            if (dto.STime != null)
            {
                expr = expr.And2(w => w.STime.Date >= DateTime.Parse(dto.STime).Date);
            }
            if (dto.ETime != null)
            {
                expr = expr.And2(w => w.STime.Date <= DateTime.Parse(dto.ETime).Date);
            }

            int count = 0;
            var query = c.Queryable<TMV_TransportationRecords>().Where(expr).OrderByDescending(px => px.STime).ToPageList(dto.PageIndex, dto.PageSize, ref count);
            var list = query.Adapt<List<TransportationRecordsDTO>>();
            return new ResultPageEntity<TransportationRecordsDTO>() { Data = list, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = count };
        }

        public ResultEntity<bool> UpTransportationRecords(TransportationRecordsModel model)
        {
            throw new NotImplementedException();
        }

        public ResultEntity<bool> GetDataInfo(AuthorizationDTO dto)
        {
            if (dto.State == 0)
            {
                return new ResultEntityUtil<bool>().Success(true, "连接成功！");
            }
            TMV_Car car = c.Queryable<TMV_Car>().Where(w => w.PlateNumber == dto.PlateNumber).First();
            if (car == null)
            {
                return new ResultEntityUtil<bool>().Failure("车牌号不存在！");
            }
            TMV_Scale scale = c.Queryable<TMV_Scale>().Where(w => w.Name == dto.ClassName.ToString()).First();
            if (scale == null)
            {
                return new ResultEntityUtil<bool>().Failure("衡不存在！");
            }
            TMV_TransportPlan tp = c.Queryable<TMV_TransportPlan>().Where(w => w.MineCode == dto.CollieryCode && w.AddTime.Date == DateTime.Now.Date).First();
            if (tp == null)
            {
                return new ResultEntityUtil<bool>().Failure("运输计划不存在！");
            }

            int scaleNum = c.Queryable<TMV_Scale>().Where(w => w.Type == 1 && w.State == 1).Count();
            List<DateTime> timeLi = c.Queryable<TMV_TransportationRecords>().OrderByDescending(px => px.STime).Take(scaleNum).Select(x => x.STime).ToList();


            TMV_TransportationRecords oldTr = c.Queryable<TMV_TransportationRecords>().Where(w => timeLi.Contains(w.STime) && w.CarId == car.Id && w.CollieryId == tp.Id).First();
            if (oldTr != null)
            {
                #region 有上一趟数据
                if (oldTr.ETime == null && oldTr.TareWeight == null && oldTr.NetWeight == null)
                {
                    #region 未记录皮重（未轻衡称重）
                    if (scale.Type == 1)
                    {
                        #region 重衡
                        return new ResultEntityUtil<bool>().Failure("有上一趟数据/未轻衡称重/重衡：异常还是重新称重！");
                        #endregion
                    }
                    else
                    {
                        #region 轻衡
                        TMV_ScalageRecords sr = new TMV_ScalageRecords();
                        sr.Id = Guid.NewGuid();
                        sr.ScaleId = scale.Id;
                        sr.TId = oldTr.Id;
                        sr.Weigh = dto.Weight;
                        sr.AddTime = DateTime.Now;
                        c.Insertable(sr).ExecuteCommand();

                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 已记录皮重（已轻衡称重）
                    if (scale.Type == 1)
                    {
                        #region 重衡
                        return new ResultEntityUtil<bool>().Failure("有上一趟数据/已轻衡称重/重衡：异常！");
                        #endregion
                    }
                    else
                    {
                        #region 轻衡
                        return new ResultEntityUtil<bool>().Failure("有上一趟数据/已轻衡称重/轻衡：异常还是重新称重！");
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                #region 没有有上一趟数据
                if (scale.Type == 1)
                {
                    #region 重衡
                    TMV_TransportationRecords tr = new TMV_TransportationRecords();
                    tr.Id = Guid.NewGuid();
                    tr.CarId = car.Id;
                    tr.CollieryId = tp.Id;
                    tr.RoughWeight = dto.Weight;
                    tr.STime = DateTime.Now;
                    tr.IsUpload = 1;
                    tr.State = 1;

                    TMV_ScalageRecords sr = new TMV_ScalageRecords();
                    sr.Id = Guid.NewGuid();
                    sr.ScaleId = scale.Id;
                    sr.TId = tr.Id;
                    sr.Weigh = dto.Weight;
                    sr.AddTime = DateTime.Now;

                    if (dto.inX == 1)
                    {
                        tr.State = 2;
                        TMV_AbnormalRecords ar = new TMV_AbnormalRecords();
                        ar.Id = Guid.NewGuid();
                        ar.TId = tr.Id;
                        ar.AbnormalCause = "入口光幕阻挡";

                        c.Insertable(ar).ExecuteCommand();
                    }
                    if (dto.outX == 1)
                    {
                        tr.State = 2;
                        TMV_AbnormalRecords ar = new TMV_AbnormalRecords();
                        ar.Id = Guid.NewGuid();
                        ar.TId = tr.Id;
                        ar.AbnormalCause = "出口光幕阻挡";
                        c.Insertable(ar).ExecuteCommand();
                    }
                    if (dto.Error != 0)
                    {
                        tr.State = 2;
                        TMV_AbnormalRecords ar = new TMV_AbnormalRecords();
                        ar.Id = Guid.NewGuid();
                        ar.TId = tr.Id;
                        ar.AbnormalCause = "错误码错误";
                        c.Insertable(ar).ExecuteCommand();
                    }
                    c.Insertable(tr).ExecuteCommand();
                    c.Insertable(sr).ExecuteCommand();
                    #endregion
                }
                else
                {
                    #region 轻衡
                    return new ResultEntityUtil<bool>().Failure("没有有上一趟数据/轻衡：异常！");

                    #endregion
                }
                #endregion
            }
            return new ResultEntityUtil<bool>().Success(true);
        }
    }
}
