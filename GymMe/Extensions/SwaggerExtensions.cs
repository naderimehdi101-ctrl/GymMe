
namespace GymMe.Extensions;

public static class SwaggerExtensions
{
    public static WebApplicationBuilder AddSwaggerAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT token directly"
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        });
        return builder;
    }

    public static WebApplication UseSwaggerAndSwaggerUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.UseResponseInterceptor(@"(httpResponse) => {
            const isSuccessfulLogin =
                httpResponse.url.endsWith('/accounts/login') &&
                httpResponse.status === 200;

            if (isSuccessfulLogin) {
                const loginResponseBody = JSON.parse(httpResponse.text);

                if (loginResponseBody.token) {
                    window.ui.preauthorizeApiKey('Bearer', loginResponseBody.token);
                }
            }
            return httpResponse;
        }");
        });

        return app;
    }
}