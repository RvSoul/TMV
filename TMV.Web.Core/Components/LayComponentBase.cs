using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.Web.Core.Components
{
    public class LayComponentBase: ComponentBase
    {
        public string T(string key)
        {
            return key;
        }
        [Inject]
        public IPopupService PopupService
        {
            get;
            set;
        }

        [Inject]
        NavigationManager Navigation { get; set; } = default!;
    }
}
