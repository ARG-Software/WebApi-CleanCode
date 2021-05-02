using System;
using System.Collections.Generic;

namespace RF.Domain.Interfaces.ValueObjects
{
    public interface IStatementErrors
    {
        void AddStatementError(Enum keyError, string error);

        IDictionary<string, HashSet<string>> GetStatementErrors();

        bool HasAny();

        string ToString();
    }
}