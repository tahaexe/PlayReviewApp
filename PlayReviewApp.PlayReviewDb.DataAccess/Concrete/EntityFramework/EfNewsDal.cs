using PlayReviewApp.Core.DataAccess.EntityFramework;
using PlayReviewApp.PlayReviewDb.DataAccess.Abstract;
using PlayReviewApp.PlayReviewDb.Entities.ComplexType;
using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.DataAccess.Concrete.EntityFramework
{
    public class EfNewsDal : EfEntityRepositoryBase<News, PlayReviewDbContext>, INewsDal
    {
        public EfNewsDal(PlayReviewDbContext context) : base(context)
        {

        }

        public List<News> GetTopNItems(int n)
        {
            return _context.News
                        .OrderBy(e => e.NewsId)
                        .Take(n)
                        .ToList();
        }

        public List<NewsDetail> NewsDetails()
        {
            var result = from n in _context.News
                         join c in _context.Categories
                         on n.CategoryId equals c.CategoryId
                         select new NewsDetail
                         {
                             NewsId = n.NewsId,
                             CategoryId = n.CategoryId,
                             NewsTitle = n.Title
                         };
            return result.ToList();
        }
    }
}
