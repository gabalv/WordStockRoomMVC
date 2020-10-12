using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Data
{
    public class Sentence
    {
        [Key]
        public int SentenceId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string SentenceContent { get; set; }

        [Required]
        public string SentenceTranslation { get; set; }

        [Required]
        [ForeignKey(nameof(Word))]
        public int WordId { get; set; }
        public virtual Word Word { get; set; }
    }
}
