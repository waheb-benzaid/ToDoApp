using Xunit;

namespace ToDoApp.EntityFrameworkCore;

[CollectionDefinition(ToDoAppTestConsts.CollectionDefinitionName)]
public class ToDoAppEntityFrameworkCoreCollection : ICollectionFixture<ToDoAppEntityFrameworkCoreFixture>
{

}
