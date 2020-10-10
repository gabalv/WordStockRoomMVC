using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStockRoom.Data;
using WordStockRoom.Models;

namespace WordStockRoom.Services
{
    public class SentenceService
    {
        private readonly Guid _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly int _wordId;

        public SentenceService(Guid userId, int wordId)
        {
            _userId = userId;
            _wordId = wordId;
        }


        // Create
        public bool AddSentence(SentenceCreate model)
        {
            var entity = new Sentence()
            {
                UserId = _userId,
                SentenceContent = model.SentenceContent,
                SentenceTranslation = model.SentenceTranslation,
                WordId = _wordId
            };

            _context.Sentences.Add(entity);
            return _context.SaveChanges() == 1;
        }

        // Get All
        public IEnumerable<SentenceListItem> GetSentencesByWord()
        {
            var query =
                _context
                .Sentences
                .Where(e => e.UserId == _userId && e.WordId == _wordId)
                .Select(e => new SentenceListItem
                {
                    SentenceId = e.SentenceId,
                    SentenceContent = e.SentenceContent,
                    SentenceTranslation = e.SentenceTranslation
                });

            return query.ToArray();
        }

        // Get by Id
        public SentenceListItem GetSentenceById(int id)
        {
            var entity =
                _context
                .Sentences
                .Single(e => e.SentenceId == id && e.UserId == _userId);

            return new SentenceListItem
            {
                SentenceId = id,
                SentenceContent = entity.SentenceContent,
                SentenceTranslation = entity.SentenceTranslation
            };
        }

        // Update
        public bool UpdateSentence(SentenceEdit model)
        {
            var entity =
                _context
                .Sentences
                .Single(e => e.UserId == _userId && e.SentenceId == model.SentenceId);

            entity.SentenceContent = model.SentenceContent;
            entity.SentenceTranslation = model.SentenceTranslation;

            return _context.SaveChanges() == 1;
        }

        // Delete
        public bool DeleteSentence(int id)
        {
            var entity =
                _context
                .Sentences
                .Single(e => e.UserId == _userId && e.SentenceId == id);

            _context.Sentences.Remove(entity);

            return _context.SaveChanges() == 1;
        }
    }
}
