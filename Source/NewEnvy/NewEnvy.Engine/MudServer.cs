using System;
using System.Threading;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class MudServer
   {
      public bool IsRunning
      {
         get;
         private set;
      }

      private readonly GlobalConnectionTable _globalConnectionTable = new GlobalConnectionTable();

      public void Run()
      {
         IsRunning = true;

         var connectionListener = Dependency.Resolve<IConnectionListener>();
         connectionListener.ClientConnected += OnClientConnected;
         connectionListener.StartAsync();

         var serverClock = new ServerClock();
         serverClock.Reset();

         serverClock.Heartbeat += ( sender, e ) =>
         {
            _globalConnectionTable.SendAll();
            serverClock.Reset();
         };

         while ( IsRunning )
         {
            serverClock.Pulse();

            //var serverClock = Dependency.Resolve<IServerClock>();
            //serverClock.StartClock();

            //GlobalCommandQueue.Instance.ProcessCommands();

            //serverClock.EndClockAndWait();
         }
      }

      private void OnClientConnected( object sender, ClientConnectionEventArgs e )
      {
         _globalConnectionTable.RegisterConnection( e.ClientConnection );
         //Console.WriteLine( "Client connected: " + e.ClientConnection.IPAddress );
      }

      public void Stop()
      {
         IsRunning = false;
      }
   }
}
