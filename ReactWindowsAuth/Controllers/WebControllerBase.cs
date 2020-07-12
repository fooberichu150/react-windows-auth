using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactWindowsAuth.Attributes;
using ReactWindowsAuth.Models.Security;

namespace ReactWindowsAuth.Controllers
{
	[Authorize]
	[RoleRequirement(RoleGroup.Any)]
	public class WebControllerBase : ControllerBase
	{
	}
}
