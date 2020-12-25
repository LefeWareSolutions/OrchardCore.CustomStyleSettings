using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;

namespace OrchardCore.CustomStyleSettings
{
    public class CustomStyleSettingsPart : ContentPart
    {
        public MediaField SiteLogo { get; set; }
        public MediaField SiteFavicon { get; set; }
    }
}
