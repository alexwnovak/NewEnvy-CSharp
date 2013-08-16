using System;
using System.Threading;

namespace NewEnvy.Engine
{
   public class ServerClock : IServerClock
   {
      private DateTime _startTime;

      public void StartClock()
      {
         _startTime = DateTime.UtcNow;
      }

      public void EndClockAndWait()
      {
         TimeSpan elapsedTime = DateTime.UtcNow - _startTime;

         const double waitTime = 100;

         Thread.Sleep( TimeSpan.FromMilliseconds( waitTime ) );

         //Console.WriteLine( "Waiting for " + waitTime + " msec" );
      }
   }
}
