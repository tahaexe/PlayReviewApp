using AutoMapper;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using PlayReviewApp.PlayReviewDb.DataAccess.Abstract;
using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using PlayReviewApp.PlayReviewDb.Entities.DTO.News;
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
        private readonly IMapper _mapper;

        public NewsManager(INewsDal newsDal, IMapper mapper)
        {
            _newsDal = newsDal;
            _mapper = mapper;
        }

        public void Create(NewsDtoForCreate newsDtoForCreate)
        {
            var news = _mapper.Map<News>(newsDtoForCreate);

            _newsDal.Add(news);
        }

        public void Update(NewsDtoForUpdate newsDtoForUpdate)
        {
            if (newsDtoForUpdate == null)
                throw new ArgumentNullException(nameof(newsDtoForUpdate), "NewsDtoForUpdate cannot be null.");

            if (newsDtoForUpdate.NewsId <= 0)
                throw new ArgumentException("Invalid NewsId. The NewsId should be greater than 0.", nameof(newsDtoForUpdate.NewsId));

            // Veritabanından mevcut haber verisini al
            var news = _newsDal.Get(n => n.NewsId == newsDtoForUpdate.NewsId);

            if (news == null)
                throw new KeyNotFoundException($"News with ID {newsDtoForUpdate.NewsId} not found.");

            // Eğer ImageUrl null veya boş ise, mevcut veriyi koru
            if (string.IsNullOrEmpty(newsDtoForUpdate.ImageUrl))
            {
                newsDtoForUpdate.ImageUrl = news.ImageUrl;
            }

            // Mapleme işlemi: DTO'dan Entity'ye veri aktarımı
            _mapper.Map(newsDtoForUpdate, news);

            // Güncelleme işlemi
            _newsDal.Update(news);
        }


        public void Delete(int newsId)
        {
            if (newsId <= 0)  // NewsId'nin sıfırdan küçük olması zaten geçersiz olmalı
                throw new ArgumentException("Invalid NewsId. The NewsId should be greater than 0.", nameof(newsId));

            // Veritabanında haber var mı kontrol et
            var news = _newsDal.Get(n => n.NewsId == newsId);

            if (news == null)
                throw new KeyNotFoundException($"News with ID {newsId} not found.");

            // Haber silme işlemi
            _newsDal.Delete(news);
        }

        public List<NewsDtoForGet> GetAll()
        {
            // Veritabanından gelen News nesnelerini almak
            var newsList = _newsDal.GetAll() ?? new List<News>();

            // AutoMapper ile News nesnelerini NewsDtoForGet'e dönüştürmek
            return _mapper.Map<List<NewsDtoForGet>>(newsList);
        }

        public NewsDtoForGet GetById(int id)
        {
            var news = _newsDal.Get(n => n.NewsId == id);

            if (news == null) throw new KeyNotFoundException($"News with ID {id} not found.");

            return _mapper.Map<NewsDtoForGet>(news);
        }

        public List<NewsDtoForGet> GetTopNews(int n)
        {
            // Veritabanından gelen News nesnelerini almak
            var newsList = _newsDal.GetTopNItems(n) ?? new List<News>();

            // AutoMapper ile News nesnelerini NewsDtoForGet'e dönüştürmek
            return _mapper.Map<List<NewsDtoForGet>>(newsList);
        }
    }
}
