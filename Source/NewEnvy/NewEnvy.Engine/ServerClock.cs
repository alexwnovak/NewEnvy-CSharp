using System;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class ServerClock
   {
      private DateTime _startTime;
      private bool _hasReset;

      public TimeSpan HeartbeatThreshold
      {
         get;
         set;
      }

      public event EventHandler Heartbeat = null;

      public ServerClock()
      {
         HeartbeatThreshold = TimeSpan.FromMilliseconds( 100 );
      }

      public void Reset()
      {
         _startTime = Dependency.Resolve<IDateTime>().UtcNow;

         _hasReset = true;
      }

      public void Pulse()
      {
         if ( !_hasReset )
         {
            throw new InvalidOperationException( "Reset must be called before Pulse." );
         }

         var dateTime = Dependency.Resolve<IDateTime>();
         var utcNow = dateTime.UtcNow;

         var elapsedTime = utcNow - _startTime;

         if ( elapsedTime.TotalMilliseconds >= HeartbeatThreshold.TotalMilliseconds )
         {
            OnHeartbeat( EventArgs.Empty );
         }
      }

      protected virtual void OnHeartbeat( EventArgs e )
      {
         var eventHandler = Heartbeat;

         if ( eventHandler != null )
         {
            eventHandler( this, e );
         }
      }
   }
}
