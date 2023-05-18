using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Mango.Service.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Service.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public ProfileService(
            UserManager<ApplicationUser> userMgr,
            RoleManager<IdentityRole> roleMgr,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory
        )
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId(); // Obtenemos el ID de usuario.
            ApplicationUser user = await _userMgr.FindByIdAsync( sub ); // Consultamos el usuario por su id.
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user); // Generamos los claims del usuario,

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            if(_userMgr.SupportsUserRole)
            {
                // Consultamos la lista de roles del usuario.
                IList<string> roles = await _userMgr.GetRolesAsync(user);
                foreach( var rolename in roles )
                {
                    // Agregamos cada rol en la lista de Claims.
                    claims.Add(new Claim(JwtClaimTypes.Role, rolename));
                    claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
                    claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
                    if (_roleMgr.SupportsRoleClaims)
                    {
                        // Consultamos el identity role por medio de su nombre.
                        IdentityRole role = await _roleMgr.FindByNameAsync(rolename);
                        if(role != null)
                        {
                            // Si el rol no existe agregamos la lista de claims del rol.
                            claims.AddRange(await _roleMgr.GetClaimsAsync(role));
                        }
                    }
                }
            }
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId(); // Obtenemos el ID de usuario.
            ApplicationUser user = await _userMgr.FindByIdAsync(sub); // Consultamos el usuario por su id.
            context.IsActive = user != null;
        }
    }
}
