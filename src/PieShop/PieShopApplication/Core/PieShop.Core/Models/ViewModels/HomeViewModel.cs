using System.Collections.Generic;

namespace PieShop.Core.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
    }
}
