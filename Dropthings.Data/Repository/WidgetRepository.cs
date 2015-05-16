namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OmarALZabir.AspectF;
    using Dropthings.Util;
    using System.Data.Objects;
    using System.Data.EntityClient;

    public class WidgetRepository : Dropthings.Data.Repository.IWidgetRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly WidgetEntity.WIDGETDAO _widgetDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public WidgetRepository( ICache cacheResolver)
        {
            this._widgetDAO = new WidgetEntity.WIDGETDAO();
            //this._database = database;
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        private void FlushWidgetCache()
        {
            CacheKeys.WidgetKeys.AllWidgetsKeys().Each(key => _cacheResolver.Remove(key));
        }

        private void FlushWidgetCache(int widgetId)
        {
            CacheKeys.WidgetKeys.AllWidgetIdBasedKeys(widgetId).Each(key => _cacheResolver.Remove(key));
            FlushWidgetCache();
        }

        public void Delete(int widgetId)
        {
            FlushWidgetCache(widgetId);

            //var param = new EntityParameter("WidgetId", System.Data.DbType.Int32);
            //param.Value = widgetId;
            //_database.ExecuteFunction("DeleteWidgetCascade", param);
            //_database.Delete<Widget>(new Widget { ID = widgetId });

            _widgetDAO.DeleteInstanceByWidgetID(widgetId);
            _widgetDAO.Delete(widgetId);
        }


        public List<WidgetEntity> GetAllWidgets()
        {
            return AspectF.Define
                .CacheList<WidgetEntity, List<WidgetEntity>>(_cacheResolver, CacheKeys.WidgetKeys.AllWidgets(),
                w => CacheKeys.WidgetKeys.Widget(w.ID.Value))
                .Return<List<WidgetEntity>>(() =>
                    _widgetDAO.Find("", null));
            //return AspectF.Define
            //    .CacheList<Widget, List<Widget>>(_cacheResolver, CacheKeys.WidgetKeys.AllWidgets(),
            //    w => CacheKeys.WidgetKeys.Widget(w.ID))
            //    .Return<List<Widget>>(() =>
            //        _database.Query(CompiledQueries.WidgetQueries.GetAllWidgets)
            //        .ToList());
        }

        public List<WidgetEntity> GetInstanceWidgetsByTabId(int tabId)
        {
            return AspectF.Define
                //.CacheList<Widget, List<Widget>>(_cacheResolver, CacheKeys.WidgetKeys.InstanceWidgetTab(tabId)[0],
                //    w => CacheKeys.WidgetKeys.InstanceWidgetTab(w.ID)[0])
             .Return<List<WidgetEntity>>(() =>
                 _widgetDAO.GetInstanceWidgetsByTabId(tabId));
            //return AspectF.Define
            // //.CacheList<Widget, List<Widget>>(_cacheResolver, CacheKeys.WidgetKeys.InstanceWidgetTab(tabId)[0],
            // //    w => CacheKeys.WidgetKeys.InstanceWidgetTab(w.ID)[0])
            // .Return<List<Widget>>(() =>
            //     _database.Query(CompiledQueries.WidgetQueries.GetInstanceWidgetsByTabId,
            //         tabId)
            //    .ToList());
        }

        public List<WidgetEntity> GetAllWidgets(Enumerations.WidgetType widgetType)
        {
            return this.GetAllWidgets().Where(w => w.WIDGETTYPE.Value == (int)widgetType).ToList();
            //return AspectF.Define
            //    .CacheList<Widget, List<Widget>>(_cacheResolver, CacheKeys.WidgetKeys.WidgetsByType((int)widgetType),
            //    w => CacheKeys.WidgetKeys.Widget(w.ID))
            //    .Return<List<Widget>>(() =>
            //        _database.GetList<Widget, Enumerations.WidgetTypeEnum>(widgetType, CompiledQueries.WidgetQueries.GetAllWidgets_ByWidgetType));
        }

        //public List<Widget> GetDefaultWidgetsByRole(string userName, Enumerations.WidgetTypeEnum widgetType, bool isDefault)
        //{
        //    // TODO: Cache this call. But cache key is complicated, it needs to be based on user's Roles and Widget Type+IsDefault
        //    return _database.GetList<Widget, string, Enumerations.WidgetTypeEnum, bool>(
        //        userName, widgetType, isDefault, 
        //        CompiledQueries.WidgetQueries.GetDefaultWidgetsByRole)
        //        .Select(w => w.Detach()).ToList();
        //}

        public WidgetEntity GetWidgetById(int id)
        {
            return AspectF.Define
                .Cache<WidgetEntity>(_cacheResolver, CacheKeys.WidgetKeys.Widget(id))
                .Return<WidgetEntity>(() =>
                    _widgetDAO.FindById(id));
            //return AspectF.Define
            //    .Cache<Widget>(_cacheResolver, CacheKeys.WidgetKeys.Widget(id))
            //    .Return<Widget>(() =>
            //        _database.Query(CompiledQueries.WidgetQueries.GetWidgetById, id)
            //        .First());
        }

        public List<WidgetEntity> GetWidgetByIsDefault(bool isDefault)
        {
            return GetAllWidgets().Where(w => w.ISDEFAULT.Value == isDefault).ToList();
        }

        public WidgetEntity Insert(WidgetEntity w)
        {
            _widgetDAO.Add(w);// new WidgetEntity();// _database.Insert<Widget>(w);
            FlushWidgetCache(w.ID.Value);
            return w;
        }

        public void Update(WidgetEntity wi)
        {
            //_database.Update<Widget>(wi);
            _widgetDAO.Update(wi);
            FlushWidgetCache(wi.ID.Value);
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