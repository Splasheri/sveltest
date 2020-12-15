using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BaseLibrary
{
    public class TCPServerBase : TCPObjects
    {
        protected const int MAX_CONNECTIONS = 32;
        public Socket listener = null;
        public TCPServerBase(int _port) : base("127.0.0.1",_port)
        {
            listener = new Socket(GetIpAddress.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            listener.Bind(GetEndPoint);
            listener.Listen(MAX_CONNECTIONS);
        }
    }
}
