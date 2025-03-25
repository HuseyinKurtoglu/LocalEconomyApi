using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace YerelEkonomiDestekleme.DataAcces.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<User> GetByIdAsync(string id);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task<IList<string>> GetRolesAsync(User user);
        new Task<IdentityResult> UpdateAsync(User user);
        new Task<IdentityResult> DeleteAsync(User user);
        Task<bool> IsEmailConfirmedAsync(User user);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
        Task<bool> IsInRoleAsync(User user, string role);
        Task SignOutAsync();
    }
}
