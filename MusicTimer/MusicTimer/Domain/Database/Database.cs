using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace MusicTimer.Domain.Database
{
    public abstract class Database<TData> where TData : class, new()
    {
        public SQLiteAsyncConnection Connection { get; }

        protected Database(string dbname)
        {
            var path = DependencyService.Get<IFileHelper>().GetLocalFilePath(dbname);
            Connection = new SQLiteAsyncConnection(path);
            Connection.CreateTableAsync<TData>().Wait();
        }

        public Task<int> InsertUpdateData(TData data)
        {
            return Connection.UpdateAsync(data);
        }

        public Task<List<TData>> GetItemsAsync()
        {
            return Connection.Table<TData>().ToListAsync();
        }     

        public Task<int> DeleteItemAsync(TData item)
        {
            return Connection.DeleteAsync(item);
        }
    }
}
