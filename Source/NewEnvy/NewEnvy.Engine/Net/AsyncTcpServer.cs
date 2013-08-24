using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewEnvy.Engine.Net
{
   public class AsyncTcpServer
   {
      public async Task<int> Listen()
      {
         var taskCompletionSource = new TaskCompletionSource<int>();

         var task = taskCompletionSource.Task;

         ThreadPool.QueueUserWorkItem( ThreadProc, taskCompletionSource );

         return task.Result;
      }

      private void ThreadProc( object state )
      {
         Thread.Sleep( 5000 );

         var taskCompletionSource = (TaskCompletionSource<int>) state;

         taskCompletionSource.SetResult( 123 );
      }

   }
}
