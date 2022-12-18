using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace Client.Components.Form;

public partial class DropDown
{
    private Dictionary<string, string> filteredOptions = new();

    private string placeholder = "Kies...";
    private string closeMenuText = "Menu sluiten";

    private string chosenOption = "Kies...";
    private bool shown = false;
    private string style = "display: none";

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter, EditorRequired]
    public Dictionary<string, string> Options { get; set; } = default!;

    [Parameter, EditorRequired]
    public EventCallback<string> OnChange { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Label { get; set; } = default!;

    [Parameter]
    public bool Required { get; set; } = false;

    public string ChosenOption
    {
        get => shown ? closeMenuText : chosenOption;
        set => chosenOption = value;
    }

    protected override void OnInitialized()
    {
        filteredOptions = Options;
    }

    private void ToggleDropDown()
    {
        shown = !shown;
        style = shown ? "display: block" : "display: none";
    }

    private async void UpdateChosenOption(string chosenOptionKey, string chosenOptionValue)
    {
        ToggleDropDown();
        ChosenOption = chosenOptionKey;
        await OnChange.InvokeAsync(chosenOptionValue);
    }

    private void ShowSearchResults(ChangeEventArgs args)
    {
        filteredOptions = Options;
        var searchTerm = args.Value?.ToString() ?? "";

        if (!searchTerm.IsNullOrEmpty())
        {
            filteredOptions = filteredOptions
                .Where(item => item.Key.ToLower().Contains(searchTerm.ToLower()))
                .ToDictionary(item => item.Key, item => item.Value);
        }
    }
}