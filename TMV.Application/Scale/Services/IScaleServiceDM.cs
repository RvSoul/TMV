using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;
using TMV.DTO.Scale;
using TMV.DTO.Tr;

namespace TMV.Application.Scale.Services
{
    public interface IScaleServiceDM
    {
        ResultPageEntity<ScaleDTO> GetScaleList(Request_Scale dto);
        ResultEntity<bool> AddScale(ScaleModel model);

        ResultEntity<bool> UpScale(ScaleModel model);

        ResultEntity<bool> QtScale(Guid id);

        /// <summary>
        /// 运煤集中计量下面的统计
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultPageEntity<ScalageRecordsDTO2> GetScalageRecordsDTO2List(Request_ScalageRecordsDTO2 dto);
    }
}
