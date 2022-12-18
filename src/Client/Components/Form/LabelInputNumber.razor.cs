using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Client.Components.Form;

public partial class LabelInputNumber : InputBase<int>
{

    [Parameter, EditorRequired]
    public Expression<Func<int>> For { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Label { get; set; } = default!;

    [Parameter]
    public string Placeholder { get; set; } = "";

    [Parameter]
    public bool Required { get; set; } = false;

    [Parameter]
    public bool InverseStyle { get; set; } = true;

    [Parameter]
    public int? TestValue { get; set; }

    [Parameter]
    public int? Minimum { get; set; }

    [Parameter]
    public int? Maximum { get; set; }


    protected override void OnInitialized()
    {
        if (TestValue.HasValue) CurrentValue = TestValue.Value;
        base.OnInitialized();
    }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out int result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        bool success = int.TryParse(value, out int number);
        result = number;
        validationErrorMessage = null;

        return success;
    }
}