using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Tr;
using TMV.DTO.Users;
using TMV.Core.CM;
using TMV.DTO;
using TMV.DTO.ModelData;
using System.Linq.Expressions;
using Furion.LinqBuilder;
using System.Xml.Linq;
using Mapster;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TMV.Core.Const;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TMV.Application.Users.Services
{
    public class UsersServiceDM : IUsersServiceDM, ITransient
    {
        ISqlSugarClient c;
        public UsersServiceDM(ISqlSugarClient db)
        {
            c = db;
        }
        public List<UsersDTO> GetUsersList(Request_Users dto, out int count)
        {
            Expression<Func<TMV_Users, bool>> expr = n => true;
            if (!dto.Name.IsNullOrEmpty())
            {
                expr = expr.And2(n => n.Name == dto.Name);
            }
             

            count = c.Queryable<TMV_Users>().Where(expr).Count();
            var query = c.Queryable<TMV_Users>().Where(expr).OrderByDescending(px => px.AddTime).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            return GetMapperDTO.GetDTOList<TMV_Users, UsersDTO>(query);
        }
  

 
        public bool AddUsers(UsersModel model)
        {
            TMV_Users data = GetMapperDTO.SetModel<TMV_Users, UsersModel>(model);
            data.Id = Guid.NewGuid();
            data.Name = model.Name;
            data.Pwd = model.Pwd;
            data.Type = model.Type;

            data.AddTime = DateTime.Now;
            var result = c.Insertable(data).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeUsers(Guid id)
        {
            var result = c.Deleteable<TMV_Users>().In(id).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UsersDTO GetUset(Guid id)
        {
            return c.Queryable<TMV_Users>().First(x => x.Id == id).Adapt<UsersDTO>();
            
        }
        public bool UpUsers(UsersModel model)
        {
            var data = c.Queryable<TMV_Users>().InSingle(model.Id);
            data.Name = model.Name;
            data.Pwd = model.Pwd;
            data.Type = model.Type;


            var result = c.Updateable(data).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
