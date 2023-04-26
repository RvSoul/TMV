using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Users.Services;
using TMV.DTO.Users;
using TMV.DTO;
using System.Xml.Linq;

namespace TMV.Application.Users
{
    [ApiDescriptionSettings("用户", Tag = "用户管理")]
    public class UsersService: IDynamicApiController, ITransient
    {
        public IUsersServices _usersServices;
        public UsersService(IUsersServices usersServices) 
        {
            _usersServices = usersServices;
        }
        [HttpPost]
        public async Task<ResultPageData> GetUsersList(UsersPageDto dto)
        {
            return await _usersServices.GetUsersList(dto);
        }
    }
}
