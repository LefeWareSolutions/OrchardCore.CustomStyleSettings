using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using OrchardCore.Environment.Cache;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Scope;
using OrchardCore.Settings;
using YesSql;

namespace OrchardCore.CustomStyleSettings.Services
{
    public class CustomStyleSettingsService : ICustomStyleSettingsService
    {
        private readonly CustomStyleSettings _options;
        private readonly ISignal _signal;
        private readonly IMemoryCache _memoryCache;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CustomStyleSettingsService> _logger;
        private readonly IShellHost _orchardHost;
        private readonly ShellSettings _currentShellSettings;

        private const string CacheKey = "CustomStyleSettings";

        public CustomStyleSettingsService(
            IOptions<CustomStyleSettings> options,
            ISignal signal,
            IMemoryCache memoryCache,
            IServiceProvider serviceProvider,
            ILogger<CustomStyleSettingsService> logger, 
            IShellHost orchardHost,
            ShellSettings currentShellSettings
            )
        {
            _options = options.Value;
            _signal = signal;
            _memoryCache = memoryCache;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _orchardHost = orchardHost;
            _currentShellSettings = currentShellSettings;
        }

        public CustomStyleSettings GetCustomStyleSettings()
        {
            if (!_memoryCache.TryGetValue<CustomStyleSettings>(CacheKey, out var settings))
            {
                settings = new CustomStyleSettings()
                {
                    SiteLogo = _options.SiteLogo,
                    SiteFavicon = _options.SiteFavicon
                };

                // First get a new token.
                var changeToken = ChangeToken;

                _memoryCache.Set(CacheKey, settings, changeToken);
            }

            return settings;
        }

        public async Task UpdateCustomStyleSiteSettingsAsync(ISite site)
        {
            // Persists new data.
            Session.Save(site);

            // Invalidates the cache after the session is committed.
            _memoryCache.Remove(CacheKey);
            _signal.DeferredSignalToken(CacheKey);

            //Reset ShellHost to apply settings
            await _orchardHost.ReloadShellContextAsync(_currentShellSettings);
        }

        public IChangeToken ChangeToken => _signal.GetToken(CacheKey);
        private ISession Session => ShellScope.Services.GetRequiredService<ISession>();
    }
}
