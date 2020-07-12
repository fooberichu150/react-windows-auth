using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ReactWindowsAuth.Services
{
	public class ActiveDirectoryUserRoleManager : IUserRoleManager
	{
		const string MemberOfAttribute = "memberOf";
		const string CanonicalName_Regex = @"(?:CN=(?<cn>[(\w|\s)]+)),.*,?OU=(?:Security Groups),?.*";
		const string CanonicalName_GroupName = "cn";

		public ActiveDirectoryUserRoleManager(ILogger<ActiveDirectoryUserRoleManager> logger,
			IHttpContextAccessor httpContextAccessor)
		{
			Logger = logger;
			HttpContext = httpContextAccessor.HttpContext;
		}

		private ILogger<ActiveDirectoryUserRoleManager> Logger { get; }

		private HttpContext HttpContext { get; }

		public IEnumerable<string> GetRoles()
		{
			string userName = HttpContext.User?.Identity?.Name;
			string ldapUrl = "127.0.0.1";

			if (string.IsNullOrWhiteSpace(userName))
				return Array.Empty<string>();

			string sanitizedUser = userName.Contains(@"\") ? userName.Substring(userName.IndexOf(@"\") + 1) : userName;
			try
			{
				using (var entry = new DirectoryEntry($"LDAP://{ldapUrl}"))
				{
					using (var searcher = new DirectorySearcher(entry))
					{
						searcher.Filter = $"(sAMAccountName={sanitizedUser})";
						searcher.PropertiesToLoad.Add(MemberOfAttribute);
						var searchResult = searcher.FindOne();

						var memberOf = (searchResult != null && searchResult.Properties.Contains(MemberOfAttribute)) ? searchResult.Properties[MemberOfAttribute] : null;

						List<string> roles = new List<string>();
						foreach (var role in memberOf)
						{
							var match = System.Text.RegularExpressions.Regex.Match(role.ToString(), CanonicalName_Regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
							if (match.Success)
								roles.Add(match.Groups[CanonicalName_GroupName].Value);
						}

						return roles;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Unable to retrieve LDAP roles");
				return Array.Empty<string>();
			}
		}
	}
}
