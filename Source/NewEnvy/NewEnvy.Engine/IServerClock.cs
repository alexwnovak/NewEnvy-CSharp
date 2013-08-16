namespace NewEnvy.Engine
{
   public interface IServerClock
   {
      void StartClock();

      void EndClockAndWait();
   }
}
