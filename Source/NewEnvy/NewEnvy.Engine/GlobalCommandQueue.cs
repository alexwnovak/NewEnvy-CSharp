using System.Collections.Concurrent;
using NewEnvy.Core;
using NewEnvy.Engine.CommandModel;

namespace NewEnvy.Engine
{
   public class GlobalCommandQueue : IGlobalCommandQueue
   {
      private static readonly IGlobalCommandQueue _globalCommandQueue = Dependency.Resolve<IGlobalCommandQueue>();

      public static IGlobalCommandQueue Instance
      {
         get
         {
            return _globalCommandQueue;
         }
      }

      private static readonly ConcurrentQueue<IssuedCommand> _commandQueue = new ConcurrentQueue<IssuedCommand>();

      private GlobalCommandQueue()
      {
      }

      public void AddCommand( ClientConnection sender, string command )
      {
         var issuedCommand = new IssuedCommand( sender, command );

         _commandQueue.Enqueue( issuedCommand );
      }

      public void ProcessCommands()
      {
         IssuedCommand issuedCommand;

         if ( _commandQueue.TryDequeue( out issuedCommand ) )
         {
            if ( issuedCommand.Command == "/gct" )
            {
               System.Diagnostics.Debug.WriteLine( "Received /gct" );
               var command = new GctCommand();

               string output = command.Execute();

               issuedCommand.Sender.Send( output );
            }
            else if ( issuedCommand.Command == "quit" )
            {
               var command = new QuitCommand();

               string output = command.Execute( issuedCommand.Sender );

               issuedCommand.Sender.Send( output );
            }
         }
      }
   }
}
