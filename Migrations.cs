using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Media.Settings;

namespace OrchardCore.CustomStyleSettings
{
    public class Migrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            /***************************************Create SiteSettings ContentItem*********************************************/
            _contentDefinitionManager.AlterPartDefinition("CustomStyleSettingsPart", builder => builder
                .WithField("SiteLogo", f => f.OfType("MediaField").WithDisplayName("Site Logo").WithSettings(new MediaFieldSettings(){Multiple = false}))
                .WithField("SiteFavicon", f => f.OfType("MediaField").WithDisplayName("Site Favicon").WithSettings(new MediaFieldSettings(){Multiple = false}))
            );

            _contentDefinitionManager.AlterTypeDefinition("CustomStyleSettings", builder => builder
                .WithPart("CustomStyleSettingsPart", part => part.WithPosition("1"))
                .Stereotype("CustomSettings")
            );

            return 1;
        }
    }
}
