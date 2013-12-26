using NewEnvy.Core;
using NewEnvy.Engine;

namespace NewEnvy.EntryPoint
{
   public class Program
   {
      private static void InitDependencyInjection()
      {
         Dependency.CreateUnityContainer();

         Dependency.RegisterType<IConnectionListener, ConnectionListener>();
         Dependency.RegisterType<IServerConfiguration, ServerConfiguration>();
         Dependency.RegisterType<IDateTime, DateTimeAdapter>();

         Dependency.RegisterInstance<IGlobalConnectionTable>( new GlobalConnectionTable() );
         Dependency.RegisterInstance<IGlobalCommandQueue>( new GlobalCommandQueue() );
         //Dependency.RegisterInstance( ClientManager.Instance );
         //Dependency.RegisterInstance( GlobalCommandQueue.Instance );
         //Dependency.RegisterType<IServerClock, ServerClock>();
      }

      public static void Main()
      {
         InitDependencyInjection();

         new MudApplication().Start();
      }
   }
}
