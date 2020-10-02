using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStockRoom.Data
{
    public enum PartOfSpeech { undefined, noun, pronoun, verb, adjective, adverb, preposition, postposition, conjunction, interjection, determiner }

    public struct ConvertEnum
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public class Word
    {
        [Key]
        public int WordId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string WordName { get; set; }

        [Required]
        public string Translation { get; set; }

        [Required]
        public PartOfSpeech PartOfSpeech { get; set; }

        public virtual ICollection<Sentence> Sentences { get; set; } = new List<Sentence>();

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();


        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }
        [Required]
        public virtual Language Language { get; set; }
    }
}
