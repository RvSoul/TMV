using TMV.DTO.Users;

namespace TMV.Web
{
    public class PageBase<T>
    {
        public List<T> Datas { get; set; }

        public string? Search { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
        public int PageCount { get; set; }

        public int CurrentCount { get; set; }

        public PageBase(List<T> datas)
        {
            Datas = new List<T>();
            Datas.AddRange(datas);
        }
        public PageBase()
        {
        }
    }
}
