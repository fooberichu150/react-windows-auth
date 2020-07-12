using System;

namespace ReactWindowsAuth.Models.Security
{
	public static class RoleGroupExtensions
	{
		public static Role[] Roles(this RoleGroup roleGroup)
		{
			switch (roleGroup)
			{
				case RoleGroup.Any:
					return RoleGroupConstants.Any;
				case RoleGroup.Write:
					return RoleGroupConstants.Write;
				default:
					return Array.Empty<Role>();
			}
		}
	}
}