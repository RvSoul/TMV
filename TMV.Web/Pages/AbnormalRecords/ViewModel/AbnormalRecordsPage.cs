using TMV.DTO.AbnormalRecords;
using TMV.DTO.Users;

namespace TMV.Web.Pages.AbnormalRecords.ViewModel
{
    public class AbnormalRecordsPage: PageBase<AbnormalRecordsDTO>
    {
        /// <summary>
        /// 车牌号
        /// </summary> 
        public string Code { get; set; }

        /// <summary>
        /// 矿号
        /// </summary> 
        public string MineCode { get; set; }

        /// <summary>
        /// 状态-1.正常，2.告警
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 衡名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary> 
        public string STime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary> 
        public string ETime { get; set; }
        public AbnormalRecordsPage() { }
        public AbnormalRecordsPage(List<AbnormalRecordsDTO> datas)
        {
            Datas = new List<AbnormalRecordsDTO>();
            Datas.AddRange(datas);
        }
    }
}
