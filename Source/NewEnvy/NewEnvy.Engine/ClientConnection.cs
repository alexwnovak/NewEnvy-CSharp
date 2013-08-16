using System.Net.Sockets;

namespace NewEnvy.Engine
{
   public class ClientConnection
   {
      public int ConnectionId
      {
         get;
         private set;
      }

      public TcpClient TcpClient
      {
         get;
         private set;
      }
      
      public ClientConnection( int connectionId, TcpClient tcpClient )
      {
         ConnectionId = connectionId;
         TcpClient = tcpClient;
      }
   }
}
