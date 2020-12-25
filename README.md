# OrchardCore.CustomStyleSettings
A module for adding custom styles to your tenant. Below is a list of custom setting you may configure:
- Site Logo
- Site Favicon

This project is still in early stages and not ready for consumption.

## Using Custom Style Settings in your Theme
You can inject the style setting into any view such as your theme layout.cshtml as follows:
```csharp
@using OrchardCore.CustomStyleSettings.Services
@inject ICustomStyleSettingsService CustomStyleSettingsService
@{
    var customStyleSettings = CustomStyleSettingsService.GetCustomStyleSettings();
}
```

You can now use your custom style settings to set your site favicon in your layout file as follows:
```csharp
@if(@customStyleSettings.SiteFavicon==null)
{
    <link type="image/x-icon" rel="shortcut icon" href="~/TheAdmin/favicon.ico" />
}
else
{
    <link type="image/x-icon" rel="shortcut icon" href="@customStyleSettings.SiteFavicon" />
}
```

You can now use your custom style settings to set your site logo in your header menu as follows:
```csharp
<a class="ta-navbar-brand" href="@Url.Content("~/")">
    @if(@customStyleSettings.SiteLogo==null)
    {
        <span>@Site.SiteName</span>
    }
    else
    {
        <img src="@customStyleSettings.SiteLogo" class="navbar-brand-img" alt="...">
    }
</a>
```

## Setting up your dev environment
1. **Prerequisites:** Make sure you have an up-to-date clone of [the Orchard Core repository](https://github.com/OrchardCMS/OrchardCore) on the `dev` branch. Please consult [the Orchard Core documentation](https://orchardcore.readthedocs.io/en/latest/) and make sure you have a working Orchard before you proceed. You'll also, of course, need all of Orchard Core's prerequisites for development (.NET Core, a code editor, etc.). The following steps assume some basic understanding of Orchard Core.
2. Clone the module under `[your Orchard Core clone's root]/src/OrchardCore.Modules`.
3. Add the existing project to the solution under `src/OrchardCore.Modules` in the solution explorer if you're using Visual Studio.
4. Add a reference to the module from the `OrchardCore.Cms.Web` project.
5. Build, run.
6. From the admin, enable the module's only feature.