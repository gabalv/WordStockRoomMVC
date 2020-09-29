using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Data
{
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LanguageFamily { get; set; }

        public virtual ICollection<Word> Words { get; set; } = new List<Word>();
    }
}
