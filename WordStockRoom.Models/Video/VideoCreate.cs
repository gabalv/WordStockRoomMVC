using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class VideoCreate
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string URL { get; set; }
    }
}
