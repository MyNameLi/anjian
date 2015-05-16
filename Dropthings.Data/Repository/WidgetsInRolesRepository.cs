namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OmarALZabir.AspectF;
    using Dropthings.Util;
    using System.Data.SqlClient;
    

    public class WidgetsInRolesRepository : Dropthings.Data.Repository.IWidgetsInRolesRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly WidgetsInRolesEntity.WidgetsInRolesDAO _widgetsInRolesDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public WidgetsInRolesRepository(ICache cacheResolver)
        {
            //this._database = database;
            _widgetsInRolesDAO = new WidgetsInRolesEntity.WidgetsInRolesDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        public void Delete(WidgetsInRolesEntity wr)
        {
            _cacheResolver.Remove(CacheKeys.WidgetsInRolesKeys.WidgetsInRolesByWidgetId(wr.WIDGETID.Value));
            _widgetsInRolesDAO.Delete(wr);
            //_database.Delete<WidgetsInRoles>(wr);
        }

        public void Delete(int roleid)
        {
            _widgetsInRolesDAO.DeleteByRoleId(roleid);
            //_database.Delete<WidgetsInRoles>(wr);
        }

        public void Add(WidgetsInRolesEntity entity)
        {
            _widgetsInRolesDAO.Add(entity);
        }

        public List<WidgetsInRolesEntity> GetWidgetsInRolesByWidgetId(int widgetId)
        {
            SqlParameter[] parameters = { new SqlParameter("@WIDGETID", widgetId) };

            return AspectF.Define
                .Cache<List<WidgetsInRolesEntity>>(_cacheResolver, CacheKeys.WidgetsInRolesKeys.WidgetsInRolesByWidgetId(widgetId))
                .Return<List<WidgetsInRolesEntity>>(() =>
                    _widgetsInRolesDAO.Find("WIDGETID=@WIDGETID", parameters));

            //return AspectF.Define
            //    .Cache<List<WidgetsInRoles>>(_cacheResolver, CacheKeys.WidgetsInRolesKeys.WidgetsInRolesByWidgetId(widgetId))
            //    .Return<List<WidgetsInRoles>>(() =>
            //        _database.Query(CompiledQueries.WidgetQueries.GetWidgetsInRoleByWidgetId, widgetId)
            //        .ToList());
        }

        //public List<WidgetsInRoles> GetWidgetsInRolessByRoleName(int widgetId, string roleName)
        //{
        //    return _database.GetList<WidgetsInRoles, int, string>(widgetId, roleName, CompiledQueries.WidgetQueries.GetWidgetsInRolessByRoleName);
        //}

        public WidgetsInRolesEntity Insert(WidgetsInRolesEntity wir)
        {
            //var widget = wir.Widget;
            //var role = wir.AspNetRole;

            //wir.Widget = null;
            //wir.AspNetRole = null;

            ////_database.Insert<Widget, AspNetRole, WidgetsInRoles>(widget, role, 
            ////    (w, wr) => wr.Widget = w,
            ////    (r, wr) => wr.AspNetRole = role,
            ////    wir);

            //wir.Widget = widget;
            //wir.AspNetRole = role;
            _widgetsInRolesDAO.Add(wir);
            _cacheResolver.Remove(CacheKeys.WidgetsInRolesKeys.WidgetsInRolesByWidgetId(wir.WIDGETID.Value));
            return wir;
        }

        public void Update(WidgetsInRolesEntity wr)
        {
            _widgetsInRolesDAO.Update(wr);
            _cacheResolver.Remove(CacheKeys.WidgetsInRolesKeys.WidgetsInRolesByWidgetId(wr.WIDGETID.Value));
            //_database.Update<WidgetsInRoles>(wr);
        }

        public IList<WidgetsInRolesEntity> GetWidGetListByRoleId(int roleid) {
            string strWher = " ROLEID=" + roleid.ToString();
            return this._widgetsInRolesDAO.Find(strWher, null);
        }



        #endregion Methods

        #region IDisposable Members

        public void Dispose()
        {
            //_database.Dispose();
        }

        #endregion
    }
}