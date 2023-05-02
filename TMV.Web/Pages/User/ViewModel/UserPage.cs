using TMV.DTO.Users;

namespace TMV.Web.Pages.User.ViewModel
{
    public class UserPage: PageBase<UsersDTO>
    {
        public string? Search { get; set; }
        public UserPage(List<UsersDTO> datas)
        {
            Datas = new List<UsersDTO>();
            Datas.AddRange(datas);
        }
        public UserPage()
        {
        }
       
    }
}
