using System.ComponentModel;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DXApplication1.Module.BusinessObjects;
using Microsoft.AspNetCore.Components;

namespace DXApplication1.Blazor.Server.Editors
{
    public class PictureItemListViewModel : ComponentModelBase
    {
        public  IListSource Data
        {
            get => GetPropertyValue<IListSource>();
            set => SetPropertyValue(value);
        }
        public EventCallback<IPictureItem> ItemClick
        {
            get => GetPropertyValue<EventCallback<IPictureItem>>();
            set => SetPropertyValue(value);
        }
        public override Type ComponentType => typeof(PictureItemListView);
    }
}
