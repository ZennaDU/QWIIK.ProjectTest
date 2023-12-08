using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QWIIK.ProjectTest.Dto;
using QWIIK.ProjectTest.Entity;
using QWIIK.ProjectTest.EntityFramework;
using QWIIK.ProjectTest.Utility;

namespace QWIIK.ProjectTest.Services
{
    public class AppointmentServices
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AppointmentServices(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public DateTime BookAppointment(UserEntity user, DateTime appointmentDate)
        {
            lock (_context.AppointmentOptions)
            {
                //check
                var maxDay = GetAppointmentMaxDay();
                lock(_context.Appointments)
                {
                    var appointments = _context.Appointments.Where(entity => entity.AppointmentDate.Date >= appointmentDate.Date).OrderBy(entity => entity.AppointmentDate).ToList();

                    if (appointments == null)
                    {
                        //add new appointment date
                        var appointment = new AppointmentsEntity
                        {
                            AppointmentDate = appointmentDate,
                            Count = 1,
                            IsAvailable = true,
                        };

                        _context.Appointments.Add(appointment);
                    }
                    else
                    {
                        appointmentDate = _CheckAvailableDate(appointments, maxDay, appointmentDate);
                        var targetAppointments = appointments.FirstOrDefault(appointment => appointment.AppointmentDate.Date == appointmentDate.Date);

                        //when there is already has appointment data
                        if (targetAppointments != null)
                        {
                            targetAppointments.Count = targetAppointments.Count++;
                            if (targetAppointments.Count >= maxDay)
                            {
                                targetAppointments.IsAvailable = false;
                            }
                            targetAppointments.ModifiedBy = "SYSTEM";
                            targetAppointments.ModifiedAt = DateTime.Now;
                            _context.Appointments.Update(targetAppointments);
                        }
                        else
                        {
                            //add new appointment date
                            var appointment = new AppointmentsEntity
                            {
                                AppointmentDate = appointmentDate,
                                Count = 1,
                                IsAvailable = true,
                            };

                            _context.Appointments.Add(appointment);
                        }
                    }

                    //add appointment
                    UserAppointments userAppointments = new UserAppointments
                    {
                        UserId = user.Id,
                        AppointmentDate = appointmentDate,
                        AppointmentToken = "asdf001",
                        CreatedBy = user.UserName,
                        User = user
                    };

                    _context.UserAppoinments.Add(userAppointments);


                    _context.SaveChanges();

                    return appointmentDate;
                }
            }
        }

        private DateTime _CheckAvailableDate(List<AppointmentsEntity> appointments, int maxDay, DateTime appointmentDate)
        {
            int index = 0;
            bool found = false;
            while(index < appointments.Count && found == false)
            {
                if(appointments[index].AppointmentDate.Date != appointmentDate.Date && appointmentDate.Date > appointments[index].AppointmentDate.Date)
                {
                    found = true;
                }else 
                if (appointments[index].IsAvailable && appointments[index].Count < maxDay && appointments[index].AppointmentDate.Date == appointmentDate.Date)
                {
                    found = true;
                }
                else
                {
                    appointmentDate = appointmentDate.AddDays(1);
                    index++;
                }
            }

            return appointmentDate;
        }

        public int GetAppointmentMaxDay()
        {
            var maxDay = AppConstant.AppointmentOptionsDefault.APPOINTMENT_PER_DAY;
            var options = _context.AppointmentOptions.FirstOrDefault();
            if(options != null)
            {
                maxDay = options.MaxAppointmentPerDay;
            }

            return maxDay;
        }

        public void UpdateAppointmentMaxDay(AppointmentOptionsDto options, string userId)
        {
            var optionsDb = _context.AppointmentOptions.FirstOrDefault();
            //get user
            var user = _context.Users.FirstOrDefault(e => e.Id == new Guid(userId));

            //if options is null
            if (optionsDb == null)
            {
                var appointmentOptions = new AppointmentOptions(options);
                appointmentOptions.CreatedBy = user.UserName;

                _context.AppointmentOptions.Add(appointmentOptions);
            }
            else
            {
                optionsDb.MaxAppointmentPerDay = options.MaxAppointmentPerDay;
                optionsDb.ModifiedAt = DateTime.Now;
                optionsDb.ModifiedBy = user.UserName;
                _context.AppointmentOptions.Update(optionsDb);
            }
            _context.SaveChanges();
        }

        public List<UserDto> GetUserAppointments(DateTime appointmentDate)
        {
            var users = _context.UserAppoinments.Include(entity => entity.User).Where(entity => entity.AppointmentDate.Date == appointmentDate.Date).Select(entity=>new UserDto(entity.User)).ToList();

            return users;
        }
    }
}
