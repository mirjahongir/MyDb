using QamarDb.Servres;

namespace QamarDb.Sessions
{
    internal class QamarTcpSession : NetCoreServer.TcpSession
    {

        public QamarTcpSession(QamarTcpServer server) : base(server)
        {

        }
        protected override void OnConnected()
        {

        }
        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            ReadOnlySpan<byte> data = buffer;

        }
        
        public override void ReceiveAsync()
        {
            
        }
    }
}
