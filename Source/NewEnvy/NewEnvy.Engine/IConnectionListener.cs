using System;

namespace NewEnvy.Engine
{
   public interface IConnectionListener
   {
      event EventHandler ClientConnected;

      void StartAsync();

      void Stop();
   }
}
