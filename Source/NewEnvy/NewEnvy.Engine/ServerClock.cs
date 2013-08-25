using System;
using System.Threading;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class ServerClock
   {
      public TimeSpan ElapsedTime
      {
         get;
         private set;
      }

      public void Reset()
      {
         ElapsedTime = TimeSpan.MinValue;
      }
   }
}
