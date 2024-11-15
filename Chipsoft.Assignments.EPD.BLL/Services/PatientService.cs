using Chipsoft.Assignments.EPD.DAL.Data;
using Chipsoft.Assignments.EPD.BLL.Exceptions;
using Chipsoft.Assignments.EPD.DAL.Models;
using Chipsoft.Assignments.EPD.BLL.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPD.BLL.Services
{
    public class PatientService
    {
        private readonly EPDDbContext _context;

        public PatientService(EPDDbContext context)
        {
            _context = context;
        }

        public void AddPatient(Patient patient)
        {
            try
            {
                PatientValidator.ValidatePatient(patient);
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Foutmelding: Toevoegen van Patiënt naar de database.", ex);
            }
        }

        public void DeletePatient(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
                throw new ValidationException("Patiënt niet gevonden.");

            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }

        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public bool DoesPatientExistByInsuranceNumber(string insuranceNumber)
        {
            return _context.Patients.Any(p => p.InsuranceNumber == insuranceNumber);
        }

    }
}
