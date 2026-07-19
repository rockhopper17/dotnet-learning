using Microsoft.EntityFrameworkCore;
using SignalRWebpack.Hubs;
using SignalRWebpack.Data;
using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Exporter;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

// using var tracerProvider = Sdk.CreateTracerProviderBuilder()
//     .UseGrafana()
//     .Build();

// register SignalR and EF Core with Sqllite
builder.Services.AddSignalR();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=chatdata.db"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddOpenTelemetry()
//     .WithTracing(tracing => tracing
//         .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("SignalRWebpack"))
//         .AddAspNetCoreInstrumentation(options =>
//         {
//             // this captures all incoming http actions and websocket connections
//             options.RecordException = true;
//         })
//         .AddEntityFrameworkCoreInstrumentation()  // this captures all sqllite queries
//         .AddOtlpExporter());
// builder.Services.AddOpenTelemetry()
//     .WithTracing(tracing => tracing.UseGrafana())
//     .WithMetrics(metrics => metrics.UseGrafana());

builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
});

// configure metrics and tracing
var serviceName = builder.Configuration["OTEL_SERVICE_NAME"] ?? "signalr-chat-app";

var otel = builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName));

otel.WithMetrics(metrics =>
{
    metrics.AddAspNetCoreInstrumentation();
    metrics.AddMeter(ChatDiagnostics.ServiceName);
    metrics.AddMeter("Microsoft.AspNetCore.Hosting");
    metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
});

otel.WithTracing(tracing =>
{
    tracing.AddAspNetCoreInstrumentation();
    tracing.AddHttpClientInstrumentation();
    tracing.AddEntityFrameworkCoreInstrumentation();  // traces sqllite queries
    tracing.AddSource(ChatDiagnostics.ServiceName);  // custom activity source
});

// exporter hookup - aspire
var otlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
// var otlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] ?? "http://localhost:4317";
if (otlpEndpoint != null)
{
    // otel.UseOtlpExporter();
    otel.UseOtlpExporter(OtlpExportProtocol.Grpc, new Uri(otlpEndpoint));
}

var app = builder.Build();

// ensure database is created on startup for local testing
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// static frontend website
app.UseDefaultFiles();
app.UseStaticFiles();

// signalr websocket channel
app.MapHub<ChatHub>("/hub");

// minimal api endpoints for ef core data
app.MapGet("/api/logs", async (AppDbContext db) =>
    await db.ChatLogs.ToListAsync());

app.MapPost("/api/logs", async (ChatLog log, AppDbContext db) =>
{
    db.ChatLogs.Add(log);
    await db.SaveChangesAsync();
    return Results.Created($"/api/logs/{log.Id}", log);
});

app.Run();
