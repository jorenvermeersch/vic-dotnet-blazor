using Client.Authentication;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Layout;

public partial class NavigationMenu
{
    private string _display = "flex";
    private void showOrHideChangeRole()
    {
        _display = _display == "flex" ? "none" : "flex";
    }

    [Parameter]
    public string name { get; set; } = "Name";

    private void LoginAsAdmin()
    {
        FakeAuthenticationProvider.ChangeAuthenticationState(FakeAuthenticationProvider.Administrator);
    }

    private void LoginAsAnonymous()
    {
        FakeAuthenticationProvider.ChangeAuthenticationState(FakeAuthenticationProvider.Anonymous);
    }

    private void LoginAsMaster()
    {
        FakeAuthenticationProvider.ChangeAuthenticationState(FakeAuthenticationProvider.Master);
    }

    private void LoginAsObserver()
    {
        FakeAuthenticationProvider.ChangeAuthenticationState(FakeAuthenticationProvider.Observer);
    }
}