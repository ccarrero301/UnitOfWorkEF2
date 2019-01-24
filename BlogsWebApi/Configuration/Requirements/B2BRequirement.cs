namespace BlogsWebApi.Configuration.Requirements
{
    using Microsoft.AspNetCore.Authorization;

    public class B2BRequirement : IAuthorizationRequirement
    {
        public string SecretKey { get; }

        public B2BRequirement(string secretKey) => SecretKey = secretKey;
    }
}
