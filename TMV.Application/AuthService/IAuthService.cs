using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.Users;

namespace TMV.Application.AuthService
{
    public interface IAuthService
    {
        Task<LoginOutDto> Login(string userName, string password);
        Task LoginOut();
    }
}
