using System;
using System.Threading.Tasks;
using CinemaWebApi.Controllers;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinemaWebApi.Tests.UnitTests;

[TestClass]
public class PersonControllerTests: BaseTests
{
    [TestMethod]
    public async Task GetPersonListPaginated()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = BuildContext(dbName);
        var mapper = BuildMap();

        context.Persons.Add(new Person { FirstName = "Person", LastName = "1" });
        context.Persons.Add(new Person { FirstName = "Person", LastName = "2" });
        context.Persons.Add(new Person { FirstName = "Person", LastName = "3" });
        await context.SaveChangesAsync();
        
        var context2 = BuildContext(dbName);

        var controller = new PersonController(context2, mapper);
        
        controller.ControllerContext.HttpContext = new DefaultHttpContext();
        var responsePage1 = await controller.Get(new PaginationDto { Page = 1, RecordsPerPage = 2 });
        var peoplePage1 = responsePage1.Value;
        Assert.AreEqual(2, peoplePage1?.Count);
        
        controller.ControllerContext.HttpContext = new DefaultHttpContext();
        var responsePage2 = await controller.Get(new PaginationDto { Page = 2, RecordsPerPage = 2 });
        var peoplePage2 = responsePage2.Value;
        Assert.AreEqual(1, peoplePage2?.Count);
        
        controller.ControllerContext.HttpContext = new DefaultHttpContext();
        var responsePage3 = await controller.Get(new PaginationDto { Page = 3, RecordsPerPage = 2 });
        var peoplePage3 = responsePage3.Value;
        Assert.AreEqual(0, peoplePage3?.Count);
    }
}
