using AuthService.Infrastructure.Helpers;

namespace AuthService.API.Middleware
{
    public class JwtDecryptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public JwtDecryptionMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var encryptedToken = authHeader.Substring("Bearer ".Length).Trim();
                try
                {
                    var decryptedToken = JwtEncryptor.Decrypt(
                        encryptedToken,
                        _config["JwtEncryption:Key"],
                        _config["JwtEncryption:IV"]
                    );

                    // Replace the header with the decrypted token
                    context.Request.Headers["Authorization"] = $"Bearer {decryptedToken}";
                }
                catch
                {
                    // If decryption fails, let it return 401 Unauthorized
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid or unreadable token.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
