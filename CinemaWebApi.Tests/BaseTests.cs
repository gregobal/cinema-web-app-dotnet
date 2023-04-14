using System.Linq;
using AutoMapper;
using CinemaWebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CinemaWebApi.Tests;

public class BaseTests
{
    protected AppDbContext BuildContext(string dbName)
    {
        var options = new DbContextOptionsBuilder()
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(dbName).Options;

        var dbContext = new AppDbContext(options);
        return dbContext;
    }

    protected IMapper BuildMap()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile(new AutoMapperProfiles());
        });

        return config.CreateMapper();
    }
}
