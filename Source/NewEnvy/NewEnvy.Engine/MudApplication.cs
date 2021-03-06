﻿using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class MudApplication
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

      public void Main()
      {
         InitDependencyInjection();

         var mudServer = new MudServer();
         mudServer.Run();
      }
   }
}
