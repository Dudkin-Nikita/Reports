using System.ComponentModel.DataAnnotations;

namespace Reports.Domain.Entities
{
    public class Report : EntityBase
    {
        [Display(Name = "Начало периода")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Окончание периода")]
        public DateTime EndDate { get; set;}

        [Display(Name = "Адрес")]
        public string Address { get; set;} = string.Empty;

        [Display(Name = "Количество позиций на складе")]
        public int Count { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}