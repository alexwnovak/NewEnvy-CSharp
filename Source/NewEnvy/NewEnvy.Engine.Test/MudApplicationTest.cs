using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewEnvy.Core;

namespace NewEnvy.Engine.Test
{
   [TestClass]
   public class MudApplicationTest
   {
      [TestInitialize]
      public void Initialize()
      {
         Dependency.CreateUnityContainer();
      }

      [TestMethod]
      public void Start_HappyPath_LoggingSubsystemIsLoaded()
      {
         var loggingSubsystemMock = new Mock<ISubsystem>();
         
         // Setup

         var subsystemLoaderMock = new Mock<ISubsystemLoader>();
         subsystemLoaderMock.Setup( sl => sl.LoadLoggingSubsystem() ).Returns( loggingSubsystemMock.Object );
         Dependency.RegisterInstance( subsystemLoaderMock.Object );

         // Test

         var mudApplication = new MudApplication();

         mudApplication.Start();

         // Assert

         subsystemLoaderMock.Verify( sl => sl.LoadLoggingSubsystem(), Times.Once() );
         loggingSubsystemMock.Verify( ls => ls.Start(), Times.Once() );
      }
   }
}
