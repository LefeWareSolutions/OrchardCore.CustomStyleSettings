using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace OrchardCore.CustomStyleSettings
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageCustomStyleSettings = new Permission("ManageCustomStyleSettings", "Manage Custom Styles");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageCustomStyleSettings
            }
            .AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageCustomStyleSettings }
                },
            };
        }
    }
}
