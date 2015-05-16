using System;
namespace Dropthings.Data.Repository
{
    public interface IWidgetRepository : IDisposable
    {
        System.Collections.Generic.List<Dropthings.Data.WidgetEntity> GetAllWidgets();
        System.Collections.Generic.List<Dropthings.Data.WidgetEntity> GetAllWidgets(Dropthings.Data.Enumerations.WidgetType widgetType);
        Dropthings.Data.WidgetEntity GetWidgetById(int id);
        System.Collections.Generic.List<Dropthings.Data.WidgetEntity> GetInstanceWidgetsByTabId(int tabId);
        System.Collections.Generic.List<Dropthings.Data.WidgetEntity> GetWidgetByIsDefault(bool isDefault);
        Dropthings.Data.WidgetEntity Insert(Dropthings.Data.WidgetEntity w);
        void Update(Dropthings.Data.WidgetEntity wi);

        void Delete(int widgetId);
    }
}
