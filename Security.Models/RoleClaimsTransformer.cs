using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Security.Models.DAL.Repositories;
using System.Linq;
using SamplesRequest.Models.Models.ViewModels;
using Microsoft.Extensions.Options;

namespace Security.Models
{
    public class RoleClaimsTransformer : IClaimsTransformation
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly AreaSettings _areaSettings;
        public RoleClaimsTransformer(IUsersRepository usersRepository, 
            IRolesRepository rolesRepository, IOptions<AreaSettings> areaSetings)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _areaSettings = areaSetings.Value;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var id = ((ClaimsIdentity) principal.Identity);

            var ci = new ClaimsIdentity(id.Claims,id.AuthenticationType,id.NameClaimType,id.RoleClaimType);

            var user = _usersRepository.GetBy(e => e.uname == id.Name,includeProperties: "roles");
            if(user != null)
            {
                var userroles = user.roles.Select(e => e.role_id);
                var roles = _rolesRepository.List().Where(e => e.area_id == _areaSettings.area_id && userroles.Contains(e.id)).Select(e => new Claim(ClaimTypes.Role, e.name));
                foreach (var role in roles)
                {
                    ci.AddClaim(role);
                }

            }
            else
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, "Nothing"));
            }

            var cp = new CustomClaimPrincipal(ci);
            
            return Task.FromResult((ClaimsPrincipal)cp);
        }
    }

    public class CustomClaimPrincipal : ClaimsPrincipal{
        public CustomClaimPrincipal(ClaimsIdentity identity):base(identity)
        {
            
        }

        public override bool IsInRole(string role)
        {
            return HasClaim(ClaimTypes.Role, role);
        }
    }
}
