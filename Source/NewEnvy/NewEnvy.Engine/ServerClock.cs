using System;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class ServerClock
   {
      private DateTime _startTime;
      private bool _hasReset;
      private TimeSpan _heartbeatThreshold;

      public TimeSpan ElapsedTime
      {
         get;
         private set;
      }

      public event EventHandler Heartbeat = null;

      public ServerClock()
      {
         ElapsedTime = TimeSpan.FromMilliseconds( 0 );

         var serverConfiguration = Dependency.Resolve<IServerConfiguration>();
         var heartbeatThreshold = serverConfiguration.Get<int>( "HeartbeatThreshold" );
         _heartbeatThreshold = TimeSpan.FromMilliseconds( heartbeatThreshold );
      }

      public void Reset()
      {
         var dateTime = Dependency.Resolve<IDateTime>();

         _startTime = dateTime.UtcNow;

         ElapsedTime = TimeSpan.FromMilliseconds( 0 );

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

         ElapsedTime += ( utcNow - _startTime );

         if ( ElapsedTime > _heartbeatThreshold )
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
