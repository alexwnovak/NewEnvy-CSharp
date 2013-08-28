using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class CommandProcessor
   {
      public void Process( ClientConnection clientConnection, string command )
      {
         if ( command == "/gct" )
         {
            DisplayGlobalCommandTable( clientConnection );
         }
         else if ( command == "quit" )
         {
            Quit( clientConnection );
         }
         else
         {
            string output = string.Format( "\"{0}\" isn't a recognized command.", command );

            clientConnection.Send( output );
         }
      }

      private void DisplayGlobalCommandTable( ClientConnection clientConnection )
      {
         var globalConnectionTable = Dependency.Resolve<IGlobalConnectionTable>();

         var connectedClients = globalConnectionTable.GetClientConnections();

         var stringBuilder = new StringBuilder();
         stringBuilder.AppendLine( new string( '=', 78 ) );
         stringBuilder.AppendLine( " Global Connection Table" );
         stringBuilder.AppendLine( new string( '=', 78 ) );

         foreach ( var connection in connectedClients )
         {
            stringBuilder.AppendLine( connection.ConnectionId.ToString() );
         }

         clientConnection.Send( stringBuilder.ToString() );
      }

      private void Quit( ClientConnection clientConnection )
      {
         var stringBuilder = new StringBuilder();
         stringBuilder.AppendLine( "See ya." );

         clientConnection.Send( stringBuilder.ToString() );

         // Need a way to wait for flushes to ensure the output goes, and THEN act
         // Maybe a way to stash an action in the next go around?

         clientConnection.Disconnect();
      }
   }
}
