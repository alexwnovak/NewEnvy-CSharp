namespace NewEnvy.Engine
{
   public class ServerConfiguration : IServerConfiguration
   {
      public T Get<T>( string name )
      {
         var threshold = (object) 100000;

         return (T) threshold;
      }
   }
}
