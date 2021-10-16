
https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/standalone-with-authentication-library?view=aspnetcore-5.0&tabs=visual-studio-code

Blazor WebAssembly project with an authentication mechanism
ASP.NET Core's Identity system
Auth support with no user database

Created using `dotnet new blazorwasm -au Individual -o {APP NAME}`

Uses the `Microsoft.AspNetCore.Components.WebAssembly.Authentication` package for 
app auth primitives for authenticating a user and obtain tokens for API calls. Including
the `AddOidcAuthentication` extension for OpenID Client support, and interaction with
the Identity Provider.

Includes namespace for components in `_Imports.razor` here `Microsoft.AspNetCore.Components.Authorization`

Authentication service for handling the OIDC protocol

In `index.html`:
```
<script src="_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js"></script>
```

User is redirected if not signed in. See `Shared/RedirectToLogin.razor` and `Pages/Authentication.razor`. Which
in turn lead to `Shared/LoginDisplay.razor`.