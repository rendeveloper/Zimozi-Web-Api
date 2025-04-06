using System.Net;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.Exceptions.Business;

namespace ZimoziSolutions.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static PaginatedResponse<T> ToPaginatedList<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, "Data not found.");

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            long count = source.Count();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return PaginatedResponse<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
