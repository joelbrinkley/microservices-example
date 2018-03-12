using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.Models.IdentityResources;

namespace Identity.Api.Configuration
{
    public class IdentityServiceConfiguration
    {
        // ApiResources define the apis in your system
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("account-client"),
                new ApiResource("account", "Account Service"),
                new ApiResource("customer", "Customer Service"),
                new ApiResource("account-view", "Account View Service")
            };
        }

        // Identity resources are data like user ID, name, or email address of a user
        // see: http://docs.identityserver.io/en/release/configuration/resources.html
        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new OpenId(),
                new Profile()
            };
        }

        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl)
        {
            var clientList = new List<Client>()
            {
                new Client
                {
                    ClientId = "AngularClient",
                    ClientName = "AngularClient",
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{clientsUrl["AngularClient"]}/" },
                    RequireConsent = false,
                    PostLogoutRedirectUris = { $"{clientsUrl["AngularClient"]}/" },
                    AllowedCorsOrigins = { $"{clientsUrl["AngularClient"]}" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "account-client",
                        "account",
                        "customer",
                        "account-view"
                    }
                }
            };

            return clientList;
        }
    }
}
