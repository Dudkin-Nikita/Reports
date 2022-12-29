using System.ComponentModel.DataAnnotations;

namespace Reports.Domain.Entities
{
    public class Item : EntityBase
    {
        [Display(Name = "Название товара")]
        public string? Name { get; set; }

        [Display(Name = "Количество товара")]
        public int? Amount { get; set; }
        public Report? Report { get; set; }
    }
}
