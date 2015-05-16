using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.Data.Repository
{
    public interface IWidgetsInRolesRepository : IDisposable
    {
        void Delete(Dropthings.Data.WidgetsInRolesEntity wr);
        System.Collections.Generic.List<Dropthings.Data.WidgetsInRolesEntity> GetWidgetsInRolesByWidgetId(int widgetId);
        Dropthings.Data.WidgetsInRolesEntity Insert(Dropthings.Data.WidgetsInRolesEntity wir);
        void Update(Dropthings.Data.WidgetsInRolesEntity wr);
        IList<WidgetsInRolesEntity> GetWidGetListByRoleId(int roleid);
        void Delete(int roleid);
        void Add(WidgetsInRolesEntity entity);
    }
}
