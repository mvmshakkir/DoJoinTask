
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DoJoinTask.Repositories
{
	public interface IUserRepository
	{
		Task<IdentityUser> GetUserByUsernameAsync(string username);
		Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
		Task<SignInResult> CheckUserPasswordAsync(IdentityUser user, string password);
	}
}
