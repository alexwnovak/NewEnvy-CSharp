using System;

namespace NewEnvy.Engine
{
   public interface IConnectionListener
   {
      event EventHandler<ClientConnectedEventArgs> ClientConnected;

      void StartAsync();

      void Stop();
   }
}
