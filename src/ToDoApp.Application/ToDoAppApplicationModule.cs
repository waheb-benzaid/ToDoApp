using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.TodoItems.Dtos;
using ToDoApp.TodoItems.Services;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace ToDoApp;

[DependsOn(
    typeof(ToDoAppDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(ToDoAppApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class ToDoAppApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<ITodoAppService, TodoAppService>();
        context.Services.AddTransient<IValidator<CreateTodoItemDto>, CreateTodoItemDtoValidator>();
        context.Services.AddTransient<IValidator<UpdateTodoItemDto>, UpdateTodoItemDtoValidator>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ToDoAppApplicationModule>();
        });
    }
}
