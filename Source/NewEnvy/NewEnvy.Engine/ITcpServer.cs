using System.Net.Sockets;

namespace NewEnvy.Engine
{
   public interface ITcpServer
   {
      void Start( int port );

      void Stop();

      TcpClient Accept();
   }
}
