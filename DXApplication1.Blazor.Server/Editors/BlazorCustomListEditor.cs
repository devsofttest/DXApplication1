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
                IList list = ((IListSource)dataSource).GetList();
                IListServer listServer = list as IListServer;
                //listServer.Apply(CriteriaOperator.Parse("StartsWith(FirstName, 'B')"), null, 0, null, null);

                // IListServer.Count() query the number of filtered records on the server.
                // With this approach, the objects is not to be loaded on the client.
                int count = listServer.Count;

                // Since the data source may contain DataViewRecord instead of real objects,
                // and the data records may contain computed properties that do not exist in real object,
                // you must use PropertyDescriptors to access the values stored in each data record.
                PropertyDescriptorCollection properties = ((ITypedList)list).GetItemProperties(null);
                // Iterate over the objects in the data source.
                // If a ServerMode source is used, this method will load data in batches (i.e. not all at once)  as you need to access it.
                List<Employee> employees = new List<Employee>();    
                for (int i = 0; i < count; i++)
                {
                    // Get object record from data source.
                    // Depending on which data source is used, it can be either an actual Employee object or a DataViewRecord structure.
                    Employee record = (Employee)listServer[i];
                    employees.Add(record);
                    // reading object properties via PropertyDescriptors
                    //string firstName = (string)properties["FirstName"].GetValue(record);
                    //string lastName = (string)properties["LastName"].GetValue(record);
                }
                ComponentModel.Data = employees.AsEnumerable();// (dataSource as IEnumerable)?.OfType<Employee>().OrderBy(i => i.FullName);
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