using Microsoft.AspNetCore.Identity;
using PlayReviewApp.PlayReviewDb.Entities.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Abstract
{
    public interface IAuthService
    {
        RoleManager<IdentityRole> RoleManager { get; set; }
        UserManager<IdentityUser> UserManager { get; set; }
        SignInManager<IdentityUser> SignInManager { get; set; }
        IEnumerable<IdentityRole> Roles { get; }
        IEnumerable<IdentityUser> GetAllUsers();
        Task<IdentityUser> GetUser(string userId);
        Task<UserDtoForUpdate> GetUserForUpdate(string userId);
        Task<IdentityResult> CreateUser(UserDtoForCreation userDtoForCreation);
        Task Update(UserDtoForUpdate userDtoForUpdate);
        Task<IdentityResult> ResetPassword(UserDtoForResetPassword userDtoForResetPassword);
        Task<IdentityResult> DeleteUser(string userId);
    }
}
