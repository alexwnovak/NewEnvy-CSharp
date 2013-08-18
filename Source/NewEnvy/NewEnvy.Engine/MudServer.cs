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
         connectionListener.StartAsync();

         while ( IsRunning )
         {
            var serverClock = Dependency.Resolve<IServerClock>();
            serverClock.StartClock();

            GlobalCommandQueue.ProcessCommands();

            serverClock.EndClockAndWait();
         }
      }

      public void Stop()
      {
         IsRunning = false;
      }
   }
}
