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
      public void Start_HappyPath_SubsystemsAreLoaded()
      {
         // Setup

         var subsystemLoaderMock = new Mock<ISubsystemLoader>();
         Dependency.RegisterInstance( subsystemLoaderMock.Object );

         // Test

         var mudApplication = new MudApplication();

         mudApplication.Start();

         // Assert

         subsystemLoaderMock.Verify( sl => sl.LoadAll(), Times.Once() );
      }
   }
}
