using Chipsoft.Assignments.EPD.DAL.Data;
using Chipsoft.Assignments.EPD.BLL.Exceptions;
using Chipsoft.Assignments.EPD.DAL.Models;
using Chipsoft.Assignments.EPD.BLL.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPD.BLL.Services
{
    public class AppointmentService
    {
        private readonly EPDDbContext _context;

        public AppointmentService(EPDDbContext context)
        {
            _context = context;
        }

        public void AddAppointment(Appointment appointment)
        {
            try
            {
                AppointmentValidator.ValidateAppointment(appointment);
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Foutmelding: Toevoegen van Afspraak naar de database.", ex);
            }
        }

        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Physician)
                .OrderBy(a => a.DateTime)
                .ToList();
        }
    }
}
