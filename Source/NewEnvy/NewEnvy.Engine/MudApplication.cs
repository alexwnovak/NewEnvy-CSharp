namespace NewEnvy.Engine
{
   public class MudApplication
   {
      public void Start()
      {
         var mudServer = new MudServer();
         mudServer.Run();
      }
   }
}
