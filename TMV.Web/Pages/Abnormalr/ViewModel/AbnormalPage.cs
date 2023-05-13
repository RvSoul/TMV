using TMV.DTO.Abnormal;
using TMV.DTO.AbnormalRecords;

namespace TMV.Web.Pages.Abnormalr.ViewModel
{
	public class AbnormalPage: PageBase<AbnormalDTO>
	{
		public string Code { get; set; }
		public AbnormalPage() { }
		public AbnormalPage(List<AbnormalDTO> datas)
		{
			Datas = new List<AbnormalDTO>();
			Datas.AddRange(datas);
		}
	}
}
