using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class SentenceCreate
    {
        [Required]
        [Display(Name = "Sentence")]
        public string SentenceContent { get; set; }

        [Required]
        [Display(Name = "Translation")]
        public string SentenceTranslation { get; set; }
    }
}
