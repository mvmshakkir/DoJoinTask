using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DoJoinTask.Repositories
{
	/// <summary>
	/// This class handles user-related database operations such as creating a new user, checking user password
	/// </summary>
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<IdentityUser> _userManager;
		// Constructor injects UserManager to interact with IdentityUser objects
		public UserRepository(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}
		/// <summary>
		/// Retrieves a user by their username.
		/// </summary>
		public async Task<IdentityUser> GetUserByUsernameAsync(string username)
		{
			return await _userManager.FindByNameAsync(username);
		}
		/// <summary>
		/// Creates a new user with a password.
		/// </summary>
		public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
		{
			return await _userManager.CreateAsync(user, password);
		}
		/// <summary>
		/// Checks if the user’s password matches the one in the system.
		/// </summary>
		public async Task<SignInResult> CheckUserPasswordAsync(IdentityUser user, string password)
		{
			// Use CheckPasswordAsync to validate the password
			var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

			// Return appropriate SignInResult based on the password validity
			if (isPasswordValid)
			{
				return SignInResult.Success;  // Password is correct
			}

			return SignInResult.Failed;  // Password is incorrect
		}
	}
}
