using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
            new ApiScope("readProjectApi", "Read Project API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // Swagger client
            new()
            {
                ClientId = "api_swagger",
                ClientName = "Swagger UI for Sample API",
                ClientSecrets = {new Secret("secret".Sha256())}, // TODO: use env var

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:7101/swagger/oauth2-redirect.html"},
                AllowedCorsOrigins = {"https://localhost:7101"},
                AllowedScopes = new List<string>
                {
                    "SampleAPI"
                }
            },

            // Web Projects frontend client
            new()
            {
                ClientId = "web-projects",
                ClientName = "Web Projects",
                ClientSecrets = {new Secret("secret".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,
                AllowOfflineAccess = true,

                RedirectUris = {"http://localhost:3000/api/auth/callback/identity-server"},

                AllowedCorsOrigins =
                {
                    "http://localhost:3000",
                    "https://localhost:3001"
                },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "scope2",
                    "readProjectApi"
                }
            },

            // m2m client credentials flow client
            new()
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},

                AllowedScopes = {"scope1"}
            },

            // interactive client using code flow + pkce
            new()
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:44300/signin-oidc"},
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:44300/signout-callback-oidc"},

                AllowOfflineAccess = true,
                AllowedScopes = {"openid", "profile", "scope2"}
            },
        };
}