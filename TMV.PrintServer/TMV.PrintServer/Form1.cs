
using Aspose.Words.XAttr;
using Furion;
using Furion.Logging;
using NPOI.OpenXmlFormats.Vml;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
//using Word;
using static System.Drawing.Printing.PrinterSettings;

namespace TMV.PrintServer
{
   
    public partial class Form1 : Form
    {
        public static Form1 form;
		DbContext dbContext;
        PrintServer printServer;
		Thread threadScan;
        public delegate void SetText(string text);
		string msg;

		public Form1()
        {
            form =this;
			dbContext = new DbContext();
			printServer = new PrintServer(dbContext);
			InitializeComponent();
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.DoWork += new DoWorkEventHandler(InitializeSocket);
                bw.RunWorkerAsync();
            }
        }
        private void InitializeSocket(object sender, DoWorkEventArgs e)
        {
            msg = "开始时初始化socket链接";
            try
            {
                var ip = textBox1.Text.Trim();
                var port = int.Parse(textBox2.Text);
                msg+="\r\n"+OpenServerSocket(ip, port, e);

                msg += "\r\n--------------------------连接应用系统socket服务-------------------------------";
				msg += "\r\n" + CliSocket();

			}
            catch (Exception  ex)
            {
                msg += $"\r\n初始化失败,失败信息{ex.Message}";
            }
            e.Result = msg;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBox3.Text = e.Result.ToString();
        }
		public string OpenServerSocket(string ip, int port, DoWorkEventArgs e)
		{
			try
			{
				Log.Information("开启socket服务");
				msg += "开启socket服务";
				//var ReceiveIp = App.GetConfig<SocketConfigs>("SocketConfigs");
				var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				//2、绑定端口、IP
				IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
				socket.Bind(iPEndPoint);
				//3、开启侦听
				socket.Listen(10);//如果同时来了100个连接请求，只能处理一个,队列中10个在等待连接的客户端，其他的则返回错误消息。
				msg += "\r\n开始监听....";
				Log.Information("开始监听");
				//4、开始接受客户端的连接
				ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
				//loadServer = 1;
			}
			catch (Exception ex)
			{
				Log.Information($"Socket服务异常，异常信息：{ex.Message}");
				msg += "\r\nSocket服务异常，异常信息：" + ex.Message;
			}
			return msg;
		}
		/// <summary>
		/// 线程池线程执行的接受客户端连接方法
		/// </summary>
		/// <param name="obj">传入的Socket</param>
		private void AcceptClientConnect(object obj)
		{
			var serverSocket = obj as Socket;
			//接受客户端的连接
			while (true)
			{
				//创建一个负责通信的Socket
				Socket proxSocket = serverSocket.Accept();
				var ip = proxSocket.RemoteEndPoint.ToString();
				ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
			}

		}
		/// <summary>
		/// 不断接收客户端信息子线程方法
		/// </summary>
		/// <param name="obj">参数Socke对象</param>
		private void ReceiveClientMsg(object obj)
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
				string msgStr = "DK1577";// Encoding.Default.GetString(data, 0, len);
				msg += "\r\n接受到的数据：" + msgStr;
				Log.Information($"接受到的数据{msgStr}");
				msg += "\r\n" + printServer.BeginPtint(msgStr);
				// });
			}
		}
		public string CliSocket()
        {
            string msg = "";
            try
            {
				Log.Information("-----------开始链接应用系统socket服务————————");
				msg = "\r\n-----------开始链接应用系统socket服务————————";
				var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.Connect("127.0.0.1", 5395);
				msg = "\r\n链接成功";
				Log.Information("链接成功");
				var bit = new byte[1024];//设置缓冲区，用来保存数据
				socket.BeginReceive(bit, 0, bit.Length, SocketFlags.None, new AsyncCallback((ls) => {

					var length = socket.EndReceive(ls);
					//读出消息内容
					var ms = Encoding.Unicode.GetString(bit, 0, length);
					//打印消息内容
					msg += $"\r\n接受消息:{ms}";
					Log.Information($"接受消息{ms}");
					msg += printServer.BeginPtint(ms);
				}), null);
			}
            catch (Exception ex)
            {
				
				msg = "\r\n链接失败";
				Log.Error("链接失败:"+ex.Message);

			}
            return msg;
		}

    }
}