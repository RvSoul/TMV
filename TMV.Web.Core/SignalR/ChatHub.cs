using Furion.InstantMessaging;
using Microsoft.AspNetCore.SignalR;

namespace TMV.Web.Core
{
	/// <summary>
	/// 集线器
	/// </summary>
	public class ChatHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}