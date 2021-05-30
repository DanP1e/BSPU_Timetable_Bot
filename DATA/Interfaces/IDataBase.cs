using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableBot.DATA.Interfaces
{
    public interface IDataBase
    {
        IDataTable<T> GetTable<T>(int tableId);
        IDataTable<T> GetTable<T>(string tableName);

        void SetTable<T>(int tableId, IDataTable<T> dataTable);
        void SetTable<T>(string tableName, IDataTable<T> dataTable);
    }
}
