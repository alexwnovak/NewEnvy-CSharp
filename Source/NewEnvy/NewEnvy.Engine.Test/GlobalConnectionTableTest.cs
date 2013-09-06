using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewEnvy.Engine.Net;

namespace NewEnvy.Engine.Test
{
   [TestClass]
   public class GlobalConnectionTableTest
   {
      [TestMethod]
      public void AddConnection_HappyPath_ConnectionIsAddedToTable()
      {
         var socketAdapterMock = new Mock<ISocketAdapter>();

            var clientConnection = new ClientConnection( socketAdapterMock.Object );

         // Test

         var globalConnectionTable = new GlobalConnectionTable();

         globalConnectionTable.RegisterConnection( clientConnection );

         // Assert

         var actualConnections = globalConnectionTable.GetClientConnections();

         Assert.AreEqual( 1, actualConnections.Length );
         Assert.AreEqual( clientConnection, actualConnections[0] );
      }
   }
}
