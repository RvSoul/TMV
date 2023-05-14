
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
            msg = "��ʼʱ��ʼ��socket����";
            try
            {
                var ip = textBox1.Text.Trim();
                var port = int.Parse(textBox2.Text);
                msg+="\r\n"+OpenServerSocket(ip, port, e);

                msg += "\r\n--------------------------����Ӧ��ϵͳsocket����-------------------------------";
				msg += "\r\n" + CliSocket();

			}
            catch (Exception  ex)
            {
                msg += $"\r\n��ʼ��ʧ��,ʧ����Ϣ{ex.Message}";
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
				Log.Information("����socket����");
				msg += "����socket����";
				//var ReceiveIp = App.GetConfig<SocketConfigs>("SocketConfigs");
				var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				//2���󶨶˿ڡ�IP
				IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
				socket.Bind(iPEndPoint);
				//3����������
				socket.Listen(10);//���ͬʱ����100����������ֻ�ܴ���һ��,������10���ڵȴ����ӵĿͻ��ˣ��������򷵻ش�����Ϣ��
				msg += "\r\n��ʼ����....";
				Log.Information("��ʼ����");
				//4����ʼ���ܿͻ��˵�����
				ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
				//loadServer = 1;
			}
			catch (Exception ex)
			{
				Log.Information($"Socket�����쳣���쳣��Ϣ��{ex.Message}");
				msg += "\r\nSocket�����쳣���쳣��Ϣ��" + ex.Message;
			}
			return msg;
		}
		/// <summary>
		/// �̳߳��߳�ִ�еĽ��ܿͻ������ӷ���
		/// </summary>
		/// <param name="obj">�����Socket</param>
		private void AcceptClientConnect(object obj)
		{
			var serverSocket = obj as Socket;
			//���ܿͻ��˵�����
			while (true)
			{
				//����һ������ͨ�ŵ�Socket
				Socket proxSocket = serverSocket.Accept();
				var ip = proxSocket.RemoteEndPoint.ToString();
				ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
			}

		}
		/// <summary>
		/// ���Ͻ��տͻ�����Ϣ���̷߳���
		/// </summary>
		/// <param name="obj">����Socke����</param>
		private void ReceiveClientMsg(object obj)
		{
			var proxSocket = obj as Socket;
			Log.Information("-----------��ʼ���ܿͻ�����Ϣ����������������");
			Log.Information($"���ܵ�Զ������{proxSocket.RemoteEndPoint.ToString()}");
			//���������ڴ棬�洢���յ���Ϣ   ,���ܷŵ�while�У�����ڴ����ѭ������
			byte[] data = new byte[1020 * 1024];
			while (true)
			{
				int len = 0;
				try
				{
					//������Ϣ,�����ֽڳ���
					len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					// LoggerHelper.Error(ex, $"ReceiveClientMsg->");
					return;
				}

				if (len <= 0)//�жϽ��յ��ֽ���
				{
					return;
				}
				string msgStr = "DK1577";// Encoding.Default.GetString(data, 0, len);
				msg += "\r\n���ܵ������ݣ�" + msgStr;
				Log.Information($"���ܵ�������{msgStr}");
				msg += "\r\n" + printServer.BeginPtint(msgStr);
				// });
			}
		}
		public string CliSocket()
        {
            string msg = "";
            try
            {
				Log.Information("-----------��ʼ����Ӧ��ϵͳsocket���񡪡�������������");
				msg = "\r\n-----------��ʼ����Ӧ��ϵͳsocket���񡪡�������������";
				var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.Connect("127.0.0.1", 5395);
				msg = "\r\n���ӳɹ�";
				Log.Information("���ӳɹ�");
				var bit = new byte[1024];//���û�������������������
				socket.BeginReceive(bit, 0, bit.Length, SocketFlags.None, new AsyncCallback((ls) => {

					var length = socket.EndReceive(ls);
					//������Ϣ����
					var ms = Encoding.Unicode.GetString(bit, 0, length);
					//��ӡ��Ϣ����
					msg += $"\r\n������Ϣ:{ms}";
					Log.Information($"������Ϣ{ms}");
					msg += printServer.BeginPtint(ms);
				}), null);
			}
            catch (Exception ex)
            {
				
				msg = "\r\n����ʧ��";
				Log.Error("����ʧ��:"+ex.Message);

			}
            return msg;
		}

    }
}