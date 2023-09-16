namespace B2B.Components.Pagination
{
    public class PagedResult<T>:PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        //oluşturulduğunda listeye göre ilk parametreleri ayarlayalım
        //şimdilik sadece çağrıldığı yerden giriş yapılıyor.
        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
