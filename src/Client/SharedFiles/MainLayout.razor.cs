using Microsoft.AspNetCore.Components;

namespace Client.SharedFiles;

public partial class MainLayout
{
    private string _currentUrl = "";
    protected override void OnInitialized()
    {
        _currentUrl = NavigationManager.Uri;
    }
}