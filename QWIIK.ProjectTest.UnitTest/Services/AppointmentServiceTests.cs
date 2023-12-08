using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using QWIIK.ProjectTest.Entity;
using QWIIK.ProjectTest.EntityFramework;
using QWIIK.ProjectTest.Services;
using System.Reflection;
using Xunit;
namespace QWIIK.ProjectTest.UnitTest.Services
{
    public class AppointmentServiceTests
    {

        private readonly DbContextOptions<ApplicationDbContext> _options;

        public AppointmentServiceTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
        }

        [Theory]
        [InlineData(11, 3, true, "2022-12-08", "2022-12-08")]
        [InlineData(10,3,true, "2022-12-08", "2022-12-09")]
        [InlineData(10, 30, false, "2022-12-08", "2022-12-10")]
        [InlineData(1, 0, true,  "2022-12-08", "2022-12-11")]
        public void _CheckAvailableDate_Success(int maxDay,int countNext,bool isAvailableNext, DateTime appointmentDate, DateTime expectedAppointmentDate)
        {
            var configuration = new Mock<IConfiguration>(); 
            var services = new AppointmentServices(configuration.Object,new  ApplicationDbContext(_options));


            var privateMethod = typeof(AppointmentServices).GetMethod("_CheckAvailableDate", BindingFlags.NonPublic | BindingFlags.Instance);

            List<AppointmentsEntity> appointments = new List<AppointmentsEntity>
            {
                new AppointmentsEntity
                {
                    AppointmentDate = appointmentDate,
                    Count = 10,
                    IsAvailable = true
                },
                new AppointmentsEntity
                {
                    AppointmentDate = appointmentDate.AddDays(1),
                    Count = countNext,
                    IsAvailable = isAvailableNext
                },
                new AppointmentsEntity
                {
                    AppointmentDate = appointmentDate.AddDays(2),
                    Count = 1,
                    IsAvailable = true
                },
                new AppointmentsEntity
                {
                    AppointmentDate = appointmentDate.AddDays(5),
                    Count = 100,
                    IsAvailable = false
                },
                new AppointmentsEntity
                {
                    AppointmentDate = appointmentDate.AddDays(6),
                    Count = 1,
                    IsAvailable = true
                },
            };

            // Invoke the private method passing necessary parameters
            var result = (DateTime)privateMethod.Invoke(services, new object[] { appointments, maxDay, appointmentDate })!;

            Assert.Equal(expectedAppointmentDate.ToString("yyyy-MM-dd"), result.ToString("yyyy-MM-dd"));
        }

        public void Dispose()
        {
            // Clean up the in-memory database after each test
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}