namespace DatingApi.Services
{
    public class LanguageService(IHttpContextAccessor httpContextAccessor) : ILanguageService
    {
        public string CurrentLanguage => getLanguage();
        private string getLanguage()
        {
            return httpContextAccessor?.HttpContext?.Request.Headers.AcceptLanguage.FirstOrDefault() ?? "en";
        }
    }
}
