using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Entities.DTO.News
{
    public record NewsDto
    {
        [Required]
        public int NewsId { get; set; }
    }
}
