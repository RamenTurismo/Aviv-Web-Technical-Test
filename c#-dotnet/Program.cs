using listingapi.Configuration;
using listingapi.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddUserSecrets("e3027d8d-095f-4a60-8acc-df396a4bb0cb", reloadOnChange: true);

builder.Services.AddDbContext<ListingsContext>(options =>
{
    string host = builder.Configuration.GetValue<string>("PGHOST");
    string databse = builder.Configuration.GetValue<string>("PGDATABASE", "listing");
    string user = builder.Configuration.GetValue<string>("PGUSER", "listing");
    string pwd = builder.Configuration.GetValue<string>("PGPASSWORD", "listing");

    options.UseNpgsql($"Host={host};port=5432;Username={user};Password={pwd};Database={databse};",
        npgsqlOptionsAction: sqlOptions =>
        {
            sqlOptions.CommandTimeout(5);
            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
        });
}, optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new GroupingByNamespaceConvention());
}).AddNewtonsoftJson();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "listingapi", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

WebApplication app = builder.Build();

if (!builder.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.RunAsync();
