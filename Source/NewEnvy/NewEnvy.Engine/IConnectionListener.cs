using System;

namespace NewEnvy.Engine
{
   public interface IConnectionListener
   {
      event EventHandler<ClientConnectionEventArgs> ClientConnected;

      void StartAsync();

      void Stop();
   }
}
