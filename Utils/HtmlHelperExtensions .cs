using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
public static class HtmlHelperExtensions
{
    public static string IsSelected(this IHtmlHelper<dynamic> html, string controller = null, string action = null, string cssClass = null)
    {
        if (string.IsNullOrEmpty(cssClass)) cssClass = "active";
        var currentAction = (string)html.ViewContext.RouteData.Values["action"];
        var currentController = (string)html.ViewContext.RouteData.Values["controller"];
        if (string.IsNullOrEmpty(controller)) controller = currentController;
        if (string.IsNullOrEmpty(action)) action = currentAction;

        return controller == currentController &&
            (action == "parent" || action == currentAction)
            ? cssClass : string.Empty;
    }

    public static string PageClass(this HtmlHelper html)
    {
        var currentAction = (string)html.ViewContext.RouteData.Values["action"];
        return currentAction;
    }
}