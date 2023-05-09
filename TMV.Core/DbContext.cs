using Furion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using TMV.Core.CM;

namespace TMV.Core
{
    /// <summary>
    /// 数据库上下文对象
    /// </summary>
    public static class DbContext
    {
        public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionConfigs = App.GetConfig<List<ConnectionConfig>>("ConnectionConfigs");
            //如果多个数数据库传 List<ConnectionConfig>
            var configConnection = new ConnectionConfig()
            { 
                ConnectionString = connectionConfigs[0].ConnectionString,
                DbType = DbType.SqlServer,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息 
            };

            SqlSugarScope sqlSugar = new SqlSugarScope(configConnection,
                db =>
                {
                    //单例参数配置，所有上下文生效
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        //Console.WriteLine(sql);//输出sql
                        //Console.WriteLine($"当前SQL语句：【{sql}】，参数：【{string.Join(",", pars.Select(t => t.Value))}】");
                    };
                });

            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        }



    }
}