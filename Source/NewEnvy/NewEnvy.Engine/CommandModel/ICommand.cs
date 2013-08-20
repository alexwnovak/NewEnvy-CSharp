namespace NewEnvy.Engine.CommandModel
{
   public interface ICommand
   {
      void PreExecute();

      void Execute();

      void PostExecute();
   }
}
