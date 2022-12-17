using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Client.Components.Form;

public partial class LabelSelect : InputBase<string>
{
    [Parameter, EditorRequired]
    public Expression<Func<string>> For { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Label { get; set; } = default!;

    [Parameter, EditorRequired]
    public List<string> Options { get; set; } = default!;

    [Parameter]
    public List<string> DisplayOptions { get; set; } = new();

    [Parameter]
    public string Placeholder { get; set; } = "";

    [Parameter]
    public bool InverseStyle { get; set; } = true;


    private bool HasDisplayOptions => DisplayOptions?.Count > 0;

    protected override bool TryParseValueFromString(string? value, out string result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value ?? "";
        validationErrorMessage = null;
        return true;
    }
}