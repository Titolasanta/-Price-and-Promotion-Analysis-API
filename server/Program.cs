using ProductApi.Data;
using ProductApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ProductDb>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Server";
    config.Title = "Server v1";
    config.Version = "v1";
});

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Allow React app's origin
              .AllowAnyMethod()                    // Allow all HTTP methods (GET, POST, etc.)
              .AllowAnyHeader();                   // Allow all headers
    });
});

var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "Server";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

// Use CORS middleware
app.UseCors("AllowReactApp"); // Apply the CORS policy

// Register endpoints
app.MapGroup("/product").MapProductEndpoints();

app.Run();
