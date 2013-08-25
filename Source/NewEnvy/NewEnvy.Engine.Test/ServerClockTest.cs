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
      public void Reset_ResetElapsedTime_TimeIsResetToMinValue()
      {
         var serverClock = new ServerClock();

         serverClock.Reset();

         // Assert

         Assert.AreEqual( TimeSpan.MinValue, serverClock.ElapsedTime );
      }

      //[TestMethod]
      //public void Reset_()
      //{
      //   var utcNow = DateTime.UtcNow;

      //   // Setup

      //   var dateTimeMock = new Mock<IDateTime>();
      //   dateTimeMock.Setup( dtm => dtm.UtcNow ).Returns( utcNow );
      //   Dependency.RegisterInstance( dateTimeMock.Object );

      //   // Test

      //   var serverClock = new ServerClock();

      //   serverClock.Reset();

      //   // Verify

      //   dateTimeMock.Verify( dtm => dtm.UtcNow, Times.Once() );
      //}
   }
}
