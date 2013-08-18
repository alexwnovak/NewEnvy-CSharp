using System.Collections.Concurrent;
using NewEnvy.Engine.CommandModel;

namespace NewEnvy.Engine
{
   public static class GlobalCommandQueue
   {
      private static readonly ConcurrentQueue<string> _commandQueue = new ConcurrentQueue<string>();

      public static void AddCommand( string command )
      {
         _commandQueue.Enqueue( command );
      }

      public static void ProcessCommands()
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
