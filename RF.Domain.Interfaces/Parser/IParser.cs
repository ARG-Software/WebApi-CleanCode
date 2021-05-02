using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.Domain.Interfaces.Parser
{
    public interface IParser<T> where T : class
    {
        Task<IEnumerable<T>> ConvertStreamFileToObjectList(Stream fileToParse, ITemplateDefinition info);
    }
}