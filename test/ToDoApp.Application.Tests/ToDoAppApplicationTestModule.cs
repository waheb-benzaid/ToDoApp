using Volo.Abp.Modularity;

namespace ToDoApp;

[DependsOn(
    typeof(ToDoAppApplicationModule),
    typeof(ToDoAppDomainTestModule)
)]
public class ToDoAppApplicationTestModule : AbpModule
{

}
