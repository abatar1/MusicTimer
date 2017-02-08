using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicTimer.Domain;
using SQLite;
using Xamarin.Forms;

namespace MusicTimer.Infrastructure
{
    public class Database<TData> where TData : class, IData, new()
    {
        private readonly SQLiteAsyncConnection _connection;

        public Database(string dbname)
        {
            var path = DependencyService.Get<IFileHelper>().GetLocalFilePath(dbname);
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<TData>().Wait();
        }

        private Task<int> InsertUpdateData(TData data)
        {
            return _connection.UpdateAsync(data);
        }

        public Task<List<TData>> GetItemsAsync()
        {
            return _connection.Table<TData>().ToListAsync();
        }

        public Task<TData> GetItemAsync(int id)
        {
            return _connection.Table<TData>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteItemAsync(TData item)
        {
            return _connection.DeleteAsync(item);
        }
    }
}
