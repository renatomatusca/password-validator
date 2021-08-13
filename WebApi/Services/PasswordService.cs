using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IPasswordService
    {
        bool ValidatePassword(string password);
        string GetValidPassword();
    }

    public class PasswordService : IPasswordService
    {
        private char[] _requiredSpecialChars = new [] {'@','#','_','-', '!'};
        private int _minLength = 15;

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) ||
                password.Length < _minLength ||
                !password.Any(x => char.IsUpper(x)) ||
                !password.Any(x => char.IsLower(x)) ||
                (_requiredSpecialChars.Any() && !password.Any(x => _requiredSpecialChars.Contains(x))) ||
                password.Where((x, i) => i >= 1 && password[i - 1] == x).Any()
               )
                return false;

            return true;
        }

        public string GetValidPassword()
        {
            var newPassword = string.Empty;
            var random = new Random();

            while(newPassword.Length < _minLength)
            {
                var randomChar = (char)random.Next('a', 'z');

                if(string.IsNullOrWhiteSpace(newPassword) || newPassword.Last() != randomChar)
                    newPassword += randomChar;
            }

            var newPasswordCharArray = newPassword.ToCharArray();

            var randomNewPasswordIndex = random.Next(newPassword.Length);
            newPasswordCharArray[randomNewPasswordIndex] = char.ToUpper(newPasswordCharArray[randomNewPasswordIndex]);

            if (_requiredSpecialChars.Any())
            {
                int randomNewPasswordIndex2;
                do
                {
                    randomNewPasswordIndex2 = random.Next(newPassword.Length);
                } while (randomNewPasswordIndex2 == randomNewPasswordIndex);

                var randomRequiredSpecialCharIndex = random.Next(_requiredSpecialChars.Length);
                newPasswordCharArray[randomNewPasswordIndex2] = _requiredSpecialChars[randomRequiredSpecialCharIndex];
            }

            return string.Join(string.Empty, newPasswordCharArray);

        }
    }
}