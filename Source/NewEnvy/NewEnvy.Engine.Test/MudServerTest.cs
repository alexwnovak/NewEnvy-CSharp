using System;
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
      public void Run_()
      {
         var tcpServerMock = new Mock<ITcpServer>();
         Dependency.RegisterInstance( tcpServerMock.Object );

         var mudServer = new MudServer();

         mudServer.Run();
      }
   }
}
