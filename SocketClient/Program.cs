using System.Net.Sockets;
using System.Net;
using System.Text;



    Console.WriteLine("开始链接");
    string host="192.168.1.4";
    int port = 5395;
    //if (args.Length == 0)
    //    // If no server name is passed as argument to this program,
    //    // use the current host name as the default.
    //    host = Dns.GetHostName();
    //else
    //    host = args[0];

    string result = SocketSendReceive(host, port);
    Console.WriteLine(result);
    Console.ReadLine();

static Socket ConnectSocket(string server, int port)
{
    try
    {
        Socket s = null;
        IPHostEntry hostEntry = null;

        // Get host related information.
        hostEntry = Dns.GetHostEntry(server);

        // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
        // an exception that occurs when the host IP Address is not compatible with the address family
        // (typical in the IPv6 case).
        foreach (IPAddress address in hostEntry.AddressList)
        {
            IPEndPoint ipe = new IPEndPoint(address, port);
            Socket tempSocket =
                new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            tempSocket.Connect(ipe);

            if (tempSocket.Connected)
            {
                s = tempSocket;
                break;
            }
            else
            {
                continue;
            }
        }
        return s;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return null;
    }
    
}
static string SocketSendReceive(string server, int port)
{
    string request = "GET / HTTP/1.1\r\nHost: " + server +
        "\r\nConnection: Close\r\n\r\n";
    Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
    Byte[] bytesReceived = new Byte[256];
    string page = "";

    // Create a socket connection with the specified server and port.
    using (Socket s = ConnectSocket(server, port))
    {

        if (s == null)
            return ("链接失败");

        // Send request to the server.
        s.Send(bytesSent, bytesSent.Length, 0);

        // Receive the server home page content.
        int bytes = 0;
        page = "Default HTML page on " + server + ":\r\n";

        // The following will block until the page is transmitted.
        do
        {
            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
            page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
        }
        while (bytes > 0);
    }

    return page;
}
/// <summary>
/// 接收服务器信息
/// </summary>
/// <param name="obj"></param>
static string ReceiveServerMsg()
{
    string host = "192.168.1.4";
    int port = 80;
    var proxSocket = ConnectSocket(host, port);
    //创建缓存内存，存储接收的信息   ,不能放到while中，这块内存可以循环利用
    byte[] data = new byte[1020 * 1024];
    while (true)
    {
        //接收消息,返回字节长度
        int len;
        try
        {
            len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
        }
        catch (Exception ex)
        {
            //关闭Socket
            ServerExit(proxSocket);
            return "";
        }

        if (len <= 0)
        {
            //关闭Socket
            ServerExit(proxSocket);
            return "";
        }

        string msgStr = Encoding.Default.GetString(data, 0, len);
        return msgStr;
    }
}
/// <summary>
/// 关闭连接
/// </summary>
/// <param name="proxSocket"></param>
static void ServerExit(Socket proxSocket)
{
    try
    {
        if (proxSocket.Connected)//如果是连接状态
        {
            proxSocket.Shutdown(SocketShutdown.Both);//关闭连接
            proxSocket.Close(100);//100秒超时间
        }
    }
    catch (Exception ex)
    {
        //LoggerHelper.Error(ex, $"ServerExit->");
    }
}

