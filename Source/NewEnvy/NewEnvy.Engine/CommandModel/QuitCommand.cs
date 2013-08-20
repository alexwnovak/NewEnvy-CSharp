namespace NewEnvy.Engine.CommandModel
{
   public class QuitCommand
   {
      public string Execute( ClientConnection sender )
      {
         return "See ya.";
      }
   }
}
