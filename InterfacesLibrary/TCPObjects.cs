using System.Net;
using System.Net.Sockets;

namespace BaseLibrary
{
    public class TCPObjects
    {
        private readonly int port;
        private readonly IPAddress ipAddress;
        private readonly IPEndPoint endPoint;

        public int GetPort { get => port; }
        public IPAddress GetIpAddress{ get => ipAddress; }
        public IPEndPoint GetEndPoint { get => endPoint; }

        protected TCPObjects(string _ipAddress, int _port)
        {
            port = _port;
            ipAddress = IPAddress.Parse(_ipAddress);
            endPoint = new IPEndPoint(ipAddress, port);
        }
        protected TCPObjects(IPAddress _iPAddress, int _port)
        {
            ipAddress = _iPAddress;
            endPoint  = new IPEndPoint(ipAddress, _port);
            port = _port;
        }
    }
}
