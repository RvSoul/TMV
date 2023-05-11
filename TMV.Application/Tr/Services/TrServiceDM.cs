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
using NPOI.SS.Formula.Functions;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling.Internal;

namespace TMV.Application.Tr.Services
{
    public class TrServiceDM : ITrServiceDM, IDynamicApiController, ITransient
    {
        ISqlSugarClient c;
        private readonly ILogger<TrService> _logger;
        public TrServiceDM(ISqlSugarClient db, ILogger<TrService> logger)
        {
            _logger = logger;
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
            var exp = Expressionable.Create<TMV_TransportationRecords>();
            if (!dto.STime.IsNullOrEmpty())
            {
                exp = exp.And(a => a.STime.Date >= DateTime.Parse(dto.STime).Date);
            }
            if (!dto.ETime.IsNullOrEmpty())
            {
                exp = exp.And(a => a.STime.Date <= DateTime.Parse(dto.ETime).Date);
            }
            if (!dto.Code.IsNullOrEmpty())
            {
                exp = exp.And(a => a.Code.Contains(dto.Code));
            }
            if (!dto.State.IsNullOrEmpty())
            {
                exp = exp.And(a => a.State == Convert.ToInt32(dto.State));
            }
            int count = 0;


            var query = c.Queryable<TMV_TransportationRecords, TMV_TransportPlan, TMV_Car>((a, b, c) => a.CollieryId == b.Id && a.CarId == c.Id).OrderByDescending(a => a.STime)
                .Where(exp.ToExpression())
                .WhereIF(!dto.PlateNumber.IsNullOrEmpty(), (a, b, c) => c.PlateNumber == dto.PlateNumber)
                .WhereIF(!dto.MineCode.IsNullOrEmpty(), (a, b, c) => b.MineCode == dto.MineCode)
                .Select((a, b, c) => new TransportationRecordsDTO()
                {
                    Id = a.Id,
                    CarId = a.CarId,
                    CollieryId = a.CollieryId,
                    ETime = a.ETime,
                    IsUpload = a.IsUpload,
                    NetWeight = a.NetWeight,
                    RoughWeight = a.RoughWeight,
                    State = a.State,
                    STime = a.STime,
                    TareWeight = a.TareWeight,
                    Code = a.Code,
                    KouWeight = a.KouWeight,
                    MineCode = b.MineCode,
                    PlateNumber = c.PlateNumber
                }).Mapper(x =>
                        {
                            x.ScalageRecordsData = c.Queryable<TMV_ScalageRecords>().Where(w => w.TId == x.Id).OrderByDescending(o => o.AddTime).Select(
                             xx => new ScalageRecordsDTO()
                             {
                                 AddTime = xx.AddTime,
                                 Id = xx.Id,
                                 ScaleId = xx.ScaleId,
                                 TId = xx.TId,
                                 Weigh = xx.Weigh,
                             }).Mapper(xx =>
                             {
                                 var sa = c.Queryable<TMV_Scale>().Where(w => w.Id == xx.ScaleId).First();
                                 if (sa != null)
                                 {
                                     xx.ScaleName = sa.Name;
                                     // xx.ScaleType = sa.Type;
                                     if (sa.Type == 1) xx.ScaleType = "重衡";
                                     if (sa.Type == 2) xx.ScaleType = "轻衡";
                                     if (sa.Type == 3) xx.ScaleType = "混合衡";


                                 }
                             }).ToList();
                            x.MineCode = c.Queryable<TMV_TransportPlan>().Where(w => w.Id == x.CollieryId).First().MineCode;
                            x.PlateNumber = c.Queryable<TMV_Car>().Where(w => w.Id == x.CarId).First().PlateNumber;

                        })
                .ToPageList(dto.PageIndex, dto.PageSize, ref count);
            return new ResultPageEntity<TransportationRecordsDTO>() { Data = query, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = count };
        }



        public ResultEntity<bool> UpTransportationRecords(TransportationRecordsModel model)
        {
            throw new NotImplementedException();
        }




        public ResultPageEntity<ScalageRecordsDTO> GetScalageRecordsList(Request_ScalageRecordsDTO dto)
        {

            var expr = Expressionable.Create<TMV_ScalageRecords>();

            if (dto.ScaleId != null)
            {
                expr = expr.And(w => w.ScaleId == Guid.Parse(dto.ScaleId));
            }
            if (dto.TId != null)
            {
                expr = expr.And(w => w.TId == Guid.Parse(dto.TId));
            }

            int count = 0;
            var query = c.Queryable<TMV_ScalageRecords, TMV_Scale>((a, b) => a.ScaleId == b.Id)
                .Where(expr.ToExpression()).OrderByDescending(px => px.AddTime)
                .Select((a, b) => new ScalageRecordsDTO()
                {
                    AddTime = a.AddTime,
                    Id = a.Id,
                    ScaleId = a.ScaleId,
                    TId = a.TId,
                    Weigh = a.Weigh,
                    ScaleName = b.Name,
                    ScaleType = b.Type.ToString(),
                }
                ).ToPageList(dto.PageIndex, dto.PageSize, ref count);

            return new ResultPageEntity<ScalageRecordsDTO>() { Data = query, PageIndex = dto.PageIndex, PageSize = dto.PageSize, Count = count };
        }


        public ResultEntity<bool> SetDataInfo(AuthorizationDTO dto)
        {

            return new ResultEntityUtil<bool>().Success(true);
        }





        [AllowAnonymous]
        public ResultInfo GetDataInfo1(AuthorizationDTO dto)
        {
            if (dto.State == 0)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "连接成功-无效数据！");
            }
            TMV_Car car = c.Queryable<TMV_Car>().Where(w => w.PlateNumber == dto.PlateNumber).First();
            if (car == null)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "车牌号不存在！");
            }

            TMV_TransportPlan tp = c.Queryable<TMV_TransportPlan>().Where(w => w.MineCode == dto.CollieryCode && w.AddTime.Date == DateTime.Now.Date).First();
            if (tp == null)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "矿号-运输计划不存在！");
            }
            //int scaleNum = c.Queryable<TMV_Scale>().Where(w => w.Type == 1 && w.State == 1).Count();
            //List<DateTime> timeLi = c.Queryable<TMV_TransportationRecords>().OrderByDescending(px => px.STime).Take(scaleNum).Select(x => x.STime.Date).ToList();
            TMV_TransportationRecords tr = new TMV_TransportationRecords();
            tr.Id = Guid.NewGuid();
            tr.State = 1;


            TMV_TransportationRecords oldTr = c.Queryable<TMV_TransportationRecords>().Where(w => w.CarId == car.Id && w.CollieryId == tp.Id).First();
            if (oldTr != null)
            {
                #region 有上一趟数据
                if (oldTr.ETime == null && oldTr.TareWeight == null && oldTr.NetWeight == null)
                {
                    #region 未记录皮重（未轻衡称重）
                    return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "有上一趟数据/未轻衡称重：上一趟没有出厂怎么又进场绑卡了！");
                    #endregion
                }
                else
                {
                    #region 已记录皮重（已轻衡称重）
                    TimeSpan ts = DateTime.Now - oldTr.STime;
                    if (ts.Minutes < 60)
                    {
                        //一个小时内再次进厂告警
                        tr.State = 2;
                        TMV_AbnormalRecords ar = new TMV_AbnormalRecords();
                        ar.Id = Guid.NewGuid();
                        ar.TId = tr.Id;
                        ar.AbnormalCause = "进场/出场时间报警";
                        ar.AddTime = DateTime.Now;
                        c.Insertable(ar).ExecuteCommand();
                    }
                    #endregion
                }
                #endregion
            }

            #region 绑卡操作 

            string ycode = "";
            do
            {
                ycode = "DT" + DateTime.Now.ToString("yyMMddHHmmssfff");
            } while (c.Queryable<TMV_TransportationRecords>().Where(n => n.Code == ycode).First() != null);
            tr.Code = ycode;

            tr.CarId = car.Id;
            tr.CollieryId = tp.Id;
            tr.RoughWeight = dto.Weight;
            tr.STime = DateTime.Now;
            tr.IsUpload = 1;
            tr.KouWeight = 0;

            c.Insertable(tr).ExecuteCommand();

            #endregion

            return new ResultInfoUtil().Success(dto.ID, dto.Sn, "1", "绑卡完成！");
        }
        [AllowAnonymous]
        public ResultInfo GetDataInfo2(AuthorizationDTO dto)
        {
            #region 数据验证
            if (dto.State == 0)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "连接成功-无效数据！");
            }
            if (dto.Inside == 0)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "未正常停靠！");
            }
            if (dto.Finish == 0)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "未锁称！");
            }

            TMV_Car car = c.Queryable<TMV_Car>().Where(w => w.PlateNumber == dto.PlateNumber).First();
            if (car == null)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "车牌号不存在！");
            }
            TMV_Scale scale = c.Queryable<TMV_Scale>().Where(w => w.Name == dto.ClassName.ToString()).First();
            if (scale == null)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "衡不存在！");
            }
            TMV_TransportPlan tp = c.Queryable<TMV_TransportPlan>().Where(w => w.MineCode == dto.CollieryCode && w.AddTime.Date == DateTime.Now.Date).First();
            if (tp == null)
            {
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "矿号-运输计划不存在！");
            }
            #endregion

            TMV_TransportationRecords oldTr = c.Queryable<TMV_TransportationRecords>().Where(w => w.CarId == car.Id && w.CollieryId == tp.Id && w.ETime == null).First();
            if (oldTr != null)
            {
                #region 有上一趟数据

                TimeSpan ts = DateTime.Now - oldTr.STime;
                if (ts.Minutes < 3)
                {
                    //3分钟内表示未采样 
                    return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "未采样！");
                }

                if (scale.Type == 1)
                {
                    #region 重衡
                    if (oldTr.RoughWeight == null)
                    {
                        #region 未记录毛重
                        TMV_ScalageRecords sr = new TMV_ScalageRecords();
                        sr.Id = Guid.NewGuid();
                        sr.ScaleId = scale.Id;
                        sr.TId = oldTr.Id;
                        sr.Weigh = dto.Weight;
                        sr.AddTime = DateTime.Now;
                        c.Insertable(sr).ExecuteCommand();
                        oldTr.RoughWeight = dto.Weight;
                        #endregion
                    }
                    else
                    {
                        #region 已记录毛重-重新称重
                        TMV_ScalageRecords sr = new TMV_ScalageRecords();
                        sr.Id = Guid.NewGuid();
                        sr.ScaleId = scale.Id;
                        sr.TId = oldTr.Id;
                        sr.Weigh = dto.Weight;
                        sr.AddTime = DateTime.Now;
                        c.Insertable(sr).ExecuteCommand();
                        oldTr.RoughWeight = dto.Weight;

                        oldTr.State = 2;
                        TMV_AbnormalRecords ar = new TMV_AbnormalRecords();
                        ar.Id = Guid.NewGuid();
                        ar.TId = oldTr.Id;
                        ar.AbnormalCause = "已记录毛重-重新过重衡";
                        ar.AddTime = DateTime.Now;
                        c.Insertable(ar).ExecuteCommand();
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 轻衡
                    if (dto.Weight < car.EmptyWeight)
                    {
                        return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "皮重小于空水空油重量异常！");
                    }
                    if (dto.Weight > car.FullWeight)
                    {
                        return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "皮重大于满水满油重量异常！");
                    }

                    if (oldTr.RoughWeight == null)
                    {
                        #region 未记录毛重
                        return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "未记录毛重：请先到重衡上去记录毛重！");
                        #endregion
                    }
                    else
                    {

                        #region 已记录毛重-轻衡称重
                        if (oldTr.TareWeight == null && oldTr.NetWeight == null)
                        {
                            #region 未记录皮重和净重
                            TMV_ScalageRecords sr = new TMV_ScalageRecords();
                            sr.Id = Guid.NewGuid();
                            sr.ScaleId = scale.Id;
                            sr.TId = oldTr.Id;
                            sr.Weigh = dto.Weight;
                            sr.AddTime = DateTime.Now;
                            c.Insertable(sr).ExecuteCommand();
                            oldTr.TareWeight = dto.Weight;
                            oldTr.NetWeight = oldTr.RoughWeight - dto.Weight;
                            #endregion
                        }
                        else
                        {
                            #region 已记录皮重和净重-重新称重
                            TMV_ScalageRecords sr = new TMV_ScalageRecords();
                            sr.Id = Guid.NewGuid();
                            sr.ScaleId = scale.Id;
                            sr.TId = oldTr.Id;
                            sr.Weigh = dto.Weight;
                            sr.AddTime = DateTime.Now;
                            c.Insertable(sr).ExecuteCommand();
                            oldTr.TareWeight = dto.Weight;
                            oldTr.NetWeight = oldTr.RoughWeight - dto.Weight;

                            oldTr.State = 2;
                            TMV_AbnormalRecords ar = new TMV_AbnormalRecords();
                            ar.Id = Guid.NewGuid();
                            ar.TId = oldTr.Id;
                            ar.AbnormalCause = "已记录皮重和净重-重新过轻衡";
                            ar.AddTime = DateTime.Now;
                            c.Insertable(ar).ExecuteCommand();
                            #endregion
                        }


                        #endregion
                    }
                    #endregion
                }
                c.Updateable(oldTr).ExecuteCommand();
                #endregion
            }
            else
            {
                #region 没有有上一趟数据
                return new ResultInfoUtil().Failure(dto.ID, dto.Sn, "1", "0", "没有有绑卡！");
                #endregion
            }
            return new ResultInfoUtil().Success(dto.ID, dto.Sn, "1", "称重完成！");
        }
    }
}
