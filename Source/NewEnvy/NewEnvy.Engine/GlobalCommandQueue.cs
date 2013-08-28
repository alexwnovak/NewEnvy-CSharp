using System.Collections.Concurrent;

namespace NewEnvy.Engine
{
   public class GlobalCommandQueue : IGlobalCommandQueue
   {
      private readonly ConcurrentQueue<IssuedCommand> _commandQueue = new ConcurrentQueue<IssuedCommand>();

      public void AddCommand( ClientConnection sender, string command )
      {
         var issuedCommand = new IssuedCommand( sender, command );

         _commandQueue.Enqueue( issuedCommand );
      }

      public void ProcessCommands()
      {
         throw new System.NotImplementedException();
      }
   }
}
