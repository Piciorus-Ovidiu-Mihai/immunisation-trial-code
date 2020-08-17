using System;
using ClinicalSurveyApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApiApplication.Data;

[assembly: HostingStartup(typeof(ClinicalSurveyApp.Areas.Identity.IdentityHostingStartup))]
namespace ClinicalSurveyApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
             
            });
        }
    }
}