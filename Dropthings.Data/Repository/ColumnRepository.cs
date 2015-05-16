namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OmarALZabir.AspectF;
    using Dropthings.Util;

    public class ColumnRepository : Dropthings.Data.Repository.IColumnRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly ICache _cacheResolver;
        private readonly ColumnEntity.ColumnDAO _columnDAO = new ColumnEntity.ColumnDAO();

        #endregion Fields

        #region Constructors

        public ColumnRepository(ICache cacheResolver)
        {
            //this._database = database;
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        private void RemoveColumnInTabCacheEntries(ColumnEntity column)
        {
            _cacheResolver.Remove(CacheKeys.TabKeys.ColumnsInTab(column.PAGEID.Value));
        }

        public void Delete(ColumnEntity column)
        {
            RemoveColumnInTabCacheEntries(column);
            _columnDAO.Delete(column.ID.Value);
            //_database.Delete<ColumnEntity>(column);
        }

        public ColumnEntity GetColumnById(int id)
        {
            return _columnDAO.FindById(id);
            //return _database.Query(
            //        CompiledQueries.TabQueries.GetColumnById, id)
            //    .First();
        }

        public ColumnEntity GetColumnByTabId_ColumnNo(int TabId, int columnNo)
        {
            return _columnDAO.GetColumnByTabId_ColumnNo(TabId, columnNo);

            //return this.GetColumnsByTabId(TabId).Where(Tab => Tab.ColumnNo == columnNo)
            //    .First();
        }

        public List<ColumnEntity> GetColumnsByTabId(int TabId)
        {
            return AspectF.Define
                .Cache<List<ColumnEntity>>(_cacheResolver, CacheKeys.TabKeys.ColumnsInTab(TabId))
                .Return<List<ColumnEntity>>(() =>
                    _columnDAO.GetColumnsByTabId(TabId));

            //return _columnDAO.GetColumnsByTabId(TabId);

            //return AspectF.Define
            //    .Cache<List<ColumnEntity>>(_cacheResolver, CacheKeys.TabKeys.ColumnsInTab(TabId))
            //    .Return<List<ColumnEntity>>(() =>
            //        _database.Query(
            //            CompiledQueries.TabQueries.GetColumnsByTabId, TabId)
            //            .ToList());
        }

        public ColumnEntity Insert(ColumnEntity column)
        {
            _columnDAO.Add(column);
            //_database.Insert<Tab, WidgetZone, ColumnEntity>(Tab, zone,
            //    (p, col) => col.Tab = Tab,
            //    (z, col) => col.WidgetZone = zone,
            //    column);

            RemoveColumnInTabCacheEntries(column);
            return column;

        }

        public void Update(ColumnEntity column)
        {
            _columnDAO.Update(column);
            RemoveColumnInTabCacheEntries(column);

            //_database.Update<ColumnEntity>(column);
        }

        public void UpdateList(List<ColumnEntity> columns)
        {
            _columnDAO.UpdateList(columns);
            columns.Each(column => RemoveColumnInTabCacheEntries(column));
            //_database.UpdateList<ColumnEntity>(columns);
        }

        #endregion Methods

        #region IDisposable Members

        public void Dispose()
        {
            // _database.Dispose();
        }

        #endregion
    }
}