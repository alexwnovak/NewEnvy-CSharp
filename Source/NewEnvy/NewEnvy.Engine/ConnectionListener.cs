using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NewEnvy.Engine
{
   public class ConnectionListener : IConnectionListener
   {
      public event EventHandler ClientConnected = null;

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

               OnClientConnected( EventArgs.Empty );

               Console.WriteLine( "Connected!" );

               //GlobalConnectionTable.Instance.AddConnection( client );
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

      protected virtual void OnClientConnected( EventArgs e )
      {
         var ev = ClientConnected;

         if ( ev != null )
         {
            ev( this, e );
         }
      }
   }
}
