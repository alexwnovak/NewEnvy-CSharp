using System;

namespace NewEnvy.Engine
{
   public class CommandEventArgs : EventArgs
   {
      public ClientConnection ClientConnection
      {
         get;
         private set;
      }

      public string Command
      {
         get;
         private set;
      }

      public CommandEventArgs( ClientConnection clientConnection, string command )
      {
         ClientConnection = clientConnection;
         Command = command;
      }
   }
}
