using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ReactWindowsAuth
{
	public class ClaimsTransformer : Microsoft.AspNetCore.Authentication.IClaimsTransformation
	{
		public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			var wi = (WindowsIdentity)principal.Identity;

			var claim = new Claim(wi.RoleClaimType, "Super-awesome");
			wi.AddClaim(claim);

			return Task.FromResult(principal);
		}
	}
}
