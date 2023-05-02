using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;
using TMV.DTO.Scale;

namespace TMV.Application.Scale.Services
{
    public interface IScaleServiceDM
    {
        ResultPageEntity<ScaleDTO> GetScaleList(Request_Scale dto);
        ResultEntity<bool> AddScale(ScaleModel model);

        ResultEntity<bool> UpScale(ScaleModel model);

        ResultEntity<bool> QtScale(Guid id);
    }
}
