using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ReactWindowsAuth.Filters;
using ReactWindowsAuth.Models.Security;

namespace ReactWindowsAuth.Attributes
{
	/// <summary>
	/// Requires one of roles listed in order to access controller or method
	/// </summary>
	/// <example>
	/// [RoleRequirement(Role.Admin, Role.Write)]
	/// [RoleRequirement(RoleGroup.Any)] // any role can access this resources
	/// [RoleRequirement(RoleGroup.Write)] // only "Write" type roles can access this resource
	/// </example>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class RoleRequirementAttribute : TypeFilterAttribute
	{
		public RoleRequirementAttribute(params Role[] roles)
			: base(typeof(RoleRequirementFilter))
		{
			Arguments = new object[] { roles };
		}

		public RoleRequirementAttribute(params RoleGroup[] roleGroups)
			: base(typeof(RoleRequirementFilter))
		{
			var roles = roleGroups
				.SelectMany(rg => rg.Roles())
				.Distinct()
				.ToArray();

			Arguments = new object[] { roles };
		}
	}
}