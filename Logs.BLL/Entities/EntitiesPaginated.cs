using System.Collections.Generic;

namespace Logs.BLL.Entities
{
    public class EntitiesPaginated<T>
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
