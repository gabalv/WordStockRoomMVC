using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStockRoom.Data;

namespace WordStockRoom.Models
{
    public class WordEdit
    {
        [Required]
        public int WordId { get; set; }

        [Required]
        [Display(Name = "Word Name")]
        public string WordName { get; set; }

        [Required]
        public string Translation { get; set; }

        [Required]
        [Display(Name = "Part of Speech")]
        public PartOfSpeech PartOfSpeech { get; set; }
    }
}
