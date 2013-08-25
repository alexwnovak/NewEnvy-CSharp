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
      public void Run_HappyPath_StartsServerAndMainLoopExecutesMajorComponents()
      {
         var mudServer = new MudServer();
         
         // Setup

         var connectionListenerMock = new Mock<IConnectionListener>();
         Dependency.RegisterInstance( connectionListenerMock.Object );

         var globalCommandQueueMock = new Mock<IGlobalCommandQueue>();
         Dependency.RegisterInstance( globalCommandQueueMock.Object );

         var serverClockMock = new Mock<IServerClock>();
         serverClockMock.Setup( scm => scm.EndClockAndWait() ).Callback( mudServer.Stop );
         Dependency.RegisterInstance( serverClockMock.Object );

         // Test

         mudServer.Run();

         // Assert

         connectionListenerMock.Verify( clm => clm.StartAsync(), Times.Once() );
         serverClockMock.Verify( scm => scm.StartClock(), Times.Once() );
         globalCommandQueueMock.Verify( gcqm => gcqm.ProcessCommands(), Times.Once() );
         serverClockMock.Verify( scm => scm.EndClockAndWait(), Times.Once() );
      }
   }
}
