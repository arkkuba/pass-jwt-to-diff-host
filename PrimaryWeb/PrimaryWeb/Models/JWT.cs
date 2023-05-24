using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PrimaryWeb.Models
{
    internal class JWT
    {
        internal int ExpireMinutes { get; set; } = 720; // 12 hours.

        internal readonly string SECRET_KEY = "ZqFBFaQB0acqSuIHkWMQ3gyUJ5RnPA7fffdj8mZqjrDR2MdKiU/r9+NsyVOW9Sen"; // This secret key should be moved to some configurations outside source code.
        
        internal string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha384Signature;

        internal Claim[]? Claims { get; set; }
    }
}
