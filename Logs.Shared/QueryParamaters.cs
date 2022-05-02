using System.Collections.Generic;

namespace Logs.Shared
{
    public class QueryParameters
    {
        private int _pageIndex = 1;
        public int? PageIndex
        {
            get => _pageIndex;
            set
            {
                int.TryParse(value.ToString(), out int val);
                if (val < 1) val = 1;
                _pageIndex = val;
            }
        }

        private int _pageSize = 10;
        public int? PageSize
        {
            get => _pageSize;
            set
            {
                int.TryParse(value.ToString(), out int val);
                if (val < 5) val = 5;
                _pageSize = val;
            }
        }

        public string Keywords { get; set; }

        public List<SortParam> SortingParams { get; set; }

        // This will return pagination with current page
        public List<KeyValuePair<int, bool>> GetPagination
        {
            get
            {
                int[] Pages = { 5, 10, 20, 50, 100 };
                var list = new List<KeyValuePair<int, bool>>();

                foreach (var item in Pages)
                {
                    list.Add(new KeyValuePair<int, bool>(item, item == PageSize));
                }

                return list;
            }
        }
    }
}
