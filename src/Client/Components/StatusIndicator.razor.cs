using Client.SharedFiles.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Client.Components;

public partial class StatusIndicator
{
    [Inject]
    public IStringLocalizer<Resource> Localizer { get; set; } = default!;

    [Parameter, EditorRequired]
    public string State { get; set; } = default!;

    public bool isActive => State == "Active" || State == "Deployed";

    private string GetIcon()
    {
        switch (State)
        {
            case "Requested":
                return "fa-regular fa-envelope";
            case "InProgress":
                return "fa-solid fa-microchip";
            case "ReadyToDeploy":
                return "fa-solid fa-gear";
            case "Deployed":
                return "fa-solid fa-check";
            case "Active":
                return "fa-solid fa-check";
            case "Inactive":
                return "fa-solid fa-xmark";
            default:
                return "";
        }
    }
}