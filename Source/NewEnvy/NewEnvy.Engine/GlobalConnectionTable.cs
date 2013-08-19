using System.Collections.Concurrent;
using System.Net.Sockets;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class GlobalConnectionTable
   {
      private static readonly GlobalConnectionTable _instance = new GlobalConnectionTable();

      public static GlobalConnectionTable Instance
      {
         get
         {
            return _instance;
         }
      }

      private readonly ConcurrentDictionary<int, ClientConnection> _clientConnections = new ConcurrentDictionary<int, ClientConnection>();

      public ConcurrentDictionary<int, ClientConnection> ClientConnections
      {
         get
         {
            return _clientConnections;
         }
      }

      private GlobalConnectionTable()
      {
      }

      public void AddConnection( TcpClient tcpClient )
      {
         int connectionId = _clientConnections.Keys.Count + 1;
         var clientConnection = new ClientConnection( connectionId, tcpClient );

         _clientConnections.TryAdd( connectionId, clientConnection );

         var clientManager = Dependency.Resolve<IClientManager>();
         clientManager.OnClientConnected( new ClientConnectedEventArgs( clientConnection ) );
      }
   }
}
