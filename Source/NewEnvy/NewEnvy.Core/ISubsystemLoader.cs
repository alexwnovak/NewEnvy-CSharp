namespace NewEnvy.Core
{
   public interface ISubsystemLoader
   {
      ISubsystem LoadLoggingSubsystem();

      ISubsystem LoadClientSubsystem();

      ISubsystem[] LoadAll();
   }
}
