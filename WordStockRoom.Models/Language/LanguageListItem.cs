using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class LanguageListItem
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }

        [Display(Name="Language Family")]
        public string LanguageFamily { get; set; }

        [Display(Name="Word Count")]
        public int WordCount { get; set; }
    }
}
