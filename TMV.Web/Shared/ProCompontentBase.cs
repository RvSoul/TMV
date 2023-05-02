using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace TMV.Web
{
    public abstract class ProCompontentBase : ComponentBase
    {
        private I18n? _languageProvider;
        [Inject]
        public IPopupService PopupService
        {
            get;
            set;
        }
        [Inject]
        NavigationManager Navigation { get; set; } = default!;
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        [CascadingParameter]
        public I18n LanguageProvider
        {
            get
            {
                return _languageProvider ?? throw new Exception("please Inject I18n!");
            }
            set
            {
                _languageProvider = value;
            }
        }
        protected async override Task OnInitializedAsync()
        {
            //var authenticationState = await authenticationStateTask;

            //if (!authenticationState.User.Identity.IsAuthenticated)
            //{
            //    Navigation.NavigateTo($"{Navigation.BaseUri}pages/authentication/login-v2");
            //}

            // rest of the code
        }
        public string T(string key)
        {
            return LanguageProvider.T(key) ?? key;
        }
    }
}