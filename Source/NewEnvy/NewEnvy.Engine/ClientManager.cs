//using System.Threading.Tasks;

//namespace NewEnvy.Engine
//{
//   public class ClientManager : IClientManager
//   {
//      private static readonly IClientManager _clientManager = new ClientManager();

//      public static IClientManager Instance
//      {
//         get
//         {
//            return _clientManager;
//         }
//      } 

//      private ClientManager()
//      {
//      }

//      public void OnClientConnected( ClientConnectedEventArgs clientConnectedEventArgs )
//      {
//         Task.Factory.StartNew( () => SendReceiveProc( clientConnectedEventArgs.ClientConnection ) );
//      }

//      private void SendReceiveProc( ClientConnection clientConnection )
//      {
//         while ( true )
//         {
//            string command = clientConnection.Receive();

//            GlobalCommandQueue.Instance.AddCommand( clientConnection, command );
//         }
//      }
//   }
//}
