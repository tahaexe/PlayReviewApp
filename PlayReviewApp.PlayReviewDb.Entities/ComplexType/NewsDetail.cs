using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Entities.ComplexType
{
    public class NewsDetail
    {
        public int NewsId { get; set; }
        public int CategoryId { get; set; }
        public string NewsTitle { get; set; }
    }
}
