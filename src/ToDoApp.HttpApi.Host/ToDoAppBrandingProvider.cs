using Microsoft.Extensions.Localization;
using ToDoApp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ToDoApp;

[Dependency(ReplaceServices = true)]
public class ToDoAppBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ToDoAppResource> _localizer;

    public ToDoAppBrandingProvider(IStringLocalizer<ToDoAppResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
