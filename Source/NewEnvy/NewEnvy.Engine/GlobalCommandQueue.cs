using System.Collections.Concurrent;
using NewEnvy.Engine.CommandModel;

namespace NewEnvy.Engine
{
   public class GlobalCommandQueue : IGlobalCommandQueue
   {
      private static readonly GlobalCommandQueue _globalCommandQueue = new GlobalCommandQueue();

      public static GlobalCommandQueue Instance
      {
         get
         {
            return _globalCommandQueue;
         }
      }
      
      private static readonly ConcurrentQueue<string> _commandQueue = new ConcurrentQueue<string>();

      private GlobalCommandQueue()
      {
      }

      public void AddCommand( string command )
      {
         _commandQueue.Enqueue( command );
      }

      public void ProcessCommands()
      {
         string commandString;
         _commandQueue.TryDequeue( out commandString );

         if ( commandString == "/gct" )
         {
            var command = new GctCommand();
            command.Execute();
         }
      }
   }
}
