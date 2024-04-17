namespace ExceleTech.API.Middlewares
{
    public static class DependencyInjection
    {
        public static void AddMiddlewares(this WebApplication application) 
        => application.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
