using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace NewEnvy.Engine
{
   public static class GlobalConnectionTable
   {
      private static readonly ConcurrentDictionary<int, ClientConnection> _clientConnections = new ConcurrentDictionary<int, ClientConnection>();

      public static event EventHandler<ClientConnectedEventArgs> ClientConnected = null;

      public static void AddConnection( TcpClient tcpClient )
      {
         int connectionId = _clientConnections.Keys.Count + 1;
         var clientConnection = new ClientConnection( connectionId, tcpClient );

         _clientConnections.TryAdd( connectionId, clientConnection );

         OnClientConnected( clientConnection );
      }

      private static void OnClientConnected( ClientConnection clientConnection )
      {
         EventHandler<ClientConnectedEventArgs> eventHandler = ClientConnected;

         if ( eventHandler != null )
         {
            eventHandler( null, new ClientConnectedEventArgs( clientConnection ) );
            Console.WriteLine( "Notifying of new connection" );
         }
      }
   }
}
