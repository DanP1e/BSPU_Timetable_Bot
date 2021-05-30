using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableBot.DATA.Interfaces
{
    public interface IDataTable<T>
    {
        string Name { get; }
        
        void AddItem(T dataItem);
        void RemoveItem(T dataItem);
        T GetItem(int index);
        T GetLastItem();
        bool ItemBeingInTable(T dataItem);
        bool ItemBeingInTable(T dataItem, out int itemIndex);
        int Length();
    }
}
