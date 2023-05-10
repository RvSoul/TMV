
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
            //Console.WriteLine("�ͻ���������������!!");
            //Console.ReadLine();

        }
        private void InitializeSocket()
        {
            string msg = "��ʼʱ��ʼ��socket����";
            try
            {
                var ip = textBox1.Text.Trim();
                var port = int.Parse(textBox2.Text);
                msg+="\r\n"+server.OpenServerSocket(ip, port);
            }
            catch (Exception  ex)
            {
                msg += $"\r\n��ʼ��ʧ��,ʧ����Ϣ{ex.Message}";
            }
            textBox3.Text = msg;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        
    }
}