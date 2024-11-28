using PlayReviewApp.PlayReviewDb.Business.Abstract;
using PlayReviewApp.PlayReviewDb.DataAccess.Abstract;
using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Concrete
{
    public class NewsManager : INewsService
    {
        private readonly INewsDal _newsDal;

        public NewsManager(INewsDal newsDal)
        {
            _newsDal = newsDal;
        }

        public void Add(News news)
        {
            _newsDal.Add(news);
        }

        public void Delete(News news)
        {
            _newsDal.Delete(news);
        }

        public List<News> GetAll()
        {
            return _newsDal.GetAll();
        }

        public News GetById(int id)
        {
            return _newsDal.Get(n => n.NewsId == id);
        }

        public List<News> GetTopNItems(int n)
        {
            return _newsDal.GetTopNItems(n);
        }

        public void Update(News news)
        {
            _newsDal.Update(news);
        }
    }
}
