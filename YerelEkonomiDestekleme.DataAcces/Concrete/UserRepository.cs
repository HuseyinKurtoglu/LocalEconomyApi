using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Concrete;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace YerelEkonomiDestekleme.DataAcces.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new InvalidOperationException($"Email {email} olan kullanıcı bulunamadı.");

            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserName == username && !u.IsDeleted);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException($"ID {id} olan kullanıcı bulunamadı.");

            return user;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await GetByEmailAsync(email);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public new async Task<IdentityResult> UpdateAsync(User entity)
        {
            return await _userManager.UpdateAsync(entity);
        }

        public new async Task<IdentityResult> DeleteAsync(User entity)
        {
            entity.IsDeleted = true;
            return await _userManager.UpdateAsync(entity);
        }

        public async Task<bool> IsEmailConfirmedAsync(User user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<bool> IsInRoleAsync(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
