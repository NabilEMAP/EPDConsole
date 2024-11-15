using Chipsoft.Assignments.EPD.BLL.Exceptions;
using Chipsoft.Assignments.EPD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPD.BLL.Validation
{
    public static class AppointmentValidator
    {
        public static void ValidateAppointment(Appointment appointment)
        {
            if (appointment.DateTime < DateTime.Now)
                throw new ValidationException("Afspraak kan niet aangemaakt worden in het verleden.");

            if (string.IsNullOrWhiteSpace(appointment.Description))
                throw new ValidationException("Beschrijving is vereist.");
        }
    }

}
