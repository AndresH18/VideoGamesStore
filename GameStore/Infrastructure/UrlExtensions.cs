using Microsoft.AspNetCore.Mvc;

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

    public static bool IsUrlActive(this IUrlHelper urlHelper)
    {
        var url = urlHelper.ActionContext.HttpContext.Request.Path.ToString();
        return url == "/";
    }

    public static bool IsUrlActive(this IUrlHelper urlHelper, string? action)
    {
        var urlAction = urlHelper.ActionContext.RouteData.Values["action"]?.ToString();
        return urlAction == action;
    }

    public static bool IsUrlActive(this IUrlHelper urlHelper, string? controller, string? action)
    {
        var urlController = urlHelper.ActionContext.RouteData.Values["controller"]?.ToString();
        var urlAction = urlHelper.ActionContext.RouteData.Values["action"]?.ToString();

        return urlController == controller && urlAction == action;
    }

    public static bool IsUrlActive(this IUrlHelper urlHelper, string? area, string? controller, string? action)
    {
        var urlArea = urlHelper.ActionContext.RouteData.Values["area"]?.ToString();
        var urlController = urlHelper.ActionContext.RouteData.Values["controller"]?.ToString();
        var urlAction = urlHelper.ActionContext.RouteData.Values["action"]?.ToString();

        return urlArea == area && urlController == controller && urlAction == action;
    }
}