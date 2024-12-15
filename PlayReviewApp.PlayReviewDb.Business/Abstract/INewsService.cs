using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using PlayReviewApp.PlayReviewDb.Entities.DTO.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Abstract
{
    public interface INewsService
    {
        List<NewsDtoForGet> GetAll();
        List<NewsDtoForGet> GetTopNews(int n);
        NewsDtoForGet GetById(int id);
        void Create(NewsDtoForCreate newsDtoForCreate);
        void Update(NewsDtoForUpdate newsDtoForUpdate);
        void Delete(int newsId);
    }
}
