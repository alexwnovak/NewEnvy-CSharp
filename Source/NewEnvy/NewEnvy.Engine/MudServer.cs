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

      public void Run()
      {
         IsRunning = true;

         var connectionListener = Dependency.Resolve<IConnectionListener>();
         connectionListener.ClientConnected += OnClientConnected;
         connectionListener.StartAsync();

         while ( IsRunning )
         {
            //var serverClock = Dependency.Resolve<IServerClock>();
            //serverClock.StartClock();

            //GlobalCommandQueue.Instance.ProcessCommands();

            //serverClock.EndClockAndWait();

            Thread.Sleep( 500 );
         }
      }

      private void OnClientConnected( object sender, ClientConnectedEventArgs e )
      {
         Console.WriteLine( "Client connected: " + e.ClientConnection.IPAddress );
      }

      public void Stop()
      {
         IsRunning = false;
      }
   }
}
