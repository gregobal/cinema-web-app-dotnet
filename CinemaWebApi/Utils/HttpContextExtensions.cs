using Microsoft.EntityFrameworkCore;

namespace CinemaWebApi.Utils;

public static class HttpContextExtensions
{
    public static async Task InsertPaginationParamsInResponse<T>(
        this HttpContext httpContext, IQueryable<T> queryable, int recordsPerPage)
    {
        if (httpContext is null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        double count = await queryable.CountAsync();
        double totalPages = Math.Ceiling(count / recordsPerPage);
        httpContext.Response.Headers.Add("total_pages", totalPages.ToString());
    }
}