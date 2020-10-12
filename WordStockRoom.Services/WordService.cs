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
        private readonly int _languageId;

        public WordService(Guid userId, int languageId)
        {
            _userId = userId;
            _languageId = languageId;
        }

        // Create
        public bool AddWord(WordCreate model)
        {
            var entity = new Word()
            {
                UserId = _userId,
                WordName = model.WordName,
                Translation = model.Translation,
                PartOfSpeech = model.PartOfSpeech,
                LanguageId = _languageId
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
                    WordId = e.WordId,
                    WordName = e.WordName,
                    LanguageId = e.LanguageId,
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
                WordId = entity.WordId,
                WordName = entity.WordName,
                Language = entity.Language.Name,
                Translation = entity.Translation,
                PartOfSpeech = entity.PartOfSpeech,
                Sentences = ConvertFromSentencesToDictionary(entity.Sentences),
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
            entity.PartOfSpeech = model.PartOfSpeech;

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
        public Dictionary<int, string> ConvertFromSentencesToDictionary(ICollection<Sentence> sentences)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            foreach (Sentence item in sentences)
            {
                result.Add(item.SentenceId, item.SentenceContent);
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
    }
}
