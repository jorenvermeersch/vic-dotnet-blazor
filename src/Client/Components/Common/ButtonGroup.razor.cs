using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class ButtonGroup
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}