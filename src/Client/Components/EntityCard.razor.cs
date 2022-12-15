using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class EntityCard
    {
        [Parameter, EditorRequired]
        public string Href { get; set; } = default!;

        [Parameter, EditorRequired]
        public string Value { get; set; } = default!;

        [Parameter]
        public bool Active { get; set; } = false;
    }
}