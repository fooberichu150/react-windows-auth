using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace ReactWindowsAuth.Services
{
	public class TranslatedUserRoleManager : IUserRoleManager
	{
		public TranslatedUserRoleManager(IHttpContextAccessor httpContextAccessor)
		{
			HttpContext = httpContextAccessor.HttpContext;
		}

		private HttpContext HttpContext { get; }

		public IEnumerable<string> GetRoles()
		{
			var wi = HttpContext.User?.Identity as WindowsIdentity;

			if (wi == null)
				return Array.Empty<string>();

			return wi.Groups
				.Select(group => group.Translate(typeof(NTAccount)).Value)
				.ToArray();
		}
	}
}
