using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Client.Components.Form;

public partial class LabelInputDate : InputBase<DateTime>
{

    [Parameter, EditorRequired]
    public Expression<Func<DateTime>> For { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Label { get; set; } = default!;

    [Parameter]
    public string Placeholder { get; set; } = "";

    [Parameter]
    public bool Required { get; set; } = false;

    [Parameter]
    public bool InverseStyle { get; set; } = true;

    [Parameter]
    public DateTime? DefaultValue { get; set; }

    [Parameter]
    public DateTime? TestValue { get; set; }


    protected override void OnInitialized()
    {
        if (DefaultValue.HasValue) CurrentValue = DefaultValue.Value;
        if (TestValue.HasValue) CurrentValue = TestValue.Value;
        base.OnInitialized();
    }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out DateTime result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = Convert.ToDateTime(value);
        validationErrorMessage = null;
        return true;
    }
}