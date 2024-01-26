namespace FullCart.Application.Common.Dto
{
    public class LoggedUserInfoDto
    {
        public string Id { get; internal set; }
        public string Username { get; internal set; }
        public string Fullname { get; internal set; }
        public string Token { get; internal set; }
        public int TokenExpiry { get; internal set; }
        public string RefreshToken { get; internal set; }
    }
}
