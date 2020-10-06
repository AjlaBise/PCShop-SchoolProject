using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineShop.Areas.Administrator.Controllers;
using OnlineShop.ViewModels;
using Xunit;

namespace OnlineShop.UnitTests
{
    public class ControllerTests
    {
        private Context _context { get; }
        private UserManager<AppUser> _userManager { get; }
        private RoleManager<AppRole> _roleManager { get; }
        public ControllerTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new Context(options);

            _roleManager = new RoleManager<AppRole>(
                new RoleStore<AppRole, Context, int>(_context),
                new List<RoleValidator<AppRole>> { new RoleValidator<AppRole>() },
                null, null,
                new Mock<ILogger<RoleManager<AppRole>>>().Object
            );

            _userManager = new UserManager<AppUser>(
                new UserStore<AppUser, AppRole, Context, int>(_context),
                null,
                new PasswordHasher<AppUser>(),
                new List<UserValidator<AppUser>> { new UserValidator<AppUser>() },
                null, null, null, null,
                new Mock<ILogger<UserManager<AppUser>>>().Object
            );

            DataFill.LoadData(_context);
            DataFill.LoadOglasi(_context);
            DataFill.AddUsers(_userManager, _context, _roleManager);
        }

        [Fact]
        public void Narudzbe_GenerisiNarudzbeTest()
        {
            var controller = new NarudzbeController(_userManager, _context);
            // Provjera da li akcija vraca praznu listu
            Assert.NotEmpty(controller.GenerisiNarudzbe());
        }

        [Fact]
        public async void Korisnici_PosaljiTokenTest()
        {
            var controller = new KorisniciController(_userManager, _context);
            var expected = "error";
            // Za nepostojeceg korisnika vraca se string 'error'
            Assert.Equal(expected, await controller.PosaljiToken("0"));
        }


        // Koju god godinu proslijedimo, treba vratiti svih 12 mjeseci bez obzira da li se podaci nalaze u bazi
        [Theory]
        [InlineData(2018)]  // Nema nijedna narudzba iz 2018. godine u bazi
        [InlineData(2019)]  // Samo jedna narudzba iz 2019. godine
        [InlineData(2020)]  // Za 2020 godinu postoje narudzbe u svakom mjesecu
        [InlineData(null)]  // Bez unesene godine
        public void Statistika_NarudzbaPartialTest(int? godina)
        {
            var expected = 12;
            var controller = new StatistikaController(_userManager, _context);

            var actionResult = controller.NarudzbaPartial(godina) as PartialViewResult;
            var model = (StatistikaNarudzbeVM) actionResult.ViewData.Model;

            Assert.Equal(expected, model.NarudzbePoGodini.Count);
        }



    }
}
