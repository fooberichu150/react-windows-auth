namespace ReactWindowsAuth.Models.Security
{
	public class RoleGroupConstants
	{
		public static Role[] Any => new[] { Role.Admin, Role.ReadOnly, Role.Write };
		public static Role[] Write => new[] { Role.Admin, Role.Write };
	}
}