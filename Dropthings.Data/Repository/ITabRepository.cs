using System;
namespace Dropthings.Data.Repository
{
    public interface ITabRepository : IDisposable
    {
        void Delete(PageEntity Tab);
        PageEntity GetFirstTabOfUser(int userid);
        System.Collections.Generic.List<PageEntity> GetLockedTabsOfUser(int userid, bool isDownForMaintenenceMode);
        System.Collections.Generic.List<PageEntity> GetLockedTabsOfUserByMaintenenceMode(int userid, bool isInMaintenenceMode);
        System.Collections.Generic.List<PageEntity> GetMaintenenceTabsOfUser(int userid);
        PageEntity GetOverridableStartTabOfUser(int userid);
        PageEntity GetTabById(int TabId);
        System.Collections.Generic.List<int> GetTabIdByUserGuid(int userid);
        string GetTabOwnerName(int TabId);
        System.Collections.Generic.List<PageEntity> GetTabsOfUser(int userid);
        PageEntity Insert(PageEntity Tab);
        void Update(PageEntity Tab);
        void UpdateList(System.Collections.Generic.IEnumerable<PageEntity> Tabs);
        System.Collections.Generic.List<PageEntity> GetPagesByRoleId(string roleid);

    }
}
