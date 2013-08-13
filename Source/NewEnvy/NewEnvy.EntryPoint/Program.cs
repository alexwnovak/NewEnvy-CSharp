using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
