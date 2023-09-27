using MRSUMobile.Models;
using MRSUMobile.Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace MRSUMobile.Services
{
	public interface IMrsuApiService
	{
		Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default);
		Task<User> GetUser(CancellationToken cancellationToken = default);
		void SetToken(Token bearer);
		bool IsAutorized();
	}
}