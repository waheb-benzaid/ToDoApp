using ToDoApp.Samples;
using Xunit;

namespace ToDoApp.EntityFrameworkCore.Applications;

[Collection(ToDoAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ToDoAppEntityFrameworkCoreTestModule>
{

}
