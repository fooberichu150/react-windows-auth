using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReactWindowsAuth.Controllers
{
	[Authorize]
	public class WebControllerBase : ControllerBase
	{
	}
}
