using System.Threading.Tasks;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Environment.Shell;

namespace OrchardCore.CustomStyleSettings.Handlers
{
    public class CustomStyleSettingsPartHandler : ContentPartHandler<CustomStyleSettingsPart>
    {
        private readonly IShellHost _orchardHost;
        private readonly ShellSettings _currentShellSettings;

        public CustomStyleSettingsPartHandler(IShellHost orchardHost, ShellSettings currentShellSettings)
        {
            _orchardHost = orchardHost;
            _currentShellSettings = currentShellSettings;
        }

        public override async Task PublishedAsync(PublishContentContext context, CustomStyleSettingsPart part)
        {
            // Reload the tenant to apply the settings
            await _orchardHost.ReloadShellContextAsync(_currentShellSettings);
        }
    }
}