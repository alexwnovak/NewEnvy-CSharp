using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewEnvy.Core;
using Moq;

namespace NewEnvy.Engine.Test
{
   [TestClass]
   public class MudServerTest
   {
      [TestInitialize]
      public void Initialize()
      {
         Dependency.CreateUnityContainer();
      }

      [TestMethod]
      public void Run_HappyPath_StartsServerAndRunsMainLoop()
      {
         var mudServer = new MudServer();
         
         // Setup

         var connectionListenerMock = new Mock<IConnectionListener>();
         Dependency.RegisterInstance( connectionListenerMock.Object );

         //var serverClockMock = new Mock<IServerClock>();
         //serverClockMock.Setup( scm => scm.Wait() ).Callback( () => mudServer.IsRunning = false );
         //Dependency.RegisterInstance( serverClockMock.Object );

         // Test

         mudServer.Run();

         // Assert

         connectionListenerMock.Verify( clm => clm.StartAsync(), Times.Once() );
         //serverClockMock.Verify( scm => scm.Wait(), Times.Once() );
      }
   }
}
