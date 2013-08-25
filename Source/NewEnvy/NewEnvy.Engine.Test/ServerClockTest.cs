using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewEnvy.Core;

namespace NewEnvy.Engine.Test
{
   [TestClass]
   public class ServerClockTest
   {
      [TestInitialize]
      public void Initialize()
      {
         Dependency.CreateUnityContainer();
      }

      [TestMethod]
      public void Reset_ResetStartTime_GetsUtcNow()
      {
         // Setup

         var dateTimeMock = new Mock<IDateTime>();
         Dependency.RegisterInstance( dateTimeMock.Object );

         // Test

         var serverClock = new ServerClock();

         serverClock.Reset();

         // Assert

         dateTimeMock.Verify( dtm => dtm.UtcNow, Times.Once() );
      }

      //[TestMethod]
      //public void Pulse_()
      //{
      //   var utcNow = DateTime.UtcNow;

      //   // Setup

      //   var dateTimeMock = new Mock<IDateTime>();
      //   dateTimeMock.Setup( dtm => dtm.UtcNow ).Returns( utcNow );
      //   Dependency.RegisterInstance( dateTimeMock.Object );

      //   // Test

      //   var serverClock = new ServerClock();

      //   serverClock.Pulse();
      //}
   }
}
