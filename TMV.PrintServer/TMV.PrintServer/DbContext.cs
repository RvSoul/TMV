using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.PrintServer
{
    public class DbContext
    {
        public ISqlSugarClient db;
        public DbContext()
        {
            db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.SqlServer,
                ConnectionString = "Data Source=124.221.218.182;uid=sa;pwd=Sql2019@;Initial Catalog=TMV;Integrated Security=false;Connect Timeout=3000;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                IsAutoCloseConnection = true,
            },
           db =>
           {
               //单例参数配置，所有上下文生效
               db.Aop.OnLogExecuting = (sql, pars) =>
               {
                   //获取IOC对象不要求在一个上下文
                   //vra log=s.GetService<Log>()

                   //获取IOC对象要求在一个上下文
                   //var appServive = s.GetService<IHttpContextAccessor>();
                   //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();
               };
           });
        }
    }
}
