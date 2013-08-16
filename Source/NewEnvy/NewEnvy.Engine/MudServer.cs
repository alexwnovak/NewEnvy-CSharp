using System;
using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class MudServer
   {
      public bool IsRunning
      {
         get;
         private set;
      }

      public void Run()
      {
         IsRunning = true;

         var connectionListener = Dependency.Resolve<IConnectionListener>();
         connectionListener.StartAsync();

         var clientManager = new ClientManager();
         clientManager.WatchForConnections();

         while ( IsRunning )
         {
            var serverClock = Dependency.Resolve<IServerClock>();
            serverClock.StartClock();

            GlobalCommandQueue.ProcessCommands();

            serverClock.EndClockAndWait();
         }
         
         //var tcpServer = Dependency.Resolve<IConnectionListener>();

         //try
         //{
         //   tcpServer.StartAsync( _port );

         //   var bytes = new byte[256];

         //   while ( true )
         //   {
         //      Console.Write( "Waiting for a connection... " );

         //      TcpClient client = tcpServer.Accept();

         //      Console.WriteLine( "Connected!" );

         //      NetworkStream stream = client.GetStream();

         //      try
         //      {
         //         int bytesRead;

         //         while ( ( bytesRead = stream.Read( bytes, 0, bytes.Length ) ) != 0 )
         //         {
         //            string data = System.Text.Encoding.ASCII.GetString( bytes, 0, bytesRead );
         //            Console.WriteLine( "Received: {0}", data );

         //            data = data.ToUpper();

         //            byte[] msg = System.Text.Encoding.ASCII.GetBytes( data );

         //            stream.Write( msg, 0, msg.Length );
         //            Console.WriteLine( "Sent: {0}", data );
         //         }

         //         client.Close();
         //      }
         //      catch ( IOException )
         //      {
         //         client.Close();
         //      }
         //   }
         //}
         //catch ( SocketException e )
         //{
         //   Console.WriteLine( "SocketException: {0}", e );
         //}
         //finally
         //{
         //   tcpServer.Stop();
         //}

         //Console.WriteLine( "\nHit enter to continue..." );
         //Console.Read();
      }

      public void Stop()
      {
         IsRunning = false;
      }
   }
}
