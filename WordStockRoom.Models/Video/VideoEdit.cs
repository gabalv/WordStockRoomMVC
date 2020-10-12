using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class VideoEdit
    {
        [Required]
        public int VideoId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
