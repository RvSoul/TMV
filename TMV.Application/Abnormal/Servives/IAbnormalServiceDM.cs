using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Abnormal;
using TMV.DTO;

namespace TMV.Application.Abnormal.Servives
{
     

    public interface IAbnormalServiceDM
    {
        ResultPageEntity<AbnormalDTO> GetAbnormalList(Request_Abnormal dto);
        ResultEntity<bool> AddAbnormal(AbnormalModel model);

        ResultEntity<bool> UpAbnormal(AbnormalModel model);

        ResultEntity<bool> DeAbnormal(Guid id);
    }
}
