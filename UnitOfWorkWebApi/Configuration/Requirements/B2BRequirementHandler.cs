namespace UnitOfWorkWebApi.Configuration.Requirements
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;

    public class B2BRequirementHandler : AuthorizationHandler<B2BRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public B2BRequirementHandler(IHttpContextAccessor contextAccessor) =>
            _contextAccessor = contextAccessor;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, B2BRequirement requirement)
        {
            _contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationToken);

            var tokenValue = authorizationToken.FirstOrDefault();

            if (tokenValue == requirement.SecretKey)
            {
                context.Succeed(requirement);
                SuccessPendingRequirements(context);

                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }

        private void SuccessPendingRequirements(AuthorizationHandlerContext context)
        {
            foreach (var pendingRequirement in context.PendingRequirements.ToList())
                context.Succeed(pendingRequirement);
        }
    }
}