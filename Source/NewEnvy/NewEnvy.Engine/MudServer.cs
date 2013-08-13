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
         TcpListener server = null;

         try
         {
            var tcpServer = Dependency.Resolve<ITcpServer>();

            server = new TcpListener( _localAddress, _port );

            server.Start();

            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop. 
            while ( true )
            {
               Console.Write( "Waiting for a connection... " );

               // Perform a blocking call to accept requests. 
               // You could also user server.AcceptSocket() here.
               TcpClient client = server.AcceptTcpClient();
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
                     // Translate data bytes to a ASCII string.
                     data = System.Text.Encoding.ASCII.GetString( bytes, 0, i );
                     Console.WriteLine( "Received: {0}", data );

                     // Process the data sent by the client.
                     data = data.ToUpper();

                     byte[] msg = System.Text.Encoding.ASCII.GetBytes( data );

                     // Send back a response.
                     stream.Write( msg, 0, msg.Length );
                     Console.WriteLine( "Sent: {0}", data );
                  }

                  // Shutdown and end connection
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
            server.Stop();
         }

         Console.WriteLine( "\nHit enter to continue..." );
         Console.Read();
      }
   }
}
