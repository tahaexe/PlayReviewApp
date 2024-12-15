using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using PlayReviewApp.PlayReviewDb.Entities.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public UserManager<IdentityUser> UserManager { get; set; }
        public SignInManager<IdentityUser> SignInManager { get; set; }
        private readonly IMapper _mapper;

        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper, SignInManager<IdentityUser> signInManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            _mapper = mapper;
            SignInManager = signInManager;
        }
        public IEnumerable<IdentityRole> Roles => RoleManager.Roles;

        public async Task<IdentityResult> CreateUser(UserDtoForCreation userDtoForCreation)
        {
            // Kullanıcıyı DTO'dan IdentityUser'a dönüştür
            var user = _mapper.Map<IdentityUser>(userDtoForCreation);

            // Kullanıcı oluştur
            var createResult = await UserManager.CreateAsync(user, userDtoForCreation.Password);

            // Oluşturma başarısızsa hata fırlat
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }

            // Rolleri kontrol et ve varsa ekle
            if (userDtoForCreation.Roles?.Any() == true)
            {
                var roleResult = await UserManager.AddToRolesAsync(user, userDtoForCreation.Roles);

                // Rol atama başarısızsa hata fırlat
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    throw new Exception($"Role assignment failed: {errors}");
                }
            }

            return createResult;
        }

        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await GetUser(userId);

            return await UserManager.DeleteAsync(user);
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return UserManager.Users.ToList();
        }

        public async Task<IdentityUser> GetUser(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId) ?? throw new KeyNotFoundException($"User with ID '{userId}' could not be found.");

            return user;
        }

        public async Task<UserDtoForUpdate> GetUserForUpdate(string userId)
        {
            var user = await GetUser(userId);
            var userDto = _mapper.Map<UserDtoForUpdate>(user);
            userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList());
            userDto.UserRoles = new HashSet<string>(await UserManager.GetRolesAsync(user));
            return userDto;
        }

        public async Task<IdentityResult> ResetPassword(UserDtoForResetPassword userDtoForResetPassword)
        {
            var user = await GetUser(userDtoForResetPassword.UserId);
            await UserManager.RemovePasswordAsync(user);
            var result = await UserManager.AddPasswordAsync(user, userDtoForResetPassword.Password);
            return result;
        }

        public async Task Update(UserDtoForUpdate userDtoForUpdate)
        {
            var user = await GetUser(userDtoForUpdate.UserId);

            user.PhoneNumber = userDtoForUpdate.PhoneNumber;
            user.Email = userDtoForUpdate.Email;

            var result = await UserManager.UpdateAsync(user);

            if (userDtoForUpdate.Roles.Count > 0)
            {
                var userRoles = await UserManager.GetRolesAsync(user);
                var r1 = await UserManager.RemoveFromRolesAsync(user, userRoles);
                var r2 = await UserManager.AddToRolesAsync(user, userDtoForUpdate.Roles);
            }
            return;
        }
    }
}
