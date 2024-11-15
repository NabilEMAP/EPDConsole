using Chipsoft.Assignments.EPD.BLL.Exceptions;
using Chipsoft.Assignments.EPD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPD.BLL.Validation
{
    public static class PatientValidator
    {
        public static void ValidatePatient(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.FirstName))
                throw new ValidationException("Voornaam is vereist.");

            if (string.IsNullOrWhiteSpace(patient.LastName))
                throw new ValidationException("Achternaam is vereist.");

            if (patient.DateOfBirth > DateTime.Now)
                throw new ValidationException("Geboortedatum kan niet in de toekomst.");

            if (!string.IsNullOrWhiteSpace(patient.Email) && !IsValidEmail(patient.Email))
                throw new ValidationException("Ongeldig email formaat.");

            if (string.IsNullOrWhiteSpace(patient.InsuranceNumber))
                throw new ValidationException("Rijksregisternummer is vereist.");
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
