﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public class Config
    {

        public static IEnumerable<Client> Clients =>
               new Client[]
               {
                   new Client
                   {
                        ClientId = "productClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "productAPI" }

                   },
                   new Client
                   {
                       ClientId = "product_mvc_client",
                       ClientName = "Product MVC Web App",
                       AllowedGrantTypes = GrantTypes.Hybrid,
                       RequirePkce = false,
                       AllowRememberConsent = false,
                       RedirectUris = new List<string>()
                       {
                           "http://localhost:5020/signin-oidc"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "http://localhost:5020/signout-callback-oidc"
                       },
                       ClientSecrets = new List<Secret>
                       {
                           new Secret("secret".Sha256())
                       },
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           IdentityServerConstants.StandardScopes.Address,
                           IdentityServerConstants.StandardScopes.Email,
                           "productAPI",
                           "roles"
                       }
                   }
               };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("productAPI", "Product API")
           };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
          };


    }
}