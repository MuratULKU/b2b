namespace DataAccess.Helpers.Pagination
{
    public class PagedList<T> : List<T>
    {
        public Metadata Metadata { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Metadata = new Metadata()
            {
                TotalCount = count,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPage = (int)Math.Ceiling(count / (decimal)pageSize),
            };
            AddRange(items);
        }
        //public static PagedList<T> ToPagedList<T>(IEnumerable<T> source, int pageNumber, int pageSize)
        //{
        //    var count = source.Count();
        //    var items = source
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //    return new PagedList<T>(items, count, pageNumber, pageSize);
        //}
    }
}
