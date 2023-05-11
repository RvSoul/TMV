using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TMV.PrintServer.Model;
using NPOI.Util;
using TMV.PrintServer.Comm;
using System.Windows.Forms;
using System.ComponentModel;

namespace TMV.PrintServer
{
   
    public class SocketServer
    {
        DbContext  dbContext;
        static List<Socket> clientScoketLis ;
        PrintServer printServer = new();
        DoWorkEventArgs work;
        public string msg = "";
        public SocketServer()
        {
            clientScoketLis = new List<Socket>();
            dbContext = new DbContext();
        }
        public  string OpenServerSocket(string ip ,int port, DoWorkEventArgs e)
        {
            try
            {
                msg+="开启socket服务";
                //var ReceiveIp = App.GetConfig<SocketConfigs>("SocketConfigs");
                work = e;
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2、绑定端口、IP
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                socket.Bind(iPEndPoint);
                //3、开启侦听
                socket.Listen(10);//如果同时来了100个连接请求，只能处理一个,队列中10个在等待连接的客户端，其他的则返回错误消息。
                msg += "\r\n开始监听....";
                //4、开始接受客户端的连接
                ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
                //loadServer = 1;
            }
            catch (Exception ex)
            {
                msg += "\r\nSocket服务异常，异常信息：" + ex.Message;
            }
            return msg;
        }
        /// <summary>
        /// 线程池线程执行的接受客户端连接方法
        /// </summary>
        /// <param name="obj">传入的Socket</param>
        private  void AcceptClientConnect(object obj)
        {
            var serverSocket = obj as Socket;
            //接受客户端的连接
            while (true)
            {
                //创建一个负责通信的Socket
                Socket proxSocket = serverSocket.Accept();
                var ip = proxSocket.RemoteEndPoint.ToString();
               // var so = socketConfigService.GetSocketConfig(ip.Substring(0, ip.IndexOf(':')));
                //if (so.Data == null)
                //{
                //    SendClientMsg(proxSocket, "IP地址不被允许链接");
                //    Log.Information($"IP地址：{ip}不被允许链接");
                //    proxSocket.Close();
                //}
                //else
                //{
                //    //接收客户端发送来的消息
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
                //}
                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
            }

        }
        /// <summary>
        /// 不断接收客户端信息子线程方法
        /// </summary>
        /// <param name="obj">参数Socke对象</param>
        private   void ReceiveClientMsg(object obj)
        {
            var proxSocket = obj as Socket;
            //Log.Information("-----------开始接受客户端信息————————");
           // Log.Information($"接受到远程链接{proxSocket.RemoteEndPoint.ToString()}");
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
                string msgStr = "DK1577";// Encoding.Default.GetString(data, 0, len);
                msg += "\r\n接受到的数据：" + msgStr;
               

                //储存到数据库
                //Task.Run(() =>
                //{
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(msgStr))
                        {
                            WordUtil wordUtil = new();
                            var printdata = dbContext.db.Queryable<TransportationRecords, TransportPlan, Car, ScalageRecords>((a, b, c, d) => a.CarId == c.Id && a.CollieryId == b.Id && a.Id == d.TId)
                            .Where((a, b, c) => c.PlateNumber.ToString() == msgStr)
                            .Select((a, b, c, d) => new PrintDto()
                            {
                                unit = a.Unit,
                                scalenumber = d.ScaleId.ToString(),
                                number = a.Code,
                                shipper = b.SendUnit,
                                consignee = b.ReceiptUnit,
                                name = b.CargoName,
                                carryunit = b.Carrier,
                                specification = "",
                                splatenumber = c.PlateNumber,
                                roughweight = a.RoughWeight.ToString(),
                                tareweight = a.TareWeight.ToString(),
                                buckleweight = a.KouWeight.ToString(),
                                netweight = a.NetWeight.ToString(),
                                shipnumber = b.ShipName,
                                truckscar = "",
                                emptycar = "",
                                trucktime = "",
                                lighttime = ""
                            }).First();
                            if (printdata == null)
                            {
                                msg += $"\r\n没有找到车牌号为{msgStr}的记录";
                            }
                            else
                            {
                                var strm=wordUtil.WordWrite(printdata);
                                printServer.Print(strm);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msg += "\r\n解析数据错误：" + ex.Message;
                    }
                work.Result+= "\r\n"+msg;
               // });
            }
        }
        private  void SendClientMsg(Socket socket, string msg)
        {
            try
            {
                Byte[] bytesSent = Encoding.ASCII.GetBytes(msg);
                socket.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception ex)
            {
                //Log.Information($"发送消息异常：{ex.Message}");
            }
        }
    }
}
