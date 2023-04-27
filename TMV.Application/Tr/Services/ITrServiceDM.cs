using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Authorization;
using TMV.DTO.Tr;

namespace TMV.Application.Tr.Services
{
    public interface ITrServiceDM
    {
        List<TransportationRecordsDTO> GetTransportationRecordsList(Request_TransportationRecords dto, out int count);
        bool AddTransportationRecords(TransportationRecordsModel model);

        bool UpTransportationRecords(TransportationRecordsModel model);

        bool DeTransportationRecords(string id);
        bool GetDataInfo(AuthorizationDTO dto);
    }
}
