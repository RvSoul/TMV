using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.SocketConfig;
using TMV.DTO;

namespace TMV.Application.SocketConfig.Service
{
    public interface ISocketConfigService
    {
        ResultEntity<SocketConfigDto> GetSocketConfig(string ip);
    }
}
