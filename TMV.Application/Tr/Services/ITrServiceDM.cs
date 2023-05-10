using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;
using TMV.DTO.Authorization;
using TMV.DTO.Tr;

namespace TMV.Application.Tr.Services
{
    public interface ITrServiceDM
    {
        /// <summary>
        /// 获取物流订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultPageEntity<TransportationRecordsDTO> GetTransportationRecordsList(Request_TransportationRecords dto );

        /// <summary>
        /// 添加物流订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultEntity<bool> AddTransportationRecords(TransportationRecordsModel model);

        /// <summary>
        /// 修改物流订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultEntity<bool> UpTransportationRecords(TransportationRecordsModel model);

        /// <summary>
        /// 删除物流订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultEntity<bool> DeTransportationRecords(Guid id);

       

        /// <summary>
        /// 获取称重记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultPageEntity<ScalageRecordsDTO> GetScalageRecordsList(Request_ScalageRecordsDTO dto);

        /// <summary>
        /// Socket解析成物流订单-进场绑卡
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultInfo GetDataInfo1(AuthorizationDTO dto);

        /// <summary>
        /// Socket解析成物流订单-毛重
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultInfo GetDataInfo2(AuthorizationDTO dto);
    }
}
