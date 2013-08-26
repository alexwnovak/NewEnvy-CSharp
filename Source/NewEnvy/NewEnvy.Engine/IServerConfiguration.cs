namespace NewEnvy.Engine
{
   public interface IServerConfiguration
   {
      T Get<T>( string name );
   }
}
