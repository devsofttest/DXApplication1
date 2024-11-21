using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using DXApplication1.Module.BusinessObjects;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp;
using System.Collections;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Data;
using System.Security.AccessControl;


namespace DXApplication1.Blazor.Server.Editors
{
    [ListEditor(typeof(Employee))]
    public class BlazorCustomListEditor : ListEditor, IComponentContentHolder
    {
        private RenderFragment _componentContent;
        private Employee[] selectedObjects = Array.Empty<Employee>();

        public  EmployeeItemListViewModel ComponentModel { get; private set; }
        public RenderFragment ComponentContent
        {
            get
            {
                _componentContent ??= ComponentModelObserver.Create(ComponentModel, ComponentModel.GetComponentContent());
                return _componentContent;
            }
        }

        public BlazorCustomListEditor(IModelListView model) : base(model) { }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateDataSource(DataSource);
        }

        private void UpdateDataSource(object dataSource)
        {
            if (ComponentModel is not null)
            {
                ComponentModel.Data = (IListSource)dataSource ;
            }
        }

        protected override object CreateControlsCore()
        {
            ComponentModel = new EmployeeItemListViewModel();
            ComponentModel.ItemClick = EventCallback.Factory.Create<Employee>(this, (item) => {
                selectedObjects = new Employee[] { item };
                OnSelectionChanged();
                OnProcessSelectedItem();
            });
            return ComponentModel;
        }

        protected override void AssignDataSourceToControl(object dataSource)
        {
            if (ComponentModel is not null)
            {
                if (ComponentModel.Data is IBindingList bindingList)
                {
                    bindingList.ListChanged -= BindingList_ListChanged;
                }
                UpdateDataSource(dataSource);
                if (dataSource is IBindingList newBindingList)
                {
                    newBindingList.ListChanged += BindingList_ListChanged;
                }
            }
        }

        public override void BreakLinksToControls()
        {
            AssignDataSourceToControl(null);
            base.BreakLinksToControls();
        }

        public override void Refresh() => UpdateDataSource(DataSource);

        public override SelectionType SelectionType => SelectionType.Full;

        public override IList GetSelectedObjects() => selectedObjects;
    }
}