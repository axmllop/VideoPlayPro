using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Xml;
/// <summary>
/// 发送(服务器)
/// </summary>
public class UdPServer : MonoBehaviour 
{
    Socket socket;
    //客户端
    EndPoint clientEnd;
    //侦听端口
    IPEndPoint ipEnd;
    //接受字符串
    public string recvStr;
    //发送字符串
    public string sendStr;
    //接受的数据
    byte[] recvData = new byte[1024];
    //发送的数据
    byte[] sendData = new byte[1024];
    //接受的数据长度
    int recvLen;
    //连接线程
    Thread connectThread;

    public Manager manager;
   
    void InitSocket()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        //定义侦听端口，侦听任何IP
        ipEnd = new IPEndPoint(IPAddress.Any,port);
        //定义套接字类型，在主线程中定义
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //服务器需要绑定IP
        socket.Bind(ipEnd);

        //定义客户端
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

        clientEnd = (EndPoint)sender;
        print("Waiting for UDP dgram");
        //开启一个线程连接，必须的，否则主线程卡死
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();

    }
    
    //发送数据
    public void SocketSend(string sendStr)
    {
        //清空发送缓存
        sendData = new byte[1024];
        //数据类型转换
        sendData = Encoding.ASCII.GetBytes(sendStr);
        //发送给指定客户端
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, clientEnd);

    }

    //服务器接受数据
    void SocketReceive()
    {
        while (true)
        {
            recvData = new byte[1024];
            //获取客户端，获取客户端数据，用引用给客户赋值
            recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
            print("message Form:" + clientEnd.ToString());
            //输出接收到的数据
            recvStr = UTF8Encoding.ASCII.GetString(recvData,0,recvLen);

            //manager.isStart = true;
            //recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            
            print("我是服务器，接受到客户端的数据" + recvStr);
            sendStr = "Form Sever:" + recvStr;
            SocketSend(sendStr);
        }

    }
    //连接关闭
    void SocketQuit()
    {
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭Socket
        if (socket != null)
        {
            socket.Close();

        }
        print("disconnect(断开)");

    }
    int port;

    
    void Start()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(Application.streamingAssetsPath + @"/config.xml");
        XmlNodeList PORT = xml.GetElementsByTagName("Port");
        
        port = int.Parse(PORT[0].InnerText);
        //初始化Socket
        InitSocket();

    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }
    //string editString="  ";
    //void OnGUI()
    //{

    //    editString = GUI.TextField(new Rect(10, 10, 100, 10), editString);
    //    if (GUI.Button(new Rect(10, 30, 60, 20), "send"))
    //        SocketSend(editString);
    //}
 
	
}
