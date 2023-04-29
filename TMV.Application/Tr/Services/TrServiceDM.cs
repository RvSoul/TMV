using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Authorization;
using TMV.DTO.Tr;

namespace TMV.Application.Tr.Services
{
    public class TrServiceDM : ITrServiceDM, IDynamicApiController, ITransient
    {
        public bool AddTransportationRecords(TransportationRecordsModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeTransportationRecords(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool GetDataInfo(AuthorizationDTO dto)
        {
            throw new NotImplementedException();
        }

        public List<TransportationRecordsDTO> GetTransportationRecordsList(Request_TransportationRecords dto, out int count)
        {
            throw new NotImplementedException();
        }

        public bool UpTransportationRecords(TransportationRecordsModel model)
        {
            throw new NotImplementedException();
        }
    }
}
