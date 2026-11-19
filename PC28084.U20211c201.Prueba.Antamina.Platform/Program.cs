using System.Text.Json.Serialization;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Application.CommandServices;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Application.Internal.CommandServices;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Repositories;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Repositories;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;
using Scalar.AspNetCore;
using InvalidRequestResponseFactory =
    PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Interfaces.Rest.ProblemDetails.InvalidRequestResponseFactory;
using ProblemDetailsFactory =
    PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Controllers with kebab-case routes. Enums travel as text (for example "InTransit").
// A request that cannot be read (malformed JSON, wrong primitive type) is answered with a localized HTTP 400.
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .ConfigureApiBehaviorOptions(options =>
        options.InvalidModelStateResponseFactory = InvalidRequestResponseFactory.Create);

// The OpenAPI document reads this configuration, so the enums are documented as text, as they travel.
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Add ProblemDetails and ExceptionHandler services
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add Database Connection
// Configure Database Context and route EF logs through the app logger pipeline.
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .AddInterceptors(serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Localization (i18n). The resource files live next to their marker classes (ErrorMessages, CommonMessages).
builder.Services.AddLocalization();

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();

// OpenAPI / Scalar documentation
builder.Services.AddOpenApi();

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DispatchDomainEventsInterceptor>();

// Logistics Bounded Context
builder.Services.AddScoped<IOreDispatchTicketRepository, OreDispatchTicketRepository>();
builder.Services.AddScoped<IOreDispatchTicketCommandService, OreDispatchTicketCommandService>();

// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(ExceptionHandlingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(TransactionCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    [typeof(Program)]);


var app = builder.Build();

// Create the antamina_haulage_ops schema and the tables when they do not exist yet
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// The language of the responses follows the Accept-Language header: English (en, en-US) or Spanish (es, es-PE).
var supportedCultures = new[] { "en", "en-US", "es", "es-PE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();
