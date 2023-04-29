using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Scale;

namespace TMV.Application.Scale.Services
{
    public interface IScaleServiceDM
    {
        List<ScaleDTO> GetScaleList(Request_Scale dto, out int count);
        bool AddScale(ScaleModel model);

        bool UpScale(ScaleModel model);

        bool QtScale(Guid id);
    }
}
