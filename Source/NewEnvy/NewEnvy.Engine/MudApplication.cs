using NewEnvy.Core;

namespace NewEnvy.Engine
{
   public class MudApplication
   {
      private static void StartSubsystems()
      {
         var subsystemLoader = Dependency.Resolve<ISubsystemLoader>();

         subsystemLoader.LoadAll();
      }

      public void Start()
      {
         StartSubsystems();
      }
   }
}
