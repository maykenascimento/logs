using System.Collections.Generic;

namespace Logs.Shared
{
    public class QueryResult<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public int Count { get; set; }

        public QueryResult() { }

        public QueryResult(IEnumerable<T> entities, int totalCount)
        {
            Count = totalCount;

            Entities = entities;
        }
    }
}
