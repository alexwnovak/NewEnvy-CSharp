using System;

namespace NewEnvy.Engine
{
   public class ClientConnectionEventArgs : EventArgs
   {
      private readonly ClientConnection _clientConnection;

      public ClientConnection ClientConnection
      {
         get
         {
            return _clientConnection;
         }
      }

      public ClientConnectionEventArgs( ClientConnection clientConnection )
      {
         _clientConnection = clientConnection;
      }
   }
}
