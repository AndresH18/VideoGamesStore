using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

// ReSharper disable ClassNeverInstantiated.Global

namespace GameStore.Infrastructure;

[HtmlTargetElement("a", Attributes = "asp-active")]
public class ActiveRouteTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    public ActiveRouteTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext, HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = default!;

    public string ActiveClass { get; set; } = "active";
    public string NormalClass { get; set; } = "";
    [HtmlAttributeName("asp-area")] public string? Area { get; set; }
    [HtmlAttributeName("asp-controller")] public string? Controller { get; set; }
    [HtmlAttributeName("asp-action")] public string? Action { get; set; }


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
        bool isActive;

        if (string.IsNullOrEmpty(Area) && string.IsNullOrEmpty(Controller) && string.IsNullOrEmpty(Action))
        {
            isActive = urlHelper.IsUrlActive();
        }
        else if (string.IsNullOrEmpty(Area) && string.IsNullOrEmpty(Controller))
        {
            isActive = urlHelper.IsUrlActive(Action);
        }
        else if (string.IsNullOrEmpty(Area))
        {
            isActive = urlHelper.IsUrlActive(Controller, Action);
        }
        else
        {
            isActive = urlHelper.IsUrlActive(Area, Controller, Action);
        }

        var currentClass = output.Attributes["class"]?.Value?.ToString() ?? "";

        output.Attributes.SetAttribute("class",
            $"{currentClass} {(isActive ? ActiveClass : NormalClass)}".Trim());
    }
}