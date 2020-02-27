using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace iBills.Services
{
    public interface IDataStore<T>
    {
        Task<int> SaveItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task<List<T>> GetItemsAsync();
        Task<List<T>> GetItemsNotDoneAsync();
    }
}
