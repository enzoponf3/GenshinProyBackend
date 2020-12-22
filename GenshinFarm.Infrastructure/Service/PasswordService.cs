using GenshinFarm.Infrastructure.Interfaces;
using System;
using Microsoft.Extensions.Options;
using GenshinFarm.Infrastructure.Options;
using System.Security.Cryptography;
using GenshinFarm.Core.Entities;
using System.Linq;
using System.Collections.Generic;

namespace GenshinFarm.Infrastructure.Service
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _passwordOptions;

        public PasswordService(IOptions<PasswordOptions> options)
        {
            _passwordOptions = options.Value;
        }

        public bool Check(string key, string salt, string password)
        {
            var _iterations = Convert.ToInt32(_passwordOptions.Iterations);
            var _salt = Convert.FromBase64String(salt);
            var _key = Convert.FromBase64String(key);

            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                _salt,
                _iterations
                ))
            {
                var keyToChek = algorithm.GetBytes(_passwordOptions.KeySize);

                return keyToChek.SequenceEqual(_key);
            }
        }

        public List<string> Hash(string password)
        {
            List<string> list = new List<string> ();
            //PBKDF2 implementation
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                _passwordOptions.SaltSize,
                _passwordOptions.Iterations
                ))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_passwordOptions.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                list.Add(key);
                list.Add(salt);
            }
            return list;
        }
    }
}
