using System;

namespace RF.Application.Core.Common.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
    
}
