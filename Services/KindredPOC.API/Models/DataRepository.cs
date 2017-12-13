using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KindredPOC.API.Models
{
    public class DataRepository : IDataRepository
    {
        private DataContext db = null;

        public DataRepository()
        {
            db = new DataContext();
        }

        public DataRepository(string Con)
        {
            db = new DataContext(Con);
        }

        public Item CreateItem(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            db.Items.Add(item);
            db.SaveChanges();
            return item;
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            db.Items.Add(item);
            await db.SaveChangesAsync();
            return item;
        }

        public Item UpdateItem(Item item)
        {
            Models.Item existitem = db.Items.Find(item.Id);
            if (existitem == null) throw new ArgumentException("Id not found");
            existitem.Text = item.Text;
            existitem.Description = item.Description;
            db.SaveChanges();
            return existitem;
        }

        public Task<Item> UpdateItemAsync(Item item)
        {
            return Task.Run(async () =>
           {
               Models.Item existitem = await db.Items.FindAsync(item.Id);
               if (existitem == null) throw new ArgumentException("Id not found");
               existitem.Text = item.Text;
               existitem.Description = item.Description;
               await db.SaveChangesAsync();
               return existitem;
           });
        }

        public bool DeleteItem(string itemId)
        {
            var item = db.Items.Find(itemId);
            if (item == null) throw new ArgumentException("Id not found");
            db.Items.Remove(item);
            int x = db.SaveChanges();
            return x > 0;
        }

        public IEnumerable<Item> GetItems(int take = 0, int skip = 0)
        {
            if (take > 0)
            {
                return db.Items.OrderBy(s=>s.Id).Skip(skip).Take(take);
            }
            else
            {
                return db.Items;
            }
        }

        public Task<bool> DeleteItemAsync(string itemId)
        {
            return Task.Run(async () =>
            {
                var item = await db.Items.FindAsync(itemId);
                if (item == null) throw new ArgumentException("Id not found");
                db.Items.Remove(item);
                int x = await db.SaveChangesAsync();
                return x > 0;
            });
        }

        public Item GetItem(string Id)
        {
            return db.Items.Find(Id);
        }

        public Task<Item> GetItemAsync(string Id)
        {
            return db.Items.FindAsync(Id);
        }
    }
}