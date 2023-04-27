﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.Application.Users.Services;
using TMV.DTO.Users;
using TMV.DTO;
using System.Xml.Linq;
using TMV.DTO.Car;

namespace TMV.Application.Users
{
    [ApiDescriptionSettings("用户", Tag = "用户管理")]
    public class UsersService : IDynamicApiController, ITransient
    {
        public IUsersServiceDM dm;
        public UsersService(IUsersServiceDM usersServices)
        {
            dm = usersServices;
        }

        #region 用户
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsersList")]
        public ResultEntity<List<UsersDTO>> GetUsersList([FromQuery] Request_Users dto)
        {
            return new ResultEntityUtil<List<UsersDTO>>().Success(dm.GetUsersList(dto, out int count), count);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddUsers")]
        public ResultEntity<bool> AddUsers([FromQuery] UsersModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.AddUsers(model));
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpUsers")]
        public ResultEntity<bool> UpUsers([FromQuery] UsersModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.UpUsers(model));
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeUsers")]
        public ResultEntity<bool> DeUsers(int Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.DeUsers(Id));
        }
        #endregion
    }
}
