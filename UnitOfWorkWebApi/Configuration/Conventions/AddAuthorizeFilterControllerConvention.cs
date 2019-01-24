namespace UnitOfWorkWebApi.Configuration.Conventions
{
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.Authorization;

    public class AddAuthorizeFilterControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.Filters.Add(new AuthorizeFilter("ViewerOrAdmin"));
        }
    }
}