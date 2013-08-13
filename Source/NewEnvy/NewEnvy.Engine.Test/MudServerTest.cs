using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewEnvy.Core;

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
         var mudServer = new MudServer();

         mudServer.Run();
      }
   }
}
