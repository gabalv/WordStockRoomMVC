using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Models
{
    public class WordDetail
    {
        public string WordName { get; set; }
        public string Language { get; set; }
        public string Translation { get; set; }
        public string PartOfSpeech { get; set; }
        public List<string> Sentences { get; set; }
        public Dictionary<int, string> Videos { get; set; }
    }
}
