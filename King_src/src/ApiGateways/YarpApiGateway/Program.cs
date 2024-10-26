using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Adđ Services
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(limiterOptions =>
{
    limiterOptions.AddFixedWindowLimiter("fixed", option =>
    {
        option.Window = TimeSpan.FromSeconds(10);
        option.PermitLimit = 5;
    });
});

var app = builder.Build();

// Use Methods
app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
