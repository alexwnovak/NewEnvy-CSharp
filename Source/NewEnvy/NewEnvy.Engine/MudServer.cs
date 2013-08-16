using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
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
         
         var tcpServer = Dependency.Resolve<ITcpServer>();

         try
         {
            tcpServer.Start( _port );

            var bytes = new byte[256];

            while ( true )
            {
               Console.Write( "Waiting for a connection... " );

               TcpClient client = tcpServer.Accept();

               Console.WriteLine( "Connected!" );

               NetworkStream stream = client.GetStream();

               try
               {
                  int bytesRead;

                  while ( ( bytesRead = stream.Read( bytes, 0, bytes.Length ) ) != 0 )
                  {
                     string data = System.Text.Encoding.ASCII.GetString( bytes, 0, bytesRead );
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
