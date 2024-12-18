using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Abstract
{
      public interface ICategoryService
      {
            List<Category> GetAll();
            Category GetById(int id);
            void Add(Category category);
            void Update(Category category);
            void Delete(int categoryId);
      }
}
