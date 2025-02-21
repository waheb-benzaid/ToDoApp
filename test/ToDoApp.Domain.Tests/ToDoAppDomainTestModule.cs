using Volo.Abp.Modularity;

namespace ToDoApp;

[DependsOn(
    typeof(ToDoAppDomainModule),
    typeof(ToDoAppTestBaseModule)
)]
public class ToDoAppDomainTestModule : AbpModule
{

}
