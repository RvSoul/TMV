
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
        SocketServer server = new();
        Thread threadScan;
        public delegate void SetText(string text);
        public Form1()
        {
            form =this;
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
            string msg = "��ʼʱ��ʼ��socket����";
            try
            {
                var ip = textBox1.Text.Trim();
                var port = int.Parse(textBox2.Text);
                msg+="\r\n"+server.OpenServerSocket(ip, port, e);
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
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        
    }
}