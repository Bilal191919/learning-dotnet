using Microsoft.AspNetCore.Identity;

namespace Stage_7.Domain
{
	public class User : IdentityUser<int>
	{
		public string FullName { get; set; }
	}
}