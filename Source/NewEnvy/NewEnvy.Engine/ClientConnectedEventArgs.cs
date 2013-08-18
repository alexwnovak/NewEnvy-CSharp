using System;

namespace NewEnvy.Engine
{
   public class ClientConnectedEventArgs : EventArgs
   {
      private readonly ClientConnection _clientConnection;

      public ClientConnection ClientConnection
      {
         get
         {
            return _clientConnection;
         }
      }

      public ClientConnectedEventArgs( ClientConnection clientConnection )
      {
         _clientConnection = clientConnection;
      }
   }
}
