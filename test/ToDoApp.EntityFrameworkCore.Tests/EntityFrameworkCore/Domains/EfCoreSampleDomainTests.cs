using ToDoApp.Samples;
using Xunit;

namespace ToDoApp.EntityFrameworkCore.Domains;

[Collection(ToDoAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ToDoAppEntityFrameworkCoreTestModule>
{

}
