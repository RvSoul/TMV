using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Scale.Services;
using TMV.DTO.Scale;
using TMV.DTO;

namespace TMV.Application.Scale
{
     
    [ApiDescriptionSettings("衡管理", Tag = "衡管理")]
    public class ScaleService : IDynamicApiController, ITransient
    {
        public IScaleServiceDM dm;
        public ScaleService(IScaleServiceDM ScaleServices)
        {
            dm = ScaleServices;
        }
        #region 衡
        /// <summary>
        /// 获取衡列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetScaleList")]
        public ResultEntity<List<ScaleDTO>> GetScaleList([FromQuery] Request_Scale dto)
        {
            return new ResultEntityUtil<List<ScaleDTO>>().Success(dm.GetScaleList(dto, out int count), count);
        }

        /// <summary>
        /// 添加衡
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddScale")]
        public ResultEntity<bool> AddScale([FromQuery] ScaleModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.AddScale(model));
        }
        /// <summary>
        /// 修改衡
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpScale")]
        public ResultEntity<bool> UpScale([FromQuery] ScaleModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.UpScale(model));
        }


        /// <summary>
        /// 启用弃用衡
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeScale")]
        public ResultEntity<bool> DeScale(Guid Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.QtScale(Id));
        }
        #endregion
    }
}
