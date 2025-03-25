using Microsoft.AspNetCore.DataProtection;
using System;

namespace UserManagementAPI.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly IDataProtector _protector;

        public PasswordHasher(IDataProtectionProvider protectionProvider)
        {
            _protector = protectionProvider.CreateProtector("UserManagement.Password");
        }

        public string HashPassword(string password)
        {
            return _protector.Protect(password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            try
            {
                var unprotected = _protector.Unprotect(hashedPassword);
                return unprotected == providedPassword;
            }
            catch
            {
                return false;
            }
        }
    }
}