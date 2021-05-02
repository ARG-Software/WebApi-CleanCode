using System;

namespace RF.Services.ETL.Common.Exceptions
{
   public class ETLException:Exception
    {
        public ETLException()
        {
        }

        public ETLException(string message)
            : base(message)
        {
        }
    }
}
