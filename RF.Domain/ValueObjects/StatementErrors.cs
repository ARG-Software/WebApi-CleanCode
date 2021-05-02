using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.Domain.ValueObjects
{
    public class StatementErrors : IStatementErrors
    {
        private readonly IDictionary<string, HashSet<string>> _statementErrors;

        public StatementErrors()
        {
            _statementErrors = new Dictionary<string, HashSet<string>>();
        }

        public void AddStatementError(System.Enum keyError, string error)
        {
            var keyAlreadyExistsOnDictionary = _statementErrors.TryGetValue($"{keyError}", out var listOfErrorsForGivenKey);
            if (keyAlreadyExistsOnDictionary)
            {
                listOfErrorsForGivenKey.Add(error);
            }
            else
            {
                _statementErrors.Add($"{keyError}", new HashSet<string> { error });
            }
        }

        public IDictionary<string, HashSet<string>> GetStatementErrors()
        {
            return _statementErrors;
        }

        public bool HasAny()
        {
            return _statementErrors.Any();
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (var (key, value) in _statementErrors)
            {
                str.Append(Environment.NewLine);
                str.Append($" {key}={string.Join(",", value)} ");
                str.Append(Environment.NewLine);
            }
            return str.ToString();
        }
    }
}