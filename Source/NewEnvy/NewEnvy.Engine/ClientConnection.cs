using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NewEnvy.Engine
{
   public class ClientConnection
   {
      public int ConnectionId
      {
         get;
         private set;
      }

      public TcpClient TcpClient
      {
         get;
         private set;
      }

      public string IPAddress
      {
         get
         {
            return TcpClient.Client.RemoteEndPoint.ToString();
         }
      }

      private readonly NetworkStream _networkStream;

      public ClientConnection( int connectionId, TcpClient tcpClient )
      {
         ConnectionId = connectionId;

         TcpClient = tcpClient;

         _networkStream = tcpClient.GetStream();
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
            TcpClient.Close();
         }

         return command;
      }

      public void Send( string output )
      {
         var bytes = Encoding.ASCII.GetBytes( output );

         _networkStream.Write( bytes, 0, bytes.Length );
      }
   }
}
