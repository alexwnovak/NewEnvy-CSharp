using System;
using System.Collections.Concurrent;

namespace NewEnvy.Engine
{
   public class GlobalConnectionTable
   {
      private readonly ConcurrentDictionary<int, ClientConnection> _clientConnections = new ConcurrentDictionary<int, ClientConnection>();
      private readonly CommandProcessor _commandProcessor = new CommandProcessor();

      public void SendAll()
      {
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
      }

      private void OnReceivedCommand( object sender, CommandEventArgs e )
      {
         _commandProcessor.Process( e.ClientConnection, e.Command );

         //e.ClientConnection.Send( "Your command: " + e.Command );
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
