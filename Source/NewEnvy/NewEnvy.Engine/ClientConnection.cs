using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NewEnvy.Engine
{
   public class ClientConnection
   {
      public int ConnectionId
      {
         get;
         private set;
      }

      private readonly TcpClient _tcpClient;

      public string IPAddress
      {
         get
         {
            return _tcpClient.Client.RemoteEndPoint.ToString();
         }
      }

      private MemoryStream _memoryStream = new MemoryStream();
      private int _bytesWritten;

      private readonly NetworkStream _networkStream;

      public event EventHandler<CommandEventArgs> ReceivedCommand = null;

      public ClientConnection( TcpClient tcpClient )
      {
         _tcpClient = tcpClient;

         _networkStream = tcpClient.GetStream();
      }

      public void Send( string data )
      {
         var bytes = Encoding.ASCII.GetBytes( data );
         _bytesWritten += bytes.Length;

         _memoryStream.Write( bytes, 0, bytes.Length );
      }

      public void BeginReceiving()
      {
         var bytes = new byte[256];

         _networkStream.BeginRead( bytes, 0, bytes.Length, EndReceive, bytes );
      }

      private void EndReceive( IAsyncResult ar )
      {
         try
         {
            int bytesRead = _networkStream.EndRead( ar );
         }
         catch ( IOException )
         {
            //OnClientDisconnected();
            Console.WriteLine( "Client disconnected" );
            return;
         }

         var bytes = (byte[]) ar.AsyncState;

         string command = Encoding.ASCII.GetString( bytes );
         command = command.Replace( "\r", string.Empty ).Replace( "\n", string.Empty );

         int nullTerminator = command.IndexOf( '\0' );

         if ( nullTerminator != -1 )
         {
            command = command.Substring( 0, nullTerminator );
         }

         OnReceivedCommand( new CommandEventArgs( this, command ) );

         BeginReceiving();
      }

      protected virtual void OnReceivedCommand( CommandEventArgs e )
      {
         var ev = ReceivedCommand;

         if ( ev != null )
         {
            ev( this, e );
         }
      }

      public string Receive()
      {
         string command = null;

         var bytes = new byte[256];

         try
         {
            int bytesRead = _networkStream.Read( bytes, 0, bytes.Length );

            if ( bytesRead != 0 )
            {
               command = Encoding.ASCII.GetString( bytes, 0, bytesRead );

               command = command.Replace( "\r", string.Empty ).Replace( "\n", string.Empty );
            }
         }
         catch ( IOException )
         {
            _tcpClient.Close();
         }

         return command;
      }

      public void Flush()
      {
         var bytes = _memoryStream.GetBuffer();

         try
         {
            _networkStream.Write( bytes, 0, _bytesWritten );
         }
         catch ( IOException )
         {
            //OnClientDisconnected();
            Console.WriteLine( "Client disconnected" );
            return;
         }

         _bytesWritten = 0;
         _memoryStream = new MemoryStream();
      }

      //public string Receive()
      //{
      //   string command = null;

      //   var bytes = new byte[256];

      //   try
      //   {
      //      int bytesRead = _networkStream.Read( bytes, 0, bytes.Length );

      //      if ( bytesRead != 0 )
      //      {
      //         command = Encoding.ASCII.GetString( bytes, 0, bytesRead );

      //         command = command.Replace( "\r", string.Empty ).Replace( "\n", string.Empty );
      //      }
      //   }
      //   catch ( IOException )
      //   {
      //      _tcpClient.Close();
      //   }

      //   return command;
      //}

      //public void Send( string output )
      //{
      //   var bytes = Encoding.ASCII.GetBytes( output );

      //   _networkStream.Write( bytes, 0, bytes.Length );
      //}
   }
}
