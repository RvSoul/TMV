using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Tr;
using TMV.DTO.Users;

namespace TMV.Application.Users
{
    public class UsersService : IUsersService, IDynamicApiController, ITransient
    {
        public bool AddUsers(UsersModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeUsers(int id)
        {
            throw new NotImplementedException();
        }

        public List<UsersDTO> GetUsersList(Request_Users dto, out int count)
        {
            throw new NotImplementedException();
        }

        public string Login(string userName, string password, out string message)
        {
            throw new NotImplementedException();
        }

        public bool UpUsers(UsersModel model)
        {
            throw new NotImplementedException();
        }
    }
}
