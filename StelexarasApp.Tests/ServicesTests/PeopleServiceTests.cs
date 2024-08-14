using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.Services;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.IServices;
using System;

namespace StelexarasApp.Tests.ServicesTests
{
    public class PeopleServiceTests
    {
        private readonly PeopleService _peopleService;
        private readonly AppDbContext _dbContext;

        public PeopleServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _dbContext = new AppDbContext(options);
            _peopleService = new PeopleService(_dbContext);
        }

        [Fact]
        public async Task AddKataskinotis_ShouldAddKataskinotisToDatabase()
        {
            var skini = GetMockUpSkini();
            var person = GetMockUpKataskinotis();
            person.Skini = skini;

            await _dbContext.Skines.AddAsync(skini);
            await _dbContext.SaveChangesAsync();

            await _peopleService.AddPaidiInDbAsync(person, skini.Name);

            var addedPerson = await _dbContext.Kataskinotes.FindAsync(person.Id);
            Assert.NotNull(addedPerson);
            Assert.Equal("Kataskinotis Test", addedPerson.FullName);
            Assert.Equal(skini.Name, addedPerson.Skini.Name);

        }

        [Fact]
        public async Task AddKataskinotis_ShouldFailToAddWhenSkiniNotFound()
        {
            var person = GetMockUpKataskinotis();
            var skini = GetMockUpSkini();

            await _dbContext.Skines.AddAsync(skini);
            await _dbContext.SaveChangesAsync();

            var result = await _peopleService.AddPaidiInDbAsync(person, string.Empty);
            Assert.False(result);
        }

        [Fact]
        public async Task AddKataskinotis_ShouldFailToAddWhenFullNameIsEmpty()
        {
            var skini = GetMockUpSkini();
            var person = GetMockUpKataskinotis();
            person.FullName = string.Empty;

            await _dbContext.Skines.AddAsync(skini);
            await _dbContext.SaveChangesAsync();

            var result = await _peopleService.AddPaidiInDbAsync(person, skini.Name);
            Assert.False(result);
        }

        [Fact]
        public async Task AddSkini_ShouldFailToAddWhenSkiniNameEmpty()
        {
            var skini = GetMockUpSkini();
            skini.Name = string.Empty;

            var result = await _peopleService.AddSkinesInDbAsync(skini);
            Assert.False(result);
        }

        [Fact]
        public async Task AddSkini_ShouldAddSkiniToDatabase()
        {
            var skini = GetMockUpSkini();
            
            await _peopleService.AddSkinesInDbAsync(skini);
            var skines = await _dbContext.Skines.ToListAsync();
            
            Assert.Single(skines);
            Assert.Equal("Skini Test", skines [0].Name);
        }

        [Fact]
        public async Task AddKataskinotis_ShouldAddPersonToDatabase()
        {
            var person = GetMockUpKataskinotis();
            var skini = GetMockUpSkini();

            await _dbContext.Skines.AddAsync(skini);
            await _dbContext.SaveChangesAsync();

            await _peopleService.AddPaidiInDbAsync(person, skini.Name);

            var people = await _dbContext.Kataskinotes.ToListAsync();
            Assert.Single(people);
            Assert.Equal("Kataskinotis Test", people [0].FullName);
        }

        [Fact]
        public async Task AddEkpaideuomenosAsync_ShouldAddPersonToDatabase()
        {
            var person = GetMockUpEkpaideuomenos();
            var skini = GetMockUpSkini();

            await _dbContext.Skines.AddAsync(skini);
            await _dbContext.SaveChangesAsync();

            await _peopleService.AddPaidiInDbAsync(person, skini.Name);

            var people = await _dbContext.Ekpaideuomenoi.ToListAsync();
            Assert.Single(people);
            Assert.Equal("Ekpaideuomenos Test", people [0].FullName);
        }

        private Kataskinotis GetMockUpKataskinotis()
        {
            return new Kataskinotis
            {
                Id = 1,
                FullName = "Kataskinotis Test",
                Age = 12,
                Skini = GetMockUpSkini()
            };
        }

        private Ekpaideuomenos GetMockUpEkpaideuomenos()
        {
            return new Ekpaideuomenos
            {
                Id = 2,
                FullName = "Ekpaideuomenos Test",
                Age = 16,
                Skini = GetMockUpSkini()
            };
        }

        private Skini GetMockUpSkini()
        {
            return new Skini
            {
                Id = 3,
                Name = "Skini Test",
                Koinotita = new Koinotita { Id = 1, Name = "Koinotita Test" }
            };
        }
    }
}