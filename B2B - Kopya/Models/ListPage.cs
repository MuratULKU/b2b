using Entity;

namespace SanalMagazaWebApiUI.Models
{
    public class ListPage<T>
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public List<T> Lines { get; set; }

    }
}
