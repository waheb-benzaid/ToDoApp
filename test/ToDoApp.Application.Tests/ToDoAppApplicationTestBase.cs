using Volo.Abp.Modularity;

namespace ToDoApp;

public abstract class ToDoAppApplicationTestBase<TStartupModule> : ToDoAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
