using MRSUMobile.Models;
using MRSUMobile.Models.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MRSUMobile.Services
{
	public interface IMrsuApiService
	{
		Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default);
		Task<Token> Refresh(Token refresh, CancellationToken cancellationToken = default);
		Task<User> GetUser(CancellationToken cancellationToken = default);
		Task<Diary> GetDiary(DateTime date, CancellationToken cancellationToken = default);
		Task<StudentAttendance> SentCode(string code, CancellationToken cancellationToken = default);
		void SetToken(Token bearer);
		bool IsAutorized();
	}
}