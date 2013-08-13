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
   }
}
