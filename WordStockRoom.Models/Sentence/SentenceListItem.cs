using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class SentenceListItem
    {
        [Required]
        public int SentenceId { get; set; }

        [Required]
        public string SentenceContent { get; set; }

        [Required]
        public string SentenceTranslation { get; set; }
    }
}
