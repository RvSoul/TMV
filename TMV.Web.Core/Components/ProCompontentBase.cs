using Blazored.SessionStorage;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace TMV.Web.Core.Components
{
    public abstract class ProCompontentBase : ComponentBase
    {
       // private I18n? _languageProvider;
        
        [Inject]
        public IPopupService PopupService
        {
            get;
            set;
        }
        private bool forceRender { get; set; } = false;
        [Inject]
        NavigationManager Navigation { get; set; } = default!;
        [Inject]
        ProtectedLocalStorage sessionStorage { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        protected async override Task OnInitializedAsync()
        {
            if (authenticationState is not null)
            {
                var authState = await authenticationState;
                var user = authState?.User;

                if (user?.Identity is not null && user.Identity.IsAuthenticated)
                {
                    //await PopupService.ToastSuccessAsync($"欢迎{user.Identity.Name} 进入系统！！") ;
                }
                else
                {
                    await PopupService.ToastErrorAsync($"没有权限，请重新登录！！");
                }
            }
        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender || forceRender)
            {
                forceRender = false;
                var usering = await sessionStorage.GetAsync<string>("identity");
                //var usering =  sessionStorage.GetItemAsync<string>("UserId");
                if (!usering.Success)
                {
                    await PopupService.ToastErrorAsync($"登录已过期，请重新登录！！");
                }
                else
                {
                    StateHasChanged();
                }
            }
        }
        public string T(string key)
        {
            return key;
        }
    }
}