﻿using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NewEnvy.Engine
{
   public class ClientManager : IClientManager
   {
      private static readonly IClientManager _clientManager = new ClientManager();

      public static IClientManager Instance
      {
         get
         {
            return _clientManager;
         }
      }

      private ClientManager()
      {
      }

      public void OnClientConnected( ClientConnectedEventArgs clientConnectedEventArgs )
      {
         Task.Factory.StartNew( () => SendReceiveProc( clientConnectedEventArgs.ClientConnection ) );
      }

      private void SendReceiveProc( ClientConnection clientConnection )
      {
         var tcpClient = clientConnection.TcpClient;
         var bytes = new byte[256];

         while ( true )
         {
            NetworkStream stream = clientConnection.TcpClient.GetStream();

            try
            {
               int bytesRead;

               while ( ( bytesRead = stream.Read( bytes, 0, bytes.Length ) ) != 0 )
               {
                  string command = Encoding.ASCII.GetString( bytes, 0, bytesRead );

                  command = command.Replace( "\r", string.Empty ).Replace( "\n", string.Empty );

                  GlobalCommandQueue.AddCommand( command );
               }

               tcpClient.Close();
            }
            catch ( IOException )
            {
               tcpClient.Close();
            }
         }
      }
   }
}