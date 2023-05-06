using TMV.DTO.Tr;

namespace TMV.Web.Pages.TransportationRecords.ViewModel
{
    public class TransportationRecordsPage: PageBase<TransportationRecordsDTO>
    {
        public string Code { get; set; }
        public string PlateNumber { get; set; }
        public string MineCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary> 
        public string STime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary> 
        public string ETime { get; set; }
        public TransportationRecordsPage(List<TransportationRecordsDTO> datas)
        {
            Datas = new List<TransportationRecordsDTO>();
            Datas.AddRange(datas);
        }
        public TransportationRecordsPage()
        {
        }
    }
}
