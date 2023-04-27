using Dm.Config;
using Microsoft.AspNetCore.Http;
using TMV.Application.Users.Services;

namespace TMV.Web.Core
{
    public class UserServerce
    {
        IUsersServiceDM _usersServiceDM;
        public UserServerce(IUsersServiceDM usersServiceDM)
        {
            _usersServiceDM=usersServiceDM;
        }
        public bool Login(string name,string pwd)
        {
            string mes = "";
            var result=_usersServiceDM.Login(name,pwd, out mes);
            Console.WriteLine(mes);
            return string.IsNullOrWhiteSpace(result);
        }
    }
}
