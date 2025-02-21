using Volo.Abp.Modularity;

namespace ToDoApp;

/* Inherit from this class for your domain layer tests. */
public abstract class ToDoAppDomainTestBase<TStartupModule> : ToDoAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
