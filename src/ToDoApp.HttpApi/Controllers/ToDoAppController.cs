using ToDoApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ToDoApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ToDoAppController : AbpControllerBase
{
    protected ToDoAppController()
    {
        LocalizationResource = typeof(ToDoAppResource);
    }
}
