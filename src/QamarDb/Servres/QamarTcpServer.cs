using System.Net;

using NetCoreServer;

using QamarDb.Sessions;

namespace QamarDb.Servres
{
    internal class QamarTcpServer : TcpServer // TcpServer
    {
        public QamarTcpServer(IPAddress address, int port) : base(address, port) { }

        protected override void OnConnected(TcpSession session)
        {
            base.OnConnected(session);
        }
        protected override void OnConnecting(TcpSession session)
        {
            base.OnConnecting(session);
        }
        protected override void OnDisconnected(TcpSession session)
        {
            base.OnDisconnected(session);
        }
        protected override TcpSession CreateSession()
        {
            return new QamarTcpSession(this);
        }
        

    }
}
