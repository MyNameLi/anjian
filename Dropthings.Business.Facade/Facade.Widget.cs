namespace Dropthings.Business.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Dropthings.Data;
    using OmarALZabir.AspectF;
    using Dropthings.Util;
    using System.Web.Security;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Facade subsystem for Widgets and WidgetInstances
    /// </summary>
    partial class Facade
    {
        #region Methods

        public List<WidgetEntity> GetAllWidgets()
        {
            return this.widgetRepository.GetAllWidgets();
        }

        public List<WidgetEntity> GetInstanceWidgetsByTabId(int tabID)
        {
            return this.widgetRepository.GetInstanceWidgetsByTabId(tabID);
        }

        public WidgetInstanceEntity GetWidgetInstanceById(int widgetInstanceId)
        {
            return this.widgetInstanceRepository.GetWidgetInstanceById(widgetInstanceId);
        }
        public WidgetEntity GetWidgetById(int widgetId)
        {
            return this.widgetRepository.GetWidgetById(widgetId);
        }
        //public string GetWidgetInstanceOwnerName(int widgetInstanceId)
        //{
        //    return this.widgetInstanceRepository.GetWidgetInstanceOwnerName(widgetInstanceId);
        //}

        public void CreateDefaultWidgetsOnTab(string userName, int pageId)
        {
            List<WidgetEntity> defaultWidgets = null;

            defaultWidgets = (/*System.Web.Security.Roles.Enabled &&*/ !string.IsNullOrEmpty(userName)) ?
                this.GetWidgetList(userName, Enumerations.WidgetType.PersonalTab).Where(w => w.ISDEFAULT == true).ToList() :
                this.widgetRepository.GetWidgetByIsDefault(true);

            var widgetsPerColumn = (int)Math.Ceiling((float)defaultWidgets.Count / 3.0);

            var row = 0;
            var col = 0;

            var widgetZone = this.widgetZoneRepository.GetWidgetZoneByTabId_ColumnNo(pageId, col);
            List<WidgetInstanceEntity> wis = defaultWidgets.ConvertAll<WidgetInstanceEntity>(widget =>
            {
                var instance = new WidgetInstanceEntity();

                instance.WIDGETZONEID = widgetZone.ID;
                instance.ORDERNO = row;
                instance.CREATEDDATE = DateTime.Now;
                instance.EXPANDED = true;
                instance.TITLE = widget.NAME;
                instance.VERSIONNO = 1;
                instance.WIDGETID = widget.ID;
                instance.STATE = widget.DEFAULTSTATE;

                row++;
                if (row >= widgetsPerColumn)
                {
                    row = 0;
                    col++;
                }

                return instance;
            });
            this.widgetInstanceRepository.InsertList(wis);
        }

        public List<WidgetEntity> GetWidgetList(Enumerations.WidgetType widgetType)
        {
            return this.widgetRepository.GetAllWidgets(widgetType);
        }

        public List<WidgetEntity> GetWidgetList(string username, Enumerations.WidgetType widgetType)
        {
            var userid = this.GetUserIDFromUserName(username);
            var userRoles = this.userRepository.GetRolesOfUser(userid);
            var widgets = this.widgetRepository.GetAllWidgets(widgetType);
            return widgets.Where(w => this.widgetsInRolesRepository.GetWidgetsInRolesByWidgetId(w.ID.Value)
                .Exists(wr => userRoles.Exists(role => role.ROLEID == wr.ROLEID))).ToList();
        }

        public bool IsWidgetInRole(int widgetId, string roleName)
        {
            List<WidgetsInRolesEntity> widgetsInRole = this.widgetsInRolesRepository.GetWidgetsInRolesByWidgetId(widgetId);
            var roleId = this.roleRepository.GetRoleByRoleName(roleName).ROLEID;
            return widgetsInRole.Exists(r => r.ROLEID == roleId);
        }

        public IEnumerable<WidgetInstanceEntity> GetWidgetInstancesInZoneWithWidget(int widgetZoneId)
        {
            var widgetInstances = this.widgetInstanceRepository.GetWidgetInstancesByWidgetZoneIdWithWidget(widgetZoneId);
            var widgetInstacesToRemove = new List<WidgetInstanceEntity>();

            var widgetsForUser = GetWidgetList(Context.CurrentUserName, Enumerations.WidgetType.PersonalTab);
            widgetInstances.Each(wi =>
            {
                if (wi.WIDGETID == 0)
                    wi.Widget = this.widgetRepository.GetWidgetById(wi.WIDGETID.Value);

                // Ensure the user has permission to see all the widgets. It's possible that
                // roles have been revoked from the widgets after it was added on the user's page
                if (!widgetsForUser.Exists(w => wi.ID == wi.WIDGETID))
                {
                    // user can no longer have this widget

                }
            });
            return widgetInstances.OrderBy(wi => wi.ORDERNO.Value);
        }

        public WidgetInstanceEntity ExpandWidget(int widgetInstanceId, bool isExpand)
        {
            EnsureOwner(0, widgetInstanceId, 0);
            var widgetInstance = this.GetWidgetInstanceById(widgetInstanceId);
            widgetInstance.EXPANDED = isExpand;

            this.widgetInstanceRepository.Update(widgetInstance);

            return this.GetWidgetInstanceById(widgetInstanceId);
        }

        public void MoveWidgetInstance(int widgetInstanceId, int toZoneId, int toRowId)
        {
            EnsureOwner(0, widgetInstanceId, 0);

            var widgetInstance = this.GetWidgetInstanceById(widgetInstanceId);
            var fromZoneId = widgetInstance.WIDGETZONEID.Value;

            //PushDownWidgetInstancesOnWidgetZoneAfterWidget(toRowId, widgetInstanceId, toZoneId);
            PushDownWidgetInstancesOnWidgetZone(toRowId, toZoneId, widgetInstanceId);
            //ChangeWidgetInstancePosition(widgetInstanceId, toZoneId, toRowId);

            // Refresh the order numbers of all widgets on the zones
            // to reset their sequence number from 0
            //ReorderWidgetInstancesOnWidgetZone(fromZoneId);
            if (fromZoneId != toZoneId)
            {
                ReorderWidgetInstancesOnWidgetZone(fromZoneId);
            }
        }

        private void PushDownWidgetInstancesOnWidgetZoneAfterWidget(int toRowId, int widgetInstanceId, int widgetZoneId)
        {
            //var widgetInstance = this.GetWidgetInstanceById(widgetInstanceId);
            //var isMovingDown = toRowId > widgetInstance.ORDERNO.Value;

            //PushDownWidgetInstancesOnWidgetZone(toRowId, widgetZoneId, isMovingDown);
            PushDownWidgetInstancesOnWidgetZone(toRowId, widgetZoneId, widgetInstanceId);
        }

        //private void PushDownWidgetInstancesOnWidgetZone(int toRowId, int widgetZoneId, bool isMovingDown)
        private void PushDownWidgetInstancesOnWidgetZone(int toRowId, int widgetZoneId, int widgetInstanceId)
        {
            List<WidgetInstanceEntity> list = this.widgetInstanceRepository.GetWidgetInstancesByWidgetZoneIdWithWidget(widgetZoneId);

            //拖动状态
            if (widgetInstanceId > 0)
            {
                var widgetInstance = list.Where(wi => wi.ID == widgetInstanceId).FirstOrDefault();
                //在同一个WidgetZone里面的拖动
                if (widgetInstance != null)
                {
                    if (toRowId > widgetInstance.ORDERNO.Value)
                    {
                        int orderNo = toRowId;
                        list.Where(wi => wi.ORDERNO.Value <= orderNo)
                            .OrderByDescending(wi => wi.ORDERNO.Value)
                            .Each(wi =>
                            {
                                wi.ORDERNO = --orderNo;
                            });
                    }
                    else
                    {
                        int orderNo = toRowId + 1;

                        list.Where(wi => wi.ORDERNO.Value >= toRowId)
                            .OrderBy(wi => wi.ORDERNO.Value)
                            .Each(wi =>
                            {
                                wi.ORDERNO = orderNo++;
                            });
                    }

                    widgetInstance.ORDERNO = toRowId;

                    int temp = 0;
                    list.OrderBy(wi => wi.ORDERNO.Value)
                            .Each(wi =>
                            {
                                wi.ORDERNO = temp++;
                            });
                }
                else
                {
                    WidgetInstanceEntity curWidgetInstance = this.widgetInstanceRepository.GetWidgetInstanceById(widgetInstanceId);
                    curWidgetInstance.WIDGETZONEID = widgetZoneId;
                    curWidgetInstance.ORDERNO = toRowId;
                    list.Add(curWidgetInstance);

                    int temp = 0;
                    list.OrderBy(wi => wi.ORDERNO.Value)
                            .Each(wi =>
                            {
                                wi.ORDERNO = temp++;
                            });
                }
            }
            else//初始加载状态
            {
                int temp = 1;
                list.OrderBy(wi => wi.ORDERNO.Value)
                        .Each(wi =>
                        {
                            wi.ORDERNO = temp++;
                        });
            }

            this.widgetInstanceRepository.UpdateList(list);
        }

        private void ChangeWidgetInstancePosition(int widgetInstanceId, int toZoneId, int rowNo)
        {
            this.GetWidgetInstanceById(widgetInstanceId).As(wi =>
            {
                var fromZoneId = wi.WIDGETZONEID.Value;
                if (fromZoneId != toZoneId)
                {
                    // widget moving from one zone to another. Need to clear all cached
                    // instances of widgets on the source zone
                    CacheKeys.WidgetZoneKeys.AllWidgetZoneIdBasedKeys(fromZoneId)
                        .Each(key => Services.Get<ICache>().Remove(key));

                    // Change the widget zone reference to reflect the new widgetzone for the
                    // widget instance
                    //var newWidgetZone = this.widgetZoneRepository.GetWidgetZoneById(toZoneId);
                    wi.WIDGETZONEID = toZoneId;//.WidgetZone = newWidgetZone;// new WidgetZone { ID = newWidgetZone.ID };
                    //wi.WidgetZoneReference = new EntityReference<WidgetZoneEntity> { EntityKey = newWidgetZone.EntityKey };

                    // The new dropped zone now has more widgets than before. So clear cache for widgets
                    // on that zone
                    CacheKeys.WidgetZoneKeys.AllWidgetZoneIdBasedKeys(toZoneId)
                            .Each(key => Services.Get<ICache>().Remove(key));
                }

                //wi.ORDERNO = rowNo;

                //this.widgetInstanceRepository.Update(wi);
            });
        }

        private void ReorderWidgetInstancesOnWidgetZone(int widgetZoneId)
        {
            var list = this.widgetInstanceRepository.GetWidgetInstancesByWidgetZoneIdWithWidget(widgetZoneId).OrderBy(wi => wi.ORDERNO.Value); ;

            int orderNo = 0;
            foreach (WidgetInstanceEntity wi in list)
            {
                wi.ORDERNO = orderNo++;
            }

            this.widgetInstanceRepository.UpdateList(list);
        }

        public string GetWidgetInstanceState(int widgetInstanceId)
        {
            EnsureOwner(0, widgetInstanceId, 0);
            var widgetInstance = this.GetWidgetInstanceById(widgetInstanceId);

            return widgetInstance.STATE;
        }

        public WidgetInstanceEntity SaveWidgetInstanceState(int widgetInstanceId, string state)
        {
            this.GetWidgetInstanceById(widgetInstanceId).As(wi =>
            {
                wi.STATE = state;
                this.widgetInstanceRepository.Update(wi);
            });

            return this.GetWidgetInstanceById(widgetInstanceId);
        }

        public WidgetInstanceEntity ResizeWidgetInstance(int widgetInstanceId, int width, int height)
        {
            EnsureOwner(0, widgetInstanceId, 0);

            this.GetWidgetInstanceById(widgetInstanceId).As(wi =>
            {
                wi.RESIZED = true;
                wi.WIDTH = width;
                wi.HEIGHT = height;
                this.widgetInstanceRepository.Update(wi);
            });

            return this.GetWidgetInstanceById(widgetInstanceId);
        }

        public WidgetInstanceEntity MaximizeWidget(int widgetInstanceId, bool isMaximized)
        {
            this.GetWidgetInstanceById(widgetInstanceId).As(wi =>
            {
                wi.MAXIMIZED = isMaximized;

                this.widgetInstanceRepository.Update(wi);
            });

            return this.GetWidgetInstanceById(widgetInstanceId);
        }

        public WidgetInstanceEntity ChangeWidgetInstanceTitle(int widgetInstanceId, string newTitle)
        {
            EnsureOwner(0, widgetInstanceId, 0);

            this.GetWidgetInstanceById(widgetInstanceId).As(wi =>
            {
                wi.TITLE = newTitle;
                this.widgetInstanceRepository.Update(wi);
            });

            return this.GetWidgetInstanceById(widgetInstanceId);
        }

        public void DeleteWidgetInstance(int widgetInstanceId)
        {
            EnsureOwner(0, widgetInstanceId, 0);

            var widgetInstance = this.GetWidgetInstanceById(widgetInstanceId);
            var widgetZoneId = widgetInstance.WIDGETZONEID.Value;
            this.widgetInstanceRepository.Delete(widgetInstance);
            ReorderWidgetInstancesOnWidgetZone(widgetZoneId);
        }

        public WidgetInstanceEntity AddWidgetInstance(int widgetId, int toRow, int columnNo, int zoneId)
        {
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            var userSetting = GetUserSetting(userGuid);

            WidgetZoneEntity widgetZone;

            if (zoneId > 0)
            {
                widgetZone = this.widgetZoneRepository.GetWidgetZoneById(zoneId);
            }
            else
            {
                widgetZone = this.widgetZoneRepository.GetWidgetZoneByTabId_ColumnNo(userSetting.CURRENTPAGEID.Value, columnNo);
            }

            PushDownWidgetInstancesOnWidgetZone(toRow, widgetZone.ID.Value, 0);

            var widget = this.widgetRepository.GetWidgetById(widgetId);

            var insertedWidget = this.widgetInstanceRepository.Insert(new WidgetInstanceEntity
            {
                TITLE = widget.NAME,
                WIDGETZONEID = widgetZone.ID.Value,
                ORDERNO = toRow,
                WIDGETID = widget.ID.Value,
                STATE = widget.DEFAULTSTATE,
                CREATEDDATE = DateTime.Now,
                EXPANDED = true,
                LASTUPDATE = DateTime.Now,
                MAXIMIZED = false,
                RESIZED = false,
                VERSIONNO = 1,
                WIDTH = 0,
                HEIGHT = 0
            });

            return this.widgetInstanceRepository.GetWidgetInstanceById(insertedWidget.ID.Value);
        }

        public WidgetEntity AddWidget(
            string name,
            string url,
            string icon,
            string description,
            string defaultState,
            bool isDefault,
            bool isLocked,
            int orderNo,
            string roleName,
            int widgetType)
        {
            return this.widgetRepository.Insert(new WidgetEntity
            {
                NAME = name,
                URL = url,
                ICON = icon,
                DESCRIPTION = description,
                DEFAULTSTATE = defaultState,
                ISDEFAULT = isDefault,
                ISLOCKED = isLocked,
                CREATEDDATE = DateTime.Now,
                ORDERNO = orderNo,
                ROLENAME = roleName,
                WIDGETTYPE = widgetType,
                ID = 0,
                LASTUPDATE = DateTime.Now,
                VERSIONNO = 1
            });
        }

        public void UpdateWidget(
            int widgetId,
            string name,
            string url,
            string icon,
            string description,
            string defaultState,
            bool isDefault,
            bool isLocked,
            int orderNo,
            string roleName,
            int widgetType)
        {
            var widget = this.widgetRepository.GetWidgetById(widgetId);

            widget.ICON = icon;
            widget.DEFAULTSTATE = defaultState;
            widget.ISLOCKED = isLocked;
            widget.ORDERNO = orderNo;
            widget.ROLENAME = roleName;
            widget.WIDGETTYPE = widgetType;
            widget.NAME = name;
            widget.URL = url;
            widget.DESCRIPTION = description;
            widget.ISDEFAULT = isDefault;
            widget.ORDERNO = orderNo;

            this.widgetRepository.Update(widget);
        }

        public void DeleteWidget(int widgetId)
        {
            this.widgetRepository.Delete(widgetId);
        }

        #endregion Methods
    }
}