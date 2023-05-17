using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Service.Identity
{
    public static class SD
    {
        // Definimos las constantes de los diferentes roles que tendremos.
        public const string Admin = "Admin";

        public const string Customer = "Customer";

        // Definimos los diferentes identity resources a los cuales podremos acceder.
        // Es posible agregar más de los que vemos aquí.
        public static IEnumerable<IdentityResource> IdentityResources => 
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        // Definimos los alcances que tendrán, en este caso lectura, escritura y eliminación.
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> 
            { 
                new ApiScope("mango", "Mango Server"),
                new ApiScope(name: "read", displayName: "Read your data"),
                new ApiScope(name: "write", displayName: "Write your data"),
                new ApiScope(name: "delete", displayName: "Delete your data")
            };

        // Definimos los tipos de clientes que tendrán acceso a la aplicación,
        // puden ser celulares, aplicaciones web, etc.
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    // El client secret debe de ser algo diferente a "secret", este es solo un ejemplo.
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                // Generamos un cliente para nuestra aplicación Mango.Web.App
                new Client
                {
                    ClientId="mango",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    // Configuramos una redirección de URLs de los sitios de este cliente.
                    // Tal como el redirect url de un app registration de Azure.

                    // Combinamos la url del archivo launchSettings.json de Mango.Web.App junto con el
                    // puerto "sslPort" que se tiene en dicho archivo.
                    RedirectUris = { "https://localhost:7193/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7193/signout-callback-oidc" },
                    // Los scopes son los que definimos en la propiedad "ApiScopes" de esta misma clase.
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email, 
                        "mango"
                    }
                },
            };
    }
}
