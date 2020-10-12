using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class VideoDetail
    {
        [Required]
        public int VideoId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public string Word { get; set; }

        [Required]
        public string Language { get; set; }
    }
}
