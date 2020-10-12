using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class LanguageEdit
    {
        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name="Language Family")]
        [Required]
        public string LanguageFamily { get; set; }
    }
}
