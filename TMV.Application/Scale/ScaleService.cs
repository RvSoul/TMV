using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Car;
using TMV.DTO.Scale;

namespace TMV.Application.Scale
{
    public class ScaleService : IScaleService, IDynamicApiController, ITransient
    {
        public bool AddScale(ScaleModel model)
        {
            throw new NotImplementedException();
        }

        public List<ScaleDTO> GetScaleList(Request_Scale dto, out int count)
        {
            throw new NotImplementedException();
        }

        public bool QtScale(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpScale(ScaleModel model)
        {
            throw new NotImplementedException();
        }
    }
}
