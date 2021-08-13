using System;
using WebApi.Entities;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool Authenticated { get; set; }
        public DateTime? ExpirationUtcDateTime { get; set; }

        public AuthenticateResponse(User user, string token = null, bool authenticated = false, DateTime? expirationUtcDateTime = null)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
            ExpirationUtcDateTime = expirationUtcDateTime;
            Authenticated = authenticated;
        }
    }
}