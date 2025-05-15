namespace HomeVital.Models.Dtos
{
    public class Envelope<T>
    {
        public T? Data { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public Envelope(T? data, int totalCount, int pageSize, int pageNumber)
        {
            Data = data;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}