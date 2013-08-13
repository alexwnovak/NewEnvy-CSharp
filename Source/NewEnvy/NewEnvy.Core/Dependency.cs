using Microsoft.Practices.Unity;

namespace NewEnvy.Core
{
   public static class Dependency
   {
      private static IUnityContainer _unityContainer;

      public static IUnityContainer UnityContainer
      {
         get
         {
            return _unityContainer;
         }
      }

      public static void CreateUnityContainer()
      {
         _unityContainer = new UnityContainer();
      }

      public static void RegisterInstance<T>( T instance )
      {
         _unityContainer.RegisterInstance( instance );
      }

      public static T Resolve<T>()
      {
         return _unityContainer.Resolve<T>();
      }
   }
}
