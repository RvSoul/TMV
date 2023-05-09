using System;
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

namespace TMV.Web.Core.SocketServer
{
    public static class SocketService
    {
        static ITrServiceDM trServiceDM { get; set; }
        public static void SocketServereMildd(this IApplicationBuilder app)
        {
            var al = app.ApplicationServices;
            trServiceDM = al.GetService<ITrServiceDM>();
            OpenServerSocket();
        }
        static List<Socket> clientScoketLis = new List<Socket>();
        private static void OpenServerSocket()
        {
            try
            {
                //var dd = App.HttpContext;
                //var sd = App.HttpContext.GetLocalIpAddressToIPv4();
                Log.Information("开启socket服务");
                var ReceiveIp = App.GetConfig<SocketConfigs>("SocketConfigs");
                //1、创建Socket对象
                //参数：寻址方式，当前为Ivp4  指定套接字类型   指定传输协议Tcp；
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2、绑定端口、IP
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ReceiveIp.ReceiveIp), ReceiveIp.Port);
                socket.Bind(iPEndPoint);
                //3、开启侦听
                socket.Listen(10);//如果同时来了100个连接请求，只能处理一个,队列中10个在等待连接的客户端，其他的则返回错误消息。
                Log.Information("开始监听....");
                //4、开始接受客户端的连接
                ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
                //loadServer = 1;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }


        }
        /// <summary>
        /// 线程池线程执行的接受客户端连接方法
        /// </summary>
        /// <param name="obj">传入的Socket</param>
        private static void AcceptClientConnect(object obj)
        {
            //转换Socket
            var serverSocket = obj as Socket;
            //接受客户端的连接
            while (true)
            {
                //5、创建一个负责通信的Socket
                Socket proxSocket = serverSocket.Accept();
                //将连接的Socket存入集合
                clientScoketLis.Add(proxSocket);
                //6、不断接收客户端发送来的消息
                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
            }

        }
        /// <summary>
        /// 不断接收客户端信息子线程方法
        /// </summary>
        /// <param name="obj">参数Socke对象</param>
        private static void ReceiveClientMsg(object obj)
        {
            var proxSocket = obj as Socket;
            Log.Information("-----------开始接受客户端信息————————");
            Log.Information($"接受到远程链接{proxSocket.RemoteEndPoint.ToString()}");
            //创建缓存内存，存储接收的信息   ,不能放到while中，这块内存可以循环利用
            byte[] data = new byte[1020 * 1024];
            while (true)
            {
                int len = 0;
                try
                {
                    //接收消息,返回字节长度
                    len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    // LoggerHelper.Error(ex, $"ReceiveClientMsg->");
                    return;
                }

                if (len <= 0)//判断接收的字节数
                {
                    return;
                }
                string msgStr = Encoding.Default.GetString(data, 0, len);
                Log.Information("接受到的数据：" + msgStr);
                try
                {
                    var qdata = msgStr.FromJson<AuthorizationDTO>();
                    var rdata = trServiceDM.GetDataInfo(qdata);
                    Log.Information("-------------------------------------------------------------------------------------");
                    Log.Information("TMV执行结果：" + rdata.ToJson());
                    var request = "ssssssssssssssssssss";
                    Byte[] bytesSent = Encoding.ASCII.GetBytes(rdata.ToJson());
                    var sd=proxSocket.Send(bytesSent, bytesSent.Length, 0);
                    
                    //SendClientMsg("")
                }
                catch (Exception ex)
                {
                    Log.Information("TMV解析错误：" + ex.Message);
                    Console.WriteLine("-------------------------------------------------------------------------------------");
                }

                //储存到数据库
                Task.Run(() =>
                {
                    // ALCDataToSqlData(msgStr);
                });

            }
        }

        private static void SendClientMsg(string ip, int port)
        {
            try
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //设置socket连接 端口为socketServer的端口
                socket.Connect(ip, port);
                if (!socket.Connected)
                {
                    Log.Information($"链接到{ip}:{port}失败");
                }
                var request = "";
                Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
                socket.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception ex)
            {

                Log.Information($"连接异常");
            }
        }
    }
}
