namespace MonoPraksaDay2.Common
{
    public class Paging
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Paging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int ReturnOffset()
        {
            return (this.PageNumber - 1) * this.PageSize;
        }
    }
}
