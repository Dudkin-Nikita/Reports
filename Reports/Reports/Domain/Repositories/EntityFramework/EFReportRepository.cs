using Microsoft.EntityFrameworkCore;
using Reports.Domain.Entities;
using Reports.Domain.Repositories.Abstract;

namespace Reports.Domain.Repositories.EntityFramework
{
    public class EFReportRepository : IRepository<Report>
    {
        private readonly AppDbContext context;
        public EFReportRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Report> GetEntities()
        {
            return context.Reports;
        }
        public Report? GetEntityById(int id)
        {
            Report? report = context.Reports.FirstOrDefault(x => x.Id == id);
            if (report != null)
            {
                context.Entry(report).Collection("Items").Load();
            }
            return report;
        }
        public void AddEntity(Report entity) 
        { 
            context.Reports.Add(entity);
            context.SaveChanges();
        }
        public void UpdateEntity(Report entity)
        {
            List<Item> items = context.Items.Where(x => x.Report == entity).ToList();
            foreach (Item item in items)
            {
                context.Items.Remove(item);
            }
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void DeleteEntity(int id)
        {
            Report? entity = context.Reports.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                context.Reports.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}