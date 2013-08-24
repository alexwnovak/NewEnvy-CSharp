using NewEnvy.Engine;
using NewEnvy.Engine.Net;

namespace NewEnvy.EntryPoint
{
   public class Program
   {
      public static void Main()
      {
         var asyncTcpServer = new AsyncTcpServer();

         asyncTcpServer.Listen().Wait();



         //new MudApplication().Main();
      }
   }
}
