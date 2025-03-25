using Microsoft.AspNetCore.DataProtection;
using System;

namespace UserManagementAPI.Models
{
    public class PasswordHasher
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
                var unprotectedPassword = _protector.Unprotect(hashedPassword);
                return unprotectedPassword == providedPassword;
            }
            catch
            {
                return false;
            }
        }
    }
}