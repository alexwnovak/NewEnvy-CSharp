using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NewEnvy.Engine.Test
{
   [TestClass]
   public class ConnectionListenerTest
   {
      [TestMethod]
      public void StartAsync_()
      {
         var connectionListener = new ConnectionListener();

         connectionListener.StartAsync();

         connectionListener.ThreadProc();
      }
   }
}
