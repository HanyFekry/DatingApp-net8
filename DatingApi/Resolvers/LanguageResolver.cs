using AutoMapper;
using DatingApi.Interfaces;
using DatingApi.Services;

namespace DatingApi.Resolvers
{
    public class LanguageResolver : IValueResolver<IMultiName, IDtoWithName, string>
    {
        private readonly ILanguageService _languageService;

        public LanguageResolver(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public string Resolve(IMultiName source, IDtoWithName destination, string destMember, ResolutionContext context)
        {
            string lang = _languageService.CurrentLanguage;
            return lang == "en" ? source.EnName : source.ArName;
        }
    }

}
