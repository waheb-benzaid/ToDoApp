using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Localization;
using Volo.Abp.Application.Services;

namespace ToDoApp;

/* Inherit your application services from this class.
 */
public abstract class ToDoAppAppService : ApplicationService
{
    protected ToDoAppAppService()
    {
        LocalizationResource = typeof(ToDoAppResource);
    }
}
