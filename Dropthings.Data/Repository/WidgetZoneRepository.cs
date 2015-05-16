namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OmarALZabir.AspectF;
    using Dropthings.Util;

    public class WidgetZoneRepository : Dropthings.Data.Repository.IWidgetZoneRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly WidgetZoneEntity.WidgetZoneDAO _widgetZoneDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public WidgetZoneRepository(ICache cacheResolver)
        {
            _widgetZoneDAO = new WidgetZoneEntity.WidgetZoneDAO();
            //this._database = database;
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        public string GetWidgetZoneOwnerName(int widgetZoneId)
        {
            return _widgetZoneDAO.GetWidgetZoneOwnerName(widgetZoneId);
            //return _database.Query(
            //    CompiledQueries.WidgetQueries.GetWidgetZoneOwnerName, 
            //    widgetZoneId)
            //    .First();
        }

        public void Delete(WidgetZoneEntity widgetZone)
        {
            _cacheResolver.Remove(CacheKeys.WidgetZoneKeys.WidgetInstancesInWidgetZone(widgetZone.ID.Value));
            _widgetZoneDAO.Delete(widgetZone);
            //_database.Delete<WidgetZone>(widgetZone);
        }

        public WidgetZoneEntity GetWidgetZoneById(int widgetZoneId)
        {
            return AspectF.Define
                .Cache<WidgetZoneEntity>(_cacheResolver, CacheKeys.WidgetZoneKeys.WidgetZone(widgetZoneId))
                .Return<WidgetZoneEntity>(() =>
                    _widgetZoneDAO.FindById(widgetZoneId));

            //return AspectF.Define
            //    .Cache<WidgetZone>(_cacheResolver, CacheKeys.WidgetZoneKeys.WidgetZone(widgetZoneId))
            //    .Return<WidgetZone>(() =>
            //        _database.Query(
            //            CompiledQueries.WidgetQueries.GetWidgetZoneById, widgetZoneId)
            //            .First());
        }

        public WidgetZoneEntity GetWidgetZoneByTabId_ColumnNo(int TabId, int columnNo)
        {
            return AspectF.Define
                .Cache<WidgetZoneEntity>(_cacheResolver, CacheKeys.TabKeys.WidgetZoneByTabIdColumnNo(TabId, columnNo))
                .Return<WidgetZoneEntity>(() =>
                    _widgetZoneDAO.GetWidgetZoneByTabId_ColumnNo(TabId, columnNo)
                    );

            //return AspectF.Define
            //    .Cache<WidgetZone>(_cacheResolver, CacheKeys.TabKeys.WidgetZoneByTabIdColumnNo(TabId, columnNo))
            //    .Return<WidgetZone>(() =>
            //        _database.Query(
            //            CompiledQueries.WidgetQueries.GetWidgetZoneByTabId_ColumnNo, TabId, columnNo)
            //            .First()
            //        );
        }

        public WidgetZoneEntity Insert(WidgetZoneEntity zone)
        {
            _widgetZoneDAO.Add(zone);
            return zone;
            //return _database.Insert<WidgetZone>(zone);
        }

        public void Update(WidgetZoneEntity zone)
        {
            _widgetZoneDAO.Update(zone);
            //_database.Update<WidgetZone>(zone);
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