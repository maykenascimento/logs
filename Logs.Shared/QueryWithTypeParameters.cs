using System.Linq;

namespace Logs.Shared
{
    public class QueryWithTypeParameters : QueryParameters
    {
        /// <summary>
        /// Log Types
        /// All = -1, Information = 1, Warning = 2, Error = 3
        /// </summary>
        protected readonly int[] types = { -1, 1, 2, 3 };

        private int? _LogType = -1;

        public int? LogType
        {
            get => _LogType;
            set
            {
                int.TryParse(value.ToString(), out int val);
                if (val == 0) val = -1;

                // Check if the value is present in the allowed types
                _LogType = types.Contains(val) ? val : -1;
            }
        }
    }
}
