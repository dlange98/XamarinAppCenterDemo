using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindredPOC.API.Models
{
    public class MockDataRepo : IDataRepository
    {
        public Item CreateItem(Item item)
        {
            item.Id = Guid.Empty.ToString();
            return item;
        }

        public Task<Item> CreateItemAsync(Item item)
        {
            return Task<Item>.Factory.StartNew(() =>
            {
                item.Id = Guid.Empty.ToString();
                return item;
            });
        }

        public bool DeleteItem(string itemId)
        {
            return true;
        }

        public Task<bool> DeleteItemAsync(string itemId)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                return true;
            });
        }

        public Item GetItem(string Id)
        {
            var item = new Item { Id = Id, Text = "NULL", Description = "NULL" };
            return item;
        }

        public Task<Item> GetItemAsync(string Id)
        {
            return Task<Item>.Factory.StartNew(() =>
            {
                var item = new Item { Id = Id, Text = "NULL", Description = "NULL" };
                return item;
            });
        }

        public IEnumerable<Item> GetItems(int take = 0, int skip = 0)
        {
            var items = new List<Item> { new Item {Id = Guid.Empty.ToString(), Text = "NULL 1", Description = "NULL 1" }, new Item { Id = Guid.Empty.ToString(), Text = "NULL 2", Description = "NULL 2" } };
            return items.AsEnumerable();
        }

        public Item UpdateItem(Item item)
        {
            return item;
        }

        public Task<Item> UpdateItemAsync(Item item)
        {
            return Task<Item>.Factory.StartNew(() =>
            {
                return item;
            });
        }
    }
}
