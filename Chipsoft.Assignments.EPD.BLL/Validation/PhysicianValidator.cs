using Chipsoft.Assignments.EPD.BLL.Exceptions;
using Chipsoft.Assignments.EPD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPD.BLL.Validation
{
    public static class PhysicianValidator
    {
        public static void ValidatePhysician(Physician physician)
        {
            if (string.IsNullOrWhiteSpace(physician.FirstName))
                throw new ValidationException("Voornaam is vereist.");

            if (string.IsNullOrWhiteSpace(physician.LastName))
                throw new ValidationException("Achternaam is vereist.");

            if (string.IsNullOrWhiteSpace(physician.Specialization))
                throw new ValidationException("Specialisatie is vereist.");
        }
    }
}
