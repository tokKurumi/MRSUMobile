namespace MRSUMobile.Services
{
    using System.Net;
    using MRSUMobile.Entities;
    using MRSUMobile.MVVM.Model;

    public interface IMrsuApiService
    {
        Token BearerToken { get; }

        Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default);

        Task<User> GetMyProfile(CancellationToken cancellationToken = default);

        Task<List<StudentTimeTable>> GetTimeTable(DateOnly date, CancellationToken cancellationToken = default);

        bool IsAutorized();

        Task<HttpStatusCode> Ping();

        Task<Token> RefreshSession(Token refreshToken, CancellationToken cancellationToken = default);

        Task<StudentAttendanceCode> SendAttendanceCode(string code, CancellationToken cancellationToken = default);

        Task<StudentSemester> GetSemester(int year, int period, CancellationToken cancellationToken = default);

        Task<StudentSemester> GetSemester(CancellationToken cancellationToken = default);

        Task<StudentRatingPlan> GetRatingPlan(int disciplineId, CancellationToken cancellationToken = default);

        void SetToken(Token bearer);
    }
}