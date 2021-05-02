using System;

namespace RF.Application.Core.Common.Exceptions
{
    public class DeleteFailureException: Exception
    {
        public DeleteFailureException(string name, object key)
            : base($"Deletion of entity \"{name}\" ({key}) failed.")
        {
        }
    }

    public class UpsertException : Exception
    {
        public UpsertException(string message)
            : base($"{message}")
        {
        }
    }

    

}
