using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class MudApplication
   {
      private static void StartSubsystems()
      {
         var subsystemLoader = Dependency.Resolve<ISubsystemLoader>();

         // Load logging subsystem first for logging capability

         var loggingSubsystem = subsystemLoader.LoadLoggingSubsystem();

         loggingSubsystem.Start();
      }

      public void Start()
      {
         StartSubsystems();
      }
   }
}
