namespace GameStore.Infrastructure;

public static class UrlExtensions
{
    /// <summary>
    /// Return the Path and query string of the <see cref="HttpRequest"/>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string PathAndQuery(this HttpRequest request) =>
        request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
}