using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ReactWindowsAuth.Services;

namespace ReactWindowsAuth
{
	public class ClaimsTransformer : Microsoft.AspNetCore.Authentication.IClaimsTransformation
	{
		public ClaimsTransformer(IUserRoleManager userRoleManager)
		{
			UserRoleManager = userRoleManager;
		}

		private IUserRoleManager UserRoleManager { get; }

		public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			var wi = principal.Identity as WindowsIdentity;

			if (wi != null)
				await AddRolesToIdentity(wi);

			return principal;
		}

		private async Task AddRolesToIdentity(WindowsIdentity identity)
		{
			foreach (var role in UserRoleManager.GetRoles())
			{
				identity.AddClaim(new Claim(identity.RoleClaimType, role));
			}

			await Task.CompletedTask;
		}
	}
}
