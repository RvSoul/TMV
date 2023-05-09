﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Furion;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using OneOf.Types;
using Microsoft.AspNetCore.Http;
using Furion.Logging;
using TMV.Application.Tr.Services;
using Microsoft.Extensions.DependencyInjection;
using TMV.DTO.Authorization;
using StackExchange.Profiling.Internal;
using TMV.Application.SocketConfig.Service;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace TMV.Web.Core.SocketServer
{
	public static class SocketService
	{
		static ITrServiceDM trServiceDM { get; set; }
		//static ISocketConfigService socketConfigService { get; set; }
		static IHubContext<ChatHub> _hubContext;

		static Socket socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		public static void SocketServereMildd(this IApplicationBuilder app)
		{
			var al = app.ApplicationServices;
			trServiceDM = al.GetService<ITrServiceDM>();
			_hubContext = (IHubContext<ChatHub>)al.GetService(typeof(IHubContext<ChatHub>));

			//socketConfigService = al.GetService<SocketConfigService>();
			var ReceiveIp = App.GetConfig<SocketConfigs>("SocketConfigs");
			ServerEnd(ReceiveIp.Port, 10, socketListener);
			Thread th = new Thread(ServerCommunity);
			th.Start(socketListener);

			//OpenServerSocket();
		}
		static void ServerEnd(int myPort, int allowNum, Socket socketListener)
		{

			IPAddress ip = IPAddress.Any;

			int port = myPort;

			IPEndPoint point = new IPEndPoint(ip, port);

			socketListener.Bind(point);

			ShowMsg($"Listening...{ip}:{port}");

			socketListener.Listen(allowNum);
		}

		static void ServerCommunity(object obListener)
		{
			Socket temp = (Socket)obListener;

			while (true)
			{
				Socket socketSender = temp.Accept();

				ShowMsg(("Client IP = " + socketSender.RemoteEndPoint.ToString()) + " Connect Succese!");

				Thread ReceiveMsg = new Thread(ReceiveClient);

				ReceiveMsg.IsBackground = true;

				Thread SendToClient = new Thread(SendMsgToClient);

				SendToClient.Start(socketSender);

				ReceiveMsg.Start(socketSender);
			}

		}

		static void SendMsgToClient(object mySocketSender)
		{
			Socket socketSender = mySocketSender as Socket;

			while (true)
			{
				if (socketSender.RemoteEndPoint == null)
				{
					ShowMsg("socketSender.RemoteEndPoint == null");
					break;
				}
				string msg = Console.ReadLine();

				byte[] buffer = Encoding.UTF8.GetBytes(msg);

				socketSender.Send(buffer);
			}

		}

		static void ReceiveClient(object mySocketSender)
		{
			Socket socketSender = mySocketSender as Socket;

			while (true)
			{

				byte[] buffer = new byte[1024];

				int rece = socketSender.Receive(buffer);

				if (rece == 0)
				{
					ShowMsg(string.Format("Client : {0} + 下線了", socketSender.RemoteEndPoint.ToString()));
					break;
				}
				string clientMsg = Encoding.UTF8.GetString(buffer, 0, rece);
				ShowMsg(string.Format("Client : {0}", clientMsg));

				tmvdata(clientMsg);

                _hubContext.Clients.All.SendAsync("ReceiveMessage", "bob", clientMsg);
			}
		}

		static void tmvdata(string msgStr) {
			try
			{
                Log.Information("-------------------------------------------------------------------------------------");
                var qdata = msgStr.FromJson<AuthorizationDTO>();
				var rdata = trServiceDM.GetDataInfo(qdata);
				
				Log.Information("TMV执行结果：" + rdata.ToJson());

				//Byte[] bytesSent = Encoding.ASCII.GetBytes(rdata.ToJson());
				//var sd = proxSocket.Send(bytesSent, bytesSent.Length, 0);
				Log.Information("-------------------------------------------------------------------------------------");
				//SendClientMsg("")
			}
			catch (Exception ex)
			{
				Log.Information("TMV解析错误：" + ex.Message);
				Log.Information("-------------------------------------------------------------------------------------");
			}
		}

		static void ShowMsg(string s)
		{
			Console.WriteLine(s);
		}
		//static List<Socket> clientScoketLis = new List<Socket>();
		//private static void OpenServerSocket()
		//{
		//    try
		//    {
		//        Log.Information("开启socket服务");
		//        var ReceiveIp = App.GetConfig<SocketConfigs>("SocketConfigs");

		//        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		//        //2、绑定端口、IP
		//        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ReceiveIp.ReceiveIp), ReceiveIp.Port);
		//        socket.Bind(iPEndPoint);
		//        //3、开启侦听
		//        socket.Listen(10);//如果同时来了100个连接请求，只能处理一个,队列中10个在等待连接的客户端，其他的则返回错误消息。
		//        Log.Information("开始监听....");
		//        //4、开始接受客户端的连接
		//        ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
		//        //loadServer = 1;
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.Error(ex.Message);
		//    }


		//}
		///// <summary>
		///// 线程池线程执行的接受客户端连接方法
		///// </summary>
		///// <param name="obj">传入的Socket</param>
		//private static void AcceptClientConnect(object obj)
		//{
		//    var serverSocket = obj as Socket;
		//    //接受客户端的连接
		//    while (true)
		//    {
		//        //创建一个负责通信的Socket
		//        Socket proxSocket = serverSocket.Accept();
		//        var ip = proxSocket.RemoteEndPoint.ToString();
		//        var so = socketConfigService.GetSocketConfig(ip.Substring(0, ip.IndexOf(':')));
		//        if (so.Data == null)
		//        {
		//            SendClientMsg(proxSocket, "IP地址不被允许链接");
		//            Log.Information($"IP地址：{ip}不被允许链接");
		//            proxSocket.Close();
		//        }
		//        else
		//        {
		//            //接收客户端发送来的消息
		//            ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
		//        }
		//        //ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
		//    }

		//}
		///// <summary>
		///// 不断接收客户端信息子线程方法
		///// </summary>
		///// <param name="obj">参数Socke对象</param>
		//private static void ReceiveClientMsg(object obj)
		//{
		//    var proxSocket = obj as Socket;
		//    Log.Information("-----------开始接受客户端信息————————");
		//    Log.Information($"接受到远程链接{proxSocket.RemoteEndPoint.ToString()}");
		//    //创建缓存内存，存储接收的信息   ,不能放到while中，这块内存可以循环利用
		//    byte[] data = new byte[1020 * 1024];
		//    while (true)
		//    {
		//        int len = 0;
		//        try
		//        {
		//            //接收消息,返回字节长度
		//            len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
		//        }
		//        catch (Exception ex)
		//        {
		//            Console.WriteLine(ex.ToString());
		//            // LoggerHelper.Error(ex, $"ReceiveClientMsg->");
		//            return;
		//        }

		//        if (len <= 0)//判断接收的字节数
		//        {
		//            return;
		//        }
		//        string msgStr = Encoding.Default.GetString(data, 0, len);
		//        Log.Information("接受到的数据：" + msgStr);
		//        try
		//        {
		//            var qdata = msgStr.FromJson<AuthorizationDTO>();
		//            var rdata = trServiceDM.GetDataInfo(qdata);
		//            Log.Information("-------------------------------------------------------------------------------------");
		//            Log.Information("TMV执行结果：" + rdata.ToJson());
		//            var request = "ssssssssssssssssssss";
		//            Byte[] bytesSent = Encoding.ASCII.GetBytes(rdata.ToJson());
		//            var sd = proxSocket.Send(bytesSent, bytesSent.Length, 0);
		//            Log.Information("-------------------------------------------------------------------------------------");
		//            //SendClientMsg("")
		//        }
		//        catch (Exception ex)
		//        {
		//            Log.Information("TMV解析错误：" + ex.Message);
		//            Log.Information("-------------------------------------------------------------------------------------");
		//        }

		//        //储存到数据库
		//        Task.Run(() =>
		//        {
		//            // ALCDataToSqlData(msgStr);
		//        });

		//    }
		//}

		//private static void SendClientMsg(Socket socket, string msg)
		//{
		//    try
		//    {
		//        Byte[] bytesSent = Encoding.ASCII.GetBytes(msg);
		//        socket.Send(bytesSent, bytesSent.Length, 0);
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.Information($"发送消息异常：{ex.Message}");
		//    }
		//}
	}
}
