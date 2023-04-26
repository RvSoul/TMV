using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Tr;
using TMV.DTO.Users;
using TMV.Core.CM;
using TMV.DTO;

namespace TMV.Application.Users.Services
{
    public class UsersServices :IUsersServices, ITransient
    {
        ISqlSugarClient _db;
        public UsersServices(ISqlSugarClient db)
        {
            _db = db;
        }
        public Task<bool> AddUsers(UserInsertDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeUsers(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultPageData> GetUsersList(UsersPageDto dto)
        {
            RefAsync<int> total = 0;
            var query = await _db.Queryable<TMV_Users>()
                .WhereIF(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
                .Select(x => new UsersView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PassWord = x.PassWord,
                    LoginName = x.LoginName,
                    Type = x.Type,
                    CreateTime = x.CreateTime,
                }).ToPageListAsync(dto.PageIndex, dto.PageSize, total);
            return new ResultPageData() { Data = query, IsSuccess = true, Total = total };
        }
        public string Login(string userName, string password, out string message)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpUsers(UserInsertDto model)
        {
            throw new NotImplementedException();
        }
    }
}
