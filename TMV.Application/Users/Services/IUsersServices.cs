using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;
using TMV.DTO.Users;

namespace TMV.Application.Users.Services
{
    public interface IUsersServices
    {
        string Login(string userName, string password, out string message);

        Task<ResultPageData> GetUsersList(UsersPageDto dto);
        Task<bool> AddUsers(UserInsertDto model);

        Task<bool> UpUsers(UserInsertDto model);

        Task<bool> DeUsers(int id);
    }
}
