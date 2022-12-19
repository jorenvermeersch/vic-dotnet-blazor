using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;

namespace Client.Components.Form;

public partial class Checkbox : InputBase<bool>
{
    [Parameter, EditorRequired]
    public string Label { get; set; } = default!;

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out bool result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value.IsNullOrEmpty();
        validationErrorMessage = null;
        return true;
    }

}