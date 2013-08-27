using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnvy.Engine
{
   public class CommandProcessor
   {
      public void Process( ClientConnection clientConnection, string command )
      {
         if ( command == "/gct" )
         {
            clientConnection.Send( "Global command table" );
         }
         else
         {
            string output = string.Format( "\"{0}\" isn't a recognized command.", command );

            clientConnection.Send( output );
         }
      }
   }
}
