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

      private readonly IGlobalConnectionTable _globalConnectionTable;

      public MudServer()
      {
         _globalConnectionTable = Dependency.Resolve<IGlobalConnectionTable>();
      }

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

         // This is our big kahuna main loop

         while ( IsRunning )
         {
            serverClock.Pulse();
         }
      }

      public void Stop()
      {
         IsRunning = false;
      }

      private void OnClientConnected( object sender, ClientConnectionEventArgs e )
      {
         _globalConnectionTable.RegisterConnection( e.ClientConnection );
      }
   }
}
