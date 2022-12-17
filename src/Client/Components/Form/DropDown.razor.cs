using Microsoft.AspNetCore.Components;

namespace Client.Components.Form;

public partial class DropDown
{
    private Dictionary<string, string> filteredItems = new();
    private string _dropDownChosenItem = "Kies...";
    private bool shown = false;
    private string style = "display: none";

    [Parameter]
    public Dictionary<string, string> Items { get; set; } = default!;

    [Parameter]
    public EventCallback<string> Action { get; set; } = default!;

    [Parameter]
    public string Label { get; set; } = "";

    public string DropDownChosenItem { get => shown ? "Menu sluiten" : _dropDownChosenItem; set => _dropDownChosenItem = value; }

    protected override void OnInitialized()
    {
        filteredItems = Items;
    }

    private void ToggleDropDown()
    {
        shown = !shown;
        style = shown ? "display: block" : "display: none";
    }

    private async void OptionChosen(string chosenOptionKey, string chosenOptionValue)
    {
        ToggleDropDown();
        DropDownChosenItem = chosenOptionKey;
        await Action.InvokeAsync(chosenOptionValue);
    }

    private void ShowValidItems(ChangeEventArgs args)
    {
        filteredItems = new();
        foreach (var item in Items)
        {
            if (item.Key.ToLower().Contains(args.Value?.ToString().ToLower()))
            {
                filteredItems.Add(item.Key, item.Value);
            }
        }
    }
}