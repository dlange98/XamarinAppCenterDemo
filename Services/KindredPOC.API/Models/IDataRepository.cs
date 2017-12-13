using System.Collections.Generic;
using System.Threading.Tasks;

namespace KindredPOC.API.Models
{
    public interface IDataRepository
    {
        Item CreateItem(Item item);
        Task<Item> CreateItemAsync(Item item);
        Item UpdateItem(Item item);
        Task<Item> UpdateItemAsync(Item item);
        IEnumerable<Item> GetItems(int take = 0, int skip = 0);
        Item GetItem(string Id);
        Task<Item> GetItemAsync(string Id);
        bool DeleteItem(string itemId);
        Task<bool> DeleteItemAsync(string itemId);

    }
}