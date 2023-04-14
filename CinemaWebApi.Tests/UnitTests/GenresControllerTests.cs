using System;
using System.Threading.Tasks;
using CinemaWebApi.Controllers;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinemaWebApi.Tests.UnitTests;

[TestClass]
public class GenresControllerTests : BaseTests
{
    [TestMethod]
    public async Task GetAllGenres()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = BuildContext(dbName);
        var mapper = BuildMap();

        context.Genres.Add(new Genre { Name = "Genre1" });
        context.Genres.Add(new Genre { Name = "Genre2" });
        await context.SaveChangesAsync();

        var context2 = BuildContext(dbName);

        var controller = new GenresController(context2, mapper);
        var response = await controller.Get();

        var genres = response.Value;
        Assert.AreEqual(2, genres?.Count);
    }

    [TestMethod]
    public async Task GetGenreByIdDoesNotExist()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = BuildContext(dbName);
        var mapper = BuildMap();

        var controller = new GenresController(context, mapper);
        var response = await controller.Get(1);
        var result = response.Result as StatusCodeResult;

        Assert.AreEqual(404, result?.StatusCode);
    }

    [TestMethod]
    public async Task GetGenreByIdExist()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = BuildContext(dbName);
        var mapper = BuildMap();

        context.Genres.Add(new Genre { Name = "Genre1" });
        context.Genres.Add(new Genre { Name = "Genre2" });
        await context.SaveChangesAsync();

        var context2 = BuildContext(dbName);

        var controller = new GenresController(context2, mapper);

        var id = 1;
        var response = await controller.Get(id);
        var result = response.Value;

        Assert.AreEqual(id, result?.Id);
    }

    [TestMethod]
    public async Task CreateGenre()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = BuildContext(dbName);
        var mapper = BuildMap();

        var newGenre = new GenreCreateDto { Name = "New Genre" };
        var controller = new GenresController(context, mapper);
        
        var response = await controller.Post(newGenre);
        var result = response.Result as CreatedAtRouteResult;
        
        Assert.AreEqual(201, result?.StatusCode);
        
        var context2 = BuildContext(dbName);
        var count = await context2.Genres.CountAsync();
        Assert.AreEqual(1, count);
    }

    [TestMethod]
    public async Task UpdateGenre()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = BuildContext(dbName);
        var mapper = BuildMap();
        
        context.Genres.Add(new Genre { Name = "Genre1" });
        await context.SaveChangesAsync();
        
        var context2 = BuildContext(dbName);
        var controller = new GenresController(context2, mapper);

        var newName = "New Genre";
        var genreCreateDto = new GenreCreateDto { Name = newName };
        var id = 1;
        var response = await controller.Put(id, genreCreateDto);

        var result = response as StatusCodeResult;
        Assert.AreEqual(204, result?.StatusCode);
        
        var context3 = BuildContext(dbName);
        var exists = await context3.Genres.AnyAsync(x => x.Name == newName);
        Assert.IsTrue(exists);
    }

    [TestMethod]
    public async Task DeleteGenreNotFound()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = BuildContext(dbName);
        var mapper = BuildMap();
        
        var controller = new GenresController(context, mapper);
        var response = await controller.Delete(1);
        var result = response as StatusCodeResult;
        Assert.AreEqual(404, result?.StatusCode);
    }

    [TestMethod]
    public async Task DeleteGenre()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = BuildContext(dbName);
        var mapper = BuildMap();
        
        context.Genres.Add(new Genre { Name = "Genre1" });
        await context.SaveChangesAsync();
        
        var context2 = BuildContext(dbName);
        var controller = new GenresController(context2, mapper);
        
        var response = await controller.Delete(1);
        var result = response as StatusCodeResult;
        Assert.AreEqual(204, result?.StatusCode);
        
        var context3 = BuildContext(dbName);
        var exists = await context3.Genres.AnyAsync();
        Assert.IsFalse(exists);
    }
}