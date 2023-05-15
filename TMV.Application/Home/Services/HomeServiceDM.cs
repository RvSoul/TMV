using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Core.CM;
using TMV.DTO.Car;
using TMV.DTO;
using TMV.DTO.Tr;
using TMV.DTO.Home;
using Microsoft.Extensions.Logging;
using TMV.Application.Tr;
using TMV.Application.Tr.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using TMV.DTO.Authorization;
using NPOI.SS.Formula.Functions;
using SqlSugar.Extensions;

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
				.Take(5)
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
					PlateNumber = c.PlateNumber,
					ZScaleName = a.ZScaleName,
					QScaleName = a.QScaleName,
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
			dto.Jzzl = c.Queryable<TMV_TransportationRecords>().Where(w => w.STime.Date == DateTime.Now.Date).Sum(s => s.NetWeight).ObjToInt();
			dto.Jrcc = c.Queryable<TMV_TransportationRecords>().Where(w => w.STime.Date == DateTime.Now.Date).Count();
			dto.Gjcs = c.Queryable<TMV_AbnormalRecords>().Where(w => w.AddTime.Date == DateTime.Now.Date).Count();
			dto.Jrks = c.Queryable<TMV_TransportPlan>().Where(w => w.AddTime.Date == DateTime.Now.Date).Select(s => s.MineCode).ToList().ToHashSet().Count();

			return new ResultEntityUtil<HomeCountDTO>().Success(dto);
		}

		public ResultEntity<List<UploadDTO>> GetScData()
		{

			var li = c.Queryable<TMV_TransportationRecords, TMV_TransportPlan, TMV_Car>((a, b, c) => a.CollieryId == b.Id && a.CarId == c.Id).Where(a => a.IsUpload == 1)
			   .OrderByDescending(a => a.STime)
			   .Select((a, b, c) => new UploadDTO()
			   {
				   Code = a.Code,
				   ID = 0,
				   CHEPH = c.PlateNumber,
				   PINZ = b.CargoName,
				   MEIKDW = b.MEIKDW,
				   GONGYS = b.GONGYS,
				   FAZ = "汽",
				   DAOZ = "汽",
				   MAOZ = Convert.ToInt32(a.RoughWeight),
				   PIZ = Convert.ToInt32(a.TareWeight),
				   FAHRQ = b.SendTime.ToShortDateString(),
				   DAOHRQ = Convert.ToDateTime(b.ArrivalTime).ToShortDateString(),
				   KOUJ = "市场采购",
				   YUNSFS = "公路",
				   JIANJFS = "过衡",
				   JINCJJY = "无人值守",
				   CHUCJJY = "无人值守",
				   JINCSJ = a.STime.ToString("G"),
				   CHUCSJ = Convert.ToDateTime(a.ETime).ToString("G"),
				   KOUZ = a.KouWeight,
				   JINCHH = a.ZScaleName,
				   CHUCHH = a.QScaleName,
				   CHENGYDW = b.Carrier,
				   CHEC = "",

			   })
				.ToList();

			return new ResultEntityUtil<List<UploadDTO>>().Success(li);
		}

		public ResultEntity<bool> UpScData(List<string> li)
		{
			var data = c.Queryable<TMV_TransportationRecords>().Where(w => li.Contains(w.Code)).ToList();
			foreach (var record in data)
			{
				record.IsUpload = 2;
			}

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

		public ResultEntity<RecordDataDTO> GetRecordData(string PlateNumber)
		{
			var query = c.Queryable<TMV_TransportationRecords, TMV_TransportPlan, TMV_Car>((a, b, c) => a.CollieryId == b.Id && a.CarId == c.Id).OrderByDescending(a => a.STime)
			  .Where((a, b, c) => c.PlateNumber == PlateNumber)
			  .Select((a, b, c) => new RecordDataDTO()
			  {
				  inX = 0,
				  outX = 0,
				  Code = a.Code,
				  PlateNumber = c.PlateNumber,
				  DriverName = c.DriverName,
				  SendUnit = b.SendUnit,
				  ReceiptUnit = b.ReceiptUnit,
				  CargoName = b.CargoName,
				  Ggxh = "",
				  Carrier = b.Carrier,
				  Hdh = "",
				  ShipName = b.ShipName,
				  Xmd = "",
			  }).Mapper(x =>
			  {
				  x.scli = c.Queryable<TMV_ScalageRecords>().Where(w => w.TId == x.Id).OrderByDescending(o => o.AddTime).Select(
					 xx => new ScalageRecordsItem()
					 {
						 AddTime = xx.AddTime,
						 ScaleId = xx.ScaleId,
						 Weigh = xx.Weigh,
					 }).Mapper(xx =>
					 {
						 var sa = c.Queryable<TMV_Scale>().Where(w => w.Id == xx.ScaleId).First();
						 if (sa != null)
						 {
							 xx.ScaleName = sa.Name;
							 // xx.ScaleType = sa.Type;
							 if (sa.Type == 1) xx.ScaleType = "重衡";
							 if (sa.Type == 2) xx.ScaleType = "混合衡";
							 if (sa.Type == 3) xx.ScaleType = "轻衡";
						 }
					 }).ToList();
			  })
			  .First();

			return new ResultEntityUtil<RecordDataDTO>().Success(query);
		}
	}
}
