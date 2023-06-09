﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.TransportPlan.Services;
using TMV.DTO.TransportPlan;
using TMV.DTO;

namespace TMV.Application.TransportPlan
{
    
    [ApiDescriptionSettings("运输计划", Tag = "运输计划管理")]
    public class TransportPlanService : IDynamicApiController, ITransient
    {
        public ITransportPlanServiceDM dm;
        public TransportPlanService(ITransportPlanServiceDM TransportPlanServices)
        {
            dm = TransportPlanServices;
        }
        #region 运输计划
        /// <summary>
        /// 获取运输计划列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTransportPlanList")]
        public ResultPageEntity<TransportPlanDTO> GetTransportPlanList([FromQuery] Request_TransportPlan dto)
        {
            return  dm.GetTransportPlanList(dto) ;
        }

        /// <summary>
        /// 添加运输计划
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddTransportPlan")]
        public ResultEntity<bool> AddTransportPlan([FromQuery] TransportPlanModel model)
        {
            return  dm.AddTransportPlan(model) ;
        }
        /// <summary>
        /// 修改运输计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpTransportPlan")]
        public ResultEntity<bool> UpTransportPlan([FromQuery] TransportPlanModel model)
        {
            return  dm.UpTransportPlan(model);
        }


        /// <summary>
        /// 删除运输计划
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeTransportPlan")]
        public ResultEntity<bool> DeTransportPlan(Guid Id)
        {
            return  dm.DeTransportPlan(Id);
        }
        #endregion
    }
}
