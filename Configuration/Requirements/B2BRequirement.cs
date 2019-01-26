namespace Configuration.Requirements
{
    using Microsoft.AspNetCore.Authorization;

    internal class B2BRequirement : IAuthorizationRequirement
    {
        internal string SecretKey { get; }

        internal B2BRequirement(string secretKey) => SecretKey = secretKey;
    }
}