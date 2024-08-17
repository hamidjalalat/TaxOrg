using Api;
using Application;
using Application.Common.Exceptions;
using Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NazmMapping;
using Persistence.Configurations.Anemic.Contexts;
using Serilog;
using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Reflection;
using ViewModels;


try
{
    //Log.Logger = new LoggerConfiguration()
    //.WriteTo.Console()
    //.CreateBootstrapLogger();
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


    // Add services to the container.
    builder.Services.AddApplicationServices();
    builder.Services.AddViewModelServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddWebUIServices();

    builder.Services.AddResponseCompression(option =>
    {
        option.EnableForHttps = true;
    });

    builder.Services.Configure<BrotliCompressionProviderOptions>(o =>
    {
        o.Level = System.IO.Compression.CompressionLevel.Fastest;
    });

    var app = builder.Build();

    app.UseResponseCompression();

    //app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
        {
            SourceCodeLineCount = 10,
        });
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();
    //  app.UseExceptionHandler("/Error");

    app.UseStatusCodePages();

    app.UseStaticFiles();

    app.UseRouting();
    app.UseCors("CorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
