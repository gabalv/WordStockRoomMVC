using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class WordListItem
    {
        public int WordId { get; set; }

        [Display(Name = "Word")]
        public string WordName { get; set; }

        public int LanguageId { get; set; }
        public string Language { get; set; }

        public string Translation { get; set; }
    }
}
