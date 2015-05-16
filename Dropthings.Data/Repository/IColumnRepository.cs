using System;
namespace Dropthings.Data.Repository
{
    public interface IColumnRepository : IDisposable
    {
        void Delete(Dropthings.Data.ColumnEntity column);
        Dropthings.Data.ColumnEntity GetColumnById(int id);
        Dropthings.Data.ColumnEntity GetColumnByTabId_ColumnNo(int TabId, int columnNo);
        System.Collections.Generic.List<Dropthings.Data.ColumnEntity> GetColumnsByTabId(int TabId);
        Dropthings.Data.ColumnEntity Insert(Dropthings.Data.ColumnEntity column);
        void Update(Dropthings.Data.ColumnEntity column);
        void UpdateList(System.Collections.Generic.List<Dropthings.Data.ColumnEntity> columns);
    }
}
