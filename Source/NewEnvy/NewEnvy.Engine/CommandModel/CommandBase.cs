using System.IO;

namespace NewEnvy.Engine.CommandModel
{
   public abstract class CommandBase : ICommand
   {
      protected ClientConnection Sender
      {
         get;
         set;
      }

      protected Stream OutputStream
      {
         get;
         set;
      }

      public virtual void PreExecute()
      {
      }

      public virtual void Execute()
      {
      }

      public virtual void PostExecute()
      {
      }
   }
}
