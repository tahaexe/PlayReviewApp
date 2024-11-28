using PlayReviewApp.Core.DataAccess;
using PlayReviewApp.PlayReviewDb.Entities.ComplexType;
using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.DataAccess.Abstract
{
    public interface INewsDal : IEntityRepository<News>
    {
        List<News> GetTopNItems(int n);
        List<NewsDetail> NewsDetails();
    }
}
