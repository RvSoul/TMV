using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;
using TMV.DTO.Authorization;
using TMV.DTO.Tr;

namespace TMV.Application.Tr.Services
{
    public interface ITrServiceDM
    {
        ResultPageEntity<TransportationRecordsDTO> GetTransportationRecordsList(Request_TransportationRecords dto );
        ResultEntity<bool> AddTransportationRecords(TransportationRecordsModel model);

        ResultEntity<bool> UpTransportationRecords(TransportationRecordsModel model);

        ResultEntity<bool> DeTransportationRecords(Guid id);
        ResultEntity<bool> GetDataInfo(AuthorizationDTO dto);
    }
}
