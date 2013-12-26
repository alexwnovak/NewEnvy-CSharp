using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewEnvy.Core;

namespace NewEnvy.Engine.Test
{
   [TestClass]
   public class MudApplicationTest
   {
      [TestInitialize]
      public void Initialize()
      {
         Dependency.CreateUnityContainer();
      }

      [TestMethod]
      public void Start_()
      {
         var mudApplication = new MudApplication();

         mudApplication.Start();
      }
   }
}
