namespace PeopleManager.Ui.Mvc.Stores
{
    public class TokenStore
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenStore(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string? GetToken()
        {
            string? jwtToken = null;
            _contextAccessor.HttpContext?.Request.Cookies.TryGetValue("JwtToken", out jwtToken);
            return jwtToken;
        }

        public void SaveToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete("JwtToken");
            _contextAccessor.HttpContext?.Response.Cookies.Append("JwtToken", token, new CookieOptions{HttpOnly = true});
        }
    }
}
