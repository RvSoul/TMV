using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMV.Core.CM;
using TMV.DTO.Authorization;
using TMV.DTO.Tr;
using TMV.DTO;
using TMV.Application.Scale.Services;
using TMV.Application.Tr.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utility;
using Microsoft.Extensions.Logging;

namespace TMV.Application.Tr
{
    [ApiDescriptionSettings("物流订单", Tag = "物流订单")]
    public class TrService : IDynamicApiController, ITransient
    {
        public ITrServiceDM dm;
        private readonly ILogger<TrService> _logger;

        public TrService(ITrServiceDM TrService, ILogger<TrService> logger)
        {
            _logger = logger;
            dm = TrService;
        }
        [HttpGet("AddTransportationRecords")]
        public ResultEntity<bool> AddTransportationRecords([FromQuery] TransportationRecordsModel model)
        {
            return dm.AddTransportationRecords(model);
        }

        [HttpGet("DeTransportationRecords")]
        public ResultEntity<bool> DeTransportationRecords(Guid id)
        {
            return dm.DeTransportationRecords(id);
        }

        [HttpGet("GetTransportationRecordsList")]
        public ResultPageEntity<TransportationRecordsDTO> GetTransportationRecordsList([FromQuery] Request_TransportationRecords dto)
        {
            return dm.GetTransportationRecordsList(dto);
        }

        [HttpGet("UpTransportationRecords")]
        public ResultEntity<bool> UpTransportationRecords([FromQuery] TransportationRecordsModel model)
        {
            return dm.UpTransportationRecords(model);
        }

        [AllowAnonymous]
        [HttpPost, HttpGet("GetDataInfo")]
        public ResultEntity<bool> GetDataInfo([FromBody] string data)
        {
            AuthorizationDTO dto = JsonHelper.FromJSON<AuthorizationDTO>(data);
            return dm.GetDataInfo(dto);
        }

        [AllowAnonymous]
        [HttpGet, HttpPost("GetDataInfo2")]
        public ResultEntity<bool> GetDataInfo2([FromQuery] AuthorizationDTO dto)
        {
            _logger.LogInformation(dto.ToJSON());
            return dm.GetDataInfo(dto);
        }

        [HttpGet("GetScalageRecordsList")]
        public ResultPageEntity<ScalageRecordsDTO> GetScalageRecordsList([FromQuery] Request_ScalageRecordsDTO dto)
        {
            return dm.GetScalageRecordsList(dto);
        }
    }
}
