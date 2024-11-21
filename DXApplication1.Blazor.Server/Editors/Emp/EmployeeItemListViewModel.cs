using System.ComponentModel;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DXApplication1.Module.BusinessObjects;
using Microsoft.AspNetCore.Components;

namespace DXApplication1.Blazor.Server.Editors.Emp
{
    public class EmployeeItemListViewModel : ComponentModelBase
    {
        public IListSource Data
        {
            get => GetPropertyValue<IListSource>();
            set => SetPropertyValue(value);
        }
        public EventCallback<Employee> ItemClick
        {
            get => GetPropertyValue<EventCallback<Employee>>();
            set => SetPropertyValue(value);
        }

        public override Type ComponentType => typeof(CustomEmployeeListView);
    }
}
