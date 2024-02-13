using System;

namespace MonoPraksaDay2.Common
{
    public class Sorting
    {
        public enum OrderByTables { Age, LastName, FirstName }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }

        public Sorting(string orderBy, string sortOrder)
        {
            OrderBy = orderBy;
            SortOrder = sortOrder;
        }

        public string ReturnOrderBy()
        {
            if(Enum.IsDefined(typeof(OrderByTables), OrderBy))
            {
                return OrderBy;
            }
            return "Age";
        }
    }
}
