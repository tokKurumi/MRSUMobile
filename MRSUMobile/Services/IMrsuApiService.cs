using MRSUMobile.Entities;
using MRSUMobile.MVVM.Model;
using System.Net;

namespace MRSUMobile.Services
{
	public interface IMrsuApiService
	{
		Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default);
		Task<User> GetMyProfile(CancellationToken cancellationToken = default);
		Task<StudentTimeTable> GetTimeTable(DateTime date, CancellationToken cancellationToken = default);
		bool IsAutorized();
		Task<HttpStatusCode> Ping();
		Task<Token> RefreshSession(Token refreshToken, CancellationToken cancellationToken = default);
		Task<StudentAttendanceCode> SendAttendanceCode(string code, CancellationToken cancellationToken = default);
		void SetToken(Token bearer);
	}
}