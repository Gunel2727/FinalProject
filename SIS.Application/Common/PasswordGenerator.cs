using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Common
{
    public static class PasswordGenerator
    {
        private const string Chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";

        public static string Generate()
        {
            var random = new Random();
            var suffix = new string(Enumerable.Range(0, 6)
                .Select(_ => Chars[random.Next(Chars.Length)])
                .ToArray());
            return $"Sis@{suffix}";
        }
    }
}
