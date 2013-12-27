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

         // Finally, load the client subsystem so people can actually connect!

         var clientSubsystem = subsystemLoader.LoadClientSubsystem();

         clientSubsystem.Start();
      }

      public void Start()
      {
         StartSubsystems();
      }
   }
}
