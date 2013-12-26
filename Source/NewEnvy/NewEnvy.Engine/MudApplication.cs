namespace NewEnvy.Engine
{
   public class MudApplication
   {
      public void Main()
      {
         var mudServer = new MudServer();
         mudServer.Run();
      }
   }
}
