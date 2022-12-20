using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class ErrorMessage
{
    [Parameter]
    public string Title { get; set; } = "Error";

    [Parameter]
    public string Message { get; set; } = "Onbekende error.";
}