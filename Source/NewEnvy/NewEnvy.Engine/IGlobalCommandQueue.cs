namespace NewEnvy.Engine
{
   public interface IGlobalCommandQueue
   {
      void AddCommand( ClientConnection clientConnection, string command );

      void ProcessCommands();
   }
}
