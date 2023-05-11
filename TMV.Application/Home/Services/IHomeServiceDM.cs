using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Car;
using TMV.DTO;
using TMV.DTO.Tr;
using TMV.DTO.Count;

namespace TMV.Application.Home.Services
{
    internal interface IHomeServiceDM
    {
        ResultEntity<HomeCountDTO> GetHomeCount();

        /// <summary>
        /// 获取首页物流订单
        /// </summary>
        /// <returns></returns>
        ResultEntity<List<TransportationRecordsDTO>> GetHomeTransportationRecordsList();
    }
}
