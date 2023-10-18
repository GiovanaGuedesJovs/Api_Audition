using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using RickAndMortyAPI.Data;
using RickAndMortyAPI.Intefaces;
using RickAndMortyAPI.Interface;
using RickAndMortyAPI.Service;
using System.ComponentModel.DataAnnotations;

namespace RickAndMortyAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(); 
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IJsonService, JsonService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJsonService, JsonService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var azureBlobStorageConnectionString = configuration.GetConnectionString("AzureBlobStorage");

            services.AddSingleton(x => new BlobServiceClient(azureBlobStorageConnectionString));
        }

    }
}
