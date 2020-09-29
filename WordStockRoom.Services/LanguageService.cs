using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WordStockRoom.Data;
using WordStockRoom.Models;

namespace WordStockRoom.Services
{
    public class LanguageService
    {
        private readonly Guid _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public LanguageService(Guid userId)
        {
            _userId = userId;
        }

        // Create
        public bool AddLanguage(LanguageCreate model)
        {
            var entity = new Language()
            {
                UserId = _userId,
                Name = model.Name,
                LanguageFamily = model.LanguageFamily
            };

            _context.Languages.Add(entity);
            return _context.SaveChanges() == 1;
        }

        // Get all
        public IEnumerable<LanguageListItem> GetLanguages()
        {
            var query =
                _context
                .Languages
                .Where(e => e.UserId == _userId)
                .Select(
                    e =>
                    new LanguageListItem
                    {
                        Name = e.Name,
                        LanguageFamily = e.LanguageFamily,
                        WordCount = e.Words.Count
                    });

            return query.ToArray();
        }

        // Get by ID
        public LanguageListItem GetLanguageById(int id)
        {
            var entity =
                _context
                .Languages
                .Single(e => e.LanguageId == id && e.UserId == _userId);

            return new LanguageListItem
            {
                Name = entity.Name,
                LanguageFamily = entity.LanguageFamily,
                WordCount = entity.Words.Count
            };
        }

        // Update
        public bool UpdateLanguage(LanguageEdit model)
        {
            var entity =
                _context
                .Languages
                .Single(e => e.LanguageId == model.LanguageId && e.UserId == _userId);

            entity.Name = model.Name;
            entity.LanguageFamily = model.LanguageFamily;

            return _context.SaveChanges() == 1;
        }

        // Delete
        public bool DeleteLanguage(int languageId)
        {
            var entity =
                _context
                .Languages
                .Single(e => e.LanguageId == languageId && e.UserId == _userId);

            _context.Languages.Remove(entity);

            return _context.SaveChanges() == 1;
        }
    }
}
