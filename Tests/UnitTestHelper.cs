using AutoMapper;
using Business;
using Business.Models;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Tests
{
    public static class UnitTestHelper
    {
        public static DbContextOptions<GameStoreDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new GameStoreDbContext(options))
            {
                SeedDataBll(context);
            }

            return options;
        }
        public static GameStoreDbContext GetDbContext(DbContextOptions<GameStoreDbContext> options)
        {
            return new GameStoreDbContext(options);
        }

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            
            return new Mapper(configuration);
        }

        public static void SeedDataBll (GameStoreDbContext context)
        {
            context.Games.AddRange(
                new Game { Id = 1, Key = "god-of-war", Name = "God Of War", Description = "none", IsDeleted = false, Genres = new List<Genre>() { }, Platforms = new List<Platform>() { } },
                new Game { Id = 2, Key = "dark-souls", Name = "Dark Souls", Description = "none", IsDeleted = false, Genres = new List<Genre>() { }, Platforms = new List<Platform>() { } });
            context.Genres.AddRange(
                new Genre { Id = 1, Name = "Action", ParentId = 2, Games = new List<Game>() { }, Nested = new List<Genre>() { }},
                new Genre { Id = 2, Name = "Shooter", ParentId = 1, Games = new List<Game>() { }, Nested = new List<Genre>() { }});
            context.Platforms.AddRange(
                new Platform { Id = 1, Type = "PC", Games = new List<Game>() { } },
                new Platform { Id = 2, Type = "Anroid", Games = new List<Game>() { } });
            context.SaveChanges();
        }

    }
}
