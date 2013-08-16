using NewEnvy.Engine;

namespace NewEnvy.EntryPoint
{
   public class Program
   {
      public static void Main()
      {
         var mudServer = new MudServer();

         mudServer.Run();
      }
   }
}
