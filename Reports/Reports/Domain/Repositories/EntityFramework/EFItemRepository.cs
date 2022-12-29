using Microsoft.EntityFrameworkCore;
using Reports.Domain.Entities;
using Reports.Domain.Repositories.Abstract;

namespace Reports.Domain.Repositories.EntityFramework
{
    public class EFItemRepository : IRepository<Item>
    {
        private readonly AppDbContext context;
        public EFItemRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Item> GetEntities()
        {
            return context.Items;
        }

        public Item? GetEntityById(int id)
        {
            return context.Items.FirstOrDefault(x => x.Id == id);
        }

        public void AddEntity(Item entity)
        {
            context.Items.Add(entity);
            context.SaveChanges();
        }

        public void UpdateEntity(Item entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            Item? entity = context.Items.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                context.Items.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
