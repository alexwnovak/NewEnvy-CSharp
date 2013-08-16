using System.Collections.Concurrent;

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
         string command;
         _commandQueue.TryDequeue( out command );

         if ( command == "/shutdown" )
         {
            
         }
      }
   }
}
