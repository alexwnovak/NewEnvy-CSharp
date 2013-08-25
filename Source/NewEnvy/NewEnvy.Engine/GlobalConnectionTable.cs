using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class GlobalConnectionTable
   {
      private readonly ConcurrentDictionary<int, ClientConnection> _clientConnections = new ConcurrentDictionary<int, ClientConnection>();

      //public ConcurrentDictionary<int, ClientConnection> ClientConnections
      //{
      //   get
      //   {
      //      return _clientConnections;
      //   }
      //}

      public void SendAll()
      {
         Console.WriteLine( "Flushing for all clients" );

         foreach ( var keyValuePair in _clientConnections )
         {
            keyValuePair.Value.Flush();
         }
      }

      public void RegisterConnection( ClientConnection clientConnection )
      {
         AddConnection( clientConnection );

         clientConnection.ReceivedCommand += OnReceivedCommand;
         clientConnection.Disconnected += OnClientDisconnect;
         clientConnection.BeginReceiving();

         //var clientManager = Dependency.Resolve<IClientManager>();
         //clientManager.OnClientConnected( new ClientConnectedEventArgs( clientConnection ) );
      }

      private void OnReceivedCommand( object sender, CommandEventArgs e )
      {
         e.ClientConnection.Send( "Your command: " + e.Command );
      }

      private void OnClientDisconnect( object sender, ClientConnectionEventArgs e )
      {
         var clientConnection = e.ClientConnection;

         _clientConnections.TryRemove( clientConnection.ConnectionId, out clientConnection );

         Console.WriteLine( "Client disconnected" );
      }

      private void AddConnection( ClientConnection clientConnection )
      {
         clientConnection.ConnectionId = GenerateConnectionId();

         _clientConnections.TryAdd( clientConnection.ConnectionId, clientConnection );
      }

      private int GenerateConnectionId()
      {
         return _clientConnections.Keys.Count + 1;
      }
   }
}
