using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class MudServer
   {
      private static readonly int _port = 13000;
      private static readonly IPAddress _localAddress = IPAddress.Loopback;

      public void Run()
      {
         // Steps:
         //   1. Start up the server to accept incoming connections
         //     a. On connection, spin up a connection thread. This is a conduit from the user to the server
         //     b. This guy receives data from each individual user and incorporates the commands into the game ecosystem
         
         ITcpServer tcpServer = Dependency.Resolve<ITcpServer>();

         try
         {
            tcpServer.Start( _port );

            byte[] bytes = new byte[256];
            string data = null;

            while ( true )
            {
               Console.Write( "Waiting for a connection... " );

               TcpClient client = tcpServer.Accept();

               Console.WriteLine( "Connected!" );

               data = null;

               // Get a stream object for reading and writing
               NetworkStream stream = client.GetStream();

               int i;

               // Loop to receive all the data sent by the client. 
               try
               {
                  while ( ( i = stream.Read( bytes, 0, bytes.Length ) ) != 0 )
                  {
                     data = System.Text.Encoding.ASCII.GetString( bytes, 0, i );
                     Console.WriteLine( "Received: {0}", data );

                     data = data.ToUpper();

                     byte[] msg = System.Text.Encoding.ASCII.GetBytes( data );

                     stream.Write( msg, 0, msg.Length );
                     Console.WriteLine( "Sent: {0}", data );
                  }

                  client.Close();
               }
               catch ( IOException )
               {
                  client.Close();
               }
            }
         }
         catch ( SocketException e )
         {
            Console.WriteLine( "SocketException: {0}", e );
         }
         finally
         {
            tcpServer.Stop();
         }

         Console.WriteLine( "\nHit enter to continue..." );
         Console.Read();
      }
   }
}
