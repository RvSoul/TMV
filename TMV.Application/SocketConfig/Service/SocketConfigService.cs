using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Tr;
using TMV.Core.CM;
using TMV.DTO;
using TMV.DTO.SocketConfig;

namespace TMV.Application.SocketConfig.Service
{
    public class SocketConfigService: ISocketConfigService,ITransient
    {
        ISqlSugarClient _db;
        private readonly ILogger<SocketConfigService> _logger;
        public SocketConfigService(ISqlSugarClient db, ILogger<SocketConfigService> logger)
        {
            _logger = logger;
            _db = db;
        }
        public ResultEntity<SocketConfigDto> GetSocketConfig(string ip)
        {
            var model = _db.Queryable<TMV_SocketConfig>().Where(x => x.Ip == ip).First();
            var result = model.Adapt<SocketConfigDto>();
            return new ResultEntity<SocketConfigDto>() { Data = result, IsSuccess = true };
        }
    }
}
