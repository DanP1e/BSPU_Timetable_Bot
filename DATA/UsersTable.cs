using System;
using System.Collections.Generic;
using System.Text;
using TimetableBot.DATA.Interfaces;
using System.Linq;

namespace TimetableBot.DATA
{
    public class UsersTable : IDataTable<ITelegramUser>
    {

        private List<ITelegramUser> connectedUsers = new List<ITelegramUser>();
        public List<ITelegramUser> ConnectedUsers { get => connectedUsers; }
        private string name = "";
        public string Name { get => name; }
        public UsersTable(string tableName)
        {
            name = tableName;
        }
        public UsersTable(string tableName, List<ITelegramUser> connectedUsers)
        {
            name = tableName;
            this.connectedUsers = connectedUsers;
        }      
        public int Length() => connectedUsers.Count;
        public void AddItem(ITelegramUser dataItem) => connectedUsers.Add(dataItem);         
        public void RemoveItem(ITelegramUser dataItem) => connectedUsers.Remove(dataItem);
        public ITelegramUser GetItem(int index) => connectedUsers[index];
        public ITelegramUser GetLastItem() => connectedUsers.Last();
        public bool ItemBeingInTable(ITelegramUser user) => connectedUsers.Any((x) => x.ChatID == user.ChatID && x.Nick == user.Nick);

        public bool ItemBeingInTable(ITelegramUser user, out int itemIndex)
        {
            int id = 0;
            bool _result = connectedUsers.Any((x) => {
                id++;
                return x.ChatID == user.ChatID && x.Nick == user.Nick; 
            });
            itemIndex = id;
            return _result;
        }
    }
}
