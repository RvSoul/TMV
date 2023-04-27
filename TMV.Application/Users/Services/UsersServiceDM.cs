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

namespace TMV.Application.Users.Services
{
    public class UsersServiceDM : IUsersServiceDM, ITransient
    {
        ISqlSugarClient c;
        public UsersServiceDM(ISqlSugarClient db)
        {
            c = db;
        }
        #region 登录
        public string Login(string userName, string password, out string message)
        { 
            var data = c.Queryable<TMV_Users>().Where(w=>w.Name==userName&&w.Name==password).First();

            if (data != null)
            {
                message = "登录成功。";
                return data.Id.ToString();
            }
            else
            {
                message = "登录失败，用户名或密码错误。";
                return "";
            }
        }
        #endregion

        public List<UsersDTO> GetUsersList(Request_Users dto, out int count)
        {
            Expression<Func<TMV_Users, bool>> expr = n => true;
            if (!dto.Name.IsNullOrEmpty())
            {
                expr = expr.And(n => n.Name == dto.Name);
            }

            Expression<Func<TMV_Users, bool>> expr2 = n => true;
            if (!dto.Name.IsNullOrEmpty())
            {
                expr2 = expr2.And2(n => n.Name == dto.Name);
            }

            Expression<Func<TMV_Users, bool>> expr3 = AutoAssemble.Splice<TMV_Users, Request_Users>(dto);


            count = c.Queryable<TMV_Users>().Where(expr).Count();
            var query = c.Queryable<TMV_Users>().Where(expr).OrderByDescending(px => px.AddTime).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            return GetMapperDTO.GetDTOList<TMV_Users, UsersDTO>(query);
        }
        public List<UsersDTO> GetUsersList2(Request_Users dto, out int count)
        {
            Expression<Func<TMV_Users, bool>> expr = AutoAssemble.Splice<TMV_Users, Request_Users>(dto);


            count = c.Queryable<TMV_Users>().Where(w => w.Name == dto.Name).Count();
            var query = c.Queryable<TMV_Users>().Where(w => w.Name == dto.Name).OrderByDescending(px => px.AddTime).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            return GetMapperDTO.GetDTOList<TMV_Users, UsersDTO>(query);
        }
 
        public bool AddUsers(UsersModel model)
        {
            TMV_Users pt = GetMapperDTO.SetModel<TMV_Users, UsersModel>(model);
            pt.AddTime = DateTime.Now;
            var result = c.Insertable(pt).ExecuteCommand();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeUsers(int id)
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


        public bool UpUsers(UsersModel model)
        {
            var data = c.Queryable<TMV_Users>().InSingle(model.Id);

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
