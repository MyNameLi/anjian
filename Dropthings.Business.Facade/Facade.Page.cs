namespace Dropthings.Business.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Dropthings.Data;

    using Dropthings.Util;
    using Dropthings.Configuration;
    using OmarALZabir.AspectF;
    using System.Data.Objects.DataClasses;
    using System.Text.RegularExpressions;
    using System.Web.Security;

    /// <summary>
    /// Facade subsystem for Tabs, Columns, WidgetZones
    /// </summary>
    partial class Facade
    {
        #region Methods

        public PageEntity GetTab(int pageId)
        {
            return this.pageRepository.GetTabById(pageId);
        }

        public List<PageEntity> GetTabsOfUser(int userid)
        {
            return this.pageRepository.GetTabsOfUser(userid);
        }

        public List<ColumnEntity> GetColumnsInTab(int pageId)
        {
            return this.columnRepository.GetColumnsByTabId(pageId);
        }

        public ColumnEntity CloneColumn(PageEntity clonedTab, ColumnEntity columnToClone)
        {
            var widgetZoneToClone = this.widgetZoneRepository.GetWidgetZoneById(columnToClone.WIDGETZONEID.Value);

            var clonedWidgetZone = this.widgetZoneRepository.Insert(new WidgetZoneEntity
            {
                TITLE = widgetZoneToClone.TITLE,
                UNIQUEID = Guid.NewGuid().ToString()
            });

            var widgetInstancesToClone = this.GetWidgetInstancesInZoneWithWidget(widgetZoneToClone.ID.Value);
            widgetInstancesToClone.Each(widgetInstanceToClone => CloneWidgetInstance(clonedWidgetZone.ID.Value, widgetInstanceToClone));
            var newColumn = new ColumnEntity
            {
                PAGEID = clonedTab.ID,
                WIDGETZONEID = clonedWidgetZone.ID,
                COLUMNNO = columnToClone.COLUMNNO,
                COLUMNWIDTH = columnToClone.COLUMNWIDTH
            };

            return this.columnRepository.Insert(newColumn);
        }

        public PageEntity CreateTab(int userid, string title, int layoutType, int toOrder)
        {
            PushDownTabs(0, toOrder);

            //张涛2010-12-25修改 begin
            title = string.IsNullOrEmpty(title) ? DecideUniqueTabName(title) : DecideUniqueTabName(title);
            //title = string.IsNullOrEmpty(title) ? DecideUniqueTabName(title) : title;
            //end

            var insertedTab = this.pageRepository.Insert(new PageEntity
            {
                USERID = userid,
                TITLE = title,
                LAYOUTTYPE = layoutType,
                ORDERNO = toOrder,
                COLUMNCOUNT = PageEntity.GetColumnWidths(layoutType).Length,
                CREATEDDATE = DateTime.Now,
                VERSIONNO = 1,
                ISDOWNFORMAINTENANCE = false,
                ISLOCKED = false,
                LASTDOWNFORMAINTENANCEAT = DateTime.Now,
                LASTLOCKEDSTATUSCHANGEDAT = DateTime.Now,
                LASTUPDATED = DateTime.Now,
                PAGETYPE = 0,
                SERVEASSTARTPAGEAFTERLOGIN = false
            });

            var page = this.pageRepository.GetTabById(insertedTab.ID.Value);

            for (int i = 0; i < insertedTab.COLUMNCOUNT; ++i)
            {
                var insertedWidgetZone = this.widgetZoneRepository.Insert(new WidgetZoneEntity
                {
                    TITLE = "Column " + (i + 1),
                    ORDERNO = 0,
                    UNIQUEID = "Column " + (i + 1)
                });

                var insertedColumn = this.columnRepository.Insert(new ColumnEntity
                {
                    COLUMNNO = i,
                    COLUMNWIDTH = (100 / insertedTab.COLUMNCOUNT),
                    WIDGETZONEID = insertedWidgetZone.ID,
                    PAGEID = insertedTab.ID,
                    LASTUPDATED = DateTime.Now
                });
            }

            ReorderTabsOfUser();

            return SetCurrentTab(userid, page.ID.Value);
        }

        public PageEntity CreateTab(string title, int layoutType)
        {
            var userid = this.GetUserIDFromUserName(Context.CurrentUserName);
            return CreateTab(userid, title, layoutType, 9999);
        }

        public PageEntity CloneTab(int userid, PageEntity pageToClone)
        {
            if (userid != 0)
            {
                var clonedTab = this.pageRepository.Insert(new PageEntity
                {
                    USERID = userid,
                    CREATEDDATE = DateTime.Now,
                    //20101225 张涛 BEGIN
                    TITLE = DecideUniqueTabName(pageToClone.TITLE),
                    //Title = pageToClone.Title,
                    //END
                    LASTUPDATED = pageToClone.LASTUPDATED,
                    VERSIONNO = pageToClone.VERSIONNO,
                    LAYOUTTYPE = pageToClone.LAYOUTTYPE,
                    PAGETYPE = pageToClone.PAGETYPE,
                    COLUMNCOUNT = pageToClone.COLUMNCOUNT,
                    ORDERNO = pageToClone.ORDERNO,
                });

                //ReorderTabsOfUser();

                var columns = this.GetColumnsInTab(pageToClone.ID.Value);
                columns.Each(columnToClone => CloneColumn(clonedTab, columnToClone));

                return clonedTab;
            }

            return null;
        }

        public WidgetInstanceEntity CloneWidgetInstance(int widgetZoneId, WidgetInstanceEntity wiToClone)
        {
            var newWidgetInstance = this.widgetInstanceRepository.Insert(new WidgetInstanceEntity
            {
                CREATEDDATE = wiToClone.CREATEDDATE,
                EXPANDED = wiToClone.EXPANDED,
                HEIGHT = wiToClone.HEIGHT,
                LASTUPDATE = wiToClone.LASTUPDATE,
                MAXIMIZED = wiToClone.MAXIMIZED,
                ORDERNO = wiToClone.ORDERNO,
                RESIZED = wiToClone.RESIZED,
                STATE = wiToClone.STATE,
                TITLE = wiToClone.TITLE,
                VERSIONNO = wiToClone.VERSIONNO,
                WIDGETID = wiToClone.WIDGETID,
                WIDGETZONEID = widgetZoneId,
                WIDTH = wiToClone.WIDTH
            });

            return newWidgetInstance;
        }

        public PageEntity SetCurrentTab(int userid, int currentTabId)
        {
            var userSetting = GetUserSetting(userid);
            var newTab = GetTab(currentTabId);

            userSetting.CURRENTPAGEID = newTab.ID;
            //userSetting.CurrentTabReference = new EntityReference<Tab> { EntityKey = newTab.EntityKey };

            this.userSettingRepository.Update(userSetting);

            return newTab;
        }


        /// <summary>
        /// Returns the shared tabs the user can see as read-only.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<PageEntity> GetSharedTabs(string userName)
        {
            if (this.IsUserAnonymous(userName))
            {
                var anonUserName = GetUserSettingTemplate().AnonUserSettingTemplate.UserName;
                return this.pageRepository.GetLockedTabsOfUser(this.GetUserIDFromUserName(anonUserName), false);
            }
            else
            {
                var registeredUserName = GetUserSettingTemplate().RegisteredUserSettingTemplate.UserName;
                var userGuid = this.GetUserIDFromUserName(registeredUserName);
                if (userGuid != null)
                    return this.pageRepository.GetLockedTabsOfUser(userGuid, false);
                else
                    return new List<PageEntity>();
            }
        }

        public List<PageEntity> GetPages(string roleid) {
            return this.pageRepository.GetPagesByRoleId(roleid);
        }

        public PageEntity DecideCurrentTab(int userid, string pageTitle, List<PageEntity> ownTabs, List<PageEntity> sharedTabs)
        {
            // Find the page that has the specified Tab Name and make it as current
            // page. This is needed to make a tab as current tab when the tab name is
            // known
            if (!string.IsNullOrEmpty(pageTitle))
            {
                foreach (PageEntity tab in ownTabs)
                {
                    if (string.Equals(tab.TITLE.Replace(' ', '_'), pageTitle) && tab.USERID > -1)
                    {
                        return tab;
                    }
                }

                if (sharedTabs != null)
                {
                    foreach (PageEntity tab in sharedTabs)
                    {
                        if (string.Equals(tab.TITLE.Replace(' ', '_') + "_Locked", pageTitle) && tab.USERID > -1)
                        {
                            return tab;
                        }
                    }
                }
            }

            // If we are here, then we haven't found a tab to show yet. so return the
            // current default tab
            return this.GetTab(this.GetUserSetting(userid).CURRENTPAGEID.Value);
        }

        public string DecideUniqueTabName(string title)
        {
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            List<PageEntity> pages = this.pageRepository.GetTabsOfUser(userGuid);

            string uniqueNamePrefix = title;
            string pageUniqueName = uniqueNamePrefix;
            for (int counter = 0; counter < 100; counter++)
            {
                if (counter > 0)
                    pageUniqueName = uniqueNamePrefix + " " + counter;
                if (pages.Exists((page) => page.TITLE == pageUniqueName) == false)
                    break;
            }

            if (pages.Exists((page) => page.TITLE == pageUniqueName))
                pageUniqueName = uniqueNamePrefix + "" + DateTime.Now.Ticks;

            return pageUniqueName;
        }

        public void ChangeTabName(string title)
        {
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            var userSetting = GetUserSetting(userGuid);

            var currentTab = this.GetTab(userSetting.CURRENTPAGEID.Value);
            // Ensure the title is unique and does not match with other pages
            var otherTabs = this.pageRepository.GetTabsOfUser(userGuid).Where(p => p.ID != currentTab.ID);

            // Keep incrementing the last digit on the page title until there's no 
            // such duplicate
            int loopCounter = 0;
            while (loopCounter++ < 100
                && otherTabs.FirstOrDefault(p => p.TITLE == title) != null)
            {
                var match = Regex.Match(title, "\\d+$");
                if (match.Success)
                {
                    var existingNumber = default(int);
                    if (int.TryParse(match.Value, out existingNumber))
                    {
                        title = Regex.Replace(title, "\\d+$", (existingNumber + 1).ToString());
                    }
                    else
                    {
                        title = title + " " + (existingNumber + 1);
                    }
                }
                else
                {
                    title = title + " 1";
                }
            }

            currentTab.TITLE = title;
            this.pageRepository.Update(currentTab);
        }

        public bool LockTab()
        {
            var success = false;
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            var userSetting = GetUserSetting(userGuid);

            if (userSetting != null && userSetting.CURRENTPAGEID > 0)
            {
                this.GetTab(userSetting.CURRENTPAGEID.Value).As(page =>
                    {
                        page.ISLOCKED = true;
                        page.LASTLOCKEDSTATUSCHANGEDAT = DateTime.Now;
                        this.pageRepository.Update(page);
                    });

                success = true;
            }

            return success;
        }

        public bool UnLockTab()
        {
            var success = false;
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            var userSetting = GetUserSetting(userGuid);

            if (userSetting != null && userSetting.CURRENTPAGEID > 0)
            {
                this.GetTab(userSetting.CURRENTPAGEID.Value).As(page =>
                    {
                        page.ISLOCKED = false;
                        page.ISDOWNFORMAINTENANCE = false;
                        page.LASTLOCKEDSTATUSCHANGEDAT = DateTime.Now;
                        this.pageRepository.Update(page);
                    });

                success = true;
            }

            return success;
        }

        public bool ChangeTabMaintenenceStatus(bool isInMaintenenceMode)
        {
            var success = false;
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            var userSetting = GetUserSetting(userGuid);

            if (userSetting != null && userSetting.CURRENTPAGEID > 0)
            {
                this.GetTab(userSetting.CURRENTPAGEID.Value).As(page =>
                    {
                        page.ISDOWNFORMAINTENANCE = isInMaintenenceMode;

                        if (isInMaintenenceMode)
                        {
                            page.LASTDOWNFORMAINTENANCEAT = DateTime.Now;
                        }
                        this.pageRepository.Update(page);
                    });

                success = true;
            }

            return success;
        }

        public bool ChangeServeAsStartPageAfterLoginStatus(bool shouldServeAsStartTab)
        {
            var success = false;
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            var userSetting = GetUserSetting(userGuid);

            if (userSetting != null && userSetting.CURRENTPAGEID > 0)
            {
                //check if there any previously overridable start page and make it false if request is for changing the start page to true
                if (shouldServeAsStartTab)
                {
                    PageEntity overridableTab = this.pageRepository.GetOverridableStartTabOfUser(userGuid);

                    if (overridableTab != null)
                    {
                        overridableTab.SERVEASSTARTPAGEAFTERLOGIN = false;
                        this.pageRepository.Update(overridableTab);
                    }
                }

                //change the overridable start page status
                this.GetTab(userSetting.CURRENTPAGEID.Value).As(page =>
                    {
                        page.SERVEASSTARTPAGEAFTERLOGIN = shouldServeAsStartTab;
                        this.pageRepository.Update(page);
                    });

                success = true;
            }

            return success;
        }

        public void DeleteColumn(int pageId, int columnNo)
        {
            var columnToDelete = this.columnRepository.GetColumnByTabId_ColumnNo(pageId, columnNo);
            WidgetZoneEntity widgetZone = this.widgetZoneRepository.GetWidgetZoneByTabId_ColumnNo(pageId, columnNo);

            var widgetInstances = this.GetWidgetInstancesInZoneWithWidget(widgetZone.ID.Value);
            widgetInstances.Each((widgetInstance) => this.widgetInstanceRepository.Delete(widgetInstance));

            this.columnRepository.Delete(columnToDelete);
            this.widgetZoneRepository.Delete(widgetZone);
        }
        /// <summary>
        /// Delete the specified page. If the specified page is the current
        /// page, then select the page before or after it as the current page
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public PageEntity DeleteTab(int pageId)
        {
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);

            var columns = this.GetColumnsInTab(pageId);
            columns.Each((column) => DeleteColumn(pageId, column.COLUMNNO.Value));

            var userSetting = GetUserSetting(userGuid);
            if (pageId == userSetting.CURRENTPAGEID.Value)
            {
                // Choose either the page before or after as the current page
                var pagesOfUser = this.pageRepository.GetTabsOfUser(userGuid);
                if (pagesOfUser.Count == 1)
                    throw new ApplicationException("Cannot delete the only page.");

                var index = pagesOfUser.FindIndex(p => p.ID == pageId);
                if (index == 0)
                    index++;
                else
                    index--;

                var newCurrentTab = pagesOfUser[index];
                SetCurrentTab(userGuid, newCurrentTab.ID.Value);
            }

            this.pageRepository.Delete(new PageEntity { ID = pageId });

            ReorderTabsOfUser();

            return this.pageRepository.GetTabById(GetUserSetting(userGuid).CURRENTPAGEID.Value);
        }

        public void ModifyTabLayout(int newLayout)
        {
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            var userSetting = GetUserSetting(userGuid);
            var newColumnDefs = PageEntity.GetColumnWidths(newLayout);
            var existingColumns = GetColumnsInTab(userSetting.CURRENTPAGEID.Value);
            var columnCounter = existingColumns.Count - 1;
            var newColumnNo = newColumnDefs.Count() - 1;

            if (newColumnDefs.Count() < existingColumns.Count)
            {
                // No of columns decreased. So, we need to move widgets from the deceased column 
                // to the last available column.

                for (var existingColumnNo = newColumnNo + 1; existingColumnNo < existingColumns.Count; existingColumnNo++)
                {
                    var oldWidgetZone = this.widgetZoneRepository.GetWidgetZoneByTabId_ColumnNo(userSetting.CURRENTPAGEID.Value, existingColumnNo);
                    var newWidgetZone = this.widgetZoneRepository.GetWidgetZoneByTabId_ColumnNo(userSetting.CURRENTPAGEID.Value, newColumnNo);

                    var widgetInstancesToMove = GetWidgetInstancesInZoneWithWidget(oldWidgetZone.ID.Value);
                    var originalWidgets = GetWidgetInstancesInZoneWithWidget(newWidgetZone.ID.Value);
                    if (originalWidgets.Count() > 0)
                    {
                        var lastWidgetPosition = originalWidgets.Max(w => w.ORDERNO.Value);

                        widgetInstancesToMove.Each((wi) => ChangeWidgetInstancePosition(wi.ID.Value, newWidgetZone.ID.Value, ++lastWidgetPosition));
                    }

                    DeleteColumn(userSetting.CURRENTPAGEID.Value, existingColumnNo);
                }
            }
            else
            {
                while (columnCounter + 1 < newColumnDefs.Length)
                {
                    var newColumnWidth = newColumnDefs[columnCounter];
                    // OMAR: Fix provided in http://code.google.com/p/dropthings/issues/detail?id=42#makechanges
                    //string title = "Column " + (newColumnNo + 1);                       
                    string title = "Column " + (columnCounter + 2);

                    // Add Column
                    var insertedWidgetZone = this.widgetZoneRepository.Insert(new WidgetZoneEntity
                    {
                        TITLE = title,
                        UNIQUEID = title,
                        ORDERNO = 0
                    });

                    var insertedColumn = this.columnRepository.Insert(new ColumnEntity
                    {
                        // OMAR: Fix provided in http://code.google.com/p/dropthings/issues/detail?id=42#makechanges
                        //newColumn.ColumnNo = newColumnNo;
                        COLUMNNO = columnCounter + 1,
                        COLUMNWIDTH = newColumnWidth,
                        WIDGETZONEID = insertedWidgetZone.ID,
                        PAGEID = userSetting.CURRENTPAGEID,
                        LASTUPDATED = DateTime.Now
                    });

                    ++columnCounter;
                }
            }

            var columns = this.columnRepository.GetColumnsByTabId(userSetting.CURRENTPAGEID.Value);
            columns.Each(column => column.COLUMNWIDTH = newColumnDefs[column.COLUMNNO.Value]);
            this.columnRepository.UpdateList(columns);

            var currentTab = this.GetTab(userSetting.CURRENTPAGEID.Value);
            currentTab.LAYOUTTYPE = newLayout;
            currentTab.COLUMNCOUNT = PageEntity.GetColumnWidths(newLayout).Length;
            this.pageRepository.Update(currentTab);
        }

        public void MoveTab(int pageId, int toOrderNo)
        {
            EnsureOwner(pageId, 0, 0);
            PushDownTabs(pageId, toOrderNo);
            ChangeTabPosition(pageId, toOrderNo);
            ReorderTabsOfUser();
        }

        public void PushDownTabs(int pageId, int toOrderNo)
        {
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);
            var isMovingDown = toOrderNo > (pageId > 0 ?
                this.GetTab(pageId).ORDERNO.GetValueOrDefault()
                : 0);

            IEnumerable<PageEntity> list = this.pageRepository.GetTabsOfUser(userGuid)
                .Where(page => (isMovingDown ? page.ORDERNO > toOrderNo : page.ORDERNO >= toOrderNo));

            //list = isMovingDown ? this.pageRepository.GetTabsOfUserAfterPosition(userGuid, toOrderNo) : this.pageRepository.GetTabsOfUserFromPosition(userGuid, toOrderNo);

            int orderNo = toOrderNo + 1;
            foreach (PageEntity item in list)
            {
                item.ORDERNO = ++orderNo;
            }

            this.pageRepository.UpdateList(list);
        }

        public void ChangeTabPosition(int pageId, int orderNo)
        {
            var page = this.GetTab(pageId);
            page.ORDERNO = orderNo > page.ORDERNO.GetValueOrDefault() ? orderNo + 1 : orderNo;
            this.pageRepository.Update(page);
        }

        public void ReorderTabsOfUser()
        {
            var userGuid = this.GetUserIDFromUserName(Context.CurrentUserName);

            var list = this.pageRepository.GetTabsOfUser(userGuid).OrderBy(p => p.ORDERNO.Value);

            int orderNo = 0;
            foreach (PageEntity page in list)
            {
                page.ORDERNO = orderNo++;
            }

            this.pageRepository.UpdateList(list);
        }

        #endregion Methods
    }
}