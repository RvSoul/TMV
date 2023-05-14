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
using TMV.Application.SocketConfig.Service;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using TMV.DTO;
using TMV.Application.Home.Services;

namespace TMV.Web.Core.SocketServer
{
    public static class SocketService
    {
        static ITrServiceDM trServiceDM { get; set; }
        static IHomeServiceDM homeServiceDM { get; set; }
        static IHubContext<ChatHub> _hubContext;
		static List<Socket> clientScoketLis=new();
		public static void SocketServereMildd(this IApplicationBuilder app)
        {
            try
            {
                var al = app.ApplicationServices;
                trServiceDM = al.GetService<ITrServiceDM>();
                homeServiceDM = al.GetService<IHomeServiceDM>();
                _hubContext = (IHubContext<ChatHub>)al.GetService(typeof(IHubContext<ChatHub>));
                var ReceiveIp = App.GetConfig<SocketConfigs>("SocketConfigs");
                var socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ServerEnd(ReceiveIp.Port, 10, socketListener);
                //Thread th = new Thread(ServerCommunity);
                ThreadPool.QueueUserWorkItem(new WaitCallback(ServerCommunity), socketListener);
                // th.Start(socketListener);
            }
            catch (Exception ex)
            {
                Log.Information("socketServer：" + ex.Message);
            }
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
				//var ip = socketSender.RemoteEndPoint.ToString().Substring(0, socketSender.RemoteEndPoint.ToString().IndexOf(':'));
				//将所有客户端链接存入clientScoketLis集合 如果客户端socket重连了直接根据ip地址更新数据
				//if (clientScoketLis.Any(x=>x.RemoteEndPoint.ToString().Substring(0, x.RemoteEndPoint.ToString().IndexOf(':'))== ip)){
    //                clientScoketLis.ForEach(x => {
    //                    if(x.RemoteEndPoint.ToString().Substring(0, x.RemoteEndPoint.ToString().IndexOf(':')) == ip)
    //                    {
				//			x = socketSender;
				//		}
                       
				//	});
    //            }
    //            else
    //            {
				//	clientScoketLis.Add(socketSender);
				//}
               
				ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClient), socketSender);
            }
        }

        static void ReceiveClient(object mySocketSender)
        {
            try
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
                    _hubContext.Clients.All.SendAsync("ReceiveMessage", "erverthing", clientMsg);

                    var rs = tmvdata(clientMsg);
                    Byte[] bytesSent = Encoding.UTF8.GetBytes(rs);
                    var sd = socketSender.Send(bytesSent, bytesSent.Length, 0);

                    //告警信息
                    ResultInfo ri = rs.FromJson<ResultInfo>();
                    if (ri.Error != "0")
                    {
                        _hubContext.Clients.All.SendAsync("ReceiveMessage", "warning", rs);
                    }



                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }

        }

        static string tmvdata(string msgStr)
        {
            try
            {
                Log.Information("-------------------------------------------------------------------------------------");
                Log.Information("接收数据:" + msgStr);
                var qdata = msgStr.FromJson<AuthorizationDTO>();
                ResultInfo rdata = new ResultInfo();

                if (qdata.PlateNumber != null)
                {
                    var dd = homeServiceDM.GetRecordData(qdata.PlateNumber);
                    _hubContext.Clients.All.SendAsync("ReceiveMessage", "Record", dd.ToJson());
                }


                if (qdata.ID.Equals("PSAIOT-000000001"))
                {
                    #region 大门口值班室-读卡器-车牌和矿号
                    rdata = trServiceDM.GetDataInfo1(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000002"))
                {
                    #region 制卡器-1号-重衡值班室-(石柱发电厂)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000003"))
                {
                    #region 备份
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000004"))
                {
                    #region 大门出口-读卡器(石柱发电厂)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000005"))
                {
                    #region 重衡2号入口-读卡器(石柱发电厂)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000006"))
                {
                    #region 重衡1号入口-读卡器(石柱发电厂)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000007"))
                {
                    #region 大门入口-读卡器(石柱发电厂)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000008"))
                {
                    #region 轻衡入口-读卡(石柱发电)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000009"))
                {
                    #region 打印读卡器-轻衡值班室
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000010"))
                {
                    #region 矿号绑定终端
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000011"))
                {
                    #region 大门口值班室-控制柜(石柱发电厂)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000012"))
                {
                    #region 重衡值班室-控制柜(石柱发电)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000013"))
                {
                    #region 轻衡值班室-控制柜(石柱发电厂)
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000014"))
                {
                    #region 备份
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000015"))
                {
                    #region 北斗授时-轻衡值班室房顶立杆(石柱发电) 
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }
                else if (qdata.ID.Equals("PSAIOT-000000016"))
                {
                    #region 备份
                    rdata = trServiceDM.GetDataInfo2(qdata);
                    #endregion
                }

                Log.Information("TMV执行结果：" + rdata.ToJson());
                Log.Information("-------------------------------------------------------------------------------------");
                return rdata.ToJson();

            }
            catch (Exception ex)
            {
                Log.Information("TMV解析错误：" + ex.Message);
                Log.Information("-------------------------------------------------------------------------------------");
                return ex.Message;
            }
        }

        static void ShowMsg(string s)
        {
            Log.Information(s);
        }
        private static void SendClientMsg(Socket socket, string msg)
        {
            try
            {
                Byte[] bytesSent = Encoding.ASCII.GetBytes(msg);
                socket.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception ex)
            {
                Log.Information($"发送消息异常：{ex.Message}");
            }
        }
    }
}
