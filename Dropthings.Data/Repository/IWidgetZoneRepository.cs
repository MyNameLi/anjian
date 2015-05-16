using System;
namespace Dropthings.Data.Repository
{
    public interface IWidgetZoneRepository : IDisposable
    {
        void Delete(Dropthings.Data.WidgetZoneEntity widgetZone);
        Dropthings.Data.WidgetZoneEntity GetWidgetZoneById(int widgetZoneId);
        Dropthings.Data.WidgetZoneEntity GetWidgetZoneByTabId_ColumnNo(int TabId, int columnNo);
        string GetWidgetZoneOwnerName(int widgetZoneId);
        Dropthings.Data.WidgetZoneEntity Insert(Dropthings.Data.WidgetZoneEntity zone);
        void Update(Dropthings.Data.WidgetZoneEntity zone);
    }
}
