using Volo.Abp.Settings;

namespace ToDoApp.Settings;

public class ToDoAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ToDoAppSettings.MySetting1));
    }
}
