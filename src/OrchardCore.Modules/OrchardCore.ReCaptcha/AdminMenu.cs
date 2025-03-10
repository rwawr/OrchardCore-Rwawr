using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.ReCaptcha.Drivers;

namespace OrchardCore.ReCaptcha;

public sealed class AdminMenu : AdminNavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCore.Settings" },
        { "groupId", ReCaptchaSettingsDisplayDriver.GroupId },
    };

    internal readonly IStringLocalizer S;

    public AdminMenu(IStringLocalizer<AdminMenu> stringLocalizer)
    {
        S = stringLocalizer;
    }

    protected override ValueTask BuildAsync(NavigationBuilder builder)
    {
        if (NavigationHelper.UseLegacyFormat())
        {
            builder
                .Add(S["Security"], security => security
                    .Add(S["Settings"], S["Settings"].PrefixPosition(), settings => settings
                        .Add(S["reCaptcha"], S["reCaptcha"].PrefixPosition(), reCaptcha => reCaptcha
                            .Permission(Permissions.ManageReCaptchaSettings)
                            .Action("Index", "Admin", _routeValues)
                            .LocalNav()
                        )
                    )
                );

            return ValueTask.CompletedTask;
        }

        builder
            .Add(S["Settings"], settings => settings
                .Add(S["Security"], S["Security"].PrefixPosition(), security => security
                    .Add(S["reCaptcha"], S["reCaptcha"].PrefixPosition(), reCaptcha => reCaptcha
                        .Permission(Permissions.ManageReCaptchaSettings)
                        .Action("Index", "Admin", _routeValues)
                        .LocalNav()
                    )
                )
            );

        return ValueTask.CompletedTask;
    }
}
