
namespace ZimoziSolutions.ApiModels.Responses
{
    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public long TotalCount { get; set; }
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;

        public PaginatedResponse(List<T> data)
        {
            Data = data;
        }

        internal PaginatedResponse(bool succeeded, List<T> data = null, List<string> messages = null, long count = 0L, int page = 1, int pageSize = 10)
        {
            Data = data;
            Page = page;
            TotalPages = (int)Math.Ceiling((double)count / (double)pageSize);
            TotalCount = count;
        }

        public static PaginatedResponse<T> Failure(List<string> messages)
        {
            return new PaginatedResponse<T>(succeeded: false, null, messages, 0L);
        }

        public static PaginatedResponse<T> Success(List<T> data, long count, int page, int pageSize)
        {
            return new PaginatedResponse<T>(succeeded: true, data, null, count, page, pageSize);
        }
    }
}
