using System;
using System.Text;

namespace NewEnvy.Engine.CommandModel
{
   public class GctCommand
   {
      public string Execute()
      {
         var stringBuilder = new StringBuilder();
         stringBuilder.AppendLine( new string( '=', 78 ) );
         stringBuilder.AppendLine( " Global Connection Table" );
         stringBuilder.AppendLine( new string( '=', 78 ) );

         var clientConnections = GlobalConnectionTable.Instance.ClientConnections;

         foreach ( int key in clientConnections.Keys )
         {
            ClientConnection clientConnection;

            if ( clientConnections.TryGetValue( key, out clientConnection ) )
            {
               stringBuilder.AppendFormat( "  {0} {1}", clientConnection.ConnectionId, Environment.NewLine );
            }
         }

         return stringBuilder.ToString();
      }
   }
}
