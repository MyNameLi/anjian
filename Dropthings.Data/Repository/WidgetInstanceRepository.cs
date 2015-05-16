namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OmarALZabir.AspectF;
    using Dropthings.Util;
    using System.Data.SqlClient;
    

    public class WidgetInstanceRepository : Dropthings.Data.Repository.IWidgetInstanceRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly WidgetInstanceEntity.WIDGETINSTANCEDAO _widgetInstanceDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public WidgetInstanceRepository( ICache cacheResolver)
        {
            //this._database = database;
            this._widgetInstanceDAO = new WidgetInstanceEntity.WIDGETINSTANCEDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        private void RemoveWidgetInstanceCacheEntries(int widgetInstanceId)
        {
            CacheKeys.WidgetInstanceKeys.AllWidgetInstanceIdBasedKeys(widgetInstanceId).Each(key => _cacheResolver.Remove(key));
        }

        private void RemoveWidgetZoneCacheEntries(int widgetZoneId)
        {
            CacheKeys.WidgetZoneKeys.AllWidgetZoneIdBasedKeys(widgetZoneId).Each(key => _cacheResolver.Remove(key));
        }

        public void Delete(int id)
        {
            //RemoveWidgetInstanceTabCacheEntries(id);//new added            
            RemoveWidgetInstanceCacheEntries(id);
            var widgetInstance = this.GetWidgetInstanceById(id);
            if (null != widgetInstance)
            {
                this.Delete(widgetInstance);
            }
        }

        public void Delete(WidgetInstanceEntity wi)
        {
            //RemoveWidgetInstanceTabCacheEntries(wi.Id);//new added
            RemoveWidgetInstanceCacheEntries(wi.ID.Value);
            RemoveWidgetZoneCacheEntries(wi.WIDGETZONEID.Value);
            _widgetInstanceDAO.Delete(wi);
            //_database.Delete<WidgetInstance>(wi);
        }

        //private void RemoveWidgetInstanceTabCacheEntries(int id)//new added
        //{
        //    var tab = AspectF.Define
        //         .Cache<Tab>(_cacheResolver, CacheKeys.TabKeys.TabOfWidgetInstance(id)[0])
        //         .Return<Tab>(() => _database.Query(CompiledQueries.TabQueries.GetTabByWidgetInstancesId, id).First());

        //    if (null != tab)
        //    {
        //        CacheKeys.WidgetKeys.InstanceWidgetTab(tab.ID).Each((key) =>
        //        {
        //            List<Widget> wiList = _cacheResolver.Get(key) as List<Widget>;
        //            wiList.Each((wi) =>
        //            {
        //                if (wi == id)
        //                {
        //                    wiList.Remove(wi);
        //                }
        //            });
        //            _cacheResolver.Set(key, wiList);
        //        });
        //    }
        //}

        public WidgetInstanceEntity GetWidgetInstanceById(int id)
        {
            return AspectF.Define
                .Cache<WidgetInstanceEntity>(_cacheResolver, CacheKeys.WidgetInstanceKeys.WidgetInstance(id))
                .Return<WidgetInstanceEntity>(() =>
                    _widgetInstanceDAO.FindById(id));
            //return AspectF.Define
            //    .Cache<WidgetInstance>(_cacheResolver, CacheKeys.WidgetInstanceKeys.WidgetInstance(id))
            //    .Return<WidgetInstance>(() =>
            //        _database.Query(CompiledQueries.WidgetQueries.GetWidgetInstanceById, id)
            //            .First());
        }

        public List<WidgetInstanceEntity> GetWidgetInstanceOnWidgetZoneAfterPosition(int widgetZoneId, int position)
        {
            return this.GetWidgetInstancesByWidgetZoneIdWithWidget(widgetZoneId)
                .Where(wi => wi.ORDERNO.Value > position).ToList();
        }

        public List<WidgetInstanceEntity> GetWidgetInstanceOnWidgetZoneFromPosition(int widgetZoneId, int position)
        {
            return this.GetWidgetInstancesByWidgetZoneIdWithWidget(widgetZoneId)
                .Where(wi => wi.ORDERNO.Value >= position).ToList();
        }

        public string GetWidgetInstanceOwnerName(int widgetInstanceId)
        {
            return AspectF.Define
                .Cache<string>(_cacheResolver, CacheKeys.WidgetInstanceKeys.WidgetInstanceOwnerName(widgetInstanceId))
                .Return<string>(() =>
                    _widgetInstanceDAO.GetWidgetInstanceOwnerName(widgetInstanceId));

            //return AspectF.Define
            //    .Cache<string>(_cacheResolver, CacheKeys.WidgetInstanceKeys.WidgetInstanceOwnerName(widgetInstanceId))
            //    .Return<string>(() =>
            //        _database.Query(CompiledQueries.WidgetQueries.GetWidgetInstanceOwnerName, widgetInstanceId)
            //        .First());
        }

        //public List<int> GetWidgetInstancesByRole(int widgetInstanceId, Guid roleId)
        //{            
        //    return _database.GetList<int, int, Guid>(widgetInstanceId, roleId, CompiledQueries.WidgetQueries.GetWidgetInstancesByRole);
        //}

        //public List<WidgetInstance> GetWidgetInstancesByWidgetAndRole(int widgetId, Guid roleId)
        //{
        //    return _database.GetList<WidgetInstance, int, Guid>(widgetId, roleId, CompiledQueries.WidgetQueries.GetAllWidgetInstancesByWidgetAndRole);
        //}

        //public List<WidgetInstance> GetWidgetInstancesByWidgetZoneId(int widgetZoneId)
        //{
        //    return AspectF.Define
        //        .CacheList<WidgetInstance, List<WidgetInstance>>(_cacheResolver, CacheKeys.WidgetZoneKeys.WidgetInstancesInWidgetZone(widgetZoneId),
        //        wi => CacheKeys.WidgetInstanceKeys.WidgetInstance(wi.Id))
        //        .Return<List<WidgetInstance>>(() =>
        //            _database.GetList<WidgetInstance, int>(widgetZoneId, CompiledQueries.WidgetQueries.GetWidgetInstancesByWidgetZoneId));
        //}

        public List<WidgetInstanceEntity> GetWidgetInstancesByWidgetZoneIdWithWidget(int widgetZoneId)
        {
            SqlParameter[] parameters = { new SqlParameter("@WIDGETZONEID", widgetZoneId) };
            //return _widgetInstanceDAO.Find("WIDGETZONEID=@WIDGETZONEID ORDER BY ORDERNO ", parameters);
            return _widgetInstanceDAO.Find("WIDGETZONEID=@WIDGETZONEID ", parameters);

            //return AspectF.Define
            //    .CacheList<WidgetInstanceEntity, List<WidgetInstanceEntity>>(_cacheResolver, CacheKeys.WidgetZoneKeys.WidgetInstancesInWidgetZoneWithWidget(widgetZoneId),
            //        wi => CacheKeys.WidgetInstanceKeys.WidgetInstanceWithWidget(wi.ID.Value))
            //    .Return<List<WidgetInstanceEntity>>(() =>
            //        _widgetInstanceDAO.Find("WIDGETZONEID=@WIDGETZONEID", parameters));

            //return AspectF.Define
            //    .CacheList<WidgetInstance, List<WidgetInstance>>(_cacheResolver, CacheKeys.WidgetZoneKeys.WidgetInstancesInWidgetZoneWithWidget(widgetZoneId),
            //        wi => CacheKeys.WidgetInstanceKeys.WidgetInstanceWithWidget(wi.Id))
            //    .Return<List<WidgetInstance>>(() =>
            //        _database.Query(CompiledQueries.WidgetQueries.GetWidgetInstancesByWidgetZoneId,
            //            widgetZoneId)
            //       .ToList());
        }

        public WidgetInstanceEntity Insert(WidgetInstanceEntity wi)
        {
            //var zone = wi.WidgetZone;
            //var widget = wi.Widget;

            //wi.WidgetZone = null;
            //wi.Widget = null;

            //var widgetInstance = new WidgetInstanceEntity();// _database.Insert<WidgetZone, Widget, WidgetInstance>(
            //    //zone, widget,
            //    //(z, me) => me.WidgetZone = z,
            //    //(w, me) => me.Widget = w,
            //    //wi);

            //wi.WidgetZone = zone;
            //wi.Widget = widget;
            _widgetInstanceDAO.Add(wi);
            RemoveWidgetZoneCacheEntries(wi.WIDGETZONEID.Value);
            return wi;
        }

        public void InsertList(IEnumerable<WidgetInstanceEntity> wis)
        {
            wis.Each(wi =>
            {
                _widgetInstanceDAO.Add(wi);
                RemoveWidgetZoneCacheEntries(wi.WIDGETZONEID.Value);
            });
            //wis.Each(wi =>
            //    {
            //        _database.Insert<WidgetZone, Widget, WidgetInstance>(
            //            wi.WidgetZone,
            //            wi.Widget,
            //            (zone, me) => zone.WidgetInstance.Add(me),
            //            (w, me) => w.WidgetInstance.Add(me),
            //            wi);
            //        RemoveWidgetZoneCacheEntries(wi.WidgetZone.ID);
            //    });
        }

        public void Update(WidgetInstanceEntity wi)
        {
            RemoveWidgetInstanceCacheEntries(wi.ID.Value);
            _widgetInstanceDAO.Update(wi);
            //_database.Update<WidgetInstance>(wi);
        }

        public void UpdateList(IEnumerable<WidgetInstanceEntity> widgetInstances)
        {
            widgetInstances.Each(wi =>
                {
                    RemoveWidgetInstanceCacheEntries(wi.ID.Value);
                    RemoveWidgetZoneCacheEntries(wi.WIDGETZONEID.Value);
                    this.Update(wi);
                });

            //_database.UpdateList<WidgetInstance>(widgetInstances);
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