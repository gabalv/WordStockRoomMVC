using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStockRoom.Data;
using WordStockRoom.Models;

namespace WordStockRoom.Services
{
    public class VideoService
    {
        private readonly Guid _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly int _wordId;

        public VideoService(Guid userId, int wordId)
        {
            _userId = userId;
            _wordId = wordId;
        }

        // Create
        public bool AddVideo(VideoCreate model)
        {
            var entity =
                new Video()
                {
                    UserId = _userId,
                    Description = model.Description,
                    URL = ReformatYoutubeURL(model.URL),
                    WordId = _wordId
                };

            _context.Videos.Add(entity);
            return _context.SaveChanges() == 1;
        }

        // Get All
        public IEnumerable<VideoDetail> GetVideosByWord()
        {
            var query =
                _context
                .Videos
                .Where(e => e.UserId == _userId && e.WordId == _wordId)
                .Select(e => new VideoDetail
                {
                    VideoId = e.VideoId,
                    Description = e.Description,
                    URL = e.URL,
                    Word = e.Word.WordName,
                    Language = e.Word.Language.Name
                });

            return query.ToArray();
        }

        // Get by Id
        public VideoDetail GetVideoById(int id)
        {
            var entity =
                _context
                .Videos
                .SingleOrDefault(e => e.VideoId == id && e.UserId == _userId);

            return new VideoDetail
            {
                VideoId = entity.VideoId,
                Description = entity.Description,
                URL = entity.URL,
                Word = entity.Word.WordName,
                Language = entity.Word.Language.Name
            };
        }

        // Update
        public bool UpdateVideo(VideoEdit model)
        {
            var entity =
                _context
                .Videos
                .SingleOrDefault(e => e.VideoId == model.VideoId && e.UserId == _userId);

            entity.Description = model.Description;

            return _context.SaveChanges() == 1;
        }

        // Delete
        public bool DeleteVideo(int id)
        {
            var entity =
                _context
                .Videos
                .SingleOrDefault(e => e.VideoId == id && e.UserId == _userId);

            _context.Videos.Remove(entity);

            return _context.SaveChanges() == 1;
        }


        // helper
        public string ReformatYoutubeURL(string url)
        {
            return url.Replace("watch?v=", "embed/");
        }
    }
}
