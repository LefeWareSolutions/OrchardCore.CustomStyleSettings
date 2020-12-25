using System.Threading.Tasks;
using OrchardCore.Settings;

namespace OrchardCore.CustomStyleSettings.Services
{
    public interface ICustomStyleSettingsService
    {
        public CustomStyleSettings GetCustomStyleSettings();
        public Task UpdateCustomStyleSiteSettingsAsync(ISite site);
    }
}
