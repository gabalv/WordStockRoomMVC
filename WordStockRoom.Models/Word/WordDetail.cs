using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStockRoom.Data;

namespace WordStockRoom.Models
{
    public class WordDetail
    {
        public int WordId { get; set; }

        [Display(Name = "Word")]
        public string WordName { get; set; }

        public string Language { get; set; }

        public string Translation { get; set; }

        [Display(Name = "Part of Speech")]
        public PartOfSpeech PartOfSpeech { get; set; }

        public Dictionary<int, string> Sentences { get; set; }

        public Dictionary<int, string> Videos { get; set; }
    }
}
