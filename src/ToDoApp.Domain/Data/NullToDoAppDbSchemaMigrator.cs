using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ToDoApp.Data;

/* This is used if database provider does't define
 * IToDoAppDbSchemaMigrator implementation.
 */
public class NullToDoAppDbSchemaMigrator : IToDoAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
