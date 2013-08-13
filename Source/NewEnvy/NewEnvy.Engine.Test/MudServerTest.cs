using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NewEnvy.Engine.Test
{
   [TestClass]
   public class MudServerTest
   {
      [TestMethod]
      public void Run_()
      {
         var mudServer = new MudServer();

         mudServer.Run();
      }
   }
}
