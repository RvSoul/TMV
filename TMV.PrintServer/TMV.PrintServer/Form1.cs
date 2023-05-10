
using System.Drawing.Printing;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
//using Word;
using static System.Drawing.Printing.PrinterSettings;

namespace TMV.PrintServer
{
    public partial class Form1 : Form
    {
        SocketServer server = new();
        public Form1()
        {
            InitializeComponent();
            InitializeSocket();
            //string message = string.Empty;
            //while ((message = Console.ReadLine()) != "exit")
            //{
            //    client.Send(message);
            //}
            //Console.WriteLine("客户端启动，测试中!!");
            //Console.ReadLine();

        }
        private void InitializeSocket()
        {
            string msg = "开始时初始化socket链接";
            try
            {
                var ip = textBox1.Text.Trim();
                var port = int.Parse(textBox2.Text);
                msg+="\r\n"+server.OpenServerSocket(ip, port);
            }
            catch (Exception  ex)
            {
                msg += $"\r\n初始化失败,失败信息{ex.Message}";
            }
            textBox3.Text = msg;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        
    }
}