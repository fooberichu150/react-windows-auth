using System.Collections.Generic;

namespace ReactWindowsAuth.Services
{
	public interface IUserRoleManager
	{
		IEnumerable<string> GetRoles();
	}
}
