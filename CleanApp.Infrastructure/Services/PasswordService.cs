using CleanApp.Core.Exceptions;
using CleanApp.Infrastructure.Interfaces;
using CleanApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace CleanApp.Infrastructure.Services
{
    public class PasswordService : IPassworService
    {
        private readonly PasswordOptions _options;
        public PasswordService(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public void Check(string hash, string password)
        {
            var parts = hash.Split('.');

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes
                (
                    password,
                    salt,
                    iterations
                ))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);

                if (!keyToCheck.SequenceEqual(key))
                {
                    throw new BusinessException("La contraseña es incorrecta.");
                }
            }
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes
                (
                    password,
                    _options.SaltSize,
                    _options.Iterations
                ))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{_options.Iterations}.{salt}.{key}";
            }
        }
    }
}
