﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class GlobalConnectionTable : IGlobalConnectionTable
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
         var globalCommandQueue = Dependency.Resolve<IGlobalCommandQueue>();

         globalCommandQueue.AddCommand( e.ClientConnection, e.Command );
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

      public ClientConnection[] GetClientConnections()
      {
         return _clientConnections.Select( keyValuePair => keyValuePair.Value ).ToArray();
      }
   }
}
