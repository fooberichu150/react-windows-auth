using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ReactWindowsAuth.Attributes;
using ReactWindowsAuth.Models.Security;
using ReactWindowsAuth.Utility;

namespace ReactWindowsAuth.Filters
{
	public class RoleRequirementFilter : IAuthorizationFilter
	{
		private readonly Role[] _roles;

		public RoleRequirementFilter(Role[] roles)
		{
			_roles = roles;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
			var actionAttributes = actionDescriptor?.MethodInfo.GetCustomAttributes(true) ?? Array.Empty<object>();
			var isRoleAtActionLevel = actionAttributes.OfType<RoleRequirementAttribute>().Any();

			bool allowAnonymous = false;
			if (isRoleAtActionLevel)
			{
				// an anonymous adjacent to the role nullifies the role requirement
				allowAnonymous = actionAttributes.OfType<AllowAnonymousAttribute>().Any();
			}
			else
			{
				// an allow anonymous at either level would override the role requirement
				var allowAnonymousAttribute = actionAttributes
					.OfType<AllowAnonymousAttribute>()
					.FirstOrDefault() ?? actionDescriptor?.ControllerTypeInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().FirstOrDefault();

				allowAnonymous = allowAnonymousAttribute != null;
			}

			if (allowAnonymous)
				return;

			var user = context.HttpContext.User;
			var userIsAnonymous = user?.Identity == null || !user.Identities.Any(i => i.IsAuthenticated);

			var isAuthorized = !userIsAnonymous
				&& !_roles.IsNullOrEmpty()
				&& _roles.Any(r => context.HttpContext.User.IsInRole(r.ToString()));

			if (!isAuthorized)
				context.Result = new ForbidResult();
		}
	}
}
