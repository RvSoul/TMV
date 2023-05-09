using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.Web.Core.SignalR
{
    public class StronglyTypedChatHub : Hub<IChatClient>
    {
        // 定义一个方法供客户端调用
        public async Task SendMessage(string user, string message)
        {
            // 触发客户端定义监听的方法
            await Clients.All.ReceiveMessage(user, message);
        }
    }
}
