using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Entities.DTO.News
{
    public record NewsDtoForGet : NewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Author { get; set; }
    }
}
