namespace NewEnvy.Engine
{
   public interface IGlobalCommandQueue
   {
      void AddCommand( string command );

      void ProcessCommands();
   }
}
