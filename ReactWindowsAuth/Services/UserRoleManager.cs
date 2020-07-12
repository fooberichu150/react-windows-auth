using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ReactWindowsAuth.Models.Security;

namespace ReactWindowsAuth.Services
{
	public class UserRoleManager : IUserRoleManager
	{
		public UserRoleManager(IHttpContextAccessor httpContextAccessor)
		{
			HttpContext = httpContextAccessor.HttpContext;
		}

		private HttpContext HttpContext { get; }

		public IEnumerable<string> GetRoles()
		{
			// normally we'd get these from a DB based on the user...
			List<string> roles = new List<string>();
			roles.Add(Role.ReadOnly.ToString());
			roles.Add(Role.Reports.ToString());

			return roles;
		}
	}
}