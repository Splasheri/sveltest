using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BaseLibrary
{
    public class TCPClientBase : TCPObjects
    {
        public Socket socket = null;
        public TCPClientBase(string _ipAddress, int _port) : base(_ipAddress, _port)
        {
            socket = new Socket(GetIpAddress.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            socket.Connect(GetEndPoint);
        }
    }
}
