using System;
using System.Threading;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class ServerClock
   {
      private DateTime _startTime;

      public TimeSpan ElapsedTime
      {
         get;
         private set;
      }

      public void Reset()
      {
         var dateTime = Dependency.Resolve<IDateTime>();

         _startTime = dateTime.UtcNow;
      }

      public void Pulse()
      {
         
      }
   }
}
