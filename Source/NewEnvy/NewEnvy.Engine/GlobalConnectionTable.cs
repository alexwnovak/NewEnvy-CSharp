using System.Collections.Concurrent;
using System.Net.Sockets;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public static class GlobalConnectionTable
   {
      private static readonly ConcurrentDictionary<int, ClientConnection> _clientConnections = new ConcurrentDictionary<int, ClientConnection>();

      public static void AddConnection( TcpClient tcpClient )
      {
         int connectionId = _clientConnections.Keys.Count + 1;
         var clientConnection = new ClientConnection( connectionId, tcpClient );

         _clientConnections.TryAdd( connectionId, clientConnection );

         var clientManager = Dependency.Resolve<IClientManager>();
         clientManager.OnClientConnected( new ClientConnectedEventArgs( clientConnection ) );
      }
   }
}
