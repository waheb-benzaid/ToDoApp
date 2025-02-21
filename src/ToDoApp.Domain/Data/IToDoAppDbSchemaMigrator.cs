using System.Threading.Tasks;

namespace ToDoApp.Data;

public interface IToDoAppDbSchemaMigrator
{
    Task MigrateAsync();
}
