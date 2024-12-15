using PlayReviewApp.Core.DataAccess.EntityFramework;
using PlayReviewApp.PlayReviewDb.DataAccess.Abstract;
using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, PlayReviewDbContext>, ICategoryDal
    {
        public EfCategoryDal(PlayReviewDbContext context) : base(context)
        {

        }
    }
}
