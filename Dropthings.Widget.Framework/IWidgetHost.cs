using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Widget.Framework
{
    public interface IWidgetHost
    {
        int ID { get; }
        void SaveState(string state);
        string GetState();
        void Expand();
        void Collaspe();
        void Maximize();
        void Restore();
        void Close();
        WidgetInstanceEntity WidgetInstance { get; set; }
        bool IsLocked { get; set; }
        event Action<WidgetInstanceEntity,IWidgetHost> Deleted;
        void ShowSettings(bool userClicked);
        void HideSettings(bool userClicked);
        void Refresh(IWidget widget);

        EventBrokerService EventBroker { get; set; }        
    }
}
