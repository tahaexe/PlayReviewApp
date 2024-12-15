using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using PlayReviewApp.PlayReviewDb.Entities.DTO.News;
using PlayReviewApp.PlayReviewDb.Entities.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Mappings.AutoMapper.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<UserDtoForUpdate, IdentityUser>().ReverseMap();

            CreateMap<NewsDtoForUpdate, News>().ReverseMap();
        }
    }
}
