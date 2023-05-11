using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Car;
using TMV.DTO;
using TMV.DTO.Tr;
using TMV.DTO.Home;
using TMV.DTO.Authorization;

namespace TMV.Application.Home.Services
{
	public interface IHomeServiceDM
	{
		/// <summary>
		/// 获取统计信息
		/// </summary>
		/// <returns></returns>
		ResultEntity<HomeCountDTO> GetHomeCount();

		/// <summary>
		/// 获取首页物流订单
		/// </summary>
		/// <returns></returns>
		ResultEntity<List<TransportationRecordsDTO>> GetHomeTransportationRecordsList();

		/// <summary>
		/// 获取上传数据信息
		/// </summary>
		/// <returns></returns>
        ResultEntity<List<UploadDTO>> GetScData();

		/// <summary>
		/// 上传完成后，处理上传数据状态
		/// </summary>
		/// <returns></returns>
        ResultEntity<bool> UpScData(List<string> li);

		/// <summary>
		/// 衡实时信息
		/// </summary>
		/// <param name="PlateNumber">车牌号</param>
		/// <returns></returns>
		ResultEntity<RecordDataDTO> GetRecordData(string PlateNumber);

	}
}
