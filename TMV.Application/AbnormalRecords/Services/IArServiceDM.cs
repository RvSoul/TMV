using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.AbnormalRecords;
using TMV.DTO;

namespace TMV.Application.AbnormalRecords.Services
{
    public interface IArServiceDM
    {
        ResultPageEntity<AbnormalRecordsDTO> GetAbnormalRecordsList(Request_AbnormalRecords dto);

        ResultEntity<bool> UpAbnormalRecords(AbnormalRecordsModel model);

        ResultEntity<bool> DeAbnormalRecords(Guid id);
    }
}
