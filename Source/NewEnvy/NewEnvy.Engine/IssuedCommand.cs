namespace NewEnvy.Engine
{
   public class IssuedCommand
   {
      private readonly string _command;
      
      public string Command
      {
         get
         {
            return _command;
         }
      }

      private readonly ClientConnection _sender;

      public ClientConnection Sender
      {
         get
         {
            return _sender;
         }
      }

      public IssuedCommand( ClientConnection sender, string command )
      {
         _sender = sender;
         _command = command;
      }
   }
}
