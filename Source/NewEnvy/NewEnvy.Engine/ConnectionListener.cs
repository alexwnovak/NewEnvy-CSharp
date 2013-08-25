using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NewEnvy.Engine
{
   public class ConnectionListener : IConnectionListener
   {
      public event EventHandler<ClientConnectedEventArgs> ClientConnected = null;

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
            tcpListener.Start();

            while ( true )
            {
               Console.Write( "Waiting for a connection... " );

               TcpClient client = tcpListener.AcceptTcpClient();

               var clientConnection = new ClientConnection( client );
               OnClientConnected( new ClientConnectedEventArgs( clientConnection ) );

               Console.WriteLine( "Connected!" );
            }
         }
         catch ( SocketException e )
         {
            Console.WriteLine( "SocketException: {0}", e );
         }
         finally
         {
            if ( tcpListener != null )
            {
               tcpListener.Stop();
            }
         }
      }

      protected virtual void OnClientConnected( ClientConnectedEventArgs e )
      {
         var ev = ClientConnected;

         if ( ev != null )
         {
            ev( this, e );
         }
      }
   }
}
