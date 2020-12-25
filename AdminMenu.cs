using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Modules;
using OrchardCore.Navigation;

namespace OrchardCore.CustomStyleSettings
{
    public class AdminMenu : INavigationProvider
    {
        private readonly ShellDescriptor _shellDescriptor;
        private readonly IStringLocalizer T;

        public AdminMenu(
            IStringLocalizer<AdminMenu> localizer,
            ShellDescriptor shellDescriptor)
        {
            T = localizer;
            _shellDescriptor = shellDescriptor;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                builder.Add(T["Site Settings"], "20", settings => settings
                        .AddClass("stylesettings").Id("stylesettings")
                        .Add(T["Styling Options"], "1", client => client
                            .Action("Index", "Admin", new { area = "OrchardCore.CustomStyleSettings", groupId = "CustomStyleSettings"})
                            .Permission(Permissions.ManageCustomStyleSettings)
                            .LocalNav()
                        )

                );
            }
            return Task.CompletedTask;
        }
    }
}
