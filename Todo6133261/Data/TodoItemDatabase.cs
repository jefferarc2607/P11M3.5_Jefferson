using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Todo6133261.Models;

namespace Todo6133261.Data
{
    public class TodoItemDatabase
    {
        SQLiteAsyncConnection Database;
        public TodoItemDatabase ()
        { 
        }
        async Task Init()
        {
            if (Database is not null)
                return;
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Models.TodoItem>();
        }

        public async Task<List<Models.TodoItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Models.TodoItem>().ToListAsync();
        }

        public async Task<Models.TodoItem> GetItemsNotDoneAsync(int id)
        {
            await Init();
            return await Database.Table<Models.TodoItem>().Where(t => t.Done).FirstOrDefaultAsync();
        }

        public async Task <TodoItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(TodoItem item)
        {
            await Init();
            if (item.ID != 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

    }
}