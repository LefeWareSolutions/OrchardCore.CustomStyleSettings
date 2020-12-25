using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OrchardCore.Entities;
using OrchardCore.Media;
using OrchardCore.Settings;

namespace OrchardCore.CustomStyleSettings.Services
{
    public class CustomStyleSettingsConfiguration : IConfigureOptions<CustomStyleSettings>
    {
        private readonly ISiteService _site;
        private readonly IMediaFileStore _mediaFileStore;
        private readonly ILogger<CustomStyleSettingsConfiguration> _logger;

        public CustomStyleSettingsConfiguration(
            ISiteService site,
            IMediaFileStore mediaFileStore,
            ILogger<CustomStyleSettingsConfiguration> logger)
        {
            _site = site;
            _mediaFileStore = mediaFileStore;
            _logger = logger;
        }

        public void Configure(CustomStyleSettings options)
        {
            var siteSettings = _site.GetSiteSettingsAsync().GetAwaiter().GetResult();

            if (siteSettings.Properties["CustomStyleSettings"] != null)
            {
                //Map SiteSettings to CustomStyleSettingsPart
                var CustomStyleSettingsJToken = siteSettings.Properties["CustomStyleSettings"]["CustomStyleSettingsPart"];
                var customStyleSettingsPart = CustomStyleSettingsJToken.ToObject<CustomStyleSettingsPart>();


                //Add public url to media fields
                if(customStyleSettingsPart.SiteLogo.Paths.Length>0)
                {
                    options.SiteLogo  = _mediaFileStore.MapPathToPublicUrl(customStyleSettingsPart.SiteLogo.Paths[0]);
                }
                if(customStyleSettingsPart.SiteFavicon.Paths.Length>0)
                {
                     options.SiteFavicon = _mediaFileStore.MapPathToPublicUrl(customStyleSettingsPart.SiteFavicon.Paths[0]);
                }

            }
        }
    }
}
