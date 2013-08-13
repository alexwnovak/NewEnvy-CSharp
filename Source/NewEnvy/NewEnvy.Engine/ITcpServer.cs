using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NewEnvy.Engine
{
   public interface ITcpServer
   {
      void Start( int port );

      void Stop();

      TcpClient Accept();
   }
}
