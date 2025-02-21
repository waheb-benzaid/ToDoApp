using ToDoApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ToDoApp.Permissions;

public class ToDoAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ToDoAppPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ToDoAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ToDoAppResource>(name);
    }
}
