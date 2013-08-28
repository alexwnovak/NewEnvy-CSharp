using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace NewEnvy.Engine
{
   public class ClientConnection
   {
      public int ConnectionId
      {
         get;
         set;
      }

      public ConnectionState ConnectionState
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
      public event EventHandler<ClientConnectionEventArgs> Disconnected = null;

      public ClientConnection( TcpClient tcpClient )
      {
         _tcpClient = tcpClient;

         _networkStream = tcpClient.GetStream();

         ConnectionState = ConnectionState.Connected;
      }

      public void Send( string data )
      {
         if ( !data.EndsWith( "\n" ) )
         {
            data += "\n";
         }

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
         int bytesRead;

         try
         {
            bytesRead = _networkStream.EndRead( ar );
         }
         catch ( IOException )
         {
            OnDisconnect( new ClientConnectionEventArgs( this ) );
            return;
         }

         var command = ParseCommand( (byte[]) ar.AsyncState, bytesRead );

         OnReceivedCommand( new CommandEventArgs( this, command ) );

         BeginReceiving();
      }

      private static string ParseCommand( byte[] bytes, int bytesRead )
      {
         string command = Encoding.ASCII.GetString( bytes, 0, bytesRead );

         return command.Replace( "\r", string.Empty ).Replace( "\n", string.Empty );
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
         if ( ConnectionState != ConnectionState.Connected )
         {
            return;
         }

         var bytes = _memoryStream.GetBuffer();

         try
         {
            _networkStream.Write( bytes, 0, _bytesWritten );
         }
         catch ( IOException )
         {
            OnDisconnect( new ClientConnectionEventArgs( this ) );
            return;
         }

         _bytesWritten = 0;
         _memoryStream = new MemoryStream();
      }

      public void Disconnect()
      {
         OnDisconnect( new ClientConnectionEventArgs( this ) );
      }

      protected virtual void OnReceivedCommand( CommandEventArgs e )
      {
         var ev = ReceivedCommand;

         if ( ev != null )
         {
            ev( this, e );
         }
      }

      protected virtual void OnDisconnect( ClientConnectionEventArgs e )
      {
         var ev = Disconnected;

         if ( ev != null )
         {
            ev( this, e );
         }
      }
   }
}
