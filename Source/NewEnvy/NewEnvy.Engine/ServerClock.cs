using System;
using System.Threading;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class ServerClock
   {
      private DateTime _startTime;
      private bool _hasReset;

      public TimeSpan ElapsedTime
      {
         get;
         private set;
      }

      public void Reset()
      {
         var dateTime = Dependency.Resolve<IDateTime>();

         _startTime = dateTime.UtcNow;

         _hasReset = true;
      }

      public void Pulse()
      {
         if ( !_hasReset )
         {
            throw new InvalidOperationException( "Reset must be called before Pulse." );
         }

         var dateTime = Dependency.Resolve<IDateTime>();

         ElapsedTime += dateTime.UtcNow - _startTime;
      }
   }
}
