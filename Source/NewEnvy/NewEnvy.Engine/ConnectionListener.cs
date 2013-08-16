using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NewEnvy.Engine
{
   public class ConnectionListener : IConnectionListener
   {
      public void StartAsync()
      {
         Console.WriteLine( "Starting connection listener" );
         Task.Factory.StartNew( ThreadProc );
      }

      public void Stop()
      {
      }

      private void ThreadProc()
      {
         TcpListener tcpListener = null;

         try
         {
            tcpListener = new TcpListener( IPAddress.Loopback, 4000 );

            while ( true )
            {
               tcpListener.Start();

               Console.Write( "Waiting for a connection... " );

               TcpClient client = tcpListener.AcceptTcpClient();

               Console.WriteLine( "Connected!" );

               GlobalConnectionTable.AddConnection( client );
            }
         }
         catch ( SocketException e )
         {
            Console.WriteLine( "SocketException: {0}", e );
         }
         finally
         {
            tcpListener.Stop();
         }
      }
   }
}
