using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Client.Components.Form;

public partial class LabelInput : InputBase<string>
{

    [Parameter, EditorRequired]
    public Expression<Func<string>> For { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Label { get; set; } = default!;

    [Parameter]
    public string Placeholder { get; set; } = "";

    [Parameter]
    public bool Required { get; set; } = false;

    [Parameter]
    public bool InverseStyle { get; set; } = true;

    [Parameter]
    public string InputType { get; set; } = "text";

    [Parameter]
    public string? TestValue { get; set; }


    protected override void OnInitialized()
    {
        if (!TestValue.IsNullOrEmpty()) CurrentValue = TestValue!;
        base.OnInitialized();
    }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out string result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value ?? "";
        validationErrorMessage = null;
        return true;
    }
}