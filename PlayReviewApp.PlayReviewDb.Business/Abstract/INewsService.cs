using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Abstract
{
      public interface INewsService
      {
            List<News> GetAll();
            News GetById(int id);
            void Add(News news);
            void Update(News news);
            void Delete(News news);
      }
}
