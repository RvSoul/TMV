using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Core.CM;
using TMV.DTO.Car;
using TMV.DTO;
using TMV.DTO.Tr;
using TMV.DTO.Count;
using Microsoft.Extensions.Logging;
using TMV.Application.Tr;
using TMV.Application.Tr.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using TMV.DTO.Authorization;

namespace TMV.Application.Home.Services
{
    public class HomeServiceDM : IHomeServiceDM, IDynamicApiController, ITransient
    {

        ISqlSugarClient c;
        private readonly ILogger<TrService> _logger;
        public HomeServiceDM(ISqlSugarClient db, ILogger<TrService> logger)
        {
            _logger = logger;
            c = db;
        }

        public ResultEntity<List<TransportationRecordsDTO>> GetHomeTransportationRecordsList()
        {
            var query = c.Queryable<TMV_TransportationRecords, TMV_TransportPlan, TMV_Car>((a, b, c) => a.CollieryId == b.Id && a.CarId == c.Id)
                .OrderByDescending(a => a.STime)
                //.Take(5)
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
                    x.MineCode = c.Queryable<TMV_TransportPlan>().Where(w => w.Id == x.CollieryId).First().MineCode;
                    x.PlateNumber = c.Queryable<TMV_Car>().Where(w => w.Id == x.CarId).First().PlateNumber;

                })
                 .ToList();
            return new ResultEntityUtil<List<TransportationRecordsDTO>>().Success(query);
        }

        public ResultEntity<HomeCountDTO> GetHomeCount()
        {
            HomeCountDTO dto = new HomeCountDTO();
            dto.Jzzl = c.Queryable<TMV_TransportationRecords>().Where(w => w.STime.Date == DateTime.Now.Date).Sum(s => s.NetWeight);
            dto.Jrcc = c.Queryable<TMV_TransportationRecords>().Where(w => w.STime.Date == DateTime.Now.Date).Count();
            dto.Gjcs = c.Queryable<TMV_AbnormalRecords>().Where(w => w.AddTime.Date == DateTime.Now.Date).Count();
            dto.Jrks = c.Queryable<TMV_TransportPlan>().Where(w => w.AddTime.Date == DateTime.Now.Date).Select(s => s.MineCode).ToList().ToHashSet().Count();

            return new ResultEntityUtil<HomeCountDTO>().Success(dto);
        }

        public ResultEntity<List<UploadDTO>> GetScData()
        {
            List<UploadDTO> li = new List<UploadDTO>();


            return new ResultEntityUtil<List<UploadDTO>>().Success(li);
        }
    }
}
