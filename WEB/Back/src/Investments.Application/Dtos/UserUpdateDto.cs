
using System.Collections.Generic;
using Investments.Domain.Identity;

namespace Investments.Application.Dtos
{
    public record UserUpdateDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Function { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}