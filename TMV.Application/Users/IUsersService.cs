using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Users;

namespace TMV.Application.Users
{
    public interface IUsersService
    {
        string Login(string userName, string password, out string message);

        List<UsersDTO> GetUsersList(Request_Users dto, out int count);
        bool AddUsers(UsersModel model);

        bool UpUsers(UsersModel model);

        bool DeUsers(int id);
    }
}
