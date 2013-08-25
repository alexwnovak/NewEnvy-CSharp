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
         clientConnection.BeginReceiving();

         //var clientManager = Dependency.Resolve<IClientManager>();
         //clientManager.OnClientConnected( new ClientConnectedEventArgs( clientConnection ) );
      }

      private void OnReceivedCommand( object sender, CommandEventArgs e )
      {
         e.ClientConnection.Send( "Your command: " + e.Command );
      }

      private void AddConnection( ClientConnection clientConnection )
      {
         int connectionId = GenerateConnectionId();

         _clientConnections.TryAdd( connectionId, clientConnection );
      }

      private int GenerateConnectionId()
      {
         return _clientConnections.Keys.Count + 1;
      }
   }
}
