using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Data
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string URL { get; set; }


        [ForeignKey(nameof(Word))]
        public int WordId { get; set; }
        [Required]
        public virtual Word Word { get; set; }
    }
}
