using Microsoft.Extensions.Options;
using SocialMediaCore.Exceptions;
using SocialMediaInfraestructure.Interfaces;
using SocialMediaInfraestructure.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace SocialMediaInfraestructure.Services
{
    public class PasswordService : IPasswordHasher
    {
        private readonly PasswordOptions options;
        public PasswordService(IOptions<PasswordOptions> _options)
        {
            options = _options.Value;
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new BusinessException("formato del hash no valido");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            using (var algorith = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations))
            {
                var keyToCheck = algorith.GetBytes(options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            using(var algorith = new Rfc2898DeriveBytes(
                password, 
                options.SaltSize,
                options.Iterations))
            {
                var key = Convert.ToBase64String(algorith.GetBytes(options.KeySize));
                var salt = Convert.ToBase64String(algorith.Salt);

                return $"{options.Iterations}.{salt}.{key}";
            }
        }
    }
}
