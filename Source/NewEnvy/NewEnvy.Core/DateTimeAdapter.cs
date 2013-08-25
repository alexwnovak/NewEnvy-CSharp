using System;

namespace NewEnvy.Core
{
   public class DateTimeAdapter : IDateTime
   {
      public DateTime UtcNow
      {
         get
         {
            return DateTime.UtcNow;
         }
      }
   }
}