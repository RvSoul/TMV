using BlazorComponent;
using Masa.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.Web.Core
{
    public static class PopupServiceExtensions
    {

        public static async Task<bool> OpenConfirmDialogAsync(this IPopupService PopupService, string title, string content)
        {
            return await PopupService.ConfirmAsync(title, content, AlertTypes.Error);
        }

        public static async Task<bool> OpenConfirmDialogAsync(this IPopupService PopupService, string title, string content, AlertTypes type)
        {
            return await PopupService.ConfirmAsync(title, content, type);
        }

        //public static async Task OpenInformationMessageAsync(this IPopupService PopupService, string message)
        //{
        //    await PopupService.EnqueueSnackbarAsync(message, AlertTypes.Info);
        //}


    }
}
