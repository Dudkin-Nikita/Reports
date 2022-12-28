using System.ComponentModel.DataAnnotations;

namespace Reports.Domain.Entities
{
    public class Report : EntityBase
    {
        [Display(Name = "Начало периода")]
        public DateOnly StartDate { get; set; }

        [Display(Name = "Окончание периода")]
        public DateOnly EndDate { get; set;}

        [Display(Name = "Адрес")]
        public string Address { get; set;} = string.Empty;
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
