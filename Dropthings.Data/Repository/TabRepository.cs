namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Linq.Expressions;
    using OmarALZabir.AspectF;
    using Dropthings.Util;
    using System.Data.SqlClient;
    using System.Data;


    public class TabRepository : Dropthings.Data.Repository.ITabRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly PageEntity.PAGEDAO _pageDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public TabRepository(ICache cacheResolver)
        {
            //this._database = database;
            this._pageDAO = new PageEntity.PAGEDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        public PageEntity GetTabById(int TabId)
        {
            string cacheKey = CacheKeys.TabKeys.TabId(TabId);
            object cachedTab = _cacheResolver.Get(cacheKey);
            if (null == cachedTab)
            {
                var Tab = _pageDAO.FindById(TabId); // new PageEntity();// _database.Query(
                //        CompiledQueries.TabQueries.GetTabById,
                //        TabId).First();
                //_cacheResolver.Add(cacheKey, Tab);
                return Tab;
            }
            else
            {
                return cachedTab as PageEntity;
            }
        }

        //public Tab GetTabById(int TabId)
        //{
        //    return AspectF.Define.Cache<Tab>(_cacheResolver, CacheKeys.TabId(TabId))
        //        .Return<Tab>(() =>
        //            _database.GetSingle<Tab, int>(TabId, CompiledQueries.TabQueries.GetTabById).Detach());
        //}        

        private void RemoveTabIdDependentItems(int id)
        {
            RemoveUserTabsCollection(id);
            CacheKeys.TabKeys.TabIdKeys(id).Each(key => _cacheResolver.Remove(key));
        }

        public List<PageEntity> GetPagesByRoleId(string roleid)
        {
            return _pageDAO.FindPagesByRoleId(roleid);
        }

        private void RemoveUserTabsCollection(int TabId)
        {
            var Tab = this.GetTabById(TabId);

            if (Tab != null)
            {
                var userid = Tab.USERID.Value;
                _cacheResolver.Remove(CacheKeys.UserKeys.TabsOfUser(userid));
            }
        }

        private void RemoveUserTabsCollectionByUserID(int userid)
        {
            _cacheResolver.Remove(CacheKeys.UserKeys.TabsOfUser(userid));
        }

        public void Delete(PageEntity Tab)
        {
            RemoveTabIdDependentItems(Tab.ID.Value);
            _pageDAO.Delete(Tab);
            //_database.Delete<Tab>(Tab);
        }

        public List<int> GetTabIdByUserGuid(int userid)
        {
            return this.GetTabsOfUser(userid).Select(Tab => Tab.ID.Value).ToList();
        }

        public string GetTabOwnerName(int TabId)
        {
            return AspectF.Define
                .Cache<string>(_cacheResolver, CacheKeys.TabKeys.TabOwnerName(TabId))
                .Return<string>(() =>
                    _pageDAO.GetTabOwnerName(TabId));
            //return AspectF.Define
            //    .Cache<string>(_cacheResolver, CacheKeys.TabKeys.TabOwnerName(TabId))
            //    .Return<string>(() =>
            //        _database.Query(
            //            CompiledQueries.TabQueries.GetTabOwnerName, TabId)
            //            .First());
        }
        /// <summary>
        /// get the pages by userid
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<PageEntity> GetTabsOfUser(int userid)
        {
            SqlParameter[] parameters ={
					new SqlParameter("@USERID",SqlDbType.Int)};
            parameters[0].Value = userid;

            return AspectF.Define
                .CacheList<PageEntity, List<PageEntity>>(_cacheResolver, CacheKeys.UserKeys.TabsOfUser(userid), Tab => CacheKeys.TabKeys.TabId(Tab.ID.Value))
                .Return<List<PageEntity>>(() =>
                    _pageDAO.Find("USERID=@USERID", parameters));
            //return AspectF.Define
            //    .CacheList<Tab, List<Tab>>(_cacheResolver, CacheKeys.UserKeys.TabsOfUser(userGuid), Tab => CacheKeys.TabKeys.TabId(Tab.ID))
            //    .Return<List<Tab>>(() =>
            //        _database.Query(CompiledQueries.TabQueries.GetTabsByUserId, userGuid)
            //        .ToList());
        }

        public List<PageEntity> GetLockedTabsOfUser(int userid, int isDownForMaintenenceMode)
        {
            if (isDownForMaintenenceMode == 1)
            {
                return this.GetTabsOfUser(userid).Where(Tab => Tab.ISLOCKED == 1 && Tab.ISDOWNFORMAINTENANCE == isDownForMaintenenceMode).ToList();
            }
            else
            {
                return this.GetTabsOfUser(userid).Where(Tab => Tab.ISLOCKED == 1).ToList();
            }

            //return isDownForMaintenenceMode ?
            //    this.GetTabsOfUser(userid).Where(Tab => Tab.ISLOCKED==1 && Tab.ISDOWNFORMAINTENANCE == isDownForMaintenenceMode).ToList()
            //    : this.GetTabsOfUser(userid).Where(Tab => Tab.ISLOCKED==1).ToList();
        }

        // TODO: Remove this
        public List<PageEntity> GetLockedTabsOfUserByMaintenenceMode(int userid, int isInMaintenenceMode)
        {
            return GetTabsOfUser(userid).Where(Tab => Tab.ISDOWNFORMAINTENANCE == isInMaintenenceMode && Tab.ISLOCKED == 1).ToList();
            //_database.GetList<Tab, Guid, bool>(userGuid, isInMaintenenceMode, CompiledQueries.TabQueries.GetLockedTabs_ByUserId_DownForMaintenence);
        }

        public List<PageEntity> GetMaintenenceTabsOfUser(int userid)
        {
            return GetTabsOfUser(userid).Where(Tab => Tab.ISDOWNFORMAINTENANCE == 1).ToList();
            //_database.GetList<Tab, Guid>(userGuid, CompiledQueries.TabQueries.GetTabsWhichIsDownForMaintenanceByUserId);
        }

        public PageEntity Insert(PageEntity Tab)
        {
            //var user = Tab.AspNetUser;
            //Tab.AspNetUser = null;
            //var newTab = new Tab();// _database.Insert<AspNetUser, Tab>(
            //user,
            //(u, p) => p.AspNetUser = u,
            //Tab);
            //Tab.AspNetUser = user;
            _pageDAO.Add(Tab);
            RemoveUserTabsCollectionByUserID(Tab.USERID.Value);
            return Tab;
        }

        public void Update(PageEntity Tab)
        {
            _pageDAO.Update(Tab);
            RemoveTabIdDependentItems(Tab.ID.Value);
            //_database.Update<Tab>(Tab);
        }

        public void UpdateList(IEnumerable<PageEntity> Tabs)
        {
            Tabs.Each(tab => this.Update(tab));

            //Tabs.Each(Tab => RemoveTabIdDependentItems(Tab.ID.Value));
            //_database.UpdateList<Tab>(Tabs);
        }

        public PageEntity GetFirstTabOfUser(int userid)
        {
            return GetTabsOfUser(userid).First();
        }

        public PageEntity GetOverridableStartTabOfUser(int userid)
        {
            return GetTabsOfUser(userid).Where(Tab => Tab.SERVEASSTARTPAGEAFTERLOGIN == 1)
                .FirstOrDefault();
        }

        #endregion Methods

        #region IDisposable Members

        public void Dispose()
        {
            //_database.Dispose();
        }

        #endregion


        public List<PageEntity> GetLockedTabsOfUser(int userid, bool isDownForMaintenenceMode)
        {
            throw new NotImplementedException();
        }

        public List<PageEntity> GetLockedTabsOfUserByMaintenenceMode(int userid, bool isInMaintenenceMode)
        {
            throw new NotImplementedException();
        }
    }
}