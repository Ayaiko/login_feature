using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LoginWebApp.Controllers;
using LoginWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoginWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppointmentTest
{
    public class AppointmentControllerTest
    {
        private readonly DbContextOptions<LoginDbContext> _options;

        public AppointmentControllerTest()
        {
            string connectionString = "Server=LAPTOP-BHJDE0S4;Database=CodeFirst;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True";

            _options = new DbContextOptionsBuilder<LoginDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        }

        private AppointmentController SetupController(string email, string password)
        {
            var context = new LoginDbContext(_options);

            // Ensure the user exists in your real database
            var user = context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new InvalidOperationException("Test user not found in the database.");
            }

            // Create a user manager using the real user store
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore, null, null, null, null, null, null, null, null);

            // Create a test controller with a mock HttpContext that simulates a logged-in user
            var controller = new AppointmentController(context, userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email)
                        }, "mock"))
                    }
                }
            };

            return controller;

        }

        [Theory]
        [InlineData("Hase212@gmail.com", "Pass1.")]
        public async Task Index_ReturnsView_WithUserAppointments(string email, string password)
        {
            // Arrange
            using (var controller = SetupController(email, password))
            {

                // Act
                var result = await controller.Index();
                var userInController = controller.User;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
            }

        }

        [Theory]
        [InlineData("Hase212@gmail.com", "Pass1.")]
        public async Task Create_Return200_WithCreatedAppointment(string email, string password)
        {
            using (var controller = SetupController(email, password))
            {
                //Arrange
                var appointment = new AppointmentModel
                {
                    Title = "Meeting",
                    Description = "Discuss project",
                    Date = DateTime.Now.Date,
                    Location = "Conference Room",
                    //UserId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier)
                };


                //Act
                var result = await controller.Create(appointment);


                //Assert
                var statusCodeResult = Assert.IsType<OkResult>(result);
                Assert.Equal(StatusCodes.Status200OK, statusCodeResult.StatusCode);
            }
        }

    }
}
