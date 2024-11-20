using System.ComponentModel;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DXApplication1.Module.BusinessObjects;
using Microsoft.AspNetCore.Components;

namespace DXApplication1.Blazor.Server.Editors
{
    public class EmployeeItemListViewModel : ComponentModelBase
    {
        public IEnumerable<Employee> Data
        {
            get => GetPropertyValue<IEnumerable<Employee>>();
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
