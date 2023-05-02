using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO;
using TMV.DTO.Users;

namespace TMV.Application.Users.Services
{
    public interface IUsersServiceDM
    {
        ResultPageEntity<UsersDTO> GetUsersList(Request_Users dto);
        ResultEntity<bool> AddUsers(UsersModel model);

        ResultEntity<bool> UpUsers(UsersModel model);

        ResultEntity<bool> DeUsers(Guid id);
        UsersDTO GetUset(Guid id);
    }
}
