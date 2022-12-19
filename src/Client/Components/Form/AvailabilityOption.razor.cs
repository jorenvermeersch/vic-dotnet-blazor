using Client.SharedFiles.Resources;
using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Client.Components.Form;

public partial class AvailabilityOption
{
    [Inject]
    private IStringLocalizer<Resource> Localizer { get; set; } = default!;

    [Parameter]
    public Availability DayOfWeek { get; set; } = Availability.Wednesday;
    [Parameter]
    public PartOfDay PartOfDay { get; set; } = PartOfDay.FullDay;
    [Parameter]
    public string customCss { get; set; } = "";
}