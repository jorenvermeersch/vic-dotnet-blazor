using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Client.Shared;

public class FakeAuthenticationProvider : AuthenticationStateProvider
{
    public static ClaimsPrincipal Anonymous => new(new ClaimsIdentity(new[]
    {
            new Claim(ClaimTypes.Name, "Anonymous"),
    }));
    public static ClaimsPrincipal Master =>
     new(new ClaimsIdentity(new[]
     {
     new Claim(ClaimTypes.Name, "Fake Master"),
     new Claim(ClaimTypes.Email, "fake-Master@gmail.com"),
     new Claim(ClaimTypes.Role, "Master"),
     }, "Fake Authentication"));

    public static ClaimsPrincipal Administrator =>
     new(new ClaimsIdentity(new[]
     {
     new Claim(ClaimTypes.Name, "Fake Administrator"),
     new Claim(ClaimTypes.Email, "fake-administrator@gmail.com"),
     new Claim(ClaimTypes.Role, "Administrator"),
     }, "Fake Authentication"));
    public static ClaimsPrincipal Observer =>
         new(new ClaimsIdentity(new[]
         {
         new Claim(ClaimTypes.Name, "Fake observer"),
         new Claim(ClaimTypes.Email, "fake-observer@gmail.com"),
         new Claim(ClaimTypes.Role, "Observer"),
         }, "Fake Authentication"));

    public ClaimsPrincipal Current { get; private set; } = Anonymous;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(Current));
    }

    public void ChangeAuthenticationState(ClaimsPrincipal claimsPrincipal)
    {
        Current = claimsPrincipal;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }


}
