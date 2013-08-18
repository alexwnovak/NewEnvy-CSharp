using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class MudApplication
   {
      private static void InitDependencyInjection()
      {
         Dependency.CreateUnityContainer();

         Dependency.RegisterType<IConnectionListener, ConnectionListener>();
         Dependency.RegisterInstance( ClientManager.Instance );
         Dependency.RegisterInstance( GlobalCommandQueue.Instance );
         Dependency.RegisterType<IServerClock, ServerClock>();
      }

      public void Main()
      {
         InitDependencyInjection();

         var mudServer = new MudServer();
         mudServer.Run();
      }
   }
}
