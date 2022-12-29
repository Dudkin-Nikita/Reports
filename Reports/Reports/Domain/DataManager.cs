using Reports.Domain.Entities;
using Reports.Domain.Repositories.Abstract;

namespace Reports.Domain
{
    public class DataManager
    {
        public IRepository<Item> Items { get; set; }
        public IRepository<Report> Reports { get; set; }
        public DataManager(IRepository<Item> itemsRepository, IRepository<Report> reportsRepository)
        {
            Items = itemsRepository;
            Reports = reportsRepository;
        }
    }
}
