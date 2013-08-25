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
      public void ElapsedTime_InitialState_ElapsedTimeIsZero()
      {
         var serverClock = new ServerClock();

         Assert.AreEqual( TimeSpan.FromMilliseconds( 0 ), serverClock.ElapsedTime );
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

      [TestMethod]
      [ExpectedException( typeof( InvalidOperationException ) )]
      public void Pulse_HasNotCalledReset_ThrowsInvalidOperationException()
      {
         var serverClock = new ServerClock();

         serverClock.Pulse();
      }

      [TestMethod]
      public void Pulse_HappyPath_AddsToElapsedTime()
      {
         var noon = new DateTime( 2013, 8, 25, 12, 0, 0 );
         var fiveSecondsAfterNoon = new DateTime( 2013, 8, 25, 12, 0, 5 );

         // Setup

         var dateTimeMock = new Mock<IDateTime>();
         dateTimeMock.Setup( dtm => dtm.UtcNow ).ReturnsInOrder( noon, fiveSecondsAfterNoon );
         Dependency.RegisterInstance( dateTimeMock.Object );

         // Test

         var serverClock = new ServerClock();

         serverClock.Reset();

         serverClock.Pulse();

         // Assert

         Assert.AreEqual( TimeSpan.FromSeconds( 5 ), serverClock.ElapsedTime );
      }

      [TestMethod]
      public void Pulse_ElapsedTimeSurpassesHeartbeatThreshold_RaisesHeartbeatEvent()
      {
         var noon = new DateTime( 2013, 8, 25, 12, 0, 0 );
         var fiveSecondsAfterNoon = new DateTime( 2013, 8, 25, 12, 0, 5 );
         bool raisedEvent = false;

         // Setup

         var dateTimeMock = new Mock<IDateTime>();
         dateTimeMock.Setup( dtm => dtm.UtcNow ).ReturnsInOrder( noon, fiveSecondsAfterNoon );
         Dependency.RegisterInstance( dateTimeMock.Object );

         // Test

         var serverClock = new ServerClock();

         serverClock.Heartbeat += ( sender, e ) =>
         {
            raisedEvent = true;
         };

         serverClock.Reset();

         serverClock.Pulse();

         // Assert

         Assert.IsTrue( raisedEvent );
      }
   }
}
