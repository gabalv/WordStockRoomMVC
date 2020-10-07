using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStockRoom.Data;
using WordStockRoom.Models;

namespace WordStockRoom.Services
{
    public class WordService
    {
        private readonly Guid _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public WordService(Guid userId)
        {
            _userId = userId;
        }

        // Create
        public bool AddWord(WordCreate model)
        {
            var entity = new Word()
            {
                UserId = _userId,
                WordName = model.WordName,
                Translation = model.Translation,
                PartOfSpeech = ConvertFromStringToEnum(model.PartOfSpeech),
                Language = _context.Languages.Single(e => e.UserId == _userId && e.Name == model.Language)
            };

            _context.Words.Add(entity);
            return _context.SaveChanges() == 1;
        }

        // Get all
        public IEnumerable<WordListItem> GetWordsByLanguage(int id)
        {
            var query =
                _context
                .Words
                .Where(e => e.UserId == _userId && e.LanguageId == id)
                .Select(e => new WordListItem
                {
                    WordName = e.WordName,
                    Language = e.Language.Name,
                    Translation = e.Translation
                });

            return query.ToArray();
        }

        // Get by Id
        public WordDetail GetWordById(int id)
        {
            var entity =
                _context
                .Words
                .Single(e => e.WordId == id && e.UserId == _userId);

            return new WordDetail
            {
                WordName = entity.WordName,
                Language = entity.Language.Name,
                Translation = entity.Translation,
                PartOfSpeech = Convert.ToString(entity.PartOfSpeech),
                Sentences = ConvertFromSentencesToStrings(entity.Sentences),
                Videos = ConvertFromVideosToDictionary(entity.Videos)
            };
        }

        // Update
        public bool UpdateWord(WordEdit model)
        {
            var entity =
                _context
                .Words
                .Single(e => e.WordId == model.WordId && e.UserId == _userId);

            entity.WordName = model.WordName;
            entity.Translation = model.Translation;
            entity.PartOfSpeech = ConvertFromStringToEnum(model.PartOfSpeech);
            entity.Language = _context.Languages.Single(e => e.UserId == _userId && e.Name == model.Language);

            return _context.SaveChanges() == 1;
        }

        // Delete
        public bool DeleteWord(int id)
        {
            var entity =
                _context
                .Words
                .Single(e => e.UserId == _userId && e.WordId == id);

            _context.Words.Remove(entity);

            return _context.SaveChanges() == 1;
        }


        // helper
        public PartOfSpeech ConvertFromStringToEnum(string input)
        {
            switch(input)
            {
                case "noun":
                    return PartOfSpeech.noun;
                case "pronoun":
                    return PartOfSpeech.pronoun;
                case "verb":
                    return PartOfSpeech.verb;
                case "adjective":
                    return PartOfSpeech.adjective;
                case "adverb":
                    return PartOfSpeech.adverb;
                case "preposition":
                    return PartOfSpeech.preposition;
                case "postposition":
                    return PartOfSpeech.postposition;
                case "conjunction":
                    return PartOfSpeech.conjunction;
                case "interjection":
                    return PartOfSpeech.interjection;
                case "determiner":
                    return PartOfSpeech.determiner;
                default:
                    return PartOfSpeech.undefined;
            }
        }

        // helper
        public List<string> ConvertFromSentencesToStrings(ICollection<Sentence> sentences)
        {
            List<string> result = new List<string>();

            foreach (Sentence item in sentences)
            {
                result.Add(item.SentenceContent);
            }

            return result;
        }

        // helper
        public Dictionary<int, string> ConvertFromVideosToDictionary(ICollection<Video> videos)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            foreach (Video item in videos)
            {
                result.Add(item.VideoId, item.Description);
            }

            return result;
        }

        // helper
        public string ConvertFromEnumToString(PartOfSpeech input)
        {
            return Convert.ToString(input);
        }
    }
}
