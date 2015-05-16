using System;
using System.Collections.Generic;
namespace Dropthings.Data.Repository
{
    public interface IWidgetInstanceRepository : IDisposable
    {
        void Delete(Dropthings.Data.WidgetInstanceEntity wi);
        void Delete(int id);
        Dropthings.Data.WidgetInstanceEntity GetWidgetInstanceById(int id);
        System.Collections.Generic.List<Dropthings.Data.WidgetInstanceEntity> GetWidgetInstanceOnWidgetZoneAfterPosition(int widgetZoneId, int position);
        System.Collections.Generic.List<Dropthings.Data.WidgetInstanceEntity> GetWidgetInstanceOnWidgetZoneFromPosition(int widgetZoneId, int position);
        string GetWidgetInstanceOwnerName(int widgetInstanceId);
        System.Collections.Generic.List<Dropthings.Data.WidgetInstanceEntity> GetWidgetInstancesByWidgetZoneIdWithWidget(int widgetZoneId);
        Dropthings.Data.WidgetInstanceEntity Insert(Dropthings.Data.WidgetInstanceEntity wi);
        void Update(Dropthings.Data.WidgetInstanceEntity wi);
        void UpdateList(System.Collections.Generic.IEnumerable<Dropthings.Data.WidgetInstanceEntity> widgetInstances);
        void InsertList(IEnumerable<WidgetInstanceEntity> wis);
    }
}
