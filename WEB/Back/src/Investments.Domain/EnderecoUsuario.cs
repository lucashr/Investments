using System.Text.Json.Serialization;
using Investments.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Investments.Domain
{
    public class UserAddress
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [JsonPropertyName("cep")]
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public User User { get; set; }

    }
}