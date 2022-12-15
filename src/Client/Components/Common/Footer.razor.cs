using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class Footer
{
    [Parameter]
    public bool InverseStyle { get; set; } = false;
}