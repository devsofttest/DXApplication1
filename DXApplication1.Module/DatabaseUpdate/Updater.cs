using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.Extensions.DependencyInjection;
using DXApplication1.Module.BusinessObjects;

namespace DXApplication1.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema()
    {
        base.UpdateDatabaseAfterUpdateSchema();
        Employee theObject = ObjectSpace.FirstOrDefault<Employee>(x => x.FullName == "Employee 0");
        if (theObject == null)
        {
            CreateEmployee();
        }
        ObjectSpace.CommitChanges();
    }

    public void CreateEmployee()
    {
        for (int i = 0; i < 100; i++)
        {
            var emp = ObjectSpace.CreateObject<Employee>();
            emp.FullName = $"Employee {i}";
        }
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
