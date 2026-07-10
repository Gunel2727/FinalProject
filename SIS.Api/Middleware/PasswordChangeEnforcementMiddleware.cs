namespace SIS.Api.Middleware;

public class PasswordChangeEnforcementMiddleware
{
    private readonly RequestDelegate _next;

    public PasswordChangeEnforcementMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
       
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var mustChange = context.User.FindFirst("mustChangePassword")?.Value;
            var path = context.Request.Path.Value?.ToLower() ?? "";

          
            var allowedPaths = new[]
            {
                "/api/auth/change-password",
                "/api/auth/login",
                "/api/auth/google-login"
            };

            if (mustChange == "True" && !allowedPaths.Any(p => path.StartsWith(p)))
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    "{\"success\":false,\"statusCode\":403,\"data\":null,\"errors\":[\"Zəhmət olmasa əvvəlcə şifrənizi dəyişin.\"]}");
                return; 
            }
        }

        await _next(context);
    }
}