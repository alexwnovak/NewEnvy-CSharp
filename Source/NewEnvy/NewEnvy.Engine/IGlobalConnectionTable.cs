namespace NewEnvy.Engine
{
   public interface IGlobalConnectionTable
   {
      void RegisterConnection( ClientConnection clientConnection );

      void SendAll();

      ClientConnection[] GetClientConnections();
   }
}
